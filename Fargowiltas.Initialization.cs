using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.ModCompatibilities;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using FargowiltasSouls.Sky;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Items.Dyes;
using FargowiltasSouls.Utilities;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.Buffs.Boss;
using FargowiltasSouls.Buffs.Souls;
using FargowiltasSouls.Patreon.Gittle;
using FargowiltasSouls.Patreon.LaBonez;
using FargowiltasSouls.Patreon.DemonKing;
using FargowiltasSouls.Patreon.Catsounds;
using FargowiltasSouls.Patreon.ManliestDove;
using FargowiltasSouls.Patreon.ParadoxWolf;
using FargowiltasSouls.Patreon.Sam;
using FargowiltasSouls.Patreon.Sasha;
using FargowiltasSouls.Patreon.Daawnz;
using FargowiltasSouls.Items.Accessories.Forces;
using FargowiltasSouls.Items.Accessories.Enchantments;
using Microsoft.Xna.Framework.Design;
using FargowiltasSouls.Items.Tiles;
using FargowiltasSouls.Items;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.Items.Misc;
using FargowiltasSouls.Items.Armor;
using FargowiltasSouls.Items.Accessories.Souls;

namespace FargowiltasSouls
{
    public partial class Fargowiltas : Mod
    {
        public enum FargoMSGType : byte
        {
            ProjectileHostility,
            SyncAI
        }

        public CalamityCompatibility CalamityCompat { get; private set; }

        public ThoriumCompatibility ThoriumCompat { get; private set; }

        public SoACompatibility SoACompat { get; private set; }

        public MasomodeEXCompatibility MasomodeEXCompat { get; private set; }

        public BossChecklistCompatibility BossChecklistCompat { get; private set; }

        public BossHealthbarCompatibility BossHealthbarCompat { get; private set; }

        public FargowiltasCompatibility FargowiltasCompat { get; private set; }

        public static Fargowiltas Instance { get; private set; }

        public static readonly Dictionary<int, int> ModProjDict = new Dictionary<int, int>();

        public static List<int> DebuffIDs;
        public static ModHotKey FreezeKey;
        public static ModHotKey GoldKey;
        public static ModHotKey SmokeBombKey;
        public static ModHotKey BetsyDashKey;
        public static ModHotKey MutantBombKey;
        public UserInterface CustomResources;
        public static float ColorTimer = 0f;

        public bool LoadedNewSprites;

        public Fargowiltas()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            Instance = this;
        }

        public override void Load()
        {
            InitializeMiscVisuals();
            InitializeShaders();
            InitializeHotkeys();
            InitializeToggles();
        }

        public override void Unload()
        {
            // TODO: Static field clean-up

            DebuffIDs?.Clear();

            // Force Terraria reload Golem textures to prevent crashes.
            Main.NPCLoaded[NPCID.Golem] = false;
            Main.NPCLoaded[NPCID.GolemFistLeft] = false;
            Main.NPCLoaded[NPCID.GolemFistRight] = false;
            Main.NPCLoaded[NPCID.GolemHead] = false;
            Main.NPCLoaded[NPCID.GolemHeadFree] = false;
        }

