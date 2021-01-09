using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class ShadowForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Force");

            string tooltip = @"Three Shadow Orbs will orbit around you
Your attacks may inflict Darkness on enemies
Slain enemies may drop a pile of bones
All of your minions gain an extra scythe attack
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Don't attack to gain a single use monk dash
Dash into any walls, to teleport through them to the next opening
Summons a Flameburst minion that will travel to your mouse after charging up
While attacking, Flameburst shots manifest themselves from your shadows
Greatly enhances Flameburst and Lightning Aura effectiveness
Effects of Master Ninja Gear
Summons several pets
'Dark, Darker, Yet Darker'";

            string tooltip_ch = @"'Dark, Darker, Yet Darker'
攻击概率造成黑暗
陷入黑暗的敌人偶尔会向其他敌人发射暗影烈焰触手
地牢守卫者偶尔会在你受到攻击时消灭敌人
所有召唤物偶尔会发射巨大镰刀
投掷烟雾弹进行传送,并获得先发制人Buff
使用裂位法杖也会获得该Buff
冲进墙壁时,会直接穿过
攻击时,焰爆炮塔的射击会从你的阴影中显现出来
大大增强焰爆炮塔和闪电光环能力
拥有忍者极意的效果
召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "暗影之力");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
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
            //warlock, shade, plague accessory effect for all
            modPlayer.ShadowForce = true;
            //shoot from where you were meme, pet
            modPlayer.DarkArtistEffect(hideVisual);
            modPlayer.ApprenticeEffect();

            //DG meme, pet
            modPlayer.NecroEffect(hideVisual);
            //shadow orbs
            modPlayer.AncientShadowEffect();
            //darkness debuff, pets
            modPlayer.ShadowEffect(hideVisual);
            //ninja gear
            player.blackBelt = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.ShinobiClimbing))
                player.spikedBoots = 2;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.ShinobiTabi, false))
                player.dash = 1;
            //tele thru walls, pet
            modPlayer.ShinobiEffect(hideVisual);
            //monk dash mayhem
            modPlayer.MonkEffect();
            //smoke bomb nonsense, pet
            modPlayer.NinjaEffect(hideVisual);
            //scythe doom, pets
            modPlayer.SpookyEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "AncientShadowEnchant");
            recipe.AddIngredient(null, "NecroEnchant");
            recipe.AddIngredient(null, "SpookyEnchant");
            recipe.AddIngredient(null, "ShinobiEnchant");
            recipe.AddIngredient(null, "DarkArtistEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}