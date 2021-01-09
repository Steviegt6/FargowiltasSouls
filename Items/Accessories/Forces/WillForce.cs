using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Items.Accessories.Enchantments;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");

            string tooltip =
@"Your attacks inflict Midas
Press the Gold hotkey to be encased in a Golden Shell
You will not be able to move or attack, but will be immune to all damage
20% chance for enemies to drop 5x loot
Spears will rain down on struck enemies
Double tap down to create a localized rain of arrows
Continually attacking an enemy will grant you the Power of Valhalla buff
Greatly enhances Ballista and Explosive Traps effectiveness
Effects of Greedy Ring, Celestial Shell, and Shiny Stone
Summons several pets
'A mind of unbreakable determination'";
            string tooltip_ch =
@"'坚不可摧的决心'
攻击造成点金手和大出血
按下金身热键,使自己被包裹在一个黄金壳中
你将不能移动或攻击,但免疫所有伤害
敌人20%概率8倍掉落
长矛将倾泄在被攻击的敌人身上
对低血量的敌人伤害增加
所有的攻击都会缓慢地移除敌人的击退免疫
极大增强弩车和爆炸陷阱的能力
拥有贪婪戒指,天界贝壳和闪耀石效果
召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "意志之力");
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
            //super bleed on all
            modPlayer.WillForce = true;
            //midas, greedy ring, pet, zhonyas
            modPlayer.GoldEffect(hideVisual);
            //loot multiply
            modPlayer.PlatinumEnchant = true;
            //javelins and pets
            modPlayer.GladiatorEffect(hideVisual);
            //wizard bonuses if somehow wearing only other enchants and not forces
            modPlayer.WizardEnchant = true;
            //arrow rain, celestial shell, pet
            modPlayer.RedRidingEffect(hideVisual);
            modPlayer.HuntressEffect();
            //immune frame kill, pet
            modPlayer.ValhallaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldEnchant");
            recipe.AddIngredient(null, "PlatinumEnchant");
            recipe.AddIngredient(null, "GladiatorEnchant");
            recipe.AddIngredient(ModContent.ItemType<WizardEnchant>());
            recipe.AddIngredient(null, "RedRidingEnchant");
            recipe.AddIngredient(null, "ValhallaKnightEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}