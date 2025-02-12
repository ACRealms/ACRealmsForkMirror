using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ACE.Database;
using ACE.Entity.Models;
using ACE.Server.Realms;
using ACE.Database.Adapter;
using ACE.Entity.Enum.Properties;
using System.Linq;
using ACE.Server.WorldObjects;
using ACE.Server.Entity;
using ACE.Server.Command.Handlers.Processors;
using Newtonsoft.Json;
using ACE.Entity.Enum;
using ACE.Database.Models.World;
using System.Threading.Tasks;
using ACE.Server.Command.Handlers;
using System.IO;
using ACE.Entity.ACRealms;
using ACE.Common.ACRealms;
using System.Configuration;
using ACE.Entity;
using ACE.Server.Realms.Peripherals;
using System.Collections.Frozen;
using ACRealms;
using ACRealms.Rulesets.Enums;
using ACRealms.Rulesets.Loader;
using ACRealms.Rulesets;
using System.Runtime.CompilerServices;

namespace ACE.Server.Managers
{
    internal static class RealmManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static Peripherals Peripherals { get; private set; }
        public static FrozenSet<WorldRealm> Realms { get; private set; } = FrozenSet<WorldRealm>.Empty;
        public static FrozenSet<WorldRealm> Rulesets { get; private set; } = FrozenSet<WorldRealm>.Empty;
        public static FrozenSet<WorldRealm> RealmsAndRulesets { get; private set; } = FrozenSet<WorldRealm>.Empty;
        private static FrozenDictionary<ushort, WorldRealm> RealmsByID = FrozenDictionary<ushort, WorldRealm>.Empty;
        private static FrozenDictionary<string, WorldRealm> RealmsByName = FrozenDictionary<string, WorldRealm>.Empty;
        private static FrozenDictionary<ReservedRealm, RealmToImport> ReservedRealmsToImport = FrozenDictionary<ReservedRealm, RealmToImport>.Empty;
        private static FrozenDictionary<ReservedRealm, WorldRealm> ReservedRealms = FrozenDictionary<ReservedRealm, WorldRealm>.Empty;
        private static Dictionary<string, RulesetTemplate> EphemeralRealmCache = new Dictionary<string, RulesetTemplate>(StringComparer.OrdinalIgnoreCase);

        // We have to use WorldRealmBase (for now) because casting is the lesser evil than polluting the template with more state
        // While doing a full realm reload, this dictionary will NOT be passed to the context.
        internal static FrozenDictionary<ushort, WorldRealmBase> CompilationContextDependencyHandles { get; private set; } = FrozenDictionary<ushort, WorldRealmBase>.Empty;

        //Todo: refactor
        public static WorldRealm DuelRealm;
        
        private static List<ushort> RealmIDsByTopologicalSort;

        private static bool FirstImportCompleted;

        public static WorldRealm DefaultRealmFallback { get; private set; }
        public static WorldRealm DefaultRealmConfigured { get; internal set; }

        public static LocalPosition UltimateDefaultLocation = Player.MarketplaceDrop;

        public static WorldRealm ServerDefaultRealm => DefaultRealmConfigured ?? DefaultRealmFallback;
        public static AppliedRuleset ServerDefaultRuleset => ServerDefaultRealm.StandardRules;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Initialize(bool liveEnvironment = true)
        {
            global::ACRealms.RealmProps.RealmPropertyPrototypes.InitializeContextMappings(System.Reflection.Assembly.GetExecutingAssembly());
            SetupReservedRealms();

            //Import-realms
            if (liveEnvironment)
            {
                RealmDataCommands.HandleImportRealms(null, null);
                if (!FirstImportCompleted)
                    throw new Exception("Import of realms.jsonc did not complete successfully.");
            }

            ReloadPeripherals();
        }

        public static void ReloadPeripherals()
        {
            Peripherals = Peripherals.Load();
            log.Info("Loaded Peripheral Configuration");
        }

