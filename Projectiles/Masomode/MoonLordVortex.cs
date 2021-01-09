using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class MoonLordVortex : Champions.CosmosVortex
    {
        public override string Texture => "Terraria/Projectile_578";

        public override bool CanDamage()
        {
            return projectile.scale >= 2;
        }

        public override void AI()
        {
            const int time = 1200;
            const int maxScale = 3;
            const float suckRange = 150;

            void Suck()
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].friendly && Main.projectile[i].damage > 0 && !Main.projectile[i].minion && Main.projectile[i].Distance(projectile.Center) < suckRange)
                    {
                        //suck in nearby friendly projs
                        Main.projectile[i].velocity = Main.projectile[i].DirectionTo(projectile.Center) * Main.projectile[i].velocity.Length();
                        Main.projectile[i].velocity *= 1.015f;

                        //kill ones that actually fall in and retaliate
                        if (Main.netMode != NetmodeID.MultiplayerClient && projectile.Colliding(projectile.Hitbox, Main.projectile[i].Hitbox))
                        {
                            Player player = Main.player[Main.projectile[i].owner];
                            if (player.active && !player.dead && !player.ghost && projectile.localAI[1] <= 0)
                            {
                                projectile.localAI[1] = 2;

                                Vector2 dir = projectile.DirectionTo(player.Center);
                                float ai1New = (Main.rand.Next(2) == 0) ? 1 : -1; //randomize starting direction
                                Vector2 vel = Vector2.Normalize(dir) * 6f;
                                Projectile.NewProjectile(projectile.Center, vel * 6, ModContent.ProjectileType<Champions.CosmosLightning>(),
                                    projectile.damage, 0, Main.myPlayer, dir.ToRotation(), ai1New);
                            }
                            Main.projectile[i].Kill();
                        }
                    }
                }
            };

            if (projectile.localAI[1] > 0)
                projectile.localAI[1]--;

            int ai1 = (int)projectile.ai[1];
            if (ai1 > -1 && ai1 < Main.maxNPCs && Main.npc[ai1].active && Main.npc[ai1].type == NPCID.MoonLordCore
                && Main.npc[ai1].ai[0] != 2f && EModeGlobalNPC.masoStateML == 1)
            {
                projectile.localAI[0]++;

                Vector2 offset;
                offset.X = 300f * (float)Math.Sin(Math.PI * 2 / 240 * projectile.localAI[0]);
                offset.Y = 150f * (float)Math.Sin(Math.PI * 2 / 120 * projectile.localAI[0]);

                projectile.Center = Main.npc[ai1].Center + offset;
            }
            else
            {
                projectile.Kill();
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * suckRange);
                offset.Y += (float)(Math.Cos(angle) * suckRange);
                Dust dust = Main.dust[Dust.NewDust(
                    projectile.Center + offset - new Vector2(4, 4), 0, 0,
                    229, 0, 0, 100, Color.White, 1f
                    )];
                dust.velocity = Main.npc[ai1].velocity / 3;
                if (Main.rand.Next(3) == 0)
                    dust.velocity += Vector2.Normalize(offset);
                dust.noGravity = true;
            }

            projectile.ai[0]++;
            if (projectile.ai[0] <= 50)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515);
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * Main.rand.Next(10, 21);
                    dust.velocity = spinningpoint.RotatedBy(1.57079637050629, new Vector2()) * 4f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515);
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * 30f;
                    dust.velocity = spinningpoint.RotatedBy(-1.57079637050629, new Vector2()) * 2f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                }
            }
            else if (projectile.ai[0] <= 90)
            {
                projectile.scale = (projectile.ai[0] - 50) / 40 * maxScale;
                projectile.alpha = 255 - (int)(255 * projectile.scale / maxScale);
                projectile.rotation = projectile.rotation - 0.1570796f;
                if (Main.rand.Next(2) == 0)
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * Main.rand.Next(10, 21);
                    dust.velocity = spinningpoint.RotatedBy(1.57079637050629, new Vector2()) * 6f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                    dust.customData = projectile.Center;
                }
                if (Main.rand.Next(2) == 0)
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * 30f;
                    dust.velocity = spinningpoint.RotatedBy(-1.57079637050629, new Vector2()) * 3f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                    dust.customData = projectile.Center;
                }

                //Suck();
            }
            else if (projectile.ai[0] <= 90 + time)
            {
                projectile.scale = maxScale;
                projectile.alpha = 0;
                projectile.rotation = projectile.rotation - (float)Math.PI / 60f;
                if (Main.rand.Next(2) == 0)
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * Main.rand.Next(10, 21);
                    dust.velocity = spinningpoint.RotatedBy(1.57079637050629, new Vector2()) * 6f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                    dust.customData = projectile.Center;
                }
                else
                {
                    Vector2 spinningpoint = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - spinningpoint * 30f, 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.noGravity = true;
                    dust.position = projectile.Center - spinningpoint * 30f;
                    dust.velocity = spinningpoint.RotatedBy(-1.57079637050629, new Vector2()) * 3f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                    dust.customData = projectile.Center;
                }

                Suck();
            }
            else
            {
                projectile.scale = (float)(1.0 - (projectile.ai[0] - time) / 60.0) * maxScale;
                projectile.alpha = 255 - (int)(255 * projectile.scale / maxScale);
                projectile.rotation = projectile.rotation - (float)Math.PI / 30f;
                if (projectile.alpha >= 255)
                    projectile.Kill();
                for (int index = 0; index < 2; ++index)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Vector2 spinningpoint1 = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                            Dust dust1 = Main.dust[Dust.NewDust(projectile.Center - spinningpoint1 * 30f, 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
                            dust1.noGravity = true;
                            dust1.position = projectile.Center - spinningpoint1 * Main.rand.Next(10, 21);
                            dust1.velocity = spinningpoint1.RotatedBy(1.57079637050629, new Vector2()) * 6f;
                            dust1.scale = 0.5f + Main.rand.NextFloat();
                            dust1.fadeIn = 0.5f;
                            dust1.customData = projectile.Center;
                            break;
                        case 1:
                            Vector2 spinningpoint2 = Vector2.UnitY.RotatedByRandom(6.28318548202515) * projectile.scale;
                            Dust dust2 = Main.dust[Dust.NewDust(projectile.Center - spinningpoint2 * 30f, 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
                            dust2.noGravity = true;
                            dust2.position = projectile.Center - spinningpoint2 * 30f;
                            dust2.velocity = spinningpoint2.RotatedBy(-1.57079637050629, new Vector2()) * 3f;
                            dust2.scale = 0.5f + Main.rand.NextFloat();
                            dust2.fadeIn = 0.5f;
                            dust2.customData = projectile.Center;
                            break;
                    }
                }
            }
            
            Dust dust3 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, new Color(), 1f)];
            dust3.velocity *= 5f;
            dust3.fadeIn = 1f;
            dust3.scale = 1f + Main.rand.NextFloat() + Main.rand.Next(4) * 0.3f;
            dust3.noGravity = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 360);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item92, projectile.Center);
            int type = 229;
            for (int index = 0; index < 80; ++index)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, type, 0.0f, 0.0f, 0, new Color(), 1f)];
                dust.velocity *= 10f;
                dust.fadeIn = 1f;
                dust.scale = 1 + Main.rand.NextFloat() + Main.rand.Next(4) * 0.3f;
                if (Main.rand.Next(3) != 0)
                {
                    dust.noGravity = true;
                    dust.velocity *= 3f;
                    dust.scale *= 2f;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.Black * projectile.Opacity, -projectile.rotation, origin2, projectile.scale * 1.25f, SpriteEffects.FlipHorizontally, 0f);
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}