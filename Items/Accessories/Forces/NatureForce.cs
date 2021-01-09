using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");

            string tooltip =
@"After taking a hit, regen is greatly increased until the hit is healed off
If you take another hit before it's healed, you lose the heal in addition to normal damage
Nearby enemies are ignited
The closer they are to you the more damage they take
When you are hurt, you violently explode to damage nearby enemies
Grants immunity to Wet
A miniature storm will appear after damaging enemies
Icicles will start to appear around you
You have a small area around you that will slow projectiles to 1/2 speed
Summons a ring of leaf crystals to shoot at nearby enemies
Jumping will release a lingering spore explosion
All herb collection is doubled
Not moving puts you in stealth
While in stealth, all attacks gain trails of mushrooms
Effects of Flower Boots
Summons several pets
'Tapped into every secret of the wilds'";

            string tooltip_ch =
@"'挖掘了荒野的每一个秘密'
极大增加生命恢复速度
点燃附近敌人
敌人距离越近, 收到的伤害越多
死亡时剧烈爆炸, 造成大量伤害
你的周围将出现冰柱
当存在3枚时, 攻击会将它们向光标位置发射
攻击造成霜火效果
受到伤害会释放出挥之不去的孢子爆炸
召唤一圈叶绿水晶射击附近的敌人
所有药草收获翻倍
站立不动时潜行
潜行时, 暴击造成3倍伤害
拥有花之靴的效果
召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "自然之力");
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
            //
            modPlayer.NatureForce = true;
            //regen, pets
            modPlayer.CrimsonEffect(hideVisual);
            //inferno and explode
            modPlayer.MoltenEffect();
            //rain
            modPlayer.RainEnchant = true;
            player.buffImmune[BuffID.Wet] = true;
            //icicles, pets
            modPlayer.FrostEffect(hideVisual);
            modPlayer.SnowEffect(hideVisual);
            //spores
            modPlayer.JungleEffect();
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual);
            modPlayer.FlowerBoots();
            //stealth, shrooms, pet
            modPlayer.ShroomiteEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CrimsonEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");
            recipe.AddIngredient(null, "RainEnchant");
            recipe.AddIngredient(null, "FrostEnchant");
            recipe.AddIngredient(null, "ChlorophyteEnchant");
            recipe.AddIngredient(null, "ShroomiteEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}