        private static void SetupReservedRealms()
        {
            var reservedRealmsToImport = new Dictionary<ReservedRealm, RealmToImport>();
            foreach (var id in Enum.GetValues(typeof(ReservedRealm)).Cast<ReservedRealm>())
            {
                if (id.ToString().StartsWith("Reserved"))
                    continue;

                string jsonc = null;
                using (var resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream($"ACE.Server.Realms.reserved_realms.{id}.jsonc"))
                    using (var sr = new System.IO.StreamReader(resource))
                        jsonc = sr.ReadToEnd();
                var realm = DeserializeRealmJson(null, $"SYSTEM FILE - {id}.jsonc", jsonc);
                if (realm.Realm.Name != id.ToString())
                    throw new Exception($"Error importing reserved realm {id}, realm name must equal the ReservedRealm enum value.");
                reservedRealmsToImport[id] = realm;
                realm.Realm.SetId((ushort)id);
            }
            ReservedRealmsToImport = reservedRealmsToImport.ToFrozenDictionary();
        }

        public static WorldRealm GetReservedRealm(ReservedRealm reservedRealmId)
        {
            ReservedRealms.TryGetValue(reservedRealmId, out var worldRealm);
            return worldRealm;
        }

        internal static ushort ConvertToValidRealmIdOrZero(int? rawRealmId)
        {
            if (((rawRealmId ?? 0) <= 0) || (uint)rawRealmId > 0x7FFF)
                return 0;

            ushort realmId = (ushort)rawRealmId;
            if (RealmsByID.ContainsKey(realmId))
                return realmId;
            else
                return 0;
        }

        public static WorldRealm GetRealm(ushort? realm_id, bool includeRulesets)
        {
            if (!realm_id.HasValue)
                return null;
            var realmId = realm_id.Value;
            if (realmId > 0x7FFF)
                return null;

            if (RealmsByID.TryGetValue(realmId, out var realm))
            {
                if (!includeRulesets && realm.Realm.Type == RealmType.Ruleset)
                    return null;
                return realm;
            }
            return null;
        }

        public static WorldRealm GetRealmByName(string name, bool includeRulesets)
        {
            if (RealmsByName.TryGetValue(name, out var realm))
            {
                if (!includeRulesets && realm.Realm.Type == RealmType.Ruleset)
                    return null;
                return realm;
            }
            return null;
        }

        internal static RulesetTemplate BuildRuleset(Realm realm,
            IDictionary<ushort, WorldRealmBase> dependencies,
            RulesetCompilationContext ctx = null)
        {
            if (dependencies == null)
                dependencies = CompilationContextDependencyHandles;
            ctx ??= Ruleset.MakeDefaultContext(dependencies);

            if (realm.ParentRealmID == null)
                return RulesetTemplate.MakeTopLevelRuleset(realm, ctx);

            var worldRealmParent = (WorldRealm)ctx.Dependencies[(ushort)realm.ParentRealmID];

            RulesetTemplate templateParent;
            if (ctx.Trace)
                templateParent = BuildRuleset(worldRealmParent.Realm, dependencies, ctx);
            else
                templateParent = worldRealmParent.RulesetTemplate;

            return RulesetTemplate.MakeRuleset(templateParent, realm, ctx);
        }

        internal static void ClearRealms()
        {
            foreach (var realm in RealmsByID.Values)
                realm.NeedsRefresh = true;

            RealmsByID = FrozenDictionary<ushort, WorldRealm>.Empty;
            RealmsByName = FrozenDictionary<string, WorldRealm>.Empty;
            ReservedRealms = FrozenDictionary<ReservedRealm, WorldRealm>.Empty;
            Realms = FrozenSet<WorldRealm>.Empty;
            Rulesets = FrozenSet<WorldRealm>.Empty;
            RealmsAndRulesets = FrozenSet<WorldRealm>.Empty;
            EphemeralRealmCache.Clear();
        }

