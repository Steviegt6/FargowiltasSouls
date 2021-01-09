using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader.Config;

namespace FargowiltasSouls
{
    public class SoulConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static SoulConfig Instance;

        // We have to manually add this config for our localization to work properly because tML is absolute utter garbage.
        public override bool Autoload(ref string name) => false;

        private void SetAll(bool val)
        {
            //bool backgroundValue = MutantBackground;
            bool recolorsValue = BossRecolors;

            IEnumerable<FieldInfo> configs = typeof(SoulConfig).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(
                i => i.FieldType == true.GetType() && !i.Name.Contains("Patreon"));
            foreach (FieldInfo config in configs)
            {
                config.SetValue(this, val);
            }

            //MutantBackground = backgroundValue;
            BossRecolors = recolorsValue;

            /*IEnumerable<FieldInfo> walletConfigs = typeof(WalletToggles).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(i => i.FieldType == true.GetType());
            foreach (FieldInfo walletConfig in walletConfigs)
            {
                walletConfig.SetValue(walletToggles, val);
            }*/

            /*IEnumerable<FieldInfo> thoriumConfigs = typeof(ThoriumToggles).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(i => i.FieldType == true.GetType());
            foreach (FieldInfo thoriumConfig in thoriumConfigs)
            {
                thoriumConfig.SetValue(thoriumToggles, val);
            }

            IEnumerable<FieldInfo> calamityConfigs = typeof(CalamityToggles).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(i => i.FieldType == true.GetType());
            foreach (FieldInfo calamityConfig in calamityConfigs)
            {
                calamityConfig.SetValue(calamityToggles, val);
            }*/
        }

        [Header("$Mods.FargowiltasSouls.Toggles.PresetHeaderToggle")]
        [Label("All Toggles On")]
        public bool PresetA
        {
            get => false;
            set
            {
                if (value)
                {
                    SetAll(true);
                }
            }
        }

        [Label("All Toggles Off")]
        public bool PresetB
        {
            get => false;
            set
            {
                if (value)
                {
                    SetAll(false);
                }
            }
        }

        [Label("Minimal Effects Only")]
        public bool PresetC
        {
            get => false;
            set
            {
                if (value)
                {
                    SetAll(false);

                    MythrilSpeed = true;
                    PalladiumHeal = true;
                    IronMagnet = true;
                    CthulhuShield = true;
                    TinCrit = true;
                    BeetleEffect = true;
                    SpiderCrits = true;
                    ShinobiTabi = true;
                    NebulaBoost = true;
                    SolarShield = true;
                    Graze = true;
                    SinisterIconDrops = true;
                    NymphPerfume = true;
                    TribalCharm = true;
                    StabilizedGravity = true;
                    BerserkerAttackSpeed = true;
                    UniverseAttackSpeed = true;
                    MinerDanger = true;
                    MinerHunter = true;
                    MinerShine = true;
                    MinerSpelunker = true;
                    EternityStacking = true;
                }
            }
        }

        [Header("$Mods.FargowiltasSouls.Toggles.WoodHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.BorealConfigToggle")]
        [DefaultValue(true)]
        public bool BorealSnowballs;

        [Label("$Mods.FargowiltasSouls.Toggles.EbonConfigToggle")]
        [DefaultValue(true)]
        public bool EbonwoodAura;

        [Label("$Mods.FargowiltasSouls.Toggles.ShadeConfigToggle")]
        [DefaultValue(true)]
        public bool ShadewoodEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.MahoganyConfigToggle")]
        [DefaultValue(true)]
        public bool MahoganyHook;

        [Label("$Mods.FargowiltasSouls.Toggles.PalmConfigToggle")]
        [DefaultValue(true)]
        public bool PalmwoodSentry;

        [Label("$Mods.FargowiltasSouls.Toggles.PearlConfigToggle")]
        [DefaultValue(true)]
        public bool PearlwoodStars;

        [Header("$Mods.FargowiltasSouls.Toggles.EarthHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.AdamantiteConfigToggle")]
        [DefaultValue(true)]
        public bool AdamantiteSplit;

        [Label("$Mods.FargowiltasSouls.Toggles.CobaltConfigToggle")]
        [DefaultValue(true)]
        public bool CobaltShards;

        [Label("$Mods.FargowiltasSouls.Toggles.AncientCobaltConfigToggle")]
        [DefaultValue(true)]
        public bool CobaltStingers;

        [Label("$Mods.FargowiltasSouls.Toggles.MythrilConfigToggle")]
        [DefaultValue(true)]
        public bool MythrilSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.OrichalcumConfigToggle")]
        [DefaultValue(true)]
        public bool OrichalcumPetals;

        [Label("$Mods.FargowiltasSouls.Toggles.PalladiumConfigToggle")]
        [DefaultValue(true)]
        public bool PalladiumHeal;

        [Label("$Mods.FargowiltasSouls.Toggles.PalladiumOrbConfigToggle")]
        [DefaultValue(true)]
        public bool PalladiumOrb;

