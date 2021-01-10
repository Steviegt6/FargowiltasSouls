using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class MoonLordSunBlast : Champions.EarthChainBlast
    {
        public override string Texture => "Terraria/Projectile_687";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sun Blast");
        }

        public override void AI()
        {
            if (projectile.position.HasNaNs())
            {
                projectile.Kill();
                return;
            }
            /*Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, new Color(), 1f)];
            dust.position = projectile.Center;
            dust.velocity = Vector2.Zero;
            dust.noGravity = true;
            dust.noLight = true;*/

            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame--;
                    projectile.Kill();
                }
            }
            //if (++projectile.ai[0] > Main.projFrames[projectile.type] * 3) projectile.Kill();

            if (++projectile.localAI[1] == 8 && projectile.ai[1] > 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                projectile.ai[1]--;
                
                Vector2 baseDirection = projectile.ai[0].ToRotationVector2();
                float random = MathHelper.ToRadians(15);
                /*if (projectile.ai[1] > 2)
                {
                    for (int i = -1; i <= 1; i++) //split into more explosions
                    {
                        if (i == 0)
                            continue;
                        Vector2 offset = projectile.width * 0.25f * baseDirection.RotatedBy(MathHelper.ToRadians(30) * i + Main.rand.NextFloat(-random, random));
                        Projectile.NewProjectile(projectile.Center + offset, Vector2.Zero, projectile.type,
                            projectile.damage, 0f, projectile.owner, projectile.ai[0], projectile.ai[1]);
                    }
                }
                else
                {
                    Vector2 offset = projectile.width * 0.5f * baseDirection.RotatedBy(Main.rand.NextFloat(-random, random));
                    Projectile.NewProjectile(projectile.Center + offset, Vector2.Zero, projectile.type,
                        projectile.damage, 0f, projectile.owner, projectile.ai[0], projectile.ai[1]);
                }*/
                if (EModeGlobalNPC.masoStateML == 0)
                {
                    bool sustainBlast = true;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == NPCID.MoonLordFreeEye
                            && Main.npc[i].ai[0] == 4 && Main.npc[i].ai[1] > 970)
                        {
                            sustainBlast = false;
                            break;
                        }
                    }

                    //don't do sustained blasts in one place if deathrays are firing
                    if (sustainBlast)
                    {
                        Projectile.NewProjectile(projectile.Center + Main.rand.NextVector2Circular(20, 20), Vector2.Zero, projectile.type,
                        projectile.damage, 0f, projectile.owner, 22, projectile.ai[1] - 1); //22
                    }

                    if (projectile.ai[0] != 22) //no real reason for this number, just some unique identifier
                    {
                        Vector2 offset = projectile.width * 0.75f * baseDirection.RotatedBy(Main.rand.NextFloat(-random, random));
                        Projectile.NewProjectile(projectile.Center + offset, Vector2.Zero, projectile.type,
                              projectile.damage, 0f, projectile.owner, projectile.ai[0], projectile.ai[1]);
                    }
                }
            }

            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item88, projectile.Center);

                projectile.position = projectile.Center;
                projectile.scale = Main.rand.NextFloat(1f, 4f);
                projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
                projectile.width = (int)(projectile.width * projectile.scale);
                projectile.height = (int)(projectile.height * projectile.scale);
                projectile.Center = projectile.position;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            //target.AddBuff(BuffID.Burning, 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color = Color.White;
            //color = Color.Lerp(new Color(255, 95, 46, 50), new Color(150, 35, 0, 100), (4 - projectile.ai[1]) / 4);

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color,
                projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}