        public override void PostSetupContent()
        {
            CalamityCompat = new CalamityCompatibility(this).TryLoad() as CalamityCompatibility;
            ThoriumCompat = new ThoriumCompatibility(this).TryLoad() as ThoriumCompatibility;
            SoACompat = new SoACompatibility(this).TryLoad() as SoACompatibility;
            MasomodeEXCompat = new MasomodeEXCompatibility(this).TryLoad() as MasomodeEXCompatibility;
            BossChecklistCompat = new BossChecklistCompatibility(this).TryLoad() as BossChecklistCompatibility;
            BossHealthbarCompat = new BossHealthbarCompatibility(this).TryLoad() as BossHealthbarCompatibility;
            FargowiltasCompat = new FargowiltasCompatibility(this).TryLoad() as FargowiltasCompatibility;

            DebuffIDs = new List<int>
                {
                    BuffID.Poisoned,
                    BuffID.Darkness,
                    BuffID.Cursed,
                    BuffID.OnFire,
                    BuffID.BrokenArmor,
                    BuffID.CursedInferno,
                    BuffID.Frostburn,
                    BuffID.Chilled,
                    BuffID.Frozen,
                    BuffID.Burning,
                    BuffID.Suffocation,
                    BuffID.Ichor,
                    BuffID.Venom,
                    BuffID.Blackout,
                    BuffID.ChaosState,
                    BuffID.ManaSickness,
                    BuffID.Wet,
                    BuffID.Slimed,
                    BuffID.Electrified,
                    BuffID.MoonLeech,
                    BuffID.Rabies, // Feral Bite
                    BuffID.Webbed,
                    BuffID.Stoned,
                    BuffID.Dazed,
                    BuffID.Obstructed,
                    BuffID.VortexDebuff,
                    BuffID.WitheredArmor,
                    BuffID.WitheredWeapon,
                    BuffID.OgreSpit,
                    BuffID.NoBuilding,
                    ModContent.BuffType<Antisocial>(),
                    ModContent.BuffType<Atrophied>(),
                    ModContent.BuffType<Berserked>(),
                    ModContent.BuffType<Bloodthirsty>(),
                    ModContent.BuffType<ClippedWings>(),
                    ModContent.BuffType<Crippled>(),
                    ModContent.BuffType<CurseoftheMoon>(),
                    ModContent.BuffType<Defenseless>(),
                    ModContent.BuffType<FlamesoftheUniverse>(),
                    ModContent.BuffType<Flipped>(),
                    ModContent.BuffType<FlippedHallow>(),
                    ModContent.BuffType<Fused>(),
                    ModContent.BuffType<GodEater>(),
                    ModContent.BuffType<Guilty>(),
                    ModContent.BuffType<Hexed>(),
                    ModContent.BuffType<Infested>(),
                    ModContent.BuffType<IvyVenom>(),
                    ModContent.BuffType<Jammed>(),
                    ModContent.BuffType<Lethargic>(),
                    ModContent.BuffType<LightningRod>(),
                    ModContent.BuffType<LivingWasteland>(),
                    ModContent.BuffType<Lovestruck>(),
                    ModContent.BuffType<MarkedforDeath>(),
                    ModContent.BuffType<Midas>(),
                    ModContent.BuffType<MutantNibble>(),
                    ModContent.BuffType<NullificationCurse>(),
                    ModContent.BuffType<Oiled>(),
                    ModContent.BuffType<OceanicMaul>(),
                    ModContent.BuffType<Purified>(),
                    ModContent.BuffType<Recovering>(),
                    ModContent.BuffType<ReverseManaFlow>(),
                    ModContent.BuffType<Rotting>(),
                    ModContent.BuffType<Shadowflame>(),
                    ModContent.BuffType<Buffs.Masomode.SqueakyToy>(),
                    ModContent.BuffType<Stunned>(),
                    ModContent.BuffType<Swarming>(),
                    ModContent.BuffType<Unstable>(),
                    ModContent.BuffType<MutantFang>(),
                    ModContent.BuffType<MutantPresence>(),
                    ModContent.BuffType<TimeFrozen>()
                };
        }

        public void InitializeMiscVisuals()
        {
            SkyManager.Instance["FargowiltasSouls:AbomBoss"] = new AbomSky();
            SkyManager.Instance["FargowiltasSouls:MutantBoss"] = new MutantSky();
            SkyManager.Instance["FargowiltasSouls:MutantBoss2"] = new MutantSky2();
            Filters.Scene["FargowiltasSouls:TimeStop"] = new Filter(new TimeStopShader("FilterMiniTower").UseColor(0.2f, 0.2f, 0.2f).UseOpacity(0.7f), EffectPriority.VeryHigh);
        }

        public void InitializeShaders()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                // Load refs for shader loading.
                Ref<Effect> lifeChampShader = new Ref<Effect>(GetEffect("Effects/LifeChampionShader"));
                Ref<Effect> willChampShader = new Ref<Effect>(GetEffect("Effects/WillChampionShader"));
                Ref<Effect> gaiaShader = new Ref<Effect>(GetEffect("Effects/GaiaShader"));
                Ref<Effect> textShader = new Ref<Effect>(GetEffect("Effects/TextShader"));

                // Properly load shaders with the use of refs.
                // BindShader binds an armor shader to a specific dye.
                GameShaders.Misc["LCWingShader"] = new MiscShaderData(lifeChampShader, "LCWings");
                GameShaders.Armor.BindShader(ModContent.ItemType<LifeDye>(), new ArmorShaderData(lifeChampShader, "LCArmor").UseColor(new Color(1f, 0.647f, 0.839f)).UseSecondaryColor(Color.Goldenrod));

                GameShaders.Misc["WCWingShader"] = new MiscShaderData(willChampShader, "WCWings");
                GameShaders.Armor.BindShader(ModContent.ItemType<WillDye>(), new ArmorShaderData(willChampShader, "WCArmor").UseColor(Color.DarkOrchid).UseSecondaryColor(Color.LightPink).UseImage("Images/Misc/Noise"));

                GameShaders.Misc["GaiaShader"] = new MiscShaderData(gaiaShader, "GaiaGlow");
                GameShaders.Armor.BindShader(ModContent.ItemType<GaiaDye>(), new ArmorShaderData(gaiaShader, "GaiaArmor").UseColor(new Color(0.44f, 1, 0.09f)).UseSecondaryColor(new Color(0.5f, 1f, 0.9f)));