        [Label("$Mods.FargowiltasSouls.Toggles.TitaniumConfigToggle")]
        [DefaultValue(true)]
        public bool TitaniumDodge;

        [Header("$Mods.FargowiltasSouls.Toggles.TerraHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.CopperConfigToggle")]
        [DefaultValue(true)]
        public bool CopperLightning;

        [Label("$Mods.FargowiltasSouls.Toggles.IronMConfigToggle")]
        [DefaultValue(true)]
        public bool IronMagnet;

        [Label("$Mods.FargowiltasSouls.Toggles.IronSConfigToggle")]
        [DefaultValue(true)]
        public bool IronShield;

        [Label("$Mods.FargowiltasSouls.Toggles.CthulhuShieldToggle")]
        [DefaultValue(true)]
        public bool CthulhuShield;

        [Label("$Mods.FargowiltasSouls.Toggles.TinConfigToggle")]
        [DefaultValue(true)]
        public bool TinCrit;

        [Label("$Mods.FargowiltasSouls.Toggles.TungstenConfigToggle")]
        [DefaultValue(true)]
        public bool TungstenSize;

        [Label("$Mods.FargowiltasSouls.Toggles.TungstenProjConfigToggle")]
        [DefaultValue(true)]
        public bool TungstenProjSize;

        [Label("$Mods.FargowiltasSouls.Toggles.ObsidianConfigToggle")]
        [DefaultValue(true)]
        public bool ObsidianExplosion;

        [Header("$Mods.FargowiltasSouls.Toggles.WillHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.GladiatorConfigToggle")]
        [DefaultValue(true)]
        public bool GladiatorJavelins;

        [Label("$Mods.FargowiltasSouls.Toggles.GoldConfigToggle")]
        [DefaultValue(true)]
        public bool LuckyCoin;

        [Label("$Mods.FargowiltasSouls.Toggles.HuntressConfigToggle")]
        [DefaultValue(true)]
        public bool HuntressAbility;

        [Label("$Mods.FargowiltasSouls.Toggles.ValhallaConfigToggle")]
        [DefaultValue(true)]
        public bool ValhallaEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.SquirePanicConfigToggle")]
        [DefaultValue(true)]
        public bool SquirePanic;

        [Header("$Mods.FargowiltasSouls.Toggles.LifeHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.BeeConfigToggle")]
        [DefaultValue(true)]
        public bool BeeEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.BeetleConfigToggle")]
        [DefaultValue(true)]
        public bool BeetleEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.CactusConfigToggle")]
        [DefaultValue(true)]
        public bool CactusNeedles;

        [Label("$Mods.FargowiltasSouls.Toggles.PumpkinConfigToggle")]
        [DefaultValue(true)]
        public bool PumpkinFire;

        [Label("$Mods.FargowiltasSouls.Toggles.SpiderConfigToggle")]
        [DefaultValue(true)]
        public bool SpiderCrits;

        [Label("$Mods.FargowiltasSouls.Toggles.TurtleConfigToggle")]
        [DefaultValue(true)]
        public bool TurtleShell;

        [Header("$Mods.FargowiltasSouls.Toggles.NatureHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ChlorophyteConfigToggle")]
        [DefaultValue(true)]
        public bool ChlorophyteCrystals;

        [Label("$Mods.FargowiltasSouls.Toggles.ChlorophyteFlowerConfigToggle")]
        [DefaultValue(true)]
        public bool ChlorophyteFlowerBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.CrimsonConfigToggle")]
        [DefaultValue(true)]
        public bool CrimsonRegen;

        [Label("$Mods.FargowiltasSouls.Toggles.FrostConfigToggle")]
        [DefaultValue(true)]
        public bool FrostIcicles;

        [Label("$Mods.FargowiltasSouls.Toggles.SnowConfigToggle")]
        [DefaultValue(true)]
        public bool SnowStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.JungleConfigToggle")]
        [DefaultValue(true)]
        public bool JungleSpores;

        [Label("$Mods.FargowiltasSouls.Toggles.CordageConfigToggle")]
        [DefaultValue(true)]
        public bool Cordage;

        [Label("$Mods.FargowiltasSouls.Toggles.MoltenConfigToggle")]
        [DefaultValue(true)]
        public bool MoltenInferno;

        [Label("$Mods.FargowiltasSouls.Toggles.MoltenEConfigToggle")]
        [DefaultValue(true)]
        public bool MoltenExplosion;

        [Label("$Mods.FargowiltasSouls.Toggles.RainConfigToggle")]
        [DefaultValue(true)]
        public bool RainCloud;

        [Label("$Mods.FargowiltasSouls.Toggles.ShroomiteConfigToggle")]
        [DefaultValue(true)]
        public bool ShroomiteStealth;

        [Label("$Mods.FargowiltasSouls.Toggles.ShroomiteShroomConfigToggle")]
        [DefaultValue(true)]
        public bool ShroomiteShrooms;

        [Header("$Mods.FargowiltasSouls.Toggles.ShadowHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.DarkArtConfigToggle")]
        [DefaultValue(true)]
        public bool DarkArtistMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.ApprenticeConfigToggle")]
        [DefaultValue(true)]
        public bool ApprenticeEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.NecroConfigToggle")]
        [DefaultValue(true)]
        public bool NecroGuardian;