        /// <summary>
        /// Gets the base realm for which a new ephemeral realm will be created from
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static WorldRealm GetBaseRealm(Player player)
        {
            // Always use the home realm for now
            return GetRealm(player.HomeRealm, includeRulesets: false);
            /*
            //Hideouts will use the player's home realm
            if (player.Location.RealmID == (ushort)ReservedRealm.hideout)
                return GetRealm(player.HomeRealm);

            //Otherwise use the current location, or the exit location (or alternatively the home realm) if already in an ephemeral realm
            if (!player.Location.IsEphemeralRealm)
                return GetRealm(player.RealmRuleset.Template.Realm.Id);

            return GetRealm(player.EphemeralRealmExitTo?.RealmID ?? player.HomeRealm);*/
        }

        internal static Landblock GetNewEphemeralLandblock(ACE.Entity.LandblockId physicalLandblockId, Player owner, List<Realm> realmTemplates)
        {
            EphemeralRealm ephemeralRealm;

            ephemeralRealm = EphemeralRealm.Initialize(owner, realmTemplates);

            var iid = LandblockManager.RequestNewEphemeralInstanceIDv1(owner.HomeRealm);
            var landblock = LandblockManager.GetLandblock(physicalLandblockId, iid, ephemeralRealm, false, false);

            log.Info($"GetNewEphemeralLandblock created for player {owner.Name}, realm ruleset {ephemeralRealm.RulesetTemplate.Realm.Id}, landcell {landblock.Id.Raw}, instance {iid}.");
            return landblock;
        }

