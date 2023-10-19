using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace DesertMod
{
	public class DesertMod : Mod
	{
		public override void Load()
		{
			On_WorldGen.ShakeTree += On_WorldGen_ShakeTree;

			On_WorldGen.CheckPot += WorldGen_CheckPot;
		}

		private void On_WorldGen_ShakeTree(On_WorldGen.orig_ShakeTree orig, int i, int j)
		{           
            orig(i, j);

            WorldGen.GetTreeBottom(i, j, out int x, out int y);

            TreeTypes treeType = WorldGen.GetTreeType((int)Main.tile[x, y].TileType);

            if (!Main.dedServ && treeType == TreeTypes.Palm)
            {
                y--;
                while (y > 10 && Main.tile[x, y].HasTile && TileID.Sets.IsShakeable[(int)Main.tile[x, y].TileType]) { y--; }

                y++;

                if (!WorldGen.IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
                {
                    return;
                }

                if (Main.rand.NextBool(7))
                Projectile.NewProjectile(WorldGen.GetNPCSource_ShakeTree(x, y), new Vector2(x * 16, y * 16), new Vector2(0, -1), ProjectileID.RollingCactus, 0, 0f, Main.myPlayer, 0, 1f);
            }
        }

		public override void Unload()
		{
            On_WorldGen.ShakeTree -= On_WorldGen_ShakeTree;
            On_WorldGen.CheckPot -= WorldGen_CheckPot;
		}

        private void WorldGen_CheckPot(On_WorldGen.orig_CheckPot func, int i, int j, int type)
        {
            if (Main.rand.NextBool(10))
            {
                int proj = Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 0f, 0f, ProjectileID.RollingCactus, 1000, 7f, Main.myPlayer, 0, 1f);
            }

            func(i, j, type);
        }
	}
}