        [Label("$Mods.FargowiltasSouls.Toggles.AncientShadowConfigToggle")]
        [DefaultValue(true)]
        public bool AncientShadow;

        [Label("$Mods.FargowiltasSouls.Toggles.ShadowConfigToggle")]
        [DefaultValue(true)]
        public bool ShadowDarkness;

        [Label("$Mods.FargowiltasSouls.Toggles.MonkConfigToggle")]
        [DefaultValue(true)]
        public bool MonkDash;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiConfigToggle")]
        [DefaultValue(true)]
        public bool ShinobiWalls;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiTabiConfigToggle")]
        [DefaultValue(true)]
        public bool ShinobiTabi;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiClimbingConfigToggle")]
        [DefaultValue(true)]
        public bool ShinobiClimbing;

        [Label("$Mods.FargowiltasSouls.Toggles.SpookyConfigToggle")]
        [DefaultValue(true)]
        public bool SpookyScythes;

        [Header("$Mods.FargowiltasSouls.Toggles.SpiritHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ForbiddenConfigToggle")]
        [DefaultValue(true)]
        public bool ForbiddenStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.HallowedConfigToggle")]
        [DefaultValue(true)]
        public bool HallowSword;

        [Label("$Mods.FargowiltasSouls.Toggles.HallowSConfigToggle")]
        [DefaultValue(true)]
        public bool HallowShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SilverConfigToggle")]
        [DefaultValue(true)]
        public bool SilverSword;

        [Label("$Mods.FargowiltasSouls.Toggles.SpectreConfigToggle")]
        [DefaultValue(true)]
        public bool SpectreOrbs;

        [Label("$Mods.FargowiltasSouls.Toggles.TikiConfigToggle")]
        [DefaultValue(true)]
        public bool TikiMinions;

        [Header("$Mods.FargowiltasSouls.Toggles.CosmoHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MeteorConfigToggle")]
        [DefaultValue(true)]
        public bool MeteorShower;

        [Label("$Mods.FargowiltasSouls.Toggles.NebulaConfigToggle")]
        [DefaultValue(true)]
        public bool NebulaBoost;

        [Label("$Mods.FargowiltasSouls.Toggles.SolarConfigToggle")]
        [DefaultValue(true)]
        public bool SolarShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SolarFlareConfigToggle")]
        [DefaultValue(true)]
        public bool SolarFlare;

        [Label("$Mods.FargowiltasSouls.Toggles.StardustConfigToggle")]
        [DefaultValue(true)]
        public bool StardustGuardian;

        [Label("$Mods.FargowiltasSouls.Toggles.VortexSConfigToggle")]
        [DefaultValue(true)]
        public bool VortexStealth;

        [Label("$Mods.FargowiltasSouls.Toggles.VortexVConfigToggle")]
        [DefaultValue(true)]
        public bool VortexVoid;

        #region maso accessories

        [Header("$Mods.FargowiltasSouls.Toggles.MasoHeaderToggle")]
        /*[Label("$Mods.FargowiltasSouls.Toggles.MasoBossBGToggle")]
        [DefaultValue(true)]
        public bool MutantBackground;*/

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBossRecolorsToggle")]
        [DefaultValue(true)]
        public bool BossRecolors;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoIconConfigToggle")]
        [DefaultValue(true)]
        public bool SinisterIcon;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoIconDropsConfigToggle")]
        [DefaultValue(true)]
        public bool SinisterIconDrops;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGrazeConfigToggle")]
        [DefaultValue(true)]
        public bool Graze;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoDevianttHeartsConfigToggle")]
        [DefaultValue(true)]
        public bool DevianttHearts;

        [Header("$Mods.FargowiltasSouls.Toggles.SupremeFairyHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoSlimeConfigToggle")]
        [DefaultValue(true)]
        public bool SlimyShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SlimeFallingConfigToggle")]
        [DefaultValue(true)]
        public bool SlimyFalling;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoEyeConfigToggle")]
        [DefaultValue(true)]
        public bool AgitatedLens;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoHoneyConfigToggle")]
        [DefaultValue(true)]
        public bool QueenStingerHoney;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoSkeleConfigToggle")]
        [DefaultValue(true)]
        public bool NecromanticBrew;

        [Header("$Mods.FargowiltasSouls.Toggles.BionomicHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoConcoctionConfigToggle")]
        [DefaultValue(true)]
        public bool TimsConcoction;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoCarrotConfigToggle")]
        [DefaultValue(true)]
        public bool Carrot;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoRainbowConfigToggle")]
        [DefaultValue(true)]
        public bool RainbowSlime;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoFrigidConfigToggle")]
        [DefaultValue(true)]
        public bool FrigidGemstone;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoNymphConfigToggle")]
        [DefaultValue(true)]
        public bool NymphPerfume;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoSqueakConfigToggle")]
        [DefaultValue(true)]
        public bool SqueakyToy;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoPouchConfigToggle")]
        [DefaultValue(true)]
        public bool WretchedPouch;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoClippedConfigToggle")]
        [DefaultValue(true)]
        public bool DragonFang;

