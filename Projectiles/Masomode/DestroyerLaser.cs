using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class DestroyerLaser : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_100";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.scale = 1.8f;
            projectile.timeLeft = 180;
            projectile.tileCollide = false;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = 1;
                Main.PlaySound(SoundID.Item12, projectile.Center);
            }
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            if (++projectile.localAI[1] > 20 && projectile.localAI[1] < 90)
                projectile.velocity *= 1.06f;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 8;
                if (projectile.alpha < 0)
                    projectile.alpha = 0;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.destroyBoss, NPCID.TheDestroyer))
                target.AddBuff(BuffID.Electrified, 90);
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
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}