                // Properly load shaders made for text usage
                GameShaders.Misc["PulseUpwards"] = new MiscShaderData(textShader, "PulseUpwards");
                GameShaders.Misc["PulseDiagonal"] = new MiscShaderData(textShader, "PulseDiagonal");
                GameShaders.Misc["PulseCircle"] = new MiscShaderData(textShader, "PulseCircle");
            }
        }

        public void InitializeHotkeys()
        {
            FreezeKey = RegisterHotKey(FargoUtilities.GetFargoTranslation("FreezeTime"), "P");
            GoldKey = RegisterHotKey(FargoUtilities.GetFargoTranslation("TurnGold"), "O");
            SmokeBombKey = RegisterHotKey(FargoUtilities.GetFargoTranslation("ThrowSmokeBomb"), "I");
            BetsyDashKey = RegisterHotKey(FargoUtilities.GetFargoTranslation("FireballDash"), "C");
            MutantBombKey = RegisterHotKey(FargoUtilities.GetFargoTranslation("MutantBomb"), "Z");
        }

        public void InitializeToggles()
        {
            ColorConverter hexToRGBConverter = new ColorConverter();

            InitializeEnchantToggles(hexToRGBConverter);
            InitializeEModeToggles();
            InitializeSoulToggles();
            InitializePetToggles();
        }

        public void InitializeEnchantToggles(ColorConverter hexToRGB)
        {
            FargoUtilities.AddToggle("PatreonHeader", ModContent.ItemType<RoombaPet>(), this);
            FargoUtilities.AddToggle("PatreonRoomba", ModContent.ItemType<RoombaPet>(), this);
            FargoUtilities.AddToggle("PatreonOrb", ModContent.ItemType<ComputationOrb>(), this);
            FargoUtilities.AddToggle("PatreonFishingRod", ModContent.ItemType<MissDrakovisFishingPole>(), this);
            FargoUtilities.AddToggle("PatreonDoor", ModContent.ItemType<SquidwardDoor>(), this);
            FargoUtilities.AddToggle("PatreonWolf", ModContent.ItemType<ParadoxWolfSoul>(), this);
            FargoUtilities.AddToggle("PatreonDove", ModContent.ItemType<FigBranch>(), this);
            FargoUtilities.AddToggle("PatreonKingSlime", ModContent.ItemType<MedallionoftheFallenKing>(), this);
            FargoUtilities.AddToggle("PatreonFishron", ModContent.ItemType<StaffOfUnleashedOcean>(), this);
            FargoUtilities.AddToggle("PatreonPlant", ModContent.ItemType<PiranhaPlantVoodooDoll>(), this);

            FargoUtilities.AddToggle("WoodHeader", ModContent.ItemType<TimberForce>(), this);
            FargoUtilities.AddToggle("BorealConfig", ModContent.ItemType<BorealWoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("8B7464"));
            FargoUtilities.AddToggle("MahoganyConfig", ModContent.ItemType<RichMahoganyEnchant>(), this, (Color)hexToRGB.ConvertFrom("b56c64"));
            FargoUtilities.AddToggle("EbonConfig", ModContent.ItemType<EbonwoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("645a8d"));
            FargoUtilities.AddToggle("ShadeConfig", ModContent.ItemType<ShadewoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("586876"));
            FargoUtilities.AddToggle("ShadeOnHitConfig", ModContent.ItemType<ShadewoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("586876"));
            FargoUtilities.AddToggle("PalmConfig", ModContent.ItemType<PalmWoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("b78d56"));
            FargoUtilities.AddToggle("PearlConfig", ModContent.ItemType<PearlwoodEnchant>(), this, (Color)hexToRGB.ConvertFrom("ad9a5f"));

            FargoUtilities.AddToggle("EarthHeader", ModContent.ItemType<EarthForce>(), this);
            FargoUtilities.AddToggle("AdamantiteConfig", ModContent.ItemType<AdamantiteEnchant>(), this, (Color)hexToRGB.ConvertFrom("dd557d"));
            FargoUtilities.AddToggle("CobaltConfig", ModContent.ItemType<CobaltEnchant>(), this, (Color)hexToRGB.ConvertFrom("3da4c4"));
            FargoUtilities.AddToggle("AncientCobaltConfig", ModContent.ItemType<AncientCobaltEnchant>(), this, (Color)hexToRGB.ConvertFrom("354c74"));
            FargoUtilities.AddToggle("MythrilConfig", ModContent.ItemType<MythrilEnchant>(), this, (Color)hexToRGB.ConvertFrom("9dd290"));
            FargoUtilities.AddToggle("OrichalcumConfig", ModContent.ItemType<OrichalcumEnchant>(), this, (Color)hexToRGB.ConvertFrom("eb3291"));
            FargoUtilities.AddToggle("PalladiumConfig", ModContent.ItemType<PalladiumEnchant>(), this, (Color)hexToRGB.ConvertFrom("f5ac28"));
            FargoUtilities.AddToggle("PalladiumOrbConfig", ModContent.ItemType<PalladiumEnchant>(), this, (Color)hexToRGB.ConvertFrom("f5ac28"));
            FargoUtilities.AddToggle("TitaniumConfig", ModContent.ItemType<TitaniumEnchant>(), this, (Color)hexToRGB.ConvertFrom("828c88"));

            FargoUtilities.AddToggle("TerraHeader", ModContent.ItemType<TerraForce>(), this);
            FargoUtilities.AddToggle("CopperConfig", ModContent.ItemType<CopperEnchant>(), this, (Color)hexToRGB.ConvertFrom("d56617"));
            FargoUtilities.AddToggle("IronMConfig", ModContent.ItemType<IronEnchant>(), this, (Color)hexToRGB.ConvertFrom("988e83"));
            FargoUtilities.AddToggle("IronSConfig", ModContent.ItemType<IronEnchant>(), this, (Color)hexToRGB.ConvertFrom("988e83"));
            FargoUtilities.AddToggle("CthulhuShield", ModContent.ItemType<IronEnchant>(), this, (Color)hexToRGB.ConvertFrom("988e83"));
            FargoUtilities.AddToggle("TinConfig", ModContent.ItemType<TinEnchant>(), this, (Color)hexToRGB.ConvertFrom("a28b4e"));
            FargoUtilities.AddToggle("TungstenConfig", ModContent.ItemType<TungstenEnchant>(), this, (Color)hexToRGB.ConvertFrom("b0d2b2"));
            FargoUtilities.AddToggle("TungstenProjConfig", ModContent.ItemType<TungstenEnchant>(), this, (Color)hexToRGB.ConvertFrom("b0d2b2"));
            FargoUtilities.AddToggle("ObsidianConfig", ModContent.ItemType<ObsidianEnchant>(), this, (Color)hexToRGB.ConvertFrom("453e73"));

            FargoUtilities.AddToggle("WillHeader", ModContent.ItemType<WillForce>(), this);
            FargoUtilities.AddToggle("GladiatorConfig", ModContent.ItemType<GladiatorEnchant>(), this, (Color)hexToRGB.ConvertFrom("9c924e"));
            FargoUtilities.AddToggle("GoldConfig", ModContent.ItemType<GoldEnchant>(), this, (Color)hexToRGB.ConvertFrom("e7b21c"));
            FargoUtilities.AddToggle("HuntressConfig", ModContent.ItemType<HuntressEnchant>(), this, (Color)hexToRGB.ConvertFrom("7ac04c"));
            FargoUtilities.AddToggle("ValhallaConfig", ModContent.ItemType<ValhallaKnightEnchant>(), this, (Color)hexToRGB.ConvertFrom("93651e"));
            FargoUtilities.AddToggle("SquirePanicConfig", ModContent.ItemType<SquireEnchant>(), this, (Color)hexToRGB.ConvertFrom("948f8c"));

            FargoUtilities.AddToggle("LifeHeader", ModContent.ItemType<LifeForce>(), this);
            FargoUtilities.AddToggle("BeeConfig", ModContent.ItemType<BeeEnchant>(), this, (Color)hexToRGB.ConvertFrom("FEF625"));
            FargoUtilities.AddToggle("BeetleConfig", ModContent.ItemType<BeetleEnchant>(), this, (Color)hexToRGB.ConvertFrom("6D5C85"));
            FargoUtilities.AddToggle("CactusConfig", ModContent.ItemType<CactusEnchant>(), this, (Color)hexToRGB.ConvertFrom("799e1d"));
            FargoUtilities.AddToggle("PumpkinConfig", ModContent.ItemType<PumpkinEnchant>(), this, (Color)hexToRGB.ConvertFrom("e3651c"));
            FargoUtilities.AddToggle("SpiderConfig", ModContent.ItemType<SpiderEnchant>(), this, (Color)hexToRGB.ConvertFrom("6d4e45"));
            FargoUtilities.AddToggle("TurtleConfig", ModContent.ItemType<TurtleEnchant>(), this, (Color)hexToRGB.ConvertFrom("f89c5c"));

            FargoUtilities.AddToggle("NatureHeader", ModContent.ItemType<NatureForce>(), this);
            FargoUtilities.AddToggle("ChlorophyteConfig", ModContent.ItemType<ChlorophyteEnchant>(), this, (Color)hexToRGB.ConvertFrom("248900"));
            FargoUtilities.AddToggle("ChlorophyteFlowerConfig", ModContent.ItemType<ChlorophyteEnchant>(), this, (Color)hexToRGB.ConvertFrom("248900"));
            FargoUtilities.AddToggle("CrimsonConfig", ModContent.ItemType<CrimsonEnchant>(), this, (Color)hexToRGB.ConvertFrom("C8364B"));
            FargoUtilities.AddToggle("RainConfig", ModContent.ItemType<RainEnchant>(), this, (Color)hexToRGB.ConvertFrom("ffec00"));
            FargoUtilities.AddToggle("FrostConfig", ModContent.ItemType<FrostEnchant>(), this, (Color)hexToRGB.ConvertFrom("7abdb9"));
            FargoUtilities.AddToggle("SnowConfig", ModContent.ItemType<SnowEnchant>(), this, (Color)hexToRGB.ConvertFrom("25c3f2"));
            FargoUtilities.AddToggle("JungleConfig", ModContent.ItemType<JungleEnchant>(), this, (Color)hexToRGB.ConvertFrom("71971f"));
            FargoUtilities.AddToggle("CordageConfig", ModContent.ItemType<JungleEnchant>(), this, (Color)hexToRGB.ConvertFrom("71971f"));
            FargoUtilities.AddToggle("MoltenConfig", ModContent.ItemType<MoltenEnchant>(), this, (Color)hexToRGB.ConvertFrom("c12b2b"));
            FargoUtilities.AddToggle("MoltenEConfig", ModContent.ItemType<MoltenEnchant>(), this, (Color)hexToRGB.ConvertFrom("c12b2b"));
            FargoUtilities.AddToggle("ShroomiteConfig", ModContent.ItemType<ShroomiteEnchant>(), this, (Color)hexToRGB.ConvertFrom("008cf4"));
            FargoUtilities.AddToggle("ShroomiteShroomConfig", ModContent.ItemType<ShroomiteEnchant>(), this, (Color)hexToRGB.ConvertFrom("008cf4"));

            FargoUtilities.AddToggle("ShadowHeader", ModContent.ItemType<ShadowForce>(), this);
            FargoUtilities.AddToggle("DarkArtConfig", ModContent.ItemType<DarkArtistEnchant>(), this, (Color)hexToRGB.ConvertFrom("9b5cb0"));
            FargoUtilities.AddToggle("ApprenticeConfig", ModContent.ItemType<ApprenticeEnchant>(), this, (Color)hexToRGB.ConvertFrom("5d86a6"));
            FargoUtilities.AddToggle("NecroConfig", ModContent.ItemType<NecroEnchant>(), this, (Color)hexToRGB.ConvertFrom("565643"));
            FargoUtilities.AddToggle("ShadowConfig", ModContent.ItemType<ShadowEnchant>(), this, (Color)hexToRGB.ConvertFrom("42356f"));
            FargoUtilities.AddToggle("AncientShadowConfig", ModContent.ItemType<AncientShadowEnchant>(), this, (Color)hexToRGB.ConvertFrom("42356f"));
            FargoUtilities.AddToggle("MonkConfig", ModContent.ItemType<MonkEnchant>(), this, (Color)hexToRGB.ConvertFrom("920520"));
            FargoUtilities.AddToggle("ShinobiConfig", ModContent.ItemType<ShinobiEnchant>(), this, (Color)hexToRGB.ConvertFrom("935b18"));
            FargoUtilities.AddToggle("ShinobiTabiConfig", ModContent.ItemType<ShinobiEnchant>(), this, (Color)hexToRGB.ConvertFrom("935b18"));
            FargoUtilities.AddToggle("ShinobiClimbingConfig", ModContent.ItemType<ShinobiEnchant>(), this, (Color)hexToRGB.ConvertFrom("935b18"));
            FargoUtilities.AddToggle("SpookyConfig", ModContent.ItemType<SpookyEnchant>(), this, (Color)hexToRGB.ConvertFrom("644e74"));

            FargoUtilities.AddToggle("SpiritHeader", ModContent.ItemType<SpiritForce>(), this);
            FargoUtilities.AddToggle("ForbiddenConfig", ModContent.ItemType<ForbiddenEnchant>(), this, (Color)hexToRGB.ConvertFrom("e7b21c"));
            FargoUtilities.AddToggle("HallowedConfig", ModContent.ItemType<HallowEnchant>(), this, (Color)hexToRGB.ConvertFrom("968564"));
            FargoUtilities.AddToggle("HallowSConfig", ModContent.ItemType<HallowEnchant>(), this, (Color)hexToRGB.ConvertFrom("968564"));
            FargoUtilities.AddToggle("SilverConfig", ModContent.ItemType<SilverEnchant>(), this, (Color)hexToRGB.ConvertFrom("b4b4cc"));
            FargoUtilities.AddToggle("SpectreConfig", ModContent.ItemType<SpectreEnchant>(), this, (Color)hexToRGB.ConvertFrom("accdfc"));
            FargoUtilities.AddToggle("TikiConfig", ModContent.ItemType<TikiEnchant>(), this, (Color)hexToRGB.ConvertFrom("56A52B"));

            FargoUtilities.AddToggle("CosmoHeader", ModContent.ItemType<CosmoForce>(), this);
            FargoUtilities.AddToggle("MeteorConfig", ModContent.ItemType<MeteorEnchant>(), this, (Color)hexToRGB.ConvertFrom("5f4752"));
            FargoUtilities.AddToggle("NebulaConfig", ModContent.ItemType<NebulaEnchant>(), this, (Color)hexToRGB.ConvertFrom("fe7ee5"));
            FargoUtilities.AddToggle("SolarConfig", ModContent.ItemType<SolarEnchant>(), this, (Color)hexToRGB.ConvertFrom("fe9e23"));
            FargoUtilities.AddToggle("SolarFlareConfig", ModContent.ItemType<SolarEnchant>(), this, (Color)hexToRGB.ConvertFrom("fe9e23"));
            FargoUtilities.AddToggle("StardustConfig", ModContent.ItemType<StardustEnchant>(), this, (Color)hexToRGB.ConvertFrom("00aeee"));
            FargoUtilities.AddToggle("VortexSConfig", ModContent.ItemType<VortexEnchant>(), this, (Color)hexToRGB.ConvertFrom("00f2aa"));
            FargoUtilities.AddToggle("VortexVConfig", ModContent.ItemType<VortexEnchant>(), this, (Color)hexToRGB.ConvertFrom("00f2aa"));
        }

        public void InitializeEModeToggles()
        {
            // EMode header
            FargoUtilities.AddToggle("MasoHeader", ModContent.ItemType<MutantStatue>(), this);
            FargoUtilities.AddToggle("MasoBossRecolors", ModContent.ItemType<Masochist>(), this);
            FargoUtilities.AddToggle("MasoIconConfig", ModContent.ItemType<SinisterIcon>(), this);
            FargoUtilities.AddToggle("MasoIconDropsConfig", ModContent.ItemType<SinisterIcon>(), this);
            FargoUtilities.AddToggle("MasoGrazeConfig", ModContent.ItemType<SparklingAdoration>(), this);
            FargoUtilities.AddToggle("MasoDevianttHeartsConfig", ModContent.ItemType<SparklingAdoration>(), this);

            // Supreme Death Fairy
            FargoUtilities.AddToggle("SupremeFairyHeader", ModContent.ItemType<SupremeDeathbringerFairy>(), this);
            FargoUtilities.AddToggle("MasoSlimeConfig", ModContent.ItemType<SlimyShield>(), this);
            FargoUtilities.AddToggle("SlimeFallingConfig", ModContent.ItemType<SlimyShield>(), this);
            FargoUtilities.AddToggle("MasoEyeConfig", ModContent.ItemType<AgitatingLens>(), this);
            FargoUtilities.AddToggle("MasoHoneyConfig", ModContent.ItemType<QueenStinger>(), this);
            FargoUtilities.AddToggle("MasoSkeleConfig", ModContent.ItemType<NecromanticBrew>(), this);

            // Bionomic
            FargoUtilities.AddToggle("BionomicHeader", ModContent.ItemType<BionomicCluster>(), this);
            FargoUtilities.AddToggle("MasoConcoctionConfig", ModContent.ItemType<TimsConcoction>(), this);
            FargoUtilities.AddToggle("MasoCarrotConfig", ModContent.ItemType<OrdinaryCarrot>(), this);
            FargoUtilities.AddToggle("MasoRainbowConfig", ModContent.ItemType<ConcentratedRainbowMatter>(), this);
            FargoUtilities.AddToggle("MasoFrigidConfig", ModContent.ItemType<FrigidGemstone>(), this);
            FargoUtilities.AddToggle("MasoNymphConfig", ModContent.ItemType<NymphsPerfume>(), this);
            FargoUtilities.AddToggle("MasoSqueakConfig", ModContent.ItemType<Items.Accessories.Masomode.SqueakyToy>(), this);
            FargoUtilities.AddToggle("MasoPouchConfig", ModContent.ItemType<WretchedPouch>(), this);
            FargoUtilities.AddToggle("MasoClippedConfig", ModContent.ItemType<WyvernFeather>(), this);
            FargoUtilities.AddToggle("TribalCharmConfig", ModContent.ItemType<TribalCharm>(), this);

            // Dubious
            FargoUtilities.AddToggle("DubiousHeader", ModContent.ItemType<DubiousCircuitry>(), this);
            FargoUtilities.AddToggle("MasoLightningConfig", ModContent.ItemType<GroundStick>(), this);
            FargoUtilities.AddToggle("MasoProbeConfig", ModContent.ItemType<GroundStick>(), this);

            // Pure Heart
            FargoUtilities.AddToggle("PureHeartHeader", ModContent.ItemType<PureHeart>(), this);
            FargoUtilities.AddToggle("MasoEaterConfig", ModContent.ItemType<CorruptHeart>(), this);
            FargoUtilities.AddToggle("MasoBrainConfig", ModContent.ItemType<GuttedHeart>(), this);

            // Lump of Flesh
            FargoUtilities.AddToggle("LumpofFleshHeader", ModContent.ItemType<LumpOfFlesh>(), this);
            FargoUtilities.AddToggle("MasoPugentConfig", ModContent.ItemType<LumpOfFlesh>(), this);

            // Chalice
            FargoUtilities.AddToggle("ChaliceHeader", ModContent.ItemType<ChaliceoftheMoon>(), this);
            FargoUtilities.AddToggle("MasoCultistConfig", ModContent.ItemType<ChaliceoftheMoon>(), this);
            FargoUtilities.AddToggle("MasoPlantConfig", ModContent.ItemType<MagicalBulb>(), this);
            FargoUtilities.AddToggle("MasoGolemConfig", ModContent.ItemType<LihzahrdTreasureBox>(), this);
            FargoUtilities.AddToggle("MasoBoulderConfig", ModContent.ItemType<LihzahrdTreasureBox>(), this);
            FargoUtilities.AddToggle("MasoCelestConfig", ModContent.ItemType<CelestialRune>(), this);
            FargoUtilities.AddToggle("MasoVisionConfig", ModContent.ItemType<CelestialRune>(), this);

            // Heart of the Masochist
            FargoUtilities.AddToggle("HeartHeader", ModContent.ItemType<HeartoftheMasochist>(), this);
            FargoUtilities.AddToggle("MasoPump", ModContent.ItemType<PumpkingsCape>(), this);
            FargoUtilities.AddToggle("MasoFlockoConfig", ModContent.ItemType<IceQueensCrown>(), this);
            FargoUtilities.AddToggle("MasoUfoConfig", ModContent.ItemType<SaucerControlConsole>(), this);
            FargoUtilities.AddToggle("MasoGravConfig", ModContent.ItemType<GalacticGlobe>(), this);
            FargoUtilities.AddToggle("MasoGrav2Config", ModContent.ItemType<GalacticGlobe>(), this);
            FargoUtilities.AddToggle("MasoTrueEyeConfig", ModContent.ItemType<GalacticGlobe>(), this);

            // Cyclonic Fin
            FargoUtilities.AddToggle("CyclonicHeader", ModContent.ItemType<CyclonicFin>(), this);
            FargoUtilities.AddToggle("MasoFishronConfig", ModContent.ItemType<CyclonicFin>(), this);

            // Mutant armor
            FargoUtilities.AddToggle("MutantArmorHeader", ModContent.ItemType<HeartoftheMasochist>(), this);
            FargoUtilities.AddToggle("MasoAbomConfig", ModContent.ItemType<MutantMask>(), this);
            FargoUtilities.AddToggle("MasoRingConfig", ModContent.ItemType<MutantMask>(), this);
            FargoUtilities.AddToggle("MasoReviveDeathrayConfig", ModContent.ItemType<MutantMask>(), this);
        }

        public void InitializeSoulToggles()
        {
            FargoUtilities.AddToggle("SoulHeader", ModContent.ItemType<UniverseSoul>(), this);
            FargoUtilities.AddToggle("MeleeConfig", ModContent.ItemType<GladiatorsSoul>(), this);
            FargoUtilities.AddToggle("MagmaStoneConfig", ModContent.ItemType<GladiatorsSoul>(), this);
            FargoUtilities.AddToggle("YoyoBagConfig", ModContent.ItemType<GladiatorsSoul>(), this);
            FargoUtilities.AddToggle("SniperConfig", ModContent.ItemType<SnipersSoul>(), this);
            FargoUtilities.AddToggle("UniverseConfig", ModContent.ItemType<UniverseSoul>(), this);
            FargoUtilities.AddToggle("MiningHuntConfig", ModContent.ItemType<MinerEnchant>(), this);
            FargoUtilities.AddToggle("MiningDangerConfig", ModContent.ItemType<MinerEnchant>(), this);
            FargoUtilities.AddToggle("MiningSpelunkConfig", ModContent.ItemType<MinerEnchant>(), this);
            FargoUtilities.AddToggle("MiningShineConfig", ModContent.ItemType<MinerEnchant>(), this);
            FargoUtilities.AddToggle("BuilderConfig", ModContent.ItemType<WorldShaperSoul>(), this);
            FargoUtilities.AddToggle("DefenseSporeConfig", ModContent.ItemType<ColossusSoul>(), this);
            FargoUtilities.AddToggle("DefenseStarConfig", ModContent.ItemType<ColossusSoul>(), this);
            FargoUtilities.AddToggle("DefenseBeeConfig", ModContent.ItemType<ColossusSoul>(), this);
            FargoUtilities.AddToggle("DefensePanicConfig", ModContent.ItemType<ColossusSoul>(), this);
            FargoUtilities.AddToggle("RunSpeedConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("MomentumConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("SupersonicConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("SupersonicJumpsConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("SupersonicRocketBootsConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("SupersonicCarpetConfig", ModContent.ItemType<SupersonicSoul>(), this);
            FargoUtilities.AddToggle("TrawlerConfig", ModContent.ItemType<TrawlerSoul>(), this);
            FargoUtilities.AddToggle("EternityConfig", ModContent.ItemType<EternitySoul>(), this);
        }

        public void InitializePetToggles()
        {
            FargoUtilities.AddToggle("PetHeader", ItemID.ZephyrFish, this);
            FargoUtilities.AddToggle("PetCatConfig", ItemID.UnluckyYarn, this);
            FargoUtilities.AddToggle("PetCubeConfig", ItemID.CompanionCube, this);
            FargoUtilities.AddToggle("PetCurseSapConfig", ItemID.CursedSapling, this);
            FargoUtilities.AddToggle("PetDinoConfig", ItemID.AmberMosquito, this);
            FargoUtilities.AddToggle("PetDragonConfig", ItemID.DD2PetDragon, this);
            FargoUtilities.AddToggle("PetEaterConfig", ItemID.EatersBone, this);
            FargoUtilities.AddToggle("PetEyeSpringConfig", ItemID.EyeSpring, this);
            FargoUtilities.AddToggle("PetFaceMonsterConfig", ItemID.BoneRattle, this);
            FargoUtilities.AddToggle("PetGatoConfig", ItemID.DD2PetGato, this);
            FargoUtilities.AddToggle("PetHornetConfig", ItemID.Nectar, this);
            FargoUtilities.AddToggle("PetLizardConfig", ItemID.LizardEgg, this);
            FargoUtilities.AddToggle("PetMinitaurConfig", ItemID.TartarSauce, this);
            FargoUtilities.AddToggle("PetParrotConfig", ItemID.ParrotCracker, this);
            FargoUtilities.AddToggle("PetPenguinConfig", ItemID.Fish, this);
            FargoUtilities.AddToggle("PetPupConfig", ItemID.DogWhistle, this);
            FargoUtilities.AddToggle("PetSeedConfig", ItemID.Seedling, this);
            FargoUtilities.AddToggle("PetDGConfig", ItemID.BoneKey, this);
            FargoUtilities.AddToggle("PetSnowmanConfig", ItemID.ToySled, this);
            FargoUtilities.AddToggle("PetGrinchConfig", ItemID.BabyGrinchMischiefWhistle, this);
            FargoUtilities.AddToggle("PetSpiderConfig", ItemID.SpiderEgg, this);
            FargoUtilities.AddToggle("PetSquashConfig", ItemID.MagicalPumpkinSeed, this);
            FargoUtilities.AddToggle("PetTikiConfig", ItemID.TikiTotem, this);
            FargoUtilities.AddToggle("PetShroomConfig", ItemID.StrangeGlowingMushroom, this);
            FargoUtilities.AddToggle("PetTurtleConfig", ItemID.Seaweed, this);
            FargoUtilities.AddToggle("PetZephyrConfig", ItemID.ZephyrFish, this);
            FargoUtilities.AddToggle("PetHeartConfig", ItemID.CrimsonHeart, this);
            FargoUtilities.AddToggle("PetNaviConfig", ItemID.FairyBell, this);
            FargoUtilities.AddToggle("PetFlickerConfig", ItemID.DD2PetGhost, this);
            FargoUtilities.AddToggle("PetLanturnConfig", ItemID.MagicLantern, this);
            FargoUtilities.AddToggle("PetOrbConfig", ItemID.ShadowOrb, this);
            FargoUtilities.AddToggle("PetSuspEyeConfig", ItemID.SuspiciousLookingTentacle, this);
            FargoUtilities.AddToggle("PetWispConfig", ItemID.WispinaBottle, this);
        }
    }
}