        [Label("$Mods.FargowiltasSouls.Toggles.TribalCharmConfigToggle")]
        [DefaultValue(true)]
        public bool TribalCharm;

        /*[Label("$Mods.FargowiltasSouls.Toggles.WalletHeaderToggle")]
        public WalletToggles walletToggles = new WalletToggles();*/

        [Header("$Mods.FargowiltasSouls.Toggles.DubiousHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoLightningConfigToggle")]
        [DefaultValue(true)]
        public bool LightningRod;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoProbeConfigToggle")]
        [DefaultValue(true)]
        public bool ProbeMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.PureHeartHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoEaterConfigToggle")]
        [DefaultValue(true)]
        public bool CorruptHeart;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBrainConfigToggle")]
        [DefaultValue(true)]
        public bool GuttedHeart;

        [Header("$Mods.FargowiltasSouls.Toggles.LumpofFleshHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoPugentConfigToggle")]
        [DefaultValue(true)]
        public bool PungentEye;

        [Header("$Mods.FargowiltasSouls.Toggles.ChaliceHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoCultistConfigToggle")]
        [DefaultValue(true)]
        public bool CultistMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoPlantConfigToggle")]
        [DefaultValue(true)]
        public bool PlanteraMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGolemConfigToggle")]
        [DefaultValue(true)]
        public bool LihzahrdBoxGeysers;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBoulderConfigToggle")]
        [DefaultValue(true)]
        public bool LihzahrdBoxBoulders;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoCelestConfigToggle")]
        [DefaultValue(true)]
        public bool CelestialRune;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoVisionConfigToggle")]
        [DefaultValue(true)]
        public bool AncientVisions;

        [Header("$Mods.FargowiltasSouls.Toggles.HeartHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoPumpToggle")]
        [DefaultValue(true)]
        public bool PumpkingCape;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoFlockoConfigToggle")]
        [DefaultValue(true)]
        public bool FlockoMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoUfoConfigToggle")]
        [DefaultValue(true)]
        public bool UFOMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGravConfigToggle")]
        [DefaultValue(true)]
        public bool GravityControl;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGrav2ConfigToggle")]
        [DefaultValue(true)]
        public bool StabilizedGravity;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoTrueEyeConfigToggle")]
        [DefaultValue(true)]
        public bool TrueEyes;

        [Header("$Mods.FargowiltasSouls.Toggles.CyclonicHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoFishronConfigToggle")]
        [DefaultValue(true)]
        public bool FishronMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.MutantArmorHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoAbomConfigToggle")]
        [DefaultValue(true)]
        public bool AbomMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoRingConfigToggle")]
        [DefaultValue(true)]
        public bool RingMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoReviveDeathrayConfigToggle")]
        [DefaultValue(true)]
        public bool ReviveDeathray;

        #endregion maso accessories

        #region souls

        [Header("$Mods.FargowiltasSouls.Toggles.SoulHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.MeleeConfigToggle")]
        [DefaultValue(true)]
        public bool BerserkerAttackSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MagmaStoneConfigToggle")]
        [DefaultValue(true)]
        public bool MagmaStone;

        [Label("$Mods.FargowiltasSouls.Toggles.YoyoBagConfigToggle")]
        [DefaultValue(true)]
        public bool YoyoBag;

        [Label("$Mods.FargowiltasSouls.Toggles.SniperConfigToggle")]
        [DefaultValue(true)]
        public bool SniperScope;

        [Label("$Mods.FargowiltasSouls.Toggles.UniverseConfigToggle")]
        [DefaultValue(true)]
        public bool UniverseAttackSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningHuntConfigToggle")]
        [DefaultValue(true)]
        public bool MinerHunter;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningDangerConfigToggle")]
        [DefaultValue(true)]
        public bool MinerDanger;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningSpelunkConfigToggle")]
        [DefaultValue(true)]
        public bool MinerSpelunker;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningShineConfigToggle")]
        [DefaultValue(true)]
        public bool MinerShine;

        [Label("$Mods.FargowiltasSouls.Toggles.BuilderConfigToggle")]
        [DefaultValue(false)]
        public bool BuilderMode;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseSporeConfigToggle")]
        [DefaultValue(true)]
        public bool SporeSac;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseStarConfigToggle")]
        [DefaultValue(true)]
        public bool StarCloak;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseBeeConfigToggle")]
        [DefaultValue(true)]
        public bool BeesOnHit;

        [Label("$Mods.FargowiltasSouls.Toggles.DefensePanicConfigToggle")]
        [DefaultValue(true)]
        public bool PanicOnHit;

        [Label("$Mods.FargowiltasSouls.Toggles.RunSpeedConfigToggle")]
        [DefaultValue(true)]
        public bool IncreasedRunSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MomentumConfigToggle")]
        [DefaultValue(true)]
        public bool NoMomentum;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicConfigToggle")]
        [DefaultValue(true)]
        public bool SupersonicSpeed;

