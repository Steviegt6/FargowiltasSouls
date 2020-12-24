using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Shield)]
    public class TerrariaSoul : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");

            string tooltip =
@"Summons fireballs, shadow orbs, icicles, leaf crystals, flameburst minion, hallowed sword and shield, beetles, and several pets
Toggle vanity to remove all Pets, Right Click to Guard
Double tap down to spawn a sentry and portal, call a storm and arrow rain, toggle stealth, and direct your empowered guardian
Gold Key encases you in gold, Freeze Key freezes time for 5 seconds, minions spew scythes
Solar shield allows you to dash, Dash into any walls, to teleport through them
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Attacks may spawn lightning, a storm cloud, flower petals, spectre orbs, a Dungeon Guardian, snowballs, spears, or buff boosters
Attacks cause increased life regen, shadow dodge, Flameburst shots, meteor showers, and reduced enemy immune frames
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, trigger a blood geyser, and reflects damage
Projectiles may split or shatter and spawn stars, item and projectile size increased, attract items from further away
Nearby enemies are ignited, You leave behind a trail of fire, jump to create a spore explosion
Grants Crimson regen, immunity to fire, fall damage, and lava, and doubled herb collection
Grants 50% chance for Mega Bees, 15% chance for minion crits, 20% chance for bonus loot
Critters have increased defense and their souls will aid you, You may summon temporary minions
All grappling hooks are more effective and fire homing shots, Greatly enhances all DD2 sentries
Your attacks inflict Midas, Enemies explode into needles
You violently explode to damage nearby enemies when hurt and revive with 200 HP when killed
Effects of Flower Boots, Master Ninja Gear, Greedy Ring, Celestial Shell, and Shiny Stone
'A true master of Terraria'";

            string tooltip_ch =
@"'真·泰拉之主'
召唤火球, 冰柱, 叶绿水晶, 神圣剑盾, 甲虫和许多宠物
切换可见度以移除所有宠物, 右键防御
双击'下'键生成一个哨兵, 召唤远古风暴, 切换潜行, 生成一个传送门, 指挥你的强化替身
按下金身热键, 使自己被包裹在一个黄金壳中, 按下时间冻结热键时停5秒, 召唤物发出镰刀
日耀护盾允许你双击冲刺, 遇到墙壁自动穿透
扔烟雾弹进行传送, 获得先发制人Buff
攻击可以产生闪电, 花瓣, 幽灵球, 地牢守卫者, 雪球, 长矛或者增益
攻击造成生命回复增加, 暗影闪避, 焰爆射击, 流星雨, 降低敌人的击退免疫
暴击率设为25%, 每次暴击增加5%, 达到100%时, 每10次攻击附带4%生命偷取
被击中会降低暴击率, 使敌人大出血, 释放出孢子爆炸, 并反弹伤害
抛射物可能会分裂或散开, 物品和抛射物尺寸增加, 增加物品拾取范围
点燃附近敌人, 在身后留下火焰路径
获得血腥套的生命回复效果, 免疫火焰, 坠落伤害和岩浆, 药草收获翻倍
蜜蜂有50%概率变为巨型蜜蜂, 召唤物获得15%暴击率, 20%获得额外掉落
大幅增加动物防御力, 它们的灵魂会在死后帮助你, 你有可能召唤临时召唤物
增强所有抓钩, 抓钩会发射追踪射击, 极大增强所有地牢守卫者2(联动的塔防内容)的哨兵
攻击造成点金术, 敌人会爆炸成刺
死亡时爆炸并以200生命值重生
拥有花之靴, 忍者极意, 贪婪戒指, 天界贝壳和闪耀石的效果";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 5000000;

            item.rare = -12;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //includes revive, both spectres, adamantite, and star heal
            modPlayer.TerrariaSoul = true;

            //WOOD
            mod.GetItem("TimberForce").UpdateAccessory(player, hideVisual);
            //TERRA
            mod.GetItem("TerraForce").UpdateAccessory(player, hideVisual);
            //EARTH
            mod.GetItem("EarthForce").UpdateAccessory(player, hideVisual);
            //NATURE
            mod.GetItem("NatureForce").UpdateAccessory(player, hideVisual);
            //LIFE
            mod.GetItem("LifeForce").UpdateAccessory(player, hideVisual);
            //SPIRIT
            mod.GetItem("SpiritForce").UpdateAccessory(player, hideVisual);
            //SHADOW
            mod.GetItem("ShadowForce").UpdateAccessory(player, hideVisual);
            //WILL
            mod.GetItem("WillForce").UpdateAccessory(player, hideVisual);
            //COSMOS
            mod.GetItem("CosmoForce").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TimberForce");
            recipe.AddIngredient(null, "TerraForce");
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "ShadowForce");
            recipe.AddIngredient(null, "WillForce");
            recipe.AddIngredient(null, "CosmoForce");
            recipe.AddIngredient(null, "MutantScale", 10);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}