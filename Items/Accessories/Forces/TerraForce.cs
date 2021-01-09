using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    [AutoloadEquip(EquipType.Shield)]
    public class TerraForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");

            string tooltip =
@"Attacks have a chance to shock enemies with lightning
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
150% increased sword size
Every quarter second a projectile will be doubled in size
Attacks may inflict enemies with Lead Poisoning
Grants lava mobility and immunity to fire and lava
Your attacks spawn explosions
'The land lends its strength'";

            string tooltip_ch =
@"'大地赐予它力量'
攻击有概率用闪电打击敌人
如果敌人处于潮湿状态,增加概率和伤害
暴击率设为10%
每次暴击增加5%
被击中降低暴击率
允许玩家向敌人冲刺
右键用盾牌防御
拾取物品半径增大
增加150%武器尺寸
增加100%抛射物尺寸
抛射物仍然具有同样的砖块碰撞箱
攻击概率使敌人铅中毒
铅中毒随时间造成伤害,并传播给附近敌人
免疫火焰,坠落伤害和岩浆
在岩浆中时,获得20点护甲穿透,15%攻击速度,攻击会点燃敌人";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉之力");
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
            //item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //lightning
            modPlayer.CopperEnchant = true;
            //crit effect improved
            modPlayer.TerraForce = true;
            //crits
            modPlayer.TinEffect();
            //lead poison
            modPlayer.LeadEnchant = true;
            //tungsten
            modPlayer.TungstenEnchant = true;
            //lava immune (obsidian)
            modPlayer.ObsidianEffect();
            //EoC Shield
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.CthulhuShield))
            {
                player.dash = 2;
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.IronShield))
            {
                //shield
                modPlayer.IronEffect();
            }
            //magnet
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.IronMagnet, false))
            {
                modPlayer.IronEnchant = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CopperEnchant");
            recipe.AddIngredient(null, "TinEnchant");
            recipe.AddIngredient(null, "IronEnchant");
            recipe.AddIngredient(null, "LeadEnchant");
            recipe.AddIngredient(null, "TungstenEnchant");
            recipe.AddIngredient(null, "ObsidianEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}