        [Label("Supersonic Speed Multiplier")]
        [Increment(1)]
        [Range(1, 10)]
        [DefaultValue(5)]
        [Slider]
        public int SupersonicMultiplier;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicJumpsConfigToggle")]
        [DefaultValue(true)]
        public bool SupersonicJumps;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicRocketBootsConfigToggle")]
        [DefaultValue(true)]
        public bool SupersonicRocketBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicCarpetConfigToggle")]
        [DefaultValue(true)]
        public bool SupersonicCarpet;

        [Label("$Mods.FargowiltasSouls.Toggles.TrawlerConfigToggle")]
        [DefaultValue(true)]
        public bool TrawlerLures;

        [Label("$Mods.FargowiltasSouls.Toggles.EternityConfigToggle")]
        [DefaultValue(true)]
        public bool EternityStacking;

        #endregion souls

        #region pets

        [Header("$Mods.FargowiltasSouls.Toggles.PetHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.PetCatConfigToggle")]
        [DefaultValue(true)]
        public bool BlackCatPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetCubeConfigToggle")]
        [DefaultValue(true)]
        public bool CompanionCubePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetCurseSapConfigToggle")]
        [DefaultValue(true)]
        public bool CursedSaplingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDinoConfigToggle")]
        [DefaultValue(true)]
        public bool DinoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDragonConfigToggle")]
        [DefaultValue(true)]
        public bool DragonPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetEaterConfigToggle")]
        [DefaultValue(true)]
        public bool EaterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetEyeSpringConfigToggle")]
        [DefaultValue(true)]
        public bool EyeSpringPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetFaceMonsterConfigToggle")]
        [DefaultValue(true)]
        public bool FaceMonsterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetGatoConfigToggle")]
        [DefaultValue(true)]
        public bool GatoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetHornetConfigToggle")]
        [DefaultValue(true)]
        public bool HornetPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetLizardConfigToggle")]
        [DefaultValue(true)]
        public bool LizardPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetMinitaurConfigToggle")]
        [DefaultValue(true)]
        public bool MinotaurPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetParrotConfigToggle")]
        [DefaultValue(true)]
        public bool ParrotPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetPenguinConfigToggle")]
        [DefaultValue(true)]
        public bool PenguinPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetPupConfigToggle")]
        [DefaultValue(true)]
        public bool PuppyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSeedConfigToggle")]
        [DefaultValue(true)]
        public bool SeedlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDGConfigToggle")]
        [DefaultValue(true)]
        public bool DGPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSnowmanConfigToggle")]
        [DefaultValue(true)]
        public bool SnowmanPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetGrinchConfigToggle")]
        [DefaultValue(true)]
        public bool GrinchPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSpiderConfigToggle")]
        [DefaultValue(true)]
        public bool SpiderPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSquashConfigToggle")]
        [DefaultValue(true)]
        public bool SquashlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetTikiConfigToggle")]
        [DefaultValue(true)]
        public bool TikiPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetShroomConfigToggle")]
        [DefaultValue(true)]
        public bool TrufflePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetTurtleConfigToggle")]
        [DefaultValue(true)]
        public bool TurtlePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetZephyrConfigToggle")]
        [DefaultValue(true)]
        public bool ZephyrFishPet;

        //LIGHT PETS
        [Label("$Mods.FargowiltasSouls.Toggles.PetHeartConfigToggle")]
        [DefaultValue(true)]
        public bool CrimsonHeartPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetNaviConfigToggle")]
        [DefaultValue(true)]
        public bool FairyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetFlickerConfigToggle")]
        [DefaultValue(true)]
        public bool FlickerwickPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetLanturnConfigToggle")]
        [DefaultValue(true)]
        public bool MagicLanternPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetOrbConfigToggle")]
        [DefaultValue(true)]
        public bool ShadowOrbPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSuspEyeConfigToggle")]
        [DefaultValue(true)]
        public bool SuspiciousEyePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetWispConfigToggle")]
        [DefaultValue(true)]
        public bool WispPet;

        #endregion pets

        [Header("$Mods.FargowiltasSouls.Toggles.PatreonHeaderToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.PatreonRoombaToggle")]
        [DefaultValue(true)]
        public bool PatreonRoomba;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonOrbToggle")]
        [DefaultValue(true)]
        public bool PatreonOrb;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonFishingRodToggle")]
        [DefaultValue(true)]
        public bool PatreonFishingRod;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonDoorToggle")]
        [DefaultValue(true)]
        public bool PatreonDoor;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonWolfToggle")]
        [DefaultValue(true)]
        public bool PatreonWolf;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonDoveToggle")]
        [DefaultValue(true)]
        public bool PatreonDove;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonKingSlimeToggle")]
        [DefaultValue(true)]
        public bool PatreonKingSlime;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonFishronToggle")]
        [DefaultValue(true)]
        public bool PatreonFishron;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonPlantToggle")]
        [DefaultValue(true)]
        public bool PatreonPlant;

        /*[Label("$Mods.FargowiltasSouls.Toggles.ThoriumHeaderToggle")]
        public ThoriumToggles thoriumToggles = new ThoriumToggles();

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHeaderToggle")]
        public CalamityToggles calamityToggles = new CalamityToggles();*/

        //soa soon tm

        public override void OnLoaded()
        {
            Instance = this;
        }

        // Proper cloning of reference types is required because behind the scenes many instances of ModConfig classes co-exist.
        /*public override ModConfig Clone()
        {
            var clone = (SoulConfig)base.Clone();

            clone.walletToggles = walletToggles == null ? null : new WalletToggles();
            clone.thoriumToggles = thoriumToggles == null ? null : new ThoriumToggles();
            clone.calamityToggles = calamityToggles == null ? null : new CalamityToggles();

            return clone;
        }*/

        public bool GetValue(bool toggle, bool checkForMutantPresence = true)
        {
            return checkForMutantPresence && Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().MutantPresence ? false : toggle;
        }
    }

    /*public class WalletToggles
    {
        [Label("Warding")]
        [DefaultValue(true)]
        public bool Warding;

        [Label("Violent")]
        [DefaultValue(true)]
        public bool Violent;

        [Label("Quick")]
        [DefaultValue(true)]
        public bool Quick;

        [Label("Lucky")]
        [DefaultValue(true)]
        public bool Lucky;

        [Label("Menacing")]
        [DefaultValue(true)]
        public bool Menacing;

        [Label("Legendary")]
        [DefaultValue(true)]
        public bool Legendary;

        [Label("Unreal")]
        [DefaultValue(true)]
        public bool Unreal;

        [Label("Mythical")]
        [DefaultValue(true)]
        public bool Mythical;

        [Label("Godly")]
        [DefaultValue(true)]
        public bool Godly;

        [Label("Demonic")]
        [DefaultValue(true)]
        public bool Demonic;

        [Label("Ruthless")]
        [DefaultValue(true)]
        public bool Ruthless;

        [Label("Light")]
        [DefaultValue(true)]
        public bool Light;

        [Label("Deadly")]
        [DefaultValue(true)]
        public bool Deadly;

        [Label("Rapid")]
        [DefaultValue(true)]
        public bool Rapid;
    }*/

    public class ThoriumToggles
    {
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCrystalScorpionConfigToggle")]
        [DefaultValue(true)]
        public bool CrystalScorpion;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumHeadMirrorConfigToggle")]
        [DefaultValue(true)]
        public bool HeadMirror;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAirWalkersConfigToggle")]
        [DefaultValue(true)]
        public bool AirWalkers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumGlitterPetConfigToggle")]
        [DefaultValue(true)]
        public bool GlitterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCoinPetConfigToggle")]
        [DefaultValue(true)]
        public bool CoinPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBoxPetConfigToggle")]
        [DefaultValue(true)]
        public bool BoxPet;

        [Header("$Mods.FargowiltasSouls.Toggles.MuspelheimForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBeeBootiesConfigToggle")]
        [DefaultValue(true)]
        public bool BeeBooties;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSaplingMinionConfigToggle")]
        [DefaultValue(true)]
        public bool SaplingMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.JotunheimForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumJellyfishPetConfigToggle")]
        [DefaultValue(true)]
        public bool JellyfishPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideFoamConfigToggle")]
        [DefaultValue(true)]
        public bool TideFoam;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumYewCritsConfigToggle")]
        [DefaultValue(true)]
        public bool YewCrits;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCryoDamageConfigToggle")]
        [DefaultValue(true)]
        public bool CryoDamage;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumOwlPetConfigToggle")]
        [DefaultValue(true)]
        public bool OwlPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIcyBarrierConfigToggle")]
        [DefaultValue(true)]
        public bool IcyBarrier;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWhisperingTentaclesConfigToggle")]
        [DefaultValue(true)]
        public bool WhisperingTentacles;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpiritPetConfigToggle")]
        [DefaultValue(true)]
        public bool SpiritPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWarlockWispsConfigToggle")]
        [DefaultValue(true)]
        public bool WarlockWisps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBiotechProbeConfigToggle")]
        [DefaultValue(true)]
        public bool BiotechProbe;

        [Header("$Mods.FargowiltasSouls.Toggles.NiflheimForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMixTapeConfigToggle")]
        [DefaultValue(true)]
        public bool MixTape;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCyberStatesConfigToggle")]
        [DefaultValue(true)]
        public bool CyberStates;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMetronomeConfigToggle")]
        [DefaultValue(true)]
        public bool Metronome;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMarchingBandConfigToggle")]
        [DefaultValue(true)]
        public bool MarchingBand;

        [Header("$Mods.FargowiltasSouls.Toggles.SvartalfheimForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumEyeoftheStormConfigToggle")]
        [DefaultValue(true)]
        public bool EyeoftheStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBronzeLightningConfigToggle")]
        [DefaultValue(true)]
        public bool BronzeLightning;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumConduitShieldConfigToggle")]
        [DefaultValue(true)]
        public bool ConduitShield;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumOmegaPetConfigToggle")]
        [DefaultValue(true)]
        public bool OmegaPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIFOPetConfigToggle")]
        [DefaultValue(true)]
        public bool IFOPet;

        [Header("$Mods.FargowiltasSouls.Toggles.MidgardForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumLodestoneConfigToggle")]
        [DefaultValue(true)]
        public bool LodestoneResist;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBeholderEyeConfigToggle")]
        [DefaultValue(true)]
        public bool BeholderEye;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIllumiteMissileConfigToggle")]
        [DefaultValue(true)]
        public bool IllumiteMissile;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTerrariumSpiritsConfigToggle")]
        [DefaultValue(true)]
        public bool TerrariumSpirits;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDiverConfigToggle")]
        [DefaultValue(true)]
        public bool ThoriumDivers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCrietzConfigToggle")]
        [DefaultValue(true)]
        public bool Crietz;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumJesterBellConfigToggle")]
        [DefaultValue(true)]
        public bool JesterBell;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumManaBootsConfigToggle")]
        [DefaultValue(true)]
        public bool ManaBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWhiteDwarfConfigToggle")]
        [DefaultValue(true)]
        public bool WhiteDwarf;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCelestialAuraConfigToggle")]
        [DefaultValue(true)]
        public bool CelestialAura;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAscensionStatueConfigToggle")]
        [DefaultValue(true)]
        public bool AscensionStatue;

        [Header("$Mods.FargowiltasSouls.Toggles.HelheimForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpiritWispsConfigToggle")]
        [DefaultValue(true)]
        public bool SpiritTrapperWisps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDreadConfigToggle")]
        [DefaultValue(true)]
        public bool DreadSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDragonFlamesConfigToggle")]
        [DefaultValue(true)]
        public bool DragonFlames;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWyvernPetConfigToggle")]
        [DefaultValue(true)]
        public bool WyvernPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDemonBloodConfigToggle")]
        [DefaultValue(true)]
        public bool DemonBloodEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumFleshDropsConfigToggle")]
        [DefaultValue(true)]
        public bool FleshDrops;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumVampireGlandConfigToggle")]
        [DefaultValue(true)]
        public bool VampireGland;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBlisterPetConfigToggle")]
        [DefaultValue(true)]
        public bool BlisterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBerserkerConfigToggle")]
        [DefaultValue(true)]
        public bool BerserkerEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSlagStompersConfigToggle")]
        [DefaultValue(true)]
        public bool SlagStompers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpringStepsConfigToggle")]
        [DefaultValue(true)]
        public bool SpringSteps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumHarbingerOverchargeConfigToggle")]
        [DefaultValue(true)]
        public bool HarbingerOvercharge;

        [Header("$Mods.FargowiltasSouls.Toggles.AsgardForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideGlobulesConfigToggle")]
        [DefaultValue(true)]
        public bool TideGlobules;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideDaggersConfigToggle")]
        [DefaultValue(true)]
        public bool TideDaggers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAssassinDamageConfigToggle")]
        [DefaultValue(true)]
        public bool AssassinDamage;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumpyromancerBurstsConfigToggle")]
        [DefaultValue(true)]
        public bool PyromancerBursts;
    }

    public class CalamityToggles
    {
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityElementalQuiverConfigToggle")]
        [DefaultValue(true)]
        public bool ElementalQuiver;

        //aerospec
        [Header("$Mods.FargowiltasSouls.Toggles.AnnihilationForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityValkyrieMinionConfigToggle")]
        [DefaultValue(true)]
        public bool ValkyrieMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGladiatorLocketConfigToggle")]
        [DefaultValue(true)]
        public bool GladiatorLocket;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityUnstablePrismConfigToggle")]
        [DefaultValue(true)]
        public bool UnstablePrism;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRotomConfigToggle")]
        [DefaultValue(true)]
        public bool RotomPet;

        //statigel
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFungalSymbioteToggle")]
        [DefaultValue(true)]
        public bool FungalSymbiote;

        //hydrothermic
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAtaxiaEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool AtaxiaEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityChaosMinionConfigToggle")]
        [DefaultValue(true)]
        public bool ChaosMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHallowedRuneConfigToggle")]
        [DefaultValue(true)]
        public bool HallowedRune;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityEtherealExtorterConfigToggle")]
        [DefaultValue(true)]
        public bool EtherealExtorter;

        //xeroc
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityXerocEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool XerocEffects;

        //fearmonger
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheEvolutionConfigToggle")]
        [DefaultValue(true)]
        public bool TheEvolution;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityStatisBeltOfCursesConfigToggle")]
        [DefaultValue(true)]
        public bool StatisBeltOfCurses;

        //reaver
        [Header("$Mods.FargowiltasSouls.Toggles.DevastationForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaverEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool ReaverEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaverMinionConfigToggle")]
        [DefaultValue(true)]
        public bool ReaverMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFabledTurtleConfigToggle")]
        [DefaultValue(false)]
        public bool FabledTurtleShell;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySparksConfigToggle")]
        [DefaultValue(true)]
        public bool SparksPet;

        //plague reaper
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPlagueHiveConfigToggle")]
        [DefaultValue(true)]
        public bool PlagueHive;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPlaguedFuelPackConfigToggle")]
        [DefaultValue(true)]
        public bool PlaguedFuelPack;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheBeeConfigToggle")]
        [DefaultValue(true)]
        public bool TheBee;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheCamperConfigToggle")]
        [DefaultValue(false)]
        public bool TheCamper;

        //demonshade
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDevilMinionConfigToggle")]
        [DefaultValue(true)]
        public bool RedDevilMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityProfanedSoulConfigToggle")]
        [DefaultValue(true)]
        public bool ProfanedSoulCrystal;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityLeviConfigToggle")]
        [DefaultValue(true)]
        public bool LeviPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityScalConfigToggle")]
        [DefaultValue(true)]
        public bool ScalPet;

        //daedalus
        [Header("$Mods.FargowiltasSouls.Toggles.DesolationForceToggle")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDaedalusEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool DaedalusEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDaedalusMinionConfigToggle")]
        [DefaultValue(true)]
        public bool DaedalusMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPermafrostPotionConfigToggle")]
        [DefaultValue(true)]
        public bool PermafrostPotion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRegeneratorConfigToggle")]
        [DefaultValue(false)]
        public bool Regenerator;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityKendraConfigToggle")]
        [DefaultValue(true)]
        public bool KendraPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBearConfigToggle")]
        [DefaultValue(true)]
        public bool BearPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityThirdSageConfigToggle")]
        [DefaultValue(true)]
        public bool ThirdSagePet;

        //astral
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAstralStarsConfigToggle")]
        [DefaultValue(true)]
        public bool AstralStars;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHideofAstrumDeusConfigToggle")]
        [DefaultValue(true)]
        public bool HideofAstrumDeus;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGravistarSabatonConfigToggle")]
        [DefaultValue(true)]
        public bool GravistarSabaton;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAstrophageConfigToggle")]
        [DefaultValue(true)]
        public bool AstrophagePet;

        //omega blue
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityOmegaTentaclesConfigToggle")]
        [DefaultValue(true)]
        public bool OmegaTentacles;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDivingSuitConfigToggle")]
        [DefaultValue(false)]
        public bool DivingSuit;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaperToothNecklaceConfigToggle")]
        [DefaultValue(false)]
        public bool ReaperToothNecklace;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityMutatedTruffleConfigToggle")]
        [DefaultValue(true)]
        public bool MutatedTruffle;

        //victide
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityUrchinConfigToggle")]
        [DefaultValue(true)]
        public bool UrchinMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityLuxorGiftConfigToggle")]
        [DefaultValue(true)]
        public bool LuxorGift;

        //organize more later ech

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBloodflareEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool BloodflareEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPolterMinesConfigToggle")]
        [DefaultValue(true)]
        public bool PolterMines;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySilvaEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool SilvaEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySilvaMinionConfigToggle")]
        [DefaultValue(true)]
        public bool SilvaMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGodlyArtifactConfigToggle")]
        [DefaultValue(true)]
        public bool GodlySoulArtifact;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityYharimGiftConfigToggle")]
        [DefaultValue(true)]
        public bool YharimGift;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFungalMinionConfigToggle")]
        [DefaultValue(true)]
        public bool FungalMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPoisonSeawaterConfigToggle")]
        [DefaultValue(true)]
        public bool PoisonSeawater;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAkatoConfigToggle")]
        [DefaultValue(true)]
        public bool AkatoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGodSlayerEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool GodSlayerEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityMechwormMinionConfigToggle")]
        [DefaultValue(true)]
        public bool MechwormMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityNebulousCoreConfigToggle")]
        [DefaultValue(true)]
        public bool NebulousCore;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityChibiiConfigToggle")]
        [DefaultValue(true)]
        public bool ChibiiPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAuricEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool AuricEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityWaifuMinionsConfigToggle")]
        [DefaultValue(true)]
        public bool WaifuMinions;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityShellfishMinionConfigToggle")]
        [DefaultValue(true)]
        public bool ShellfishMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAmidiasPendantConfigToggle")]
        [DefaultValue(true)]
        public bool AmidiasPendant;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGiantPearlConfigToggle")]
        [DefaultValue(true)]
        public bool GiantPearl;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDannyConfigToggle")]
        [DefaultValue(true)]
        public bool DannyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBrimlingConfigToggle")]
        [DefaultValue(true)]
        public bool BrimlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTarragonEffectsConfigToggle")]
        [DefaultValue(true)]
        public bool TarragonEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRadiatorConfigToggle")]
        [DefaultValue(true)]
        public bool RadiatorPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGhostBellConfigToggle")]
        [DefaultValue(true)]
        public bool GhostBellPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFlakConfigToggle")]
        [DefaultValue(true)]
        public bool FlakPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFoxConfigToggle")]
        [DefaultValue(true)]
        public bool FoxPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySirenConfigToggle")]
        [DefaultValue(true)]
        public bool SirenPet;
    }
}