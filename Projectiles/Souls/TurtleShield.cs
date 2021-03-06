﻿using FargowiltasSouls.Buffs.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Souls
{
    public class TurtleShield : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;

            Main.projFrames[projectile.type] = 7;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (projectile.frame != 6)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 4)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = (projectile.frame + 1) % 7;
                }
            }

            if (modPlayer.TurtleShellHP <= 3)
            {
                projectile.localAI[0] = 1;
            }

            if (!modPlayer.ShellHide)
            {
                projectile.Kill();
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.localAI[0] == 1)
            {
                return new Color(255, 132, 105);
            }

            return base.GetAlpha(lightColor);
        }
    }
}