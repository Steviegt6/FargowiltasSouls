using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");

            string tooltip =
@"If you reach zero HP you will revive with 50 HP and spawn several bones
Collect the bones once they stop moving to heal for 15 HP each
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain 60% damage
Summons an Enchanted Sword familiar
You gain a shield that can reflect projectiles
You may continue to summon temporary minions after maxing out on your slots
Damage has a chance to spawn damaging orbs
If you crit, you might also get a healing orb
Summons several pets
'Ascend from this mortal realm'";

            string tooltip_ch =
@"'从尘世飞升'
血量为0时避免死亡, 回复100生命
重生后几秒内, 免疫所有伤害并生成骨头
双击'下'键可召唤远古风暴到光标位置
任何穿过风暴的抛射物获得额外50%伤害
召唤一柄附魔剑
获得一个可以反射抛射物的护盾
召唤数量达到上限后, 仍然可以召唤临时召唤物
随着时间的推移, 感染造成越来越多的伤害
攻击有概率生成伤害球
暴击有概率生成治疗球
召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "心灵之力");
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
            //spectre works for all, spirit trapper works for all
            modPlayer.SpiritForce = true;
            //revive, bone zone, pet
            modPlayer.FossilEffect(hideVisual);
            //storm
            modPlayer.ForbiddenEffect();
            //sword, shield, pet
            modPlayer.HallowEffect(hideVisual);
            //infested debuff, pet
            modPlayer.TikiEffect(hideVisual);
            //pet
            modPlayer.SpectreEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "FossilEnchant");
            recipe.AddIngredient(null, "ForbiddenEnchant");
            recipe.AddIngredient(null, "HallowEnchant");
            recipe.AddIngredient(null, "TikiEnchant");
            recipe.AddIngredient(null, "SpectreEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}