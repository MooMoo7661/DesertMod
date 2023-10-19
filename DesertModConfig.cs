using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace DesertMod
{
    [Label("Config")]
    public class DesertModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Every NPC and enemy is a cactus")]
        [Tooltip("Enable / Disable every enemy from having their texture as a rolling cactus.\n(Default OFF)")]
        [DefaultValue(false)]
        public bool EveryNPCIsCactus { get; set; }

        [Label("Rolling Cactus Homing Type")]
        [Tooltip("1: Naturally Spawned / placed Cacti\n2: All Cacti\n3: No Cacti\n(Default 3)")]
        [Increment(1)]
        [Range(1, 3)]
        [DefaultValue(3)]
        [Slider]
        [ReloadRequired]
        public int HomingType { get; set; }

        [Label("Enable boss textures being a cactus")]
        [Tooltip("Enable / Disable bosses having their texture as a rolling cactus.\nReccomended to keep on, as it is quite entertaining.\n(Default ON)")]
        [DefaultValue(true)]
        public bool BossTexturesAreCactus { get; set; }

        [Label("Special Worldgen")]
        [Tooltip("Enable / Disable converting dirt to sand, stone to sandstone, dirt walls to sandstone walls,\nAsh to sand and sandstone, painting certain structures, and placing extra underground deserts.\n(Default ON)")]
        [DefaultValue(true)]
        public bool SpecialWorldgen { get; set; }

        [Label("Paint dungeon")]
        [Tooltip("Enable / Disable painting the dungeon yellow on world creation.\n(Default ON)")]
        [DefaultValue(true)]
        public bool PaintDungeon { get; set; }

        [Label("Paint hives")]
        [Tooltip("Enable / Disable painting hives yellow on world creation.\n(Default ON)")]
        [DefaultValue(true)]
        public bool PaintHives { get; set; }

        [Label("Paint temple")]
        [Tooltip("Enable / Disable painting the temple yellow on world creation.\n(Default ON)")]
        [DefaultValue(true)]
        public bool PaintTemple { get; set; }

    }
}