﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Projectiles.BossWeapons;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class GeminiGlaives : ModItem
    {
        int lastThrown = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gemini Glaives");
            Tooltip.SetDefault("Fire a different glaive depending on mouse click" +
                "Alternating clicks will enhance attacks" +
                "'The compressed forms of defeated foes..'");

            Tooltip.AddTranslation(GameCulture.Chinese, "被打败的敌人的压缩形态..");
        }

        public override void SetDefaults()
        {
            item.damage = 200; //
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 100000;
            item.rare = 5;
            item.shootSpeed = 20;
            item.shoot = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = ModContent.ProjectileType<Retiglaive>();
                item.shootSpeed = 15f;
            }
            else
            {
                item.shoot = ModContent.ProjectileType<Spazmaglaive>();
                item.shootSpeed = 45f;
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                damage = 0;
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, lastThrown);

            lastThrown = type;

            return false;
        }
    }
}