﻿using ACE.Entity.Enum.RealmProperties;
using System;
using System.ComponentModel;
using RealmPropertyStringAttribute = ACE.Entity.Enum.Properties.RealmPropertyPrimaryAttribute<string>;

namespace ACE.Entity.Enum.Properties;

[RequiresPrimaryAttribute<RealmPropertyPrimaryAttribute<string>, string>]
public enum RealmPropertyStringStaging : ushort
{
    Undef = 0,

    [Description("A description of the ruleset.")]
    [RealmPropertyStringAttribute("No Description")]
    Core_Realm_Description,

    [Description("For the landblocks matching the name of this dungeon set, classical instances will be enabled for players with the ClassicalInstancesActive boolean property, if the ruleset also has UseClassicalInstances set to true.")]
    [RealmPropertyStringAttribute("default")]
    Peripheral_ClassicalInstance_DungeonSet,

    [Description("When using a portal, determines which mode to use to select the realm or instance.")]
    [RealmPropertyStringAttribute("HomeRealm")]
    Core_Instance_PortalInstanceSelectMode,

    [Description("When recalling, determines which mode to use to select the realm or instance.")]
    [RealmPropertyStringAttribute("HomeRealm")]
    Core_Instance_RecallInstanceSelectMode,
}
