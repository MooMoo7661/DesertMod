using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DesertMod
{
    public class GlobalCactus : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.RollingCactus;
        }

        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
           int homingType = ModContent.GetInstance<DesertModConfig>().HomingType;
            if (homingType == 2)
            {
                projectile.ai[1] = 0f;
            }
            else if (homingType == 3)
            {
                projectile.ai[1] = 1f;
            }
            
            if (projectile.ai[1] != 1f)
            {
                float maxDetectRadius = 800f;
                float projSpeed = 9f;

                projectile.rotation += 0.4f;
                projectile.hostile = true;
                projectile.friendly = false;
                projectile.tileCollide = false;

                Player closestNPC = FindClosestNPC(maxDetectRadius, projectile);

                if (closestNPC is null)
                {
                    projectile.Kill();
                    return true;
                }

                projectile.velocity = (closestNPC.Center - projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                return false;
            }

            return true;
        }

        public Player FindClosestNPC(float maxDetectDistance, Projectile projectile)
        {
            Player closestNPC = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                Player target = Main.player[k];

                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, projectile.Center);


                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }

            return closestNPC;
        }
    }
}
