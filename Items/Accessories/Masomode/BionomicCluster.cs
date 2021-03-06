﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class BionomicCluster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bionomic Cluster");
            Tooltip.SetDefault(@"Grants immunity to Frostburn, Shadowflame, Squeaky Toy, Guilty, Mighty Wind, and Suffocation
Grants immunity to Flames of the Universe, Clipped Wings, Crippled, Webbed, and Purified
Grants immunity to Lovestruck, Stinky, Midas, Hexed, cactus damage, and enemies that steal items
Your attacks can inflict Clipped Wings, spawn Frostfireballs, and produce hearts
You have autofire, improved night vision, and faster respawn when no boss is alive
Automatically use mana potions when needed
Attacks have a chance to squeak and deal 1 damage to you
You erupt into Shadowflame tentacles when injured and respawn with more life
Certain enemies will drop potions when defeated and 50% discount on reforges
Summons a friendly rainbow slime
Use to teleport to your last death point and right click to zoom
'The amalgamate born of a thousand common enemies'");
            DisplayName.AddTranslation(GameCulture.Chinese, "生态集群");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'由上千普通敌人融合而成'
免疫寒焰,暗影烈焰,吱吱响的玩具,内疚,强风和窒息
免疫宇宙之火,剪除羽翼,残疾,织网和净化
免疫热恋,恶臭,点金手,着魔,仙人掌的伤害和偷取物品的敌人
攻击造成剪除羽翼,发射霜火球,并且产生心
一键连发,提高夜视能力,
没有Boss存活时,重生速度加快
在需要时自动使用魔力药水,并给予词缀保护
敌人攻击概率无效,只造成1点伤害
受伤时爆发暗影烈焰触须
召唤一个友善的彩虹史莱姆
按下快捷键传送到上次死亡地点");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
            item.defense = 6;

            item.useTime = 90;
            item.useAnimation = 90;
            item.useStyle = 4;
            item.useTurn = true;
            item.UseSound = SoundID.Item6;
        }

        public override void UpdateInventory(Player player)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.manaFlower = true;
            fargoPlayer.SecurityWallet = true;
            fargoPlayer.TribalCharm = true;
            fargoPlayer.NymphsPerfumeRespawn = true;
            player.nightVision = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.Carrot, false))
                player.scope = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

            //concentrated rainbow matter
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.RainbowSlime))
                player.AddBuff(mod.BuffType("RainbowSlime"), 2);

            //dragon fang
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.DragonFang))
                fargoPlayer.DragonFang = true;

            //frigid gemstone
            player.buffImmune[BuffID.Frostburn] = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.FrigidGemstone))
            {
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD--;
            }

            //wretched pouch
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[mod.BuffType("Shadowflame")] = true;
            player.GetModPlayer<FargoPlayer>().WretchedPouch = true;

            //sands of time
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;

            //squeaky toy
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            fargoPlayer.SqueakyAcc = true;

            //tribal charm
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            fargoPlayer.TribalCharm = true;

            //mystic skull
            player.buffImmune[BuffID.Suffocation] = true;
            player.manaFlower = true;

            //security wallet
            player.buffImmune[mod.BuffType("Midas")] = true;
            fargoPlayer.SecurityWallet = true;

            //carrot
            player.nightVision = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.Carrot, false))
                player.scope = true;

            //nymph's perfume
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[mod.BuffType("Lovestruck")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            player.buffImmune[BuffID.Stinky] = true;
            fargoPlayer.NymphsPerfumeRespawn = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.NymphPerfume))
            {
                fargoPlayer.NymphsPerfume = true;
                if (fargoPlayer.NymphsPerfumeCD > 0)
                    fargoPlayer.NymphsPerfumeCD--;
            }

            //tim's concoction
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.TimsConcoction))
                player.GetModPlayer<FargoPlayer>().TimsConcoction = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.lastDeathPostion != Vector2.Zero;
        }

        public override bool UseItem(Player player)
        {
            for (int index = 0; index < 70; ++index)
            {
                int d = Dust.NewDust(player.position, player.width, player.height, 87, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, new Color(), 1.5f);
                Main.dust[d].velocity *= 4f;
                Main.dust[d].noGravity = true;
            }

            player.grappling[0] = -1;
            player.grapCount = 0;
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
                    Main.projectile[index].Kill();
            }

            if (player.whoAmI == Main.myPlayer)
            {
                player.Teleport(player.lastDeathPostion, 1);
                player.velocity = Vector2.Zero;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 1);
            }

            for (int index = 0; index < 70; ++index)
            {
                int d = Dust.NewDust(player.position, player.width, player.height, 87, 0.0f, 0.0f, 150, new Color(), 1.5f);
                Main.dust[d].velocity *= 4f;
                Main.dust[d].noGravity = true;
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("ConcentratedRainbowMatter"));
            recipe.AddIngredient(mod.ItemType("WyvernFeather"));
            recipe.AddIngredient(mod.ItemType("FrigidGemstone"));
            recipe.AddIngredient(mod.ItemType("SandsofTime"));
            recipe.AddIngredient(mod.ItemType("SqueakyToy"));
            recipe.AddIngredient(mod.ItemType("TribalCharm"));
            recipe.AddIngredient(mod.ItemType("MysticSkull"));
            recipe.AddIngredient(mod.ItemType("SecurityWallet"));
            recipe.AddIngredient(mod.ItemType("OrdinaryCarrot"));
            recipe.AddIngredient(mod.ItemType("WretchedPouch"));
            recipe.AddIngredient(mod.ItemType("NymphsPerfume"));
            recipe.AddIngredient(mod.ItemType("TimsConcoction"));
            //recipe.AddIngredient(ItemID.SoulofLight, 20);
            //recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddIngredient(mod.ItemType("DeviatingEnergy"), 10);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