        internal static void FullUpdateRealmsRepository(Dictionary<string, RealmToImport> realmsDict,
            Dictionary<ushort, RealmToImport> realmsById)
        {
            if (!PrepareRealmUpdates(realmsDict, realmsById))
                return;

            try
            {
                // DatabaseManager.World.ReplaceAllRealms(realmsById);
                LoadAllRealms(realmsById);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
            FirstImportCompleted = true;
        }

        private static void LoadAllRealms(Dictionary<ushort, RealmToImport> realms)
        {
            // var realms = DatabaseManager.World.GetAllRealms().ToDictionary(x => x.Id);

            var realmsById = new Dictionary<ushort, WorldRealm>();
            var realmsByName = new Dictionary<string, WorldRealm>(StringComparer.OrdinalIgnoreCase);
            var reservedRealms = new Dictionary<ReservedRealm, WorldRealm>();
            WorldRealm defaultRealmFallback = null;
            WorldRealm duelRealm = null;

            var dependencyContext = new Dictionary<ushort, WorldRealmBase>();
            foreach(var realmid in RealmIDsByTopologicalSort)
            {
                var erealm = global::ACRealms.Rulesets.DBOld.RealmConverter.ConvertToEntityRealm(realms[realmid].Realm, true);
                
                var ruleset = BuildRuleset(erealm, dependencyContext);
                var wrealm = new WorldRealm(erealm, ruleset);
                realmsById.Add(erealm.Id, wrealm);
                dependencyContext.Add(erealm.Id, wrealm);
                realmsByName.Add(erealm.Name, wrealm);
                if (Enum.IsDefined(typeof(ReservedRealm), erealm.Id))
                {
                    reservedRealms.Add((ReservedRealm)erealm.Id, wrealm);
                    if (erealm.Id == (ushort)ReservedRealm.@default)
                        defaultRealmFallback = wrealm;
                }

                // Needs improvement: Only one realm can be set to the dueling realm and it uses a static property, which seems wrong to me
                if (wrealm.Realm.Type == RealmType.Realm && Props.Pvp.World.IsDuelingRealm(wrealm.StandardRules))
                    duelRealm = wrealm;
            };

            var realmsByNameFrozen = realmsByName.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

            // Make sure every data structure is built before committing them
            var realmsByIdFrozen = realmsById.ToFrozenDictionary();
            var targetRealms = realmsByIdFrozen.Values.Where(x => x.Realm.Type == RealmType.Realm).ToFrozenSet();
            var rulesets = realmsByIdFrozen.Values.Where(x => x.Realm.Type == RealmType.Ruleset).ToFrozenSet();
            var realmsAndRulesets = realmsByIdFrozen.Values.ToFrozenSet();
            var reservedRealmsFrozen = reservedRealms.ToFrozenDictionary();
            var dependencyContextFrozen = dependencyContext.ToFrozenDictionary();

            // Commit
            DefaultRealmConfigured = realmsByNameFrozen[ACRealmsConfigManager.Config.DefaultRealm];
            CompilationContextDependencyHandles = dependencyContextFrozen;
            RealmsByID = realmsByIdFrozen;
            RealmsByName = realmsByNameFrozen;
            Realms = targetRealms;
            Rulesets = rulesets;
            RealmsAndRulesets = realmsAndRulesets;
            DuelRealm = duelRealm;
            DefaultRealmFallback = defaultRealmFallback;
            ReservedRealms = reservedRealmsFrozen;
        }

        private static void ValidateConfiguredRealmName(Dictionary<string, RealmType> realmTypesByName, string realmName, string configLabel, bool allowDefault = false, bool allowNull = false)
        {
            if (Enum.TryParse(realmName, true, out ReservedRealm reserved))
            {
                bool valid = false;
                valid |= allowDefault && reserved == ReservedRealm.@default;
                valid |= allowNull && reserved == ReservedRealm.NULL;

                if (!valid)
                    throw new ConfigurationErrorsException($"{configLabel} in Config.realms.js must choose a user-defined realm, not the reserved realm '{reserved}'");
            }
            if (!realmTypesByName.ContainsKey(realmName))
                throw new ConfigurationErrorsException($"Config.realms.js specified {configLabel} '{realmName}', but no json file was defined for that realm. See the README doc for instructions.");
            else if (realmTypesByName[realmName] != RealmType.Realm)
                throw new ConfigurationErrorsException($"Config.realms.js specified {configLabel} '{realmName}', but this must be a realm, not a ruleset.");
        }

        private static bool PrepareRealmUpdates(Dictionary<string, RealmToImport> newRealmsByName,
            Dictionary<ushort, RealmToImport> newRealmsById)
        {
            //Add reserved realms and ensure no conflicts
            foreach(var kvp in ReservedRealmsToImport)
            {
                var enumid = kvp.Key;
                var reservedrealm = kvp.Value;
                if (newRealmsById.ContainsKey((ushort)enumid))
                    throw new InvalidDataException($"realms.jsonc may not contain id {(ushort)enumid}, which is a reserved id.");
                if (newRealmsByName.ContainsKey(enumid.ToString()))
                    throw new InvalidDataException($"May not import a realm named {enumid}, which is a reserved name.");
                newRealmsByName[enumid.ToString()] = reservedrealm;
                newRealmsById[(ushort)enumid] = reservedrealm;
            }

            //Check for renames
            foreach (var realm in RealmsByName.Values)
            {
                if (realm.Realm.Id == 0)
                    continue;

                if (newRealmsByName.TryGetValue(realm.Realm.Name, out var newRealmToImport) && newRealmToImport.Realm.Id != realm.Realm.Id)
                    throw new InvalidDataException($"Realm {realm.Realm.Name} attempted to have its numeric ID changed to a different value during realm import, which is not supported.");
                if (newRealmsById.TryGetValue(realm.Realm.Id, out var newRealmToImport2) && newRealmToImport2.Realm.Name != realm.Realm.Name)
                    throw new InvalidDataException($"Realm {realm.Realm.Id} ({realm.Realm.Name}) attempted to have its unique name changed to a different value during realm import, which is not supported.");
            }

            //Check for deletions
            foreach (var realmId in RealmsByID.Keys)
            {
                if (!newRealmsById.ContainsKey(realmId))
                    throw new InvalidDataException($"Realm {realmId} is missing in realms.jsonc. Realms may not be removed once added. Unable to continue sync.");
            }
            foreach (var realmName in RealmsByName.Keys)
            {
                if (!newRealmsByName.ContainsKey(realmName))
                    throw new InvalidDataException($"Realm {realmName} is missing in realms.jsonc. Realms may not be removed once added. Unable to continue sync.");
            }

            //Ensure realm in each link exists
            foreach (var realm in newRealmsByName.Values)
            {
                foreach (var link in realm.Links)
                {
                    if (!newRealmsByName.ContainsKey(link.Import_RulesetToApply))
                        throw new InvalidDataException($"New realm {realm.Realm.Name} has a linked realm {link.Import_RulesetToApply} which was not found in the import set. Unable to continue sync.");
                    link.RealmId = realm.Realm.Id;
                    link.LinkedRealmId = newRealmsByName[link.Import_RulesetToApply].Realm.Id;
                }
            }

            //Check for circular dependencies - from top to bottom of tree
            Queue<global::ACRealms.Rulesets.DBOld.Realm> realmsToCheck = new Queue<global::ACRealms.Rulesets.DBOld.Realm>();
            foreach (var importItem in newRealmsByName.Values)
            {
                if (importItem.Realm.ParentRealmId == null)
                    realmsToCheck.Enqueue(importItem.Realm);
            }

            HashSet<ushort> realmsChecked = new HashSet<ushort>();
            while (realmsToCheck.TryDequeue(out var realmToCheck))
            {
                if (realmsChecked.Contains(realmToCheck.Id))
                    throw new InvalidDataException($"A circular dependency was detected when attempting to import realm {realmToCheck.Id}.");

                realmsChecked.Add(realmToCheck.Id);
                foreach (var realm in realmToCheck.Descendants.Values)
                    realmsToCheck.Enqueue(realm);
            }

            if (realmsChecked.Count != newRealmsById.Count)
            {
                var badRealm = newRealmsById.First(x => !realmsChecked.Contains(x.Key)).Value;
                throw new InvalidDataException($"A circular dependency was detected when attempting to import realm {badRealm.Realm.Name}.");
            }

            // Check for circular dependencies in realm links. Not sure of a good algorithm for this as it gets complex.
            // So for now, restrict realm links to only rulesets that do not have a parent.
            var itemsToCheck = newRealmsByName.Values.Select(i => new RealmToImportMarked() { ImportItem = i }).ToDictionary(x => x.ImportItem.Realm.Id);
            var unmarkedNodes = itemsToCheck.Values.ToHashSet();

            //Check for duplicate names (may be unnecessary due to the RecursiveCheckCircularDependency section below)
            foreach (var item in itemsToCheck.Values)
            {
                foreach (var link in item.ImportItem.Links)
                {
                    var thisRealm = newRealmsById[link.RealmId].Realm;
                    if (thisRealm.Name == link.Import_RulesetToApply || thisRealm.Id == link.LinkedRealmId || link.RealmId == link.LinkedRealmId)
                        throw new InvalidDataException($"Error importing realm {thisRealm.Name}: A realm cannot have a linked ruleset with the same name as that realm.");
                }
            }

            //DepthFirstSearch traversal to find circular references on entire graph
            RealmIDsByTopologicalSort = new List<ushort>();
            while (unmarkedNodes.Count > 0)
            {
                var item = unmarkedNodes.First();
                try
                {
                    RecursiveCheckCircularDependency(unmarkedNodes.First(), itemsToCheck, unmarkedNodes);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidDataException($"Error importing realm {item.ImportItem.Realm.Name}: A circular dependency was detected.", ex);
                }
            }

            var validateCheck = newRealmsByName.ToDictionary(kvp => kvp.Key, kvp => (RealmType)kvp.Value.Realm.Type);

            ValidateConfiguredRealmName(validateCheck, ACRealmsConfigManager.Config.DefaultRealm, "DefaultRealm", allowDefault: ACRealmsConfigManager.Config.AllowUndefinedDefaultRealm);
            ValidateConfiguredRealmName(validateCheck, ACRealmsConfigManager.Config.CharacterMigrationOptions.AutoAssignToRealm, "CharacterMigrationOptions.AutoAssignToRealm",
                allowDefault: ACRealmsConfigManager.Config.AllowUndefinedDefaultRealm,
                allowNull: true);

            return true;
        }

        private class RealmToImportMarked
        {
            public RealmToImport ImportItem { get; set; }
            public bool TemporaryMark { get; set; }
            public bool PermanentMark { get; set; }
        }

        //https://en.wikipedia.org/wiki/Topological_sorting#Depth-first_search
        private static void RecursiveCheckCircularDependency(RealmToImportMarked item, Dictionary<ushort, RealmToImportMarked> dict, HashSet<RealmToImportMarked> unmarkedNodes)
        {
            if (item.PermanentMark)
                return;
            if (item.TemporaryMark)
                throw new InvalidOperationException();
            item.TemporaryMark = true;
            if (item.ImportItem.Realm.ParentRealmId.HasValue)
                RecursiveCheckCircularDependency(dict[item.ImportItem.Realm.ParentRealmId.Value], dict, unmarkedNodes);
            foreach (var link in item.ImportItem.Links)
                RecursiveCheckCircularDependency(dict[link.LinkedRealmId], dict, unmarkedNodes);
            item.TemporaryMark = false;
            item.PermanentMark = true;
            unmarkedNodes.Remove(item);
            RealmIDsByTopologicalSort.Add(item.ImportItem.Realm.Id);
        }

        internal static RulesetTemplate GetEphemeralRealmRulesetTemplate(string key) => EphemeralRealmCache[key];
        internal static void CacheEphemeralRealmTemplate(string key, RulesetTemplate template) => EphemeralRealmCache[key] = template;

        internal static RealmToImport DeserializeRealmJson(Network.ISession session, string filename, string fileContent)
        {
            return global::ACRealms.Rulesets.Loader.Loader.DeserializeRealmJson(filename, fileContent);
            
        }

        public static bool TryParseReservedRealm(ushort realmId, out ReservedRealm? reservedRealm)
        {
            if (Enum.IsDefined(typeof(ReservedRealm), realmId))
            {
                reservedRealm = (ReservedRealm)realmId;
                return true;
            }
            reservedRealm = null;
            return false;
        }

        internal static bool SetHomeRealm(ObjectGuid playerGuidOfflinePlayer, WorldRealm realm)
        {
            var player = PlayerManager.GetOfflinePlayer(playerGuidOfflinePlayer);
            if (player == null)
                log.Error($"SetHomeRealm: Could not find offline player with guid {playerGuidOfflinePlayer}");
            return SetHomeRealm(player, realm);
        }

        // This will be used for auto home realm migration from older servers, but may also be used from an admin command.
        public static bool SetHomeRealm(OfflinePlayer offlinePlayer, WorldRealm realm)
        {
            log.Info($"Setting HomeRealm for offline character '{offlinePlayer.Name}' to '{realm.Realm.Name}' (ID {realm.Realm.Id}).");
            int oldHomeRealmInt = offlinePlayer.GetProperty(PropertyInt.HomeRealm) ?? 0;
            ushort oldHomeRealmId;
            if (oldHomeRealmInt < 0 || oldHomeRealmInt > 0x7FFF)
                oldHomeRealmId = 0;
            else
                oldHomeRealmId = (ushort)oldHomeRealmInt;

            // var oldHomeRealm = GetRealm(oldHomeRealmId);

            foreach(var type in Enum.GetValues<PositionType>())
            {
                UsablePosition previousPosition;
                try
                {
                    previousPosition = offlinePlayer.GetPositionUnsafe(type);
                }
                catch (NullRealmException)
                {
                    previousPosition = offlinePlayer.GetLocalPositionUnsafe(type);
                }

                if (previousPosition == null)
                    continue;
                var destPositionAsLocal = previousPosition as LocalPosition ?? ((InstancedPosition)previousPosition).AsLocalPosition();

                if (previousPosition is InstancedPosition ip)
                {
                    if (ip.IsEphemeralRealm)
                        destPositionAsLocal = UltimateDefaultLocation;
                    else if (ip.RealmID == realm.Realm.Id)
                        continue;
                    else if (ip.RealmID != oldHomeRealmId)
                        continue;
                }

                if (DuelRealm == realm)
                    destPositionAsLocal = DuelRealmHelpers.GetDuelingAreaDrop(offlinePlayer).AsLocalPosition();

                var iid = realm.StandardRules.GetDefaultInstanceID(offlinePlayer, destPositionAsLocal);

                var destPosition = destPositionAsLocal.AsInstancedPosition(iid);
                offlinePlayer.SetPositionUnsafe(type, destPosition);
            }

            offlinePlayer.SetProperty(PropertyInt.HomeRealm, realm.Realm.Id);
            var result = TryMoveHousesToNewRealm(offlinePlayer, realm);
            offlinePlayer.SaveBiotaToDatabase();
            return result;
        }

        public static bool SetHomeRealm(IPlayer player, WorldRealm realm)
        {
            return player switch
            {
                OfflinePlayer offline => SetHomeRealm(offline, realm),
                Player online => SetHomeRealm(online, realm.Realm.Id, false),
                _ => throw new NotImplementedException()
            };
        }

        private static bool TryMoveHousesToNewRealm(IPlayer player, WorldRealm destinationRealm)
        {
            var houses = HouseManager.GetCharacterHouses(player.Guid.Full);
            bool failed = false;

            foreach (var house in houses)
            {
                var srcHouse = HouseManager.GetHouseSynchronously(house.Guid, true, isRealmMigration: true);
                failed |= !srcHouse.TryMoveHouseToNewRealmInstance(destinationRealm.StandardRules.GetDefaultInstanceID(player, house.Location.AsLocalPosition()));
            }
            return !failed;
        }

        public static bool SetHomeRealm(Player player, ushort realmId, bool settingFromRealmSelector, bool saveImmediately = true)
        {
            var realm = GetRealm(realmId, includeRulesets: false);
            if (realm == null)
                throw new InvalidOperationException($"Attempted to use SetHomeRealm with a realm id {realmId}, which was not found.");

            log.Info($"Setting HomeRealm for character '{player.Name}' to '{realm.Realm.Name}' (ID {realm.Realm.Id}).");
            player.HomeRealm = realm.Realm.Id;
            
            if (settingFromRealmSelector)
            {
                player.SetProperty(PropertyBool.RecallsDisabled, false);
                var loc = realm.DefaultStartingLocation(player);
                player.Sanctuary = loc.AsLocalPosition();
                WorldManager.ThreadSafeTeleport(player, loc, false, new Entity.Actions.ActionEventDelegate(() =>
                {
                    if (Props.Pvp.World.IsDuelingRealm(realm.StandardRules))
                        DuelRealmHelpers.SetupNewCharacter(player);
                }));
            }
            bool validate = saveImmediately;
            if (validate)
                player.ValidateCurrentRealm();
            if (saveImmediately)
                player.SavePlayerToDatabase();

            if (!TryMoveHousesToNewRealm(player, realm))
            {
                player.Session.Network.EnqueueSend(new Network.GameMessages.Messages.GameMessageSystemChat("Your house was unable to be transferred to the new realm.", ChatMessageType.Broadcast));
                return false;
            }
            return true;
        }
    }
}
