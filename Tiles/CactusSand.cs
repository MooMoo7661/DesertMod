using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DesertMod.Tiles
{
    public class CactusSand : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            Main.tileMerge[Type][TileID.Sand] = true;
            Main.tileMerge[Type][Type] = true;
            Main.tileMerge[Type][TileID.Crimsand] = true;
            Main.tileMerge[Type][TileID.Ebonsand] = true;
            Main.tileMerge[Type][TileID.Sandstone] = true;
            RegisterItemDrop(ItemID.SandBlock);

            AddMapEntry(new Color(186, 168, 84));
        }
    }   

    public class GlobalCactusBlocks : GlobalTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.PreventsSandfall[TileID.Sand] = true;
            TileID.Sets.PreventsSandfall[TileID.Ebonsand] = true;
            TileID.Sets.PreventsSandfall[TileID.Crimsand] = true;
            TileID.Sets.PreventsSandfall[TileID.HardenedSand] = true;
            TileID.Sets.PreventsSandfall[TileID.Sandstone] = true;
        }
    }
}