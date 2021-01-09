using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");

            string tooltip =
@"You will grow pumpkins while walking on the ground
100% of contact damage is reflected
Enemies may explode into needles on death
Any friendly bee or wasp will become a Mega Bee
30% chance for minions to crit
When standing still and not attacking, you will enter your shell
Beetles protect you from damage
Increases flight time by 50%
Summons several pets
'Rare is a living thing that dare disobey your will'";
            string tooltip_ch =
@"'罕有活物敢违背你的意愿'
走路时会留下一道火焰路径
南瓜派会使你回满血
反弹100%接触伤害
敌人死亡时有概率爆炸成针
50%概率使友善的蜜蜂成为巨型蜜蜂
巨型蜜蜂无视大部分敌人防御,无敌帧,并且持续时间翻倍
召唤物拥有15%的暴击率
当站立不动且不攻击时,获得缩壳Buff
缩壳时免疫抛射物,但收到更多接触伤害
甲虫保护你免受伤害
增加50%飞行时间";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "生命之力");
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
            //tide hunter, yew wood, iridescent effects
            modPlayer.LifeForce = true;
            //bees ignore defense, super bees, pet
            modPlayer.BeeEffect(hideVisual);
            //minion crits and pet
            modPlayer.SpiderEffect(hideVisual);
            //defense beetle bois
            modPlayer.BeetleEffect();
            if (!modPlayer.TerrariaSoul)
                modPlayer.wingTimeModifier += .5f;
            //flame trail, pie heal, pet
            modPlayer.PumpkinEffect(hideVisual);
            //shell hide, pets
            modPlayer.TurtleEffect(hideVisual);
            player.thorns = 1f;
            player.turtleThorns = true;
            //needle spray
            modPlayer.CactusEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "PumpkinEnchant");
            recipe.AddIngredient(null, "BeeEnchant");
            recipe.AddIngredient(null, "SpiderEnchant");
            recipe.AddIngredient(null, "TurtleEnchant");
            recipe.AddIngredient(null, "BeetleEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}