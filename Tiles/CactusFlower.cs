using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DesertMod.Tiles
{
    public class CactusFlower : ModTile
    {
        public override void SetStaticDefaults()
        {
            //Main.tileFrameCounter[Type] = 1;
            Main.tileFrameImportant[Type] = true;
            //RegisterItemDrop(ItemID.Cactus);

            DustType = DustID.Grass;
            HitSound = SoundID.Grass;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) => offsetY = 4;

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);

            if (tileBelow.TileType != TileID.Cactus)
            {
                WorldGen.KillTile(i, j);
            }
        }
    }   

    public class CactusFlowerRed : CactusFlower
    {
        
    }
}