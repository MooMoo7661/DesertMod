using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.GameContent;
using DesertMod.Tiles;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Configuration;
using Terraria.GameContent.Biomes;

namespace DesertMod
{
    public class WorldGenTasks
    {

        static WorldGenConfiguration configuration = WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");
        static StructureMap structures = new StructureMap();

        public static void PlaceDesertPlants()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    Tile tile3 = Framing.GetTileSafely(x + 1, y);

                    if (tile.TileType == TileID.Sand && tile.HasTile && Main.rand.NextBool(13))
                    {
                        WorldGen.GrowPalmTree(x, y);
                    }

                    if (Main.rand.NextBool(20) && (tile.TileType == TileID.Sand || tile.TileType == TileID.Sandstone) && tile3.HasTile && tile.HasTile)
                    {
                        WorldGen.PlaceObject(x, y - 1, TileID.RollingCactus, true);
                    }
                }
            }
        }   

        public static void ConvertHardenedSandToSand()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.HasTile && tile.TileType == TileID.HardenedSand)
                    {
                        WorldGen.PlaceTile(x, y, TileID.Sand, true, true);
                    }
                }
            }
        }

        public static void PaintDungeon()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if ((tile.TileType == TileID.GreenDungeonBrick || tile.TileType == TileID.BlueDungeonBrick ||
                        tile.TileType == TileID.PinkDungeonBrick || tile.TileType == TileID.CrackedGreenDungeonBrick ||
                        tile.TileType == TileID.CrackedPinkDungeonBrick || tile.TileType == TileID.CrackedBlueDungeonBrick ||
                        tile.TileType == TileID.Spikes) && tile.HasTile)
                    {
                        WorldGen.paintTile(x, y, 15);
                    }

                    if ((tile.WallType == WallID.GreenDungeonSlabUnsafe || tile.WallType == WallID.GreenDungeonUnsafe || tile.WallType == WallID.GreenDungeonTileUnsafe ||
                        tile.WallType == WallID.BlueDungeonSlabUnsafe || tile.WallType == WallID.BlueDungeonTileUnsafe || tile.WallType == WallID.BlueDungeonUnsafe ||
                        tile.WallType == WallID.PinkDungeonSlabUnsafe || tile.WallType == WallID.PinkDungeonTileUnsafe || tile.WallType == WallID.PinkDungeonUnsafe))
                    {
                        WorldGen.paintWall(x, y, 15);
                    }
                }
            }
        }

        public static void PaintHives()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if ((tile.TileType == TileID.Hive) && tile.HasTile)
                    {
                        WorldGen.paintTile(x, y, PaintID.DeepYellowPaint);
                    }

                    if ((tile.WallType == WallID.HiveUnsafe))
                    {
                        WorldGen.paintWall(x, y, PaintID.DeepYellowPaint);
                    }
                }
            }
        }

        public static void PaintTemple()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if ((tile.TileType == TileID.LihzahrdBrick) && tile.HasTile)
                    {
                        WorldGen.paintTile(x, y, PaintID.DeepYellowPaint);
                    }

                    if ((tile.WallType == WallID.LihzahrdBrickUnsafe))
                    {
                        WorldGen.paintWall(x, y, PaintID.DeepYellowPaint);
                    }
                }
            }
        }

        public static void ReplaceDirtWalls()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.WallType == WallID.Dirt ||
                        tile.WallType == WallID.Cave6Unsafe || tile.WallType == WallID.DirtUnsafe)
                    {
                        tile.WallType = WallID.Sandstone;
                    }
                }
            }
        }

        public static void GenerateUndergroundDesert()
        {
            DesertBiome desertBiome = configuration.CreateBiome<DesertBiome>();

            for (int i = 1; i <= 20; i++)
            {
                int placePoint = i * 500;

                if (placePoint < Main.maxTilesX - 500)
                desertBiome.Place(new Point(placePoint, (int)Main.worldSurface), structures);

                
            }
        }

        public static void ConvertHellToSandstone()
        {
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.002); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next(0, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);

                if (tile.HasTile && tile.TileType == TileID.Ash)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(18, 40), WorldGen.genRand.Next(20, 61), TileID.Sandstone);
                }
            }
        }
         
        public static void ConvertHellToHardenedSand()
        {
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.002); k++)
            {
               int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next(0, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);

                if (tile.HasTile && tile.TileType == TileID.Ash)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(13, 25), WorldGen.genRand.Next(12, 27), TileID.HardenedSand);
                }
            }
        }

        public static void CleanupHellConversion()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.HasTile && tile.TileType == TileID.Ash)
                    {
                        WorldGen.PlaceTile(x, y, TileID.Sandstone, true, true);
                    }
                }
            }
        }

        public static void GrowCacti()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.TileType == TileID.Cactus)
                    {
                        WorldGenUtils.GetAroundTiles(x, y, out Tile left, out Tile right, out Tile up, out Tile down);

                        if (!left.HasTile && !right.HasTile && !up.HasTile && WorldGen.genRand.NextBool(5))
                        {
                            switch (WorldGen.genRand.Next(2))
                            {
                                case 0:
                                    WorldGen.PlaceTile(x, y - 1, ModContent.TileType<CactusFlower>(), true, true);
                                    break;

                                case 1:
                                    WorldGen.PlaceTile(x, y - 1, ModContent.TileType<CactusFlowerRed>(), true, true);
                                    break;
                            }
                        }

                        /*if (WorldGen.genRand.NextBool(20))
                        {
                            GrowModCactus(x, y);
                            for (int k = 0; k < 150; k++)
                            {
                                int i2 = WorldGen.genRand.Next(x - 1, x + 2);
                                int j2 = WorldGen.genRand.Next(y - 10, y + 2);
                                GrowModCactus(i2, j2);
                            }
                        }*/
                    }
                }
            }
        }

        public static void GrowModCactus(int i, int j)
        {
            int num = j;
            int num2 = i;

            int cactus = 80;

            Tile tile = Framing.GetTileSafely(i, j);

            if (!tile.HasTile)
            {
                return;
            }

            tile = Main.tile[i, j];
            if (tile.IsHalfBlock)
            {
                return;
            }

            if (!WorldGen.gen)
            {
                tile = Main.tile[i, j];
                if (tile.Slope != 0)
                {
                    return;
                }
            }

            tile = Main.tile[i, j - 1];
            if (tile.LiquidType > 0)
            {
                return;
            }

            tile = Main.tile[i, j];
            if (tile.TileType != 53)
            {
                tile = Main.tile[i, j];
                if (tile.TileType != cactus)
                {
                    tile = Main.tile[i, j];
                    if (tile.TileType != 234)
                    {
                        tile = Main.tile[i, j];
                        if (tile.TileType != 112)
                        {
                            tile = Main.tile[i, j];
                            if (tile.TileType != 116)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            int num3 = 0;

            if ((!Main.remixWorld || !((double)j > Main.worldSurface)) && num3 / 255 > WorldGen.cactusWaterLimit)
            {
                return;
            }

            tile = Main.tile[i, j];
            if (tile.TileType != 53)
            {
                tile = Main.tile[i, j];
                if (tile.TileType != 112)
                {
                    tile = Main.tile[i, j];
                    if (tile.TileType != 116)
                    {
                        tile = Main.tile[i, j];
                        if (tile.TileType != 234)
                        {
                            tile = Main.tile[i, j];
                            if (!TileLoader.CanGrowModCactus(tile.TileType))
                            {
                                tile = Main.tile[i, j];
                                if (tile.TileType != cactus)
                                {
                                    return;
                                }

                                while (true)
                                {
                                    tile = Main.tile[num2, num];
                                    if (!tile.HasTile)
                                    {
                                        break;
                                    }

                                    tile = Main.tile[num2, num];
                                    if (tile.TileType != cactus)
                                    {
                                        break;
                                    }

                                    num++;
                                    tile = Main.tile[num2, num];
                                    if (tile.HasTile)
                                    {
                                        tile = Main.tile[num2, num];
                                        if (tile.TileType == cactus)
                                        {
                                            continue;
                                        }
                                    }

                                    tile = Main.tile[num2 - 1, num];
                                    if (tile.HasTile)
                                    {
                                        tile = Main.tile[num2 - 1, num];
                                        if (tile.TileType == cactus)
                                        {
                                            tile = Main.tile[num2 - 1, num - 1];
                                            if (tile.HasTile)
                                            {
                                                tile = Main.tile[num2 - 1, num - 1];
                                                if (tile.TileType == cactus && num2 >= i)
                                                {
                                                    num2--;
                                                }
                                            }
                                        }
                                    }   

                                    tile = Main.tile[num2 + 1, num];
                                    if (!tile.HasTile)
                                    {
                                        continue;
                                    }

                                    tile = Main.tile[num2 + 1, num];
                                    if (tile.TileType != cactus)
                                    {
                                        continue;
                                    }

                                    tile = Main.tile[num2 + 1, num - 1];
                                    if (tile.HasTile)
                                    {
                                        tile = Main.tile[num2 + 1, num - 1];
                                        if (tile.TileType == cactus && num2 <= i)
                                        {
                                            num2++;
                                        }
                                    }
                                }

                                num--;
                                int num5 = num - j;
                                int num6 = i - num2;
                                num2 = i - num6;
                                num = j;
                                int num7 = 11 - num5;
                                int num8 = 0;
                                for (int m = num2 - 2; m <= num2 + 2; m++)
                                {
                                    for (int n = num - num7; n <= num + num5; n++)
                                    {
                                        tile = Main.tile[m, n];
                                        if (tile.HasTile)
                                        {
                                            tile = Main.tile[m, n];
                                            if (tile.TileType == cactus)
                                            {
                                                num8++;
                                            }
                                        }
                                    }
                                }

                                if (Main.drunkWorld)
                                {
                                    if (num8 >= WorldGen.genRand.Next(11, 20))
                                    {
                                        return;
                                    }
                                }
                                else if (num8 >= WorldGen.genRand.Next(11, 13))
                                {
                                    return;
                                }

                                num2 = i;
                                num = j;
                                if (num6 == 0)
                                {
                                    if (num5 == 0)
                                    {
                                        tile = Main.tile[num2, num - 1];
                                        if (!tile.HasTile)
                                        {
                                            tile = Main.tile[num2, num - 1];
                                            tile.HasTile = true;
                                            tile = Main.tile[num2, num - 1];
                                            tile.TileType = (ushort)cactus;
                                            WorldGen.SquareTileFrame(num2, num - 1);
                                            if (Main.netMode == 2)
                                            {
                                                NetMessage.SendTileSquare(-1, num2, num - 1);
                                            }
                                        }

                                        return;
                                    }

                                    bool flag = false;
                                    bool flag2 = false;
                                    tile = Main.tile[num2, num - 1];
                                    if (tile.HasTile)
                                    {
                                        tile = Main.tile[num2, num - 1];
                                        if (tile.TileType == cactus)
                                        {
                                            tile = Main.tile[num2 - 1, num];
                                            if (!tile.HasTile)
                                            {
                                                tile = Main.tile[num2 - 2, num + 1];
                                                if (!tile.HasTile)
                                                {
                                                    tile = Main.tile[num2 - 1, num - 1];
                                                    if (!tile.HasTile)
                                                    {
                                                        tile = Main.tile[num2 - 1, num + 1];
                                                        if (!tile.HasTile)
                                                        {
                                                            tile = Main.tile[num2 - 2, num];
                                                            if (!tile.HasTile)
                                                            {
                                                                flag = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            tile = Main.tile[num2 + 1, num];
                                            if (!tile.HasTile)
                                            {
                                                tile = Main.tile[num2 + 2, num + 1];
                                                if (!tile.HasTile)
                                                {
                                                    tile = Main.tile[num2 + 1, num - 1];
                                                    if (!tile.HasTile)
                                                    {
                                                        tile = Main.tile[num2 + 1, num + 1];
                                                        if (!tile.HasTile)
                                                        {
                                                            tile = Main.tile[num2 + 2, num];
                                                            if (!tile.HasTile)
                                                            {
                                                                flag2 = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    int num9 = WorldGen.genRand.Next(3);
                                    if (num9 == 0 && flag)
                                    {
                                        tile = Main.tile[num2 - 1, num];
                                        tile.HasTile = true;
                                        tile = Main.tile[num2 - 1, num];
                                        tile.TileType = (ushort)cactus;
                                        WorldGen.SquareTileFrame(num2 - 1, num);
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendTileSquare(-1, num2 - 1, num);
                                        }
                                    }
                                    else if (num9 == 1 && flag2)
                                    {
                                        tile = Main.tile[num2 + 1, num];
                                        tile.HasTile = true;
                                        tile = Main.tile[num2 + 1, num];
                                        tile.TileType = (ushort)cactus;
                                        WorldGen.SquareTileFrame(num2 + 1, num);
                                        if (Main.netMode == 2)
                                        {
                                            NetMessage.SendTileSquare(-1, num2 + 1, num);
                                        }
                                    }
                                    else
                                    {
                                        if (num5 >= WorldGen.genRand.Next(2, 8))
                                        {
                                            return;
                                        }

                                        tile = Main.tile[num2 - 1, num - 1];
                                        if (tile.HasTile)
                                        {
                                            tile = Main.tile[num2 - 1, num - 1];
                                            _ = ref tile.TileType;
                                        }

                                        tile = Main.tile[num2 + 1, num - 1];
                                        if (tile.HasTile)
                                        {
                                            tile = Main.tile[num2 + 1, num - 1];
                                            if (tile.TileType == cactus)
                                            {
                                                return;
                                            }
                                        }

                                        tile = Main.tile[num2, num - 1];
                                        if (!tile.HasTile)
                                        {
                                            tile = Main.tile[num2, num - 1];
                                            tile.HasTile = true;
                                            tile = Main.tile[num2, num - 1];
                                            tile.TileType = (ushort)cactus;
                                            WorldGen.SquareTileFrame(num2, num - 1);
                                            if (Main.netMode == 2)
                                            {
                                                NetMessage.SendTileSquare(-1, num2, num - 1);
                                            }
                                        }
                                    }

                                    return;
                                }

                                tile = Main.tile[num2, num - 1];
                                if (tile.HasTile)
                                {
                                    return;
                                }

                                tile = Main.tile[num2, num - 2];
                                if (tile.HasTile)
                                {
                                    return;
                                }

                                tile = Main.tile[num2 + num6, num - 1];
                                if (tile.HasTile)
                                {
                                    return;
                                }

                                tile = Main.tile[num2 - num6, num - 1];
                                if (!tile.HasTile)
                                {
                                    return;
                                }

                                tile = Main.tile[num2 - num6, num - 1];
                                if (tile.TileType == cactus)
                                {
                                    tile = Main.tile[num2, num - 1];
                                    tile.HasTile = true;
                                    tile = Main.tile[num2, num - 1];
                                    tile.TileType = (ushort)cactus;
                                    WorldGen.SquareTileFrame(num2, num - 1);
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendTileSquare(-1, num2, num - 1);
                                    }
                                }

                                return;
                            }
                        }
                    }
                }
            }

            tile = Main.tile[i, j - 1];
            if (tile.HasTile)
            {
                return;
            }

            tile = Framing.GetTileSafely(i - 1, j - 1);
            if (tile.HasTile)
            {
                return;
            }

            tile = Main.tile[i + 1, j - 1];
            if (tile.HasTile)
            {
                return;
            }

            int num10 = 0;
            int num11 = 0;
            for (int num12 = i - 6; num12 <= i + 6; num12++)
            {
                for (int num13 = j - 3; num13 <= j + 1; num13++)
                {
                    try
                    {
                        tile = Framing.GetTileSafely(num12, num13);
                        if (!tile.HasTile)
                        {
                            continue;
                        }

                        tile = Main.tile[num12, num13];
                        if (tile.TileType == cactus)
                        {
                            num10++;
                            if (num10 >= 4)
                            {
                                return;
                            }
                        }

                        tile = Main.tile[num12, num13];
                        if (tile.TileType == 53)
                        {
                            goto IL_032c;
                        }

                        tile = Main.tile[num12, num13];
                        if (tile.TileType == 112)
                        {
                            goto IL_032c;
                        }

                        tile = Main.tile[num12, num13];
                        if (tile.TileType == 116)
                        {
                            goto IL_032c;
                        }

                        tile = Main.tile[num12, num13];
                        if (tile.TileType == 234)
                        {
                            goto IL_032c;
                        }

                        tile = Main.tile[num12, num13];
                        if (TileLoader.CanGrowModCactus(tile.TileType))
                        {
                            goto IL_032c;
                        }

                        goto end_IL_0257;
                        IL_032c:
                        num11++;
                        end_IL_0257:;
                    }
                    catch
                    {
                    }
                }
            }

            if (num11 > 10)
            {
                if (WorldGen.gen && WorldGen.genRand.Next(2) == 0)
                {
                    tile = Main.tile[i, j];
                    tile.Slope = 0;
                }

                tile = Main.tile[i, j - 1];
                tile.HasTile = true;
                tile = Main.tile[i, j - 1];
                tile.TileType = (ushort)cactus;
                if (Main.netMode == 2)
                {
                    NetMessage.SendTileSquare(-1, i, j - 1);
                }

                WorldGen.SquareTileFrame(num2, num - 1);
            }
        }

    }
}
