using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DesertMod
{
    public class DesertModGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (Main.rand.NextBool(5) && !npc.CountsAsACritter)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_None(), npc.position.X, npc.position.Y, 0f, 0f, ProjectileID.RollingCactus, 1000, 7f, Main.myPlayer, 0, 1f);
            }

            if (npc.CountsAsACritter)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 vel = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(i * 45)) * (1 + i / 15f) * 6f;

                    int proj = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, vel,
                        ProjectileID.RollingCactus, 1000, 7f, Main.myPlayer, 0, 1);

                    int proj2 = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, vel,
                        ProjectileID.RollingCactusSpike, 20, 7f, Main.myPlayer, 0, 1);

                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;

                    for (int j = 0; j < 10; j++)
                    {
                        Vector2 velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 8f;
                        int proj3 = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, velocity,
                        ProjectileID.RollingCactusSpike, 20, 7f, Main.myPlayer, 0, 1);
                    }
                }
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (ModContent.GetInstance<DesertModConfig>().EveryNPCIsCactus || (ModContent.GetInstance<DesertModConfig>().BossTexturesAreCactus && (npc.boss || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail ||
                npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.DungeonGuardian || npc.type == NPCID.SkeletronHand)))
            {
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("DesertMod/Cactus");
                float longest = MathF.Max(npc.width, npc.height);
                float texLongest = MathF.Max(texture.Width, texture.Height);

                Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
                
                Main.spriteBatch.Draw(texture, npc.Center - screenPos, null, drawColor, npc.rotation, texture.Size() / 2, longest / texLongest, SpriteEffects.None, 0);

                return false;
            }

            return true;
        }
    }
}
