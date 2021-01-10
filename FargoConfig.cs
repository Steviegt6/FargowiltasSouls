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

        [Header("$Mods.FargowiltasSouls.Toggles.PresetHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.AllTogglesOn")]
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

        [Label("$Mods.FargowiltasSouls.Toggles.AllTogglesOff")]
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

        [Label("$Mods.FargowiltasSouls.Toggles.MinimalEffectsOnly")]
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

        [Header("$Mods.FargowiltasSouls.Toggles.WoodHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.BorealConfig")]
        [DefaultValue(true)]
        public bool BorealSnowballs;

        [Label("$Mods.FargowiltasSouls.Toggles.EbonConfig")]
        [DefaultValue(true)]
        public bool EbonwoodAura;

        [Label("$Mods.FargowiltasSouls.Toggles.ShadeConfig")]
        [DefaultValue(true)]
        public bool ShadewoodEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.MahoganyConfig")]
        [DefaultValue(true)]
        public bool MahoganyHook;

        [Label("$Mods.FargowiltasSouls.Toggles.PalmConfig")]
        [DefaultValue(true)]
        public bool PalmwoodSentry;

        [Label("$Mods.FargowiltasSouls.Toggles.PearlConfig")]
        [DefaultValue(true)]
        public bool PearlwoodStars;

        [Header("$Mods.FargowiltasSouls.Toggles.EarthHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.AdamantiteConfig")]
        [DefaultValue(true)]
        public bool AdamantiteSplit;

        [Label("$Mods.FargowiltasSouls.Toggles.CobaltConfig")]
        [DefaultValue(true)]
        public bool CobaltShards;

        [Label("$Mods.FargowiltasSouls.Toggles.AncientCobaltConfig")]
        [DefaultValue(true)]
        public bool CobaltStingers;

        [Label("$Mods.FargowiltasSouls.Toggles.MythrilConfig")]
        [DefaultValue(true)]
        public bool MythrilSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.OrichalcumConfig")]
        [DefaultValue(true)]
        public bool OrichalcumPetals;

        [Label("$Mods.FargowiltasSouls.Toggles.PalladiumConfig")]
        [DefaultValue(true)]
        public bool PalladiumHeal;

        [Label("$Mods.FargowiltasSouls.Toggles.PalladiumOrbConfig")]
        [DefaultValue(true)]
        public bool PalladiumOrb;

        [Label("$Mods.FargowiltasSouls.Toggles.TitaniumConfig")]
        [DefaultValue(true)]
        public bool TitaniumDodge;

        [Header("$Mods.FargowiltasSouls.Toggles.TerraHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.CopperConfig")]
        [DefaultValue(true)]
        public bool CopperLightning;

        [Label("$Mods.FargowiltasSouls.Toggles.IronMConfig")]
        [DefaultValue(true)]
        public bool IronMagnet;

        [Label("$Mods.FargowiltasSouls.Toggles.IronSConfig")]
        [DefaultValue(true)]
        public bool IronShield;

        [Label("$Mods.FargowiltasSouls.Toggles.CthulhuShield")]
        [DefaultValue(true)]
        public bool CthulhuShield;

        [Label("$Mods.FargowiltasSouls.Toggles.TinConfig")]
        [DefaultValue(true)]
        public bool TinCrit;

        [Label("$Mods.FargowiltasSouls.Toggles.TungstenConfig")]
        [DefaultValue(true)]
        public bool TungstenSize;

        [Label("$Mods.FargowiltasSouls.Toggles.TungstenProjConfig")]
        [DefaultValue(true)]
        public bool TungstenProjSize;

        [Label("$Mods.FargowiltasSouls.Toggles.ObsidianConfig")]
        [DefaultValue(true)]
        public bool ObsidianExplosion;

        [Header("$Mods.FargowiltasSouls.Toggles.WillHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.GladiatorConfig")]
        [DefaultValue(true)]
        public bool GladiatorJavelins;

        [Label("$Mods.FargowiltasSouls.Toggles.GoldConfig")]
        [DefaultValue(true)]
        public bool LuckyCoin;

        [Label("$Mods.FargowiltasSouls.Toggles.HuntressConfig")]
        [DefaultValue(true)]
        public bool HuntressAbility;

        [Label("$Mods.FargowiltasSouls.Toggles.ValhallaConfig")]
        [DefaultValue(true)]
        public bool ValhallaEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.SquirePanicConfig")]
        [DefaultValue(true)]
        public bool SquirePanic;

        [Header("$Mods.FargowiltasSouls.Toggles.LifeHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.BeeConfig")]
        [DefaultValue(true)]
        public bool BeeEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.BeetleConfig")]
        [DefaultValue(true)]
        public bool BeetleEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.CactusConfig")]
        [DefaultValue(true)]
        public bool CactusNeedles;

        [Label("$Mods.FargowiltasSouls.Toggles.PumpkinConfig")]
        [DefaultValue(true)]
        public bool PumpkinFire;

        [Label("$Mods.FargowiltasSouls.Toggles.SpiderConfig")]
        [DefaultValue(true)]
        public bool SpiderCrits;

        [Label("$Mods.FargowiltasSouls.Toggles.TurtleConfig")]
        [DefaultValue(true)]
        public bool TurtleShell;

        [Header("$Mods.FargowiltasSouls.Toggles.NatureHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.ChlorophyteConfig")]
        [DefaultValue(true)]
        public bool ChlorophyteCrystals;

        [Label("$Mods.FargowiltasSouls.Toggles.ChlorophyteFlowerConfig")]
        [DefaultValue(true)]
        public bool ChlorophyteFlowerBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.CrimsonConfig")]
        [DefaultValue(true)]
        public bool CrimsonRegen;

        [Label("$Mods.FargowiltasSouls.Toggles.FrostConfig")]
        [DefaultValue(true)]
        public bool FrostIcicles;

        [Label("$Mods.FargowiltasSouls.Toggles.SnowConfig")]
        [DefaultValue(true)]
        public bool SnowStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.JungleConfig")]
        [DefaultValue(true)]
        public bool JungleSpores;

        [Label("$Mods.FargowiltasSouls.Toggles.CordageConfig")]
        [DefaultValue(true)]
        public bool Cordage;

        [Label("$Mods.FargowiltasSouls.Toggles.MoltenConfig")]
        [DefaultValue(true)]
        public bool MoltenInferno;

        [Label("$Mods.FargowiltasSouls.Toggles.MoltenEConfig")]
        [DefaultValue(true)]
        public bool MoltenExplosion;

        [Label("$Mods.FargowiltasSouls.Toggles.RainConfig")]
        [DefaultValue(true)]
        public bool RainCloud;

        [Label("$Mods.FargowiltasSouls.Toggles.ShroomiteConfig")]
        [DefaultValue(true)]
        public bool ShroomiteStealth;

        [Label("$Mods.FargowiltasSouls.Toggles.ShroomiteShroomConfig")]
        [DefaultValue(true)]
        public bool ShroomiteShrooms;

        [Header("$Mods.FargowiltasSouls.Toggles.ShadowHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.DarkArtConfig")]
        [DefaultValue(true)]
        public bool DarkArtistMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.ApprenticeConfig")]
        [DefaultValue(true)]
        public bool ApprenticeEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.NecroConfig")]
        [DefaultValue(true)]
        public bool NecroGuardian;

        [Label("$Mods.FargowiltasSouls.Toggles.AncientShadowConfig")]
        [DefaultValue(true)]
        public bool AncientShadow;

        [Label("$Mods.FargowiltasSouls.Toggles.ShadowConfig")]
        [DefaultValue(true)]
        public bool ShadowDarkness;

        [Label("$Mods.FargowiltasSouls.Toggles.MonkConfig")]
        [DefaultValue(true)]
        public bool MonkDash;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiConfig")]
        [DefaultValue(true)]
        public bool ShinobiWalls;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiTabiConfig")]
        [DefaultValue(true)]
        public bool ShinobiTabi;

        [Label("$Mods.FargowiltasSouls.Toggles.ShinobiClimbingConfig")]
        [DefaultValue(true)]
        public bool ShinobiClimbing;

        [Label("$Mods.FargowiltasSouls.Toggles.SpookyConfig")]
        [DefaultValue(true)]
        public bool SpookyScythes;

        [Header("$Mods.FargowiltasSouls.Toggles.SpiritHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.ForbiddenConfig")]
        [DefaultValue(true)]
        public bool ForbiddenStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.HallowedConfig")]
        [DefaultValue(true)]
        public bool HallowSword;

        [Label("$Mods.FargowiltasSouls.Toggles.HallowSConfig")]
        [DefaultValue(true)]
        public bool HallowShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SilverConfig")]
        [DefaultValue(true)]
        public bool SilverSword;

        [Label("$Mods.FargowiltasSouls.Toggles.SpectreConfig")]
        [DefaultValue(true)]
        public bool SpectreOrbs;

        [Label("$Mods.FargowiltasSouls.Toggles.TikiConfig")]
        [DefaultValue(true)]
        public bool TikiMinions;

        [Header("$Mods.FargowiltasSouls.Toggles.CosmoHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MeteorConfig")]
        [DefaultValue(true)]
        public bool MeteorShower;

        [Label("$Mods.FargowiltasSouls.Toggles.NebulaConfig")]
        [DefaultValue(true)]
        public bool NebulaBoost;

        [Label("$Mods.FargowiltasSouls.Toggles.SolarConfig")]
        [DefaultValue(true)]
        public bool SolarShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SolarFlareConfig")]
        [DefaultValue(true)]
        public bool SolarFlare;

        [Label("$Mods.FargowiltasSouls.Toggles.StardustConfig")]
        [DefaultValue(true)]
        public bool StardustGuardian;

        [Label("$Mods.FargowiltasSouls.Toggles.VortexSConfig")]
        [DefaultValue(true)]
        public bool VortexStealth;

        [Label("$Mods.FargowiltasSouls.Toggles.VortexVConfig")]
        [DefaultValue(true)]
        public bool VortexVoid;

        #region maso accessories

        [Header("$Mods.FargowiltasSouls.Toggles.MasoHeader")]
        /*[Label("$Mods.FargowiltasSouls.Toggles.MasoBossBG")]
        [DefaultValue(true)]
        public bool MutantBackground;*/

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBossRecolors")]
        [DefaultValue(true)]
        public bool BossRecolors;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoIconConfig")]
        [DefaultValue(true)]
        public bool SinisterIcon;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoIconDropsConfig")]
        [DefaultValue(true)]
        public bool SinisterIconDrops;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGrazeConfig")]
        [DefaultValue(true)]
        public bool Graze;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoDevianttHeartsConfig")]
        [DefaultValue(true)]
        public bool DevianttHearts;

        [Header("$Mods.FargowiltasSouls.Toggles.SupremeFairyHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoSlimeConfig")]
        [DefaultValue(true)]
        public bool SlimyShield;

        [Label("$Mods.FargowiltasSouls.Toggles.SlimeFallingConfig")]
        [DefaultValue(true)]
        public bool SlimyFalling;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoEyeConfig")]
        [DefaultValue(true)]
        public bool AgitatedLens;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoHoneyConfig")]
        [DefaultValue(true)]
        public bool QueenStingerHoney;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoSkeleConfig")]
        [DefaultValue(true)]
        public bool NecromanticBrew;

        [Header("$Mods.FargowiltasSouls.Toggles.BionomicHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoConcoctionConfig")]
        [DefaultValue(true)]
        public bool TimsConcoction;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoCarrotConfig")]
        [DefaultValue(true)]
        public bool Carrot;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoRainbowConfig")]
        [DefaultValue(true)]
        public bool RainbowSlime;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoFrigidConfig")]
        [DefaultValue(true)]
        public bool FrigidGemstone;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoNymphConfig")]
        [DefaultValue(true)]
        public bool NymphPerfume;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoSqueakConfig")]
        [DefaultValue(true)]
        public bool SqueakyToy;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoPouchConfig")]
        [DefaultValue(true)]
        public bool WretchedPouch;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoClippedConfig")]
        [DefaultValue(true)]
        public bool DragonFang;

        [Label("$Mods.FargowiltasSouls.Toggles.TribalCharmConfig")]
        [DefaultValue(true)]
        public bool TribalCharm;

        /*[Label("$Mods.FargowiltasSouls.Toggles.WalletHeader")]
        public WalletToggles walletToggles = new WalletToggles();*/

        [Header("$Mods.FargowiltasSouls.Toggles.DubiousHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoLightningConfig")]
        [DefaultValue(true)]
        public bool LightningRod;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoProbeConfig")]
        [DefaultValue(true)]
        public bool ProbeMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.PureHeartHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoEaterConfig")]
        [DefaultValue(true)]
        public bool CorruptHeart;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBrainConfig")]
        [DefaultValue(true)]
        public bool GuttedHeart;

        [Header("$Mods.FargowiltasSouls.Toggles.LumpofFleshHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoPugentConfig")]
        [DefaultValue(true)]
        public bool PungentEye;

        [Header("$Mods.FargowiltasSouls.Toggles.ChaliceHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoCultistConfig")]
        [DefaultValue(true)]
        public bool CultistMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoPlantConfig")]
        [DefaultValue(true)]
        public bool PlanteraMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGolemConfig")]
        [DefaultValue(true)]
        public bool LihzahrdBoxGeysers;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoBoulderConfig")]
        [DefaultValue(true)]
        public bool LihzahrdBoxBoulders;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoCelestConfig")]
        [DefaultValue(true)]
        public bool CelestialRune;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoVisionConfig")]
        [DefaultValue(true)]
        public bool AncientVisions;

        [Header("$Mods.FargowiltasSouls.Toggles.HeartHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoPump")]
        [DefaultValue(true)]
        public bool PumpkingCape;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoFlockoConfig")]
        [DefaultValue(true)]
        public bool FlockoMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoUfoConfig")]
        [DefaultValue(true)]
        public bool UFOMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGravConfig")]
        [DefaultValue(true)]
        public bool GravityControl;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoGrav2Config")]
        [DefaultValue(true)]
        public bool StabilizedGravity;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoTrueEyeConfig")]
        [DefaultValue(true)]
        public bool TrueEyes;

        [Header("$Mods.FargowiltasSouls.Toggles.CyclonicHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoFishronConfig")]
        [DefaultValue(true)]
        public bool FishronMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.MutantArmorHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MasoAbomConfig")]
        [DefaultValue(true)]
        public bool AbomMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoRingConfig")]
        [DefaultValue(true)]
        public bool RingMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.MasoReviveDeathrayConfig")]
        [DefaultValue(true)]
        public bool ReviveDeathray;

        #endregion maso accessories

        #region souls

        [Header("$Mods.FargowiltasSouls.Toggles.SoulHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.MeleeConfig")]
        [DefaultValue(true)]
        public bool BerserkerAttackSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MagmaStoneConfig")]
        [DefaultValue(true)]
        public bool MagmaStone;

        [Label("$Mods.FargowiltasSouls.Toggles.YoyoBagConfig")]
        [DefaultValue(true)]
        public bool YoyoBag;

        [Label("$Mods.FargowiltasSouls.Toggles.SniperConfig")]
        [DefaultValue(true)]
        public bool SniperScope;

        [Label("$Mods.FargowiltasSouls.Toggles.UniverseConfig")]
        [DefaultValue(true)]
        public bool UniverseAttackSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningHuntConfig")]
        [DefaultValue(true)]
        public bool MinerHunter;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningDangerConfig")]
        [DefaultValue(true)]
        public bool MinerDanger;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningSpelunkConfig")]
        [DefaultValue(true)]
        public bool MinerSpelunker;

        [Label("$Mods.FargowiltasSouls.Toggles.MiningShineConfig")]
        [DefaultValue(true)]
        public bool MinerShine;

        [Label("$Mods.FargowiltasSouls.Toggles.BuilderConfig")]
        [DefaultValue(false)]
        public bool BuilderMode;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseSporeConfig")]
        [DefaultValue(true)]
        public bool SporeSac;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseStarConfig")]
        [DefaultValue(true)]
        public bool StarCloak;

        [Label("$Mods.FargowiltasSouls.Toggles.DefenseBeeConfig")]
        [DefaultValue(true)]
        public bool BeesOnHit;

        [Label("$Mods.FargowiltasSouls.Toggles.DefensePanicConfig")]
        [DefaultValue(true)]
        public bool PanicOnHit;

        [Label("$Mods.FargowiltasSouls.Toggles.RunSpeedConfig")]
        [DefaultValue(true)]
        public bool IncreasedRunSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.MomentumConfig")]
        [DefaultValue(true)]
        public bool NoMomentum;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicConfig")]
        [DefaultValue(true)]
        public bool SupersonicSpeed;

        [Label("Supersonic Speed Multiplier")]
        [Increment(1)]
        [Range(1, 10)]
        [DefaultValue(5)]
        [Slider]
        public int SupersonicMultiplier;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicJumpsConfig")]
        [DefaultValue(true)]
        public bool SupersonicJumps;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicRocketBootsConfig")]
        [DefaultValue(true)]
        public bool SupersonicRocketBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.SupersonicCarpetConfig")]
        [DefaultValue(true)]
        public bool SupersonicCarpet;

        [Label("$Mods.FargowiltasSouls.Toggles.TrawlerConfig")]
        [DefaultValue(true)]
        public bool TrawlerLures;

        [Label("$Mods.FargowiltasSouls.Toggles.EternityConfig")]
        [DefaultValue(true)]
        public bool EternityStacking;

        #endregion souls

        #region pets

        [Header("$Mods.FargowiltasSouls.Toggles.PetHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.PetCatConfig")]
        [DefaultValue(true)]
        public bool BlackCatPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetCubeConfig")]
        [DefaultValue(true)]
        public bool CompanionCubePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetCurseSapConfig")]
        [DefaultValue(true)]
        public bool CursedSaplingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDinoConfig")]
        [DefaultValue(true)]
        public bool DinoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDragonConfig")]
        [DefaultValue(true)]
        public bool DragonPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetEaterConfig")]
        [DefaultValue(true)]
        public bool EaterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetEyeSpringConfig")]
        [DefaultValue(true)]
        public bool EyeSpringPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetFaceMonsterConfig")]
        [DefaultValue(true)]
        public bool FaceMonsterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetGatoConfig")]
        [DefaultValue(true)]
        public bool GatoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetHornetConfig")]
        [DefaultValue(true)]
        public bool HornetPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetLizardConfig")]
        [DefaultValue(true)]
        public bool LizardPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetMinitaurConfig")]
        [DefaultValue(true)]
        public bool MinotaurPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetParrotConfig")]
        [DefaultValue(true)]
        public bool ParrotPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetPenguinConfig")]
        [DefaultValue(true)]
        public bool PenguinPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetPupConfig")]
        [DefaultValue(true)]
        public bool PuppyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSeedConfig")]
        [DefaultValue(true)]
        public bool SeedlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetDGConfig")]
        [DefaultValue(true)]
        public bool DGPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSnowmanConfig")]
        [DefaultValue(true)]
        public bool SnowmanPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetGrinchConfig")]
        [DefaultValue(true)]
        public bool GrinchPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSpiderConfig")]
        [DefaultValue(true)]
        public bool SpiderPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSquashConfig")]
        [DefaultValue(true)]
        public bool SquashlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetTikiConfig")]
        [DefaultValue(true)]
        public bool TikiPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetShroomConfig")]
        [DefaultValue(true)]
        public bool TrufflePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetTurtleConfig")]
        [DefaultValue(true)]
        public bool TurtlePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetZephyrConfig")]
        [DefaultValue(true)]
        public bool ZephyrFishPet;

        //LIGHT PETS
        [Label("$Mods.FargowiltasSouls.Toggles.PetHeartConfig")]
        [DefaultValue(true)]
        public bool CrimsonHeartPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetNaviConfig")]
        [DefaultValue(true)]
        public bool FairyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetFlickerConfig")]
        [DefaultValue(true)]
        public bool FlickerwickPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetLanturnConfig")]
        [DefaultValue(true)]
        public bool MagicLanternPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetOrbConfig")]
        [DefaultValue(true)]
        public bool ShadowOrbPet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetSuspEyeConfig")]
        [DefaultValue(true)]
        public bool SuspiciousEyePet;

        [Label("$Mods.FargowiltasSouls.Toggles.PetWispConfig")]
        [DefaultValue(true)]
        public bool WispPet;

        #endregion pets

        [Header("$Mods.FargowiltasSouls.Toggles.PatreonHeader")]
        [Label("$Mods.FargowiltasSouls.Toggles.PatreonRoomba")]
        [DefaultValue(true)]
        public bool PatreonRoomba;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonOrb")]
        [DefaultValue(true)]
        public bool PatreonOrb;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonFishingRod")]
        [DefaultValue(true)]
        public bool PatreonFishingRod;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonDoor")]
        [DefaultValue(true)]
        public bool PatreonDoor;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonWolf")]
        [DefaultValue(true)]
        public bool PatreonWolf;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonDove")]
        [DefaultValue(true)]
        public bool PatreonDove;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonKingSlime")]
        [DefaultValue(true)]
        public bool PatreonKingSlime;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonFishron")]
        [DefaultValue(true)]
        public bool PatreonFishron;

        [Label("$Mods.FargowiltasSouls.Toggles.PatreonPlant")]
        [DefaultValue(true)]
        public bool PatreonPlant;

        /*[Label("$Mods.FargowiltasSouls.Toggles.ThoriumHeader")]
        public ThoriumToggles thoriumToggles = new ThoriumToggles();

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHeader")]
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
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCrystalScorpionConfig")]
        [DefaultValue(true)]
        public bool CrystalScorpion;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumHeadMirrorConfig")]
        [DefaultValue(true)]
        public bool HeadMirror;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAirWalkersConfig")]
        [DefaultValue(true)]
        public bool AirWalkers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumGlitterPetConfig")]
        [DefaultValue(true)]
        public bool GlitterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCoinPetConfig")]
        [DefaultValue(true)]
        public bool CoinPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBoxPetConfig")]
        [DefaultValue(true)]
        public bool BoxPet;

        [Header("$Mods.FargowiltasSouls.Toggles.MuspelheimForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBeeBootiesConfig")]
        [DefaultValue(true)]
        public bool BeeBooties;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSaplingMinionConfig")]
        [DefaultValue(true)]
        public bool SaplingMinion;

        [Header("$Mods.FargowiltasSouls.Toggles.JotunheimForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumJellyfishPetConfig")]
        [DefaultValue(true)]
        public bool JellyfishPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideFoamConfig")]
        [DefaultValue(true)]
        public bool TideFoam;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumYewCritsConfig")]
        [DefaultValue(true)]
        public bool YewCrits;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCryoDamageConfig")]
        [DefaultValue(true)]
        public bool CryoDamage;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumOwlPetConfig")]
        [DefaultValue(true)]
        public bool OwlPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIcyBarrierConfig")]
        [DefaultValue(true)]
        public bool IcyBarrier;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWhisperingTentaclesConfig")]
        [DefaultValue(true)]
        public bool WhisperingTentacles;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpiritPetConfig")]
        [DefaultValue(true)]
        public bool SpiritPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWarlockWispsConfig")]
        [DefaultValue(true)]
        public bool WarlockWisps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBiotechProbeConfig")]
        [DefaultValue(true)]
        public bool BiotechProbe;

        [Header("$Mods.FargowiltasSouls.Toggles.NiflheimForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMixTapeConfig")]
        [DefaultValue(true)]
        public bool MixTape;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCyberStatesConfig")]
        [DefaultValue(true)]
        public bool CyberStates;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMetronomeConfig")]
        [DefaultValue(true)]
        public bool Metronome;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumMarchingBandConfig")]
        [DefaultValue(true)]
        public bool MarchingBand;

        [Header("$Mods.FargowiltasSouls.Toggles.SvartalfheimForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumEyeoftheStormConfig")]
        [DefaultValue(true)]
        public bool EyeoftheStorm;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBronzeLightningConfig")]
        [DefaultValue(true)]
        public bool BronzeLightning;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumConduitShieldConfig")]
        [DefaultValue(true)]
        public bool ConduitShield;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumOmegaPetConfig")]
        [DefaultValue(true)]
        public bool OmegaPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIFOPetConfig")]
        [DefaultValue(true)]
        public bool IFOPet;

        [Header("$Mods.FargowiltasSouls.Toggles.MidgardForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumLodestoneConfig")]
        [DefaultValue(true)]
        public bool LodestoneResist;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBeholderEyeConfig")]
        [DefaultValue(true)]
        public bool BeholderEye;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumIllumiteMissileConfig")]
        [DefaultValue(true)]
        public bool IllumiteMissile;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTerrariumSpiritsConfig")]
        [DefaultValue(true)]
        public bool TerrariumSpirits;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDiverConfig")]
        [DefaultValue(true)]
        public bool ThoriumDivers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCrietzConfig")]
        [DefaultValue(true)]
        public bool Crietz;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumJesterBellConfig")]
        [DefaultValue(true)]
        public bool JesterBell;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumManaBootsConfig")]
        [DefaultValue(true)]
        public bool ManaBoots;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWhiteDwarfConfig")]
        [DefaultValue(true)]
        public bool WhiteDwarf;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumCelestialAuraConfig")]
        [DefaultValue(true)]
        public bool CelestialAura;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAscensionStatueConfig")]
        [DefaultValue(true)]
        public bool AscensionStatue;

        [Header("$Mods.FargowiltasSouls.Toggles.HelheimForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpiritWispsConfig")]
        [DefaultValue(true)]
        public bool SpiritTrapperWisps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDreadConfig")]
        [DefaultValue(true)]
        public bool DreadSpeed;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDragonFlamesConfig")]
        [DefaultValue(true)]
        public bool DragonFlames;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumWyvernPetConfig")]
        [DefaultValue(true)]
        public bool WyvernPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumDemonBloodConfig")]
        [DefaultValue(true)]
        public bool DemonBloodEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumFleshDropsConfig")]
        [DefaultValue(true)]
        public bool FleshDrops;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumVampireGlandConfig")]
        [DefaultValue(true)]
        public bool VampireGland;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBlisterPetConfig")]
        [DefaultValue(true)]
        public bool BlisterPet;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumBerserkerConfig")]
        [DefaultValue(true)]
        public bool BerserkerEffect;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSlagStompersConfig")]
        [DefaultValue(true)]
        public bool SlagStompers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumSpringStepsConfig")]
        [DefaultValue(true)]
        public bool SpringSteps;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumHarbingerOverchargeConfig")]
        [DefaultValue(true)]
        public bool HarbingerOvercharge;

        [Header("$Mods.FargowiltasSouls.Toggles.AsgardForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideGlobulesConfig")]
        [DefaultValue(true)]
        public bool TideGlobules;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumTideDaggersConfig")]
        [DefaultValue(true)]
        public bool TideDaggers;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumAssassinDamageConfig")]
        [DefaultValue(true)]
        public bool AssassinDamage;

        [Label("$Mods.FargowiltasSouls.Toggles.ThoriumpyromancerBurstsConfig")]
        [DefaultValue(true)]
        public bool PyromancerBursts;
    }

    public class CalamityToggles
    {
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityElementalQuiverConfig")]
        [DefaultValue(true)]
        public bool ElementalQuiver;

        //aerospec
        [Header("$Mods.FargowiltasSouls.Toggles.AnnihilationForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityValkyrieMinionConfig")]
        [DefaultValue(true)]
        public bool ValkyrieMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGladiatorLocketConfig")]
        [DefaultValue(true)]
        public bool GladiatorLocket;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityUnstablePrismConfig")]
        [DefaultValue(true)]
        public bool UnstablePrism;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRotomConfig")]
        [DefaultValue(true)]
        public bool RotomPet;

        //statigel
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFungalSymbiote")]
        [DefaultValue(true)]
        public bool FungalSymbiote;

        //hydrothermic
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAtaxiaEffectsConfig")]
        [DefaultValue(true)]
        public bool AtaxiaEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityChaosMinionConfig")]
        [DefaultValue(true)]
        public bool ChaosMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHallowedRuneConfig")]
        [DefaultValue(true)]
        public bool HallowedRune;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityEtherealExtorterConfig")]
        [DefaultValue(true)]
        public bool EtherealExtorter;

        //xeroc
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityXerocEffectsConfig")]
        [DefaultValue(true)]
        public bool XerocEffects;

        //fearmonger
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheEvolutionConfig")]
        [DefaultValue(true)]
        public bool TheEvolution;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityStatisBeltOfCursesConfig")]
        [DefaultValue(true)]
        public bool StatisBeltOfCurses;

        //reaver
        [Header("$Mods.FargowiltasSouls.Toggles.DevastationForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaverEffectsConfig")]
        [DefaultValue(true)]
        public bool ReaverEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaverMinionConfig")]
        [DefaultValue(true)]
        public bool ReaverMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFabledTurtleConfig")]
        [DefaultValue(false)]
        public bool FabledTurtleShell;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySparksConfig")]
        [DefaultValue(true)]
        public bool SparksPet;

        //plague reaper
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPlagueHiveConfig")]
        [DefaultValue(true)]
        public bool PlagueHive;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPlaguedFuelPackConfig")]
        [DefaultValue(true)]
        public bool PlaguedFuelPack;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheBeeConfig")]
        [DefaultValue(true)]
        public bool TheBee;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTheCamperConfig")]
        [DefaultValue(false)]
        public bool TheCamper;

        //demonshade
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDevilMinionConfig")]
        [DefaultValue(true)]
        public bool RedDevilMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityProfanedSoulConfig")]
        [DefaultValue(true)]
        public bool ProfanedSoulCrystal;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityLeviConfig")]
        [DefaultValue(true)]
        public bool LeviPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityScalConfig")]
        [DefaultValue(true)]
        public bool ScalPet;

        //daedalus
        [Header("$Mods.FargowiltasSouls.Toggles.DesolationForce")]
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDaedalusEffectsConfig")]
        [DefaultValue(true)]
        public bool DaedalusEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDaedalusMinionConfig")]
        [DefaultValue(true)]
        public bool DaedalusMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPermafrostPotionConfig")]
        [DefaultValue(true)]
        public bool PermafrostPotion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRegeneratorConfig")]
        [DefaultValue(false)]
        public bool Regenerator;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityKendraConfig")]
        [DefaultValue(true)]
        public bool KendraPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBearConfig")]
        [DefaultValue(true)]
        public bool BearPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityThirdSageConfig")]
        [DefaultValue(true)]
        public bool ThirdSagePet;

        //astral
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAstralStarsConfig")]
        [DefaultValue(true)]
        public bool AstralStars;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityHideofAstrumDeusConfig")]
        [DefaultValue(true)]
        public bool HideofAstrumDeus;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGravistarSabatonConfig")]
        [DefaultValue(true)]
        public bool GravistarSabaton;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAstrophageConfig")]
        [DefaultValue(true)]
        public bool AstrophagePet;

        //omega blue
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityOmegaTentaclesConfig")]
        [DefaultValue(true)]
        public bool OmegaTentacles;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDivingSuitConfig")]
        [DefaultValue(false)]
        public bool DivingSuit;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityReaperToothNecklaceConfig")]
        [DefaultValue(false)]
        public bool ReaperToothNecklace;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityMutatedTruffleConfig")]
        [DefaultValue(true)]
        public bool MutatedTruffle;

        //victide
        [Label("$Mods.FargowiltasSouls.Toggles.CalamityUrchinConfig")]
        [DefaultValue(true)]
        public bool UrchinMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityLuxorGiftConfig")]
        [DefaultValue(true)]
        public bool LuxorGift;

        //organize more later ech

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBloodflareEffectsConfig")]
        [DefaultValue(true)]
        public bool BloodflareEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPolterMinesConfig")]
        [DefaultValue(true)]
        public bool PolterMines;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySilvaEffectsConfig")]
        [DefaultValue(true)]
        public bool SilvaEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySilvaMinionConfig")]
        [DefaultValue(true)]
        public bool SilvaMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGodlyArtifactConfig")]
        [DefaultValue(true)]
        public bool GodlySoulArtifact;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityYharimGiftConfig")]
        [DefaultValue(true)]
        public bool YharimGift;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFungalMinionConfig")]
        [DefaultValue(true)]
        public bool FungalMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityPoisonSeawaterConfig")]
        [DefaultValue(true)]
        public bool PoisonSeawater;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAkatoConfig")]
        [DefaultValue(true)]
        public bool AkatoPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGodSlayerEffectsConfig")]
        [DefaultValue(true)]
        public bool GodSlayerEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityMechwormMinionConfig")]
        [DefaultValue(true)]
        public bool MechwormMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityNebulousCoreConfig")]
        [DefaultValue(true)]
        public bool NebulousCore;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityChibiiConfig")]
        [DefaultValue(true)]
        public bool ChibiiPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAuricEffectsConfig")]
        [DefaultValue(true)]
        public bool AuricEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityWaifuMinionsConfig")]
        [DefaultValue(true)]
        public bool WaifuMinions;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityShellfishMinionConfig")]
        [DefaultValue(true)]
        public bool ShellfishMinion;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityAmidiasPendantConfig")]
        [DefaultValue(true)]
        public bool AmidiasPendant;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGiantPearlConfig")]
        [DefaultValue(true)]
        public bool GiantPearl;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityDannyConfig")]
        [DefaultValue(true)]
        public bool DannyPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityBrimlingConfig")]
        [DefaultValue(true)]
        public bool BrimlingPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityTarragonEffectsConfig")]
        [DefaultValue(true)]
        public bool TarragonEffects;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityRadiatorConfig")]
        [DefaultValue(true)]
        public bool RadiatorPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityGhostBellConfig")]
        [DefaultValue(true)]
        public bool GhostBellPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFlakConfig")]
        [DefaultValue(true)]
        public bool FlakPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamityFoxConfig")]
        [DefaultValue(true)]
        public bool FoxPet;

        [Label("$Mods.FargowiltasSouls.Toggles.CalamitySirenConfig")]
        [DefaultValue(true)]
        public bool SirenPet;
    }
}