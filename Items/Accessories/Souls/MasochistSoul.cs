using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Buffs.Masomode;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(/*EquipType.Head, */EquipType.Front, EquipType.Back, EquipType.Shield)]
    public class MasochistSoul : SoulsItem
    {
        public override bool Eternity => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Siblings");
            Tooltip.SetDefault(
@"Increases wing time by 200%, armor penetration by 50, and movement speed by 20%
Increases max life by 100%, damage by 50%, and damage reduction by 10%
Increases life regen drastically, increases max number of minions and sentries by 10
Grants gravity control, fastfall, and immunity to knockback, almost all Eternity Mode debuffs, and more
Grants autofire to all weapons and you automatically use mana potions when needed
Makes armed and magic skeletons less hostile outside the Dungeon, zoom with right click
Your attacks create additional attacks, hearts, and inflict a cocktail of Eternity Mode debuffs
Press the Fireball Dash key to perform a short invincible dash
Certain enemies will drop potions when defeated, 50% discount on reforges, you respawn with more life
You respawn twice as fast, attacks spawn honey, have improved night vision, and erupt into various attacks when injured
Prevents boss spawns, increases spawn rate, increases loot, and attacks may squeak and deal 1 damage to you
Summons the aid of all Eternity Mode bosses to your side
'Embrace eternity'");

            DisplayName.AddTranslation(GameCulture.Chinese, "受虐之魂");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'要制造痛苦,首先必须接受它'
增加200%飞行时间, 50点护甲穿透, 20%移动速度
增加100%最大生命值, 50%伤害, 10%伤害减免
极大增加生命恢复速率, +10最大召唤栏和哨兵栏
重力控制, 快速下落, 免疫击退, 免疫几乎所有受虐模式的Debuff, 以及更多其他效果
所有武器自动连发, 词缀保护, 需要时自动使用魔力药水
增强超可爱猪鲨, 地牢外的装甲和魔法骷髅敌意减小
攻击造成额外攻击, 生成心, 并造成混合的受虐模式Debuff
重生速度加倍, 提高夜视能力, 受伤时爆发各种攻击
阻止Boss自然生成, 增加刷怪速率, 敌人攻击概率发出吱吱声, 并只造成1点伤害
召唤所有受虐模式Boss的援助到你身边");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = ItemRarityID.Purple;
            item.value = 5000000;
            item.defense = 30;
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
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
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.MasochistSoul = true;

            player.AddBuff(mod.BuffType("SouloftheMasochist"), 2);

            //stat modifiers
            fargoPlayer.AllDamageUp(.5f);
            player.endurance += 0.1f;
            player.maxMinions += 10;
            player.maxTurrets += 10;
            player.armorPenetration += 50;
            player.statLifeMax2 += player.statLifeMax;
            player.lifeRegen += 7;
            player.lifeRegenTime += 7;
            player.lifeRegenCount += 7;
            fargoPlayer.wingTimeModifier += 2f;
            player.moveSpeed += 0.2f;

            //slimy shield
            player.buffImmune[BuffID.Slimed] = true;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SlimyFalling))
            {
                player.maxFallSpeed *= 1.5f;
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SlimyShield))
            {
                fargoPlayer.SlimyShield = true;
            }

            //agitating lens
            fargoPlayer.AgitatingLens = true;

            //queen stinger
            //player.honey = true;
            player.npcTypeNoAggro[210] = true;
            player.npcTypeNoAggro[211] = true;
            player.npcTypeNoAggro[42] = true;
            player.npcTypeNoAggro[176] = true;
            player.npcTypeNoAggro[231] = true;
            player.npcTypeNoAggro[232] = true;
            player.npcTypeNoAggro[233] = true;
            player.npcTypeNoAggro[234] = true;
            player.npcTypeNoAggro[235] = true;
            fargoPlayer.QueenStinger = true;

            //necromantic brew
            fargoPlayer.NecromanticBrew = true;

            //supreme deathbringer fairy
            fargoPlayer.SupremeDeathbringerFairy = true;

            //pure heart
            fargoPlayer.PureHeart = true;

            //corrupt heart
            fargoPlayer.CorruptHeart = true;
            if (fargoPlayer.CorruptHeartCD > 0)
                fargoPlayer.CorruptHeartCD -= 2;

            //gutted heart
            fargoPlayer.GuttedHeart = true;
            fargoPlayer.GuttedHeartCD -= 2; //faster spawns

            //mutant antibodies
            player.buffImmune[BuffID.Wet] = true;
            player.buffImmune[BuffID.Rabies] = true;
            fargoPlayer.MutantAntibodies = true;
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
                player.dripping = true;

            //lump of flesh
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            fargoPlayer.SkullCharm = true;
            if (!player.ZoneDungeon)
            {
                player.npcTypeNoAggro[NPCID.SkeletonSniper] = true;
                player.npcTypeNoAggro[NPCID.SkeletonCommando] = true;
                player.npcTypeNoAggro[NPCID.TacticalSkeleton] = true;
                player.npcTypeNoAggro[NPCID.DiabolistRed] = true;
                player.npcTypeNoAggro[NPCID.DiabolistWhite] = true;
                player.npcTypeNoAggro[NPCID.Necromancer] = true;
                player.npcTypeNoAggro[NPCID.NecromancerArmored] = true;
                player.npcTypeNoAggro[NPCID.RaggedCaster] = true;
                player.npcTypeNoAggro[NPCID.RaggedCasterOpenCoat] = true;
            }

            //sinister icon
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SinisterIcon))
                fargoPlayer.SinisterIcon = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SinisterIconDrops))
                fargoPlayer.SinisterIconDrops = true;

            //sparkling adoration
            /*if (SoulConfig.Instance.GetValue(SoulConfig.Instance.Graze, false))
                player.GetModPlayer<FargoPlayer>().Graze = true;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.DevianttHearts))
                player.GetModPlayer<FargoPlayer>().DevianttHearts = true;*/

            //dragon fang
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.DragonFang))
                fargoPlayer.DragonFang = true;

            //frigid gemstone
            player.buffImmune[BuffID.Frostburn] = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.FrigidGemstone))
            {
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD -= 5;
            }

            //wretched pouch
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[mod.BuffType("Shadowflame")] = true;
            player.GetModPlayer<FargoPlayer>().WretchedPouch = true;

            //sands of time
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;

            //mystic skull
            player.buffImmune[BuffID.Suffocation] = true;
            player.manaFlower = true;

            //security wallet
            fargoPlayer.SecurityWallet = true;

            //carrot
            player.nightVision = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.Carrot, false))
                player.scope = true;

            //squeaky toy
            fargoPlayer.SqueakyAcc = true;

            //tribal charm
            player.buffImmune[BuffID.Webbed] = true;
            fargoPlayer.TribalCharm = true;

            //nymph's perfume
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[BuffID.Stinky] = true;
            fargoPlayer.NymphsPerfumeRespawn = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.NymphPerfume))
            {
                fargoPlayer.NymphsPerfume = true;
                if (fargoPlayer.NymphsPerfumeCD > 0)
                    fargoPlayer.NymphsPerfumeCD -= 10;
            }

            //tim's concoction
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.TimsConcoction))
                player.GetModPlayer<FargoPlayer>().TimsConcoction = true;

            //dubious circuitry
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            fargoPlayer.FusedLens = true;
            fargoPlayer.GroundStick = true;
            player.noKnockback = true;

            //magical bulb
            player.buffImmune[BuffID.Venom] = true;

            //ice queen's crown
            player.buffImmune[BuffID.Frozen] = true;

            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            fargoPlayer.LihzahrdTreasureBox = true;

            //saucer control console
            player.buffImmune[BuffID.Electrified] = true;

            //betsy's heart
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            fargoPlayer.BetsysHeart = true;

            //celestial rune/pumpking's cape
            fargoPlayer.CelestialRune = true;
            fargoPlayer.PumpkingsCape = true;
            fargoPlayer.AdditionalAttacks = true;
            if (fargoPlayer.AdditionalAttacksTimer > 0)
                fargoPlayer.AdditionalAttacksTimer -= 2;

            //chalice
            fargoPlayer.MoonChalice = true;

            //galactic globe
            player.buffImmune[BuffID.VortexDebuff] = true;
            //player.buffImmune[BuffID.ChaosState] = true;
            fargoPlayer.GravityGlobeEX = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.GravityControl))
                player.gravControl = true;

            //heart of maso
            fargoPlayer.MasochistHeart = true;
            player.buffImmune[BuffID.MoonLeech] = true;

            //cyclonic fin
            /*fargoPlayer.CyclonicFin = true;
            if (fargoPlayer.CyclonicFinCD > 0)
                fargoPlayer.CyclonicFinCD -= 2;*/
            /*if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("CuteFishronRitual")] < 1 && player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(player.MountedCenter, Vector2.Zero, mod.ProjectileType("CuteFishronRitual"), 0, 0f, Main.myPlayer);
                player.MountFishronSpecialCounter = 300;
                player.meleeDamage += 0.15f;
                player.rangedDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.minionDamage += 0.15f;
                player.meleeCrit += 30;
                player.rangedCrit += 30;
                player.magicCrit += 30;
                player.statDefense += 30;
                player.lifeRegen += 3;
                player.lifeRegenCount += 3;
                player.lifeRegenTime += 3;
                if (player.controlLeft == player.controlRight)
                {
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                }
                else if (player.controlLeft)
                {
                    player.velocity.X -= player.mount.Acceleration * 4f;
                    if (player.velocity.X < -16f)
                        player.velocity.X = -16f;
                    if (!player.controlUseItem)
                        player.direction = -1;
                }
                else if (player.controlRight)
                {
                    player.velocity.X += player.mount.Acceleration * 4f;
                    if (player.velocity.X > 16f)
                        player.velocity.X = 16f;
                    if (!player.controlUseItem)
                        player.direction = 1;
                }
                if (player.controlUp == player.controlDown)
                {
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                }
                else if (player.controlUp)
                {
                    player.velocity.Y -= player.mount.Acceleration * 4f;
                    if (player.velocity.Y < -16f)
                        player.velocity.Y = -16f;
                }
                else if (player.controlDown)
                {
                    player.velocity.Y += player.mount.Acceleration * 4f;
                    if (player.velocity.Y > 16f)
                        player.velocity.Y = 16f;
                }
            }*/

            //sadism
            player.buffImmune[ModContent.BuffType<Antisocial>()] = true;
            player.buffImmune[ModContent.BuffType<Atrophied>()] = true;
            player.buffImmune[ModContent.BuffType<Berserked>()] = true;
            player.buffImmune[ModContent.BuffType<Bloodthirsty>()] = true;
            player.buffImmune[ModContent.BuffType<ClippedWings>()] = true;
            player.buffImmune[ModContent.BuffType<Crippled>()] = true;
            player.buffImmune[ModContent.BuffType<CurseoftheMoon>()] = true;
            player.buffImmune[ModContent.BuffType<Defenseless>()] = true;
            player.buffImmune[ModContent.BuffType<FlamesoftheUniverse>()] = true;
            player.buffImmune[ModContent.BuffType<Flipped>()] = true;
            player.buffImmune[ModContent.BuffType<FlippedHallow>()] = true;
            player.buffImmune[ModContent.BuffType<Fused>()] = true;
            player.buffImmune[ModContent.BuffType<GodEater>()] = true;
            player.buffImmune[ModContent.BuffType<Guilty>()] = true;
            player.buffImmune[ModContent.BuffType<Hexed>()] = true;
            player.buffImmune[ModContent.BuffType<Hypothermia>()] = true;
            player.buffImmune[ModContent.BuffType<Infested>()] = true;
            player.buffImmune[ModContent.BuffType<IvyVenom>()] = true;
            player.buffImmune[ModContent.BuffType<Jammed>()] = true;
            player.buffImmune[ModContent.BuffType<Lethargic>()] = true;
            player.buffImmune[ModContent.BuffType<LihzahrdCurse>()] = true;
            player.buffImmune[ModContent.BuffType<LightningRod>()] = true;
            player.buffImmune[ModContent.BuffType<LivingWasteland>()] = true;
            player.buffImmune[ModContent.BuffType<Lovestruck>()] = true;
            player.buffImmune[ModContent.BuffType<LowGround>()] = true;
            player.buffImmune[ModContent.BuffType<MarkedforDeath>()] = true;
            player.buffImmune[ModContent.BuffType<Midas>()] = true;
            player.buffImmune[ModContent.BuffType<MutantNibble>()] = true;
            player.buffImmune[ModContent.BuffType<NanoInjection>()] = true;
            player.buffImmune[ModContent.BuffType<NullificationCurse>()] = true;
            player.buffImmune[ModContent.BuffType<Oiled>()] = true;
            player.buffImmune[ModContent.BuffType<OceanicMaul>()] = true;
            player.buffImmune[ModContent.BuffType<Purified>()] = true;
            player.buffImmune[ModContent.BuffType<ReverseManaFlow>()] = true;
            player.buffImmune[ModContent.BuffType<Rotting>()] = true;
            player.buffImmune[ModContent.BuffType<Shadowflame>()] = true;
            player.buffImmune[ModContent.BuffType<SqueakyToy>()] = true;
            player.buffImmune[ModContent.BuffType<Swarming>()] = true;
            player.buffImmune[ModContent.BuffType<Stunned>()] = true;
            player.buffImmune[ModContent.BuffType<Unstable>()] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("SinisterIcon"));
            //recipe.AddIngredient(mod.ItemType("SparklingAdoration"));
            recipe.AddIngredient(mod.ItemType("SupremeDeathbringerFairy"));
            recipe.AddIngredient(mod.ItemType("BionomicCluster"));
            recipe.AddIngredient(mod.ItemType("DubiousCircuitry"));
            recipe.AddIngredient(mod.ItemType("PureHeart"));
            recipe.AddIngredient(mod.ItemType("LumpOfFlesh"));
            recipe.AddIngredient(mod.ItemType("ChaliceoftheMoon"));
            recipe.AddIngredient(mod.ItemType("HeartoftheMasochist"));
            //recipe.AddIngredient(mod.ItemType("CyclonicFin"));
            //recipe.AddIngredient(mod.ItemType("Sadism"), 30);
            recipe.AddIngredient(mod.ItemType("MutantScale"), 15);
            recipe.AddIngredient(mod.ItemType("DeviatingEnergy"), 15);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}