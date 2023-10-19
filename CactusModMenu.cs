using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace DesertMod
{
    public class CactusModMenu : ModMenu
    {
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("DesertMod/CactusModMenu", (AssetRequestMode)2);

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("DesertMod/Cactus", (AssetRequestMode)2);

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("DesertMod/Cactus", (AssetRequestMode)2);
    }
}
