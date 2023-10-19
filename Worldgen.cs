using DesertMod.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace DesertMod
{

    public class Worldgen : ModSystem
    {
        public override void PostWorldGen()
        {
            var instance = ModContent.GetInstance<DesertModConfig>();

            if (instance.PaintDungeon)
                WorldGenTasks.PaintDungeon();

            if (instance.PaintHives)
            WorldGenTasks.PaintHives();

            if (instance.PaintTemple)
            WorldGenTasks.PaintTemple();

            if (!instance.SpecialWorldgen) { return; }

            WorldGenTasks.PlaceDesertPlants();
            WorldGenTasks.ReplaceDirtWalls();
            WorldGenTasks.ConvertHellToSandstone();
            WorldGenTasks.ConvertHellToHardenedSand();
            WorldGenTasks.CleanupHellConversion();
            WorldGenTasks.GrowCacti();
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            var instance = ModContent.GetInstance<DesertModConfig>();

            if (!instance.SpecialWorldgen) { return; }

            int DesertIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Full Desert"));
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -1)
            {
                tasks.Insert(DesertIndex + 1, new SandstonePass("Sandstone Generation", 237.4298f));
                tasks.Insert(DesertIndex + 2, new FossilPass("Fossil Generation", 237.4298f));
                tasks.Insert(DesertIndex + 3, new UndergroundDesertPass("Generating Excessive Deserts(This will take a while!)", 237.4298f));
            }
        }
    }

    public class SandstonePass : GenPass
    {
        public SandstonePass(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Extreme desertification...";
            

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.002); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next(0, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.HasTile && tile.TileType == TileID.Stone)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(18, 40), WorldGen.genRand.Next(20, 61), TileID.Sandstone);
                }
            }

            Desertification();
        }

        public static void Desertification()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if ((tile.TileType == TileID.Dirt || tile.TileType == TileID.Grass) && tile.HasTile)
                    {
                        if (!WorldGen.SolidOrSlopedTile(x, y + 1))
                        {
                            if (Framing.GetTileSafely(x, y - 1).HasTile)
                            {
                                if (WorldGen.genRand.NextBool() && Framing.GetTileSafely(x, y - 2).HasTile)
                                {
                                    WorldGen.PlaceTile(x, y - 2, TileID.HardenedSand, false, true);
                                }

                                WorldGen.PlaceTile(x, y - 1, TileID.HardenedSand, false, true);
                            }

                            WorldGen.PlaceTile(x, y, TileID.HardenedSand, false, true);
                        }
                        else
                        {
                            WorldGen.PlaceTile(x, y, TileID.Sand, false, true);
                        }
                    }
                }
            }
        }
    }

    public class FossilPass : GenPass
    {
        public FossilPass(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Fossilization...";

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.002); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next(0, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);

                if (tile.HasTile && tile.TileType == TileID.Sandstone)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(13, 25), WorldGen.genRand.Next(12, 27), TileID.DesertFossil);
                }
            }
        }
    }

    public class UndergroundDesertPass : GenPass
    {
        public UndergroundDesertPass(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating Excessive Deserts... (This will take a while!)";

            WorldGenTasks.GenerateUndergroundDesert();
        }
    }
}