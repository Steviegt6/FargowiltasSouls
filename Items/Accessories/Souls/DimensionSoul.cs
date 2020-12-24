using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class DimensionSoul : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Dimensions");
            DisplayName.AddTranslation(GameCulture.Chinese, "维度之魂");
            string tooltip =
@"Increases HP by 300
20% damage reduction
Increases life regeneration by 8
Grants immunity to knockback and several debuffs
Enemies are more likely to target you
Allows Supersonic running and infinite flight
Increases fishing skill substantially, All fishing rods will have 10 extra lures
Increased block and wall placement speed by 50%
Near infinite block placement and mining reach, Mining speed tripled
Shine, Spelunker, Hunter, and Dangersense effects
Auto paint and actuator effect
Grants the ability to enable Builder Mode
Effects of the Brain of Confusion, Star Veil, Sweetheart Necklace, Bee Cloak, and Spore Sac
Effects of Paladin's Shield, Frozen Turtle Shell, Arctic Diving Gear, Frog Legs, and Flying Carpet
Effects of Lava Waders, Angler Tackle Bag, Paint Sprayer, Presserator, Cell Phone, and Gravity Globe
'The dimensions of Terraria at your fingertips'";

            string tooltip_ch =
@"'泰拉瑞亚维度触手可及'
增加300最大生命值
增加20%伤害减免
+8生命回复
免疫击退和诸多Debuff
敌人更有可能以你为目标
允许超音速奔跑和无限飞行
大幅提升钓鱼技能,所有鱼竿额外增加10个鱼饵
增加50%放置物块及墙壁的速度
近乎无限的放置和采掘距离, 四倍采掘速度
获得发光, 探索者, 猎人和危险感知效果
获得开启建造模式的能力
拥有混乱之脑, 星辰项链, 甜心项链, 蜜蜂斗篷和孢子囊的效果
拥有圣骑士护盾, 冰霜龟壳, 北极潜水装备, 蛙腿和飞毯的效果
拥有熔岩行走靴, 渔具包, 油漆喷雾器, 促动安装器, 手机和重力球的效果";

            Tooltip.SetDefault(tooltip);
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 18));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.defense = 15;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 5000000;
            item.rare = -12;
            item.expert = true;

            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 1;
            item.UseSound = SoundID.Item6;
            item.useAnimation = 1;
            NumFrames = 18;
        }

        public override bool UseItem(Player player)
        {
            player.Spawn();

            for (int num348 = 0; num348 < 70; num348++)
            {
                Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, default(Color), 1.5f);
            }

            return base.UseItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            //cell phone
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accDreamCatcher = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCritterGuide = true;
            player.accJarOfSouls = true;
            player.accThirdEye = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.ColossusSoul(300, 0.2f, 8, hideVisual);
            modPlayer.SupersonicSoul(hideVisual);
            modPlayer.FlightMasterySoul();
            modPlayer.TrawlerSoul(hideVisual);
            modPlayer.WorldShaperSoul(hideVisual);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.3f;
            maxCanAscendMultiplier = 1.5f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.15f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = SoulConfig.Instance.GetValue(SoulConfig.Instance.SupersonicSpeed) ? 25f : 18f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ColossusSoul");
            recipe.AddIngredient(null, "SupersonicSoul");
            recipe.AddIngredient(null, "FlightMasterySoul");
            recipe.AddIngredient(null, "TrawlerSoul");
            recipe.AddIngredient(null, "WorldShaperSoul");
            recipe.AddIngredient(null, "MutantScale", 10);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}