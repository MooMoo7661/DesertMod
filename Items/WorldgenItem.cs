using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace DesertMod.Items
{
    public class WorldgenItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.width = 30;
            Item.height = 26;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 0;
            Item.rare = ItemRarityID.Lime;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.UseSound = new SoundStyle?(SoundID.Item1);
            Item.value = Item.sellPrice(0, 17, 0, 0);
        }


        public override bool? UseItem(Player player)
        {
            WorldGen.clearWorld();
            WorldGen.GenerateWorld(Main.rand.Next(999999999), null);

            return true;
        }
    }
}