using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class MoonLordNebulaBlaze2 : MoonLordNebulaBlaze
    {
        public override string Texture => "Terraria/Projectile_634";

        public override void AI()
        {
            base.AI();

            int ai0 = (int)projectile.ai[0];
            if (!(ai0 > -1 && ai0 < Main.maxNPCs && Main.npc[ai0].active && Main.npc[ai0].type == NPCID.MoonLordCore 
                && Main.npc[ai0].ai[0] != 2f && EModeGlobalNPC.masoStateML == 2))
            {
                projectile.Kill();
                return;
            }

            if (projectile.ai[1] == 0) //identify the ritual CLIENT SIDE
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<LunarRitual>() && Main.projectile[i].ai[1] == projectile.ai[0])
                    {
                        projectile.localAI[1] = i;
                        break;
                    }
                }
            }

            int localAi1 = (int)projectile.localAI[1];
            if (localAi1 > -1 && localAi1 < Main.maxProjectiles && Main.projectile[localAi1].active && Main.projectile[localAi1].type == ModContent.ProjectileType<LunarRitual>() && Main.projectile[localAi1].ai[1] == projectile.ai[0])
            {
                if (projectile.Distance(Main.projectile[localAi1].Center) > 1600f) //bounce off arena walls
                {
                    projectile.velocity = -projectile.velocity;
                    Vector2 toCenter = Main.projectile[localAi1].Center - projectile.Center;
                    float rotationDifference = toCenter.ToRotation() - projectile.velocity.ToRotation();
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.WrapAngle(2 * rotationDifference));
                }
            }
            else
            {
                projectile.Kill();
                return;
            }
        }
    }
}