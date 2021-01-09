using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TimberForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Timber");

            Tooltip.SetDefault(
@"Critters will explode 1 second after being released
50% chance to not consume critters
Attacks will periodically be accompanied by several snowballs
All grappling hooks shoot, pull, and retract 2.5x as fast
You have an aura of Shadowflame, Cursed Flames, and Bleeding
Double tap down to spawn a palm tree sentry that throws nuts at enemies
Projectiles may spawn a star when they hit something
'Extremely rigid'");
            DisplayName.AddTranslation(GameCulture.Chinese, "森林之力");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'很刚'
大幅增加动物防御力
杀死动物不会再获得内疚Debuff
动物死后,释放它们的灵魂来帮助你
每5次攻击附带着数个雪球
所有抓钩速度翻倍
所有抓钩会定期向敌人发射追踪射击
周围环绕巨大暗影烈焰光环
受伤时,对敌人造成大出血
留下一道可以让敌人退缩的彩虹路径");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = ItemRarityID.Purple;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.WoodForce = true;
            //wood
            modPlayer.WoodEnchant = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            //boreal
            modPlayer.BorealEnchant = true;
            modPlayer.AdditionalAttacks = true;
            //mahogany
            modPlayer.MahoganyEnchant = true;

            //ebon
            if (!modPlayer.TerrariaSoul)
                modPlayer.EbonEffect();

            //shade
            modPlayer.ShadeEnchant = true;
            //palm
            modPlayer.PalmEffect();
            //pearl
            modPlayer.PearlEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WoodEnchant");
            recipe.AddIngredient(null, "BorealWoodEnchant");
            recipe.AddIngredient(null, "RichMahoganyEnchant");
            recipe.AddIngredient(null, "EbonwoodEnchant");
            recipe.AddIngredient(null, "ShadewoodEnchant");
            recipe.AddIngredient(null, "PalmWoodEnchant");
            recipe.AddIngredient(null, "PearlwoodEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}