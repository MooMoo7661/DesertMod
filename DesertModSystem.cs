using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI.States;

namespace DesertMod
{
    public class DesertModSystem : ModSystem
    {
        public Asset<Texture2D> original;

        public override void PostSetupContent()
        {
            original = TextureAssets.Sun;
            TextureAssets.Sun = ModContent.Request<Texture2D>("DesertMod/Cactus"); 
        }

        public override void Unload()
        {
            TextureAssets.Sun = original;
        }
    }
}
