using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.GameContent;

namespace DesertMod
{
    public class WorldGenUtils
    {

        public static void GetAroundTiles(int x, int y, out Tile left, out Tile right, out Tile up, out Tile down)
        {
            left = Framing.GetTileSafely(x + 1, y);
            right = Framing.GetTileSafely(x - 1, y);
            up = Framing.GetTileSafely(x, y - 1);
            down = Framing.GetTileSafely(x, y + 1);
        }
    }
}
