using FargowiltasSouls.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using FargowiltasSouls.ModCompatibilities;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.NPCs.AbomBoss;
using FargowiltasSouls.NPCs.Champions;
using FargowiltasSouls.NPCs.DeviBoss;
using FargowiltasSouls.NPCs.MutantBoss;
using FargowiltasSouls.NPCs.EternityMode;
using FargowiltasSouls.Sky;
using Fargowiltas.Items.Summons.Deviantt;
using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Explosives;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Items.Dyes;
using FargowiltasSouls.Utilities;
using FargowiltasSouls.Items;
using FargowiltasSouls.Items.Accessories.Forces;
using FargowiltasSouls.Items.Accessories.Enchantments;
using FargowiltasSouls.Items.Tiles;
using FargowiltasSouls.Items.Misc;
using FargowiltasSouls.Items.Armor;
using FargowiltasSouls.Items.Accessories.Souls;
using FargowiltasSouls.Patreon.Gittle;
using FargowiltasSouls.Patreon.Daawnz;
using FargowiltasSouls.Patreon.Sasha;
using FargowiltasSouls.Patreon.Sam;
using FargowiltasSouls.Patreon.ParadoxWolf;
using FargowiltasSouls.Patreon.ManliestDove;
using FargowiltasSouls.Patreon.Catsounds;
using FargowiltasSouls.Patreon.DemonKing;
using FargowiltasSouls.Patreon.LaBonez;

namespace FargowiltasSouls
{
    public class Fargowiltas : Mod
    {
        internal static ModHotKey FreezeKey;
        internal static ModHotKey GoldKey;
        internal static ModHotKey SmokeBombKey;
        internal static ModHotKey BetsyDashKey;
        internal static ModHotKey MutantBombKey;

        internal static List<int> DebuffIDs;

        internal static Fargowiltas Instance;

        internal bool LoadedNewSprites;

        public UserInterface CustomResources;

        internal static readonly Dictionary<int, int> ModProjDict = new Dictionary<int, int>();

        #region Compatibilities

        public CalamityCompatibility CalamityCompatibility { get; private set; }
        public bool CalamityLoaded => CalamityCompatibility != null;

        public ThoriumCompatibility ThoriumCompatibility { get; private set; }
        public bool ThoriumLoaded => ThoriumCompatibility != null;

        public SoACompatibility SoACompatibility { get; private set; }
        public bool SoALoaded => SoACompatibility != null;

        public MasomodeEXCompatibility MasomodeEXCompatibility { get; private set; }
        public bool MasomodeEXLoaded => MasomodeEXCompatibility != null;

        public BossChecklistCompatibility BossChecklistCompatibility { get; private set; }
        public bool BossChecklistLoaded => BossChecklistCompatibility != null;

        #endregion Compatibilities

        public Fargowiltas()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            Instance = this;

            SkyManager.Instance["FargowiltasSouls:AbomBoss"] = new AbomSky();
            SkyManager.Instance["FargowiltasSouls:MutantBoss"] = new MutantSky();
            SkyManager.Instance["FargowiltasSouls:MutantBoss2"] = new MutantSky2();
            Filters.Scene["FargowiltasSouls:TimeStop"] = new Filter(
                new TimeStopShader("FilterMiniTower").UseColor(0.2f, 0.2f, 0.2f).UseOpacity(0.7f), EffectPriority.VeryHigh);

            FreezeKey = RegisterHotKey(FargoLangHelper.GetHotkeyText("FreezeKey"), "P");
            GoldKey = RegisterHotKey(FargoLangHelper.GetHotkeyText("GoldKey"), "O");
            SmokeBombKey = RegisterHotKey(FargoLangHelper.GetHotkeyText("SmokeBombKey"), "I");
            BetsyDashKey = RegisterHotKey(FargoLangHelper.GetHotkeyText("BetsyDashKey"), "C");
            MutantBombKey = RegisterHotKey(FargoLangHelper.GetHotkeyText("MutantBombKey"), "Z");

            #region Toggles

            AddToggle("PresetHeader", ModContent.ItemType<Masochist>());

            #region enchants

            AddToggle("WoodHeader", ModContent.ItemType<TimberForce>());
            AddToggle("BorealConfig", ModContent.ItemType<BorealWoodEnchant>(), "8B7464");
            AddToggle("MahoganyConfig", ModContent.ItemType<RichMahoganyEnchant>(), "b56c64");
            AddToggle("EbonConfig", ModContent.ItemType<EbonwoodEnchant>(), "645a8d");
            AddToggle("ShadeConfig", ModContent.ItemType<ShadewoodEnchant>(), "586876");
            AddToggle("ShadeOnHitConfig", ModContent.ItemType<ShadewoodEnchant>(), "586876");
            AddToggle("PalmConfig", ModContent.ItemType<PalmWoodEnchant>(), "b78d56");
            AddToggle("PearlConfig", ModContent.ItemType<PearlwoodEnchant>(), "ad9a5f");

            AddToggle("EarthHeader", ModContent.ItemType<EarthForce>());
            AddToggle("AdamantiteConfig", ModContent.ItemType<AdamantiteEnchant>(), "dd557d");
            AddToggle("CobaltConfig", ModContent.ItemType<CobaltEnchant>(), "3da4c4");
            AddToggle("AncientCobaltConfig", ModContent.ItemType<AncientCobaltEnchant>(), "354c74");
            AddToggle("MythrilConfig", ModContent.ItemType<MythrilEnchant>(), "9dd290");
            AddToggle("OrichalcumConfig", ModContent.ItemType<OrichalcumEnchant>(), "eb3291");
            AddToggle("PalladiumConfig", ModContent.ItemType<PalladiumEnchant>(), "f5ac28");
            AddToggle("PalladiumOrbConfig", ModContent.ItemType<PalladiumEnchant>(), "f5ac28");
            AddToggle("TitaniumConfig", ModContent.ItemType<TitaniumEnchant>(), "828c88");

            AddToggle("TerraHeader", ModContent.ItemType<TerraForce>());
            AddToggle("CopperConfig", ModContent.ItemType<CopperEnchant>(), "d56617");
            AddToggle("IronMConfig", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggle("IronSConfig", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggle("CthulhuShield", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggle("TinConfig", ModContent.ItemType<TinEnchant>(), "a28b4e");
            AddToggle("TungstenConfig", ModContent.ItemType<TungstenEnchant>(), "b0d2b2");
            AddToggle("TungstenProjConfig", ModContent.ItemType<TungstenEnchant>(), "b0d2b2");
            AddToggle("ObsidianConfig", ModContent.ItemType<ObsidianEnchant>(), "453e73");

            AddToggle("WillHeader", ModContent.ItemType<WillForce>());
            AddToggle("GladiatorConfig", ModContent.ItemType<GladiatorEnchant>(), "9c924e");
            AddToggle("GoldConfig", ModContent.ItemType<GoldEnchant>(), "e7b21c");
            AddToggle("HuntressConfig", ModContent.ItemType<HuntressEnchant>(), "7ac04c");
            AddToggle("ValhallaConfig", ModContent.ItemType<ValhallaKnightEnchant>(), "93651e");
            AddToggle("SquirePanicConfig", ModContent.ItemType<SquireEnchant>(), "948f8c");

            AddToggle("LifeHeader", ModContent.ItemType<LifeForce>());
            AddToggle("BeeConfig", ModContent.ItemType<BeeEnchant>(), "FEF625");
            AddToggle("BeetleConfig", ModContent.ItemType<BeetleEnchant>(), "6D5C85");
            AddToggle("CactusConfig", ModContent.ItemType<CactusEnchant>(), "799e1d");
            AddToggle("PumpkinConfig", ModContent.ItemType<PumpkinEnchant>(), "e3651c");
            AddToggle("SpiderConfig", ModContent.ItemType<SpiderEnchant>(), "6d4e45");
            AddToggle("TurtleConfig", ModContent.ItemType<TurtleEnchant>(), "f89c5c");

            AddToggle("NatureHeader", ModContent.ItemType<NatureForce>());
            AddToggle("ChlorophyteConfig", ModContent.ItemType<ChlorophyteEnchant>(), "248900");
            AddToggle("ChlorophyteFlowerConfig", ModContent.ItemType<ChlorophyteEnchant>(), "248900");
            AddToggle("CrimsonConfig", ModContent.ItemType<CrimsonEnchant>(), "C8364B");
            AddToggle("RainConfig", ModContent.ItemType<RainEnchant>(), "ffec00");
            AddToggle("FrostConfig", ModContent.ItemType<FrostEnchant>(), "7abdb9");
            AddToggle("SnowConfig", ModContent.ItemType<SnowEnchant>(), "25c3f2");
            AddToggle("JungleConfig", ModContent.ItemType<JungleEnchant>(), "71971f");
            AddToggle("CordageConfig", ModContent.ItemType<JungleEnchant>(), "71971f");
            AddToggle("MoltenConfig", ModContent.ItemType<MoltenEnchant>(), "c12b2b");
            AddToggle("MoltenEConfig", ModContent.ItemType<MoltenEnchant>(), "c12b2b");
            AddToggle("ShroomiteConfig", ModContent.ItemType<ShroomiteEnchant>(), "008cf4");
            AddToggle("ShroomiteShroomConfig", ModContent.ItemType<ShroomiteEnchant>(), "008cf4");

            AddToggle("ShadowHeader", ModContent.ItemType<ShadowForce>());
            AddToggle("DarkArtConfig", ModContent.ItemType<DarkArtistEnchant>(), "9b5cb0");
            AddToggle("ApprenticeConfig", ModContent.ItemType<ApprenticeEnchant>(), "5d86a6");
            AddToggle("NecroConfig", ModContent.ItemType<NecroEnchant>(), "565643");
            AddToggle("ShadowConfig", ModContent.ItemType<ShadowEnchant>(), "42356f");
            AddToggle("AncientShadowConfig", ModContent.ItemType<AncientShadowEnchant>(), "42356f");
            AddToggle("MonkConfig", ModContent.ItemType<MonkEnchant>(), "920520");
            AddToggle("ShinobiConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggle("ShinobiTabiConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggle("ShinobiClimbingConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggle("SpookyConfig", ModContent.ItemType<SpookyEnchant>(), "644e74");

            AddToggle("SpiritHeader", ModContent.ItemType<SpiritForce>());
            AddToggle("ForbiddenConfig", ModContent.ItemType<ForbiddenEnchant>(), "e7b21c");
            AddToggle("HallowedConfig", ModContent.ItemType<HallowEnchant>(), "968564");
            AddToggle("HallowSConfig", ModContent.ItemType<HallowEnchant>(), "968564");
            AddToggle("SilverConfig", ModContent.ItemType<SilverEnchant>(), "b4b4cc");
            AddToggle("SpectreConfig", ModContent.ItemType<SpectreEnchant>(), "accdfc");
            AddToggle("TikiConfig", ModContent.ItemType<TikiEnchant>(), "56A52B");

            AddToggle("CosmoHeader", ModContent.ItemType<CosmoForce>());
            AddToggle("MeteorConfig", ModContent.ItemType<MeteorEnchant>(), "5f4752");
            AddToggle("NebulaConfig", ModContent.ItemType<NebulaEnchant>(), "fe7ee5");
            AddToggle("SolarConfig", ModContent.ItemType<SolarEnchant>(), "fe9e23");
            AddToggle("SolarFlareConfig", ModContent.ItemType<SolarEnchant>(), "fe9e23");
            AddToggle("StardustConfig", ModContent.ItemType<StardustEnchant>(), "00aeee");
            AddToggle("VortexSConfig", ModContent.ItemType<VortexEnchant>(), "00f2aa");
            AddToggle("VortexVConfig", ModContent.ItemType<VortexEnchant>(), "00f2aa");

            #endregion enchants

            #region masomode toggles

            //Masomode Header
            AddToggle("MasoHeader", ModContent.ItemType<MutantStatue>());
            //AddToggle("MasoBossBG", ModContent.ItemType<Masochist>());
            AddToggle("MasoBossRecolors", ModContent.ItemType<Masochist>());
            AddToggle("MasoIconConfig", ModContent.ItemType<SinisterIcon>());
            AddToggle("MasoIconDropsConfig", ModContent.ItemType<SinisterIcon>());
            AddToggle("MasoGrazeConfig", ModContent.ItemType<SparklingAdoration>());
            AddToggle("MasoDevianttHeartsConfig", ModContent.ItemType<SparklingAdoration>());

            //supreme death fairy header
            AddToggle("SupremeFairyHeader", ModContent.ItemType<SupremeDeathbringerFairy>());
            AddToggle("MasoSlimeConfig", ModContent.ItemType<SlimyShield>());
            AddToggle("SlimeFallingConfig", ModContent.ItemType<SlimyShield>());
            AddToggle("MasoEyeConfig", ModContent.ItemType<AgitatingLens>());
            AddToggle("MasoHoneyConfig", ModContent.ItemType<QueenStinger>());
            AddToggle("MasoSkeleConfig", ModContent.ItemType<NecromanticBrew>());

            //bionomic
            AddToggle("BionomicHeader", ModContent.ItemType<BionomicCluster>());
            AddToggle("MasoConcoctionConfig", ModContent.ItemType<TimsConcoction>());
            AddToggle("MasoCarrotConfig", ModContent.ItemType<OrdinaryCarrot>());
            AddToggle("MasoRainbowConfig", ModContent.ItemType<ConcentratedRainbowMatter>());
            AddToggle("MasoFrigidConfig", ModContent.ItemType<FrigidGemstone>());
            AddToggle("MasoNymphConfig", ModContent.ItemType<NymphsPerfume>());
            AddToggle("MasoSqueakConfig", ModContent.ItemType<SqueakyToy>());
            AddToggle("MasoPouchConfig", ModContent.ItemType<WretchedPouch>());
            AddToggle("MasoClippedConfig", ModContent.ItemType<WyvernFeather>());
            AddToggle("TribalCharmConfig", ModContent.ItemType<TribalCharm>());
            //AddToggle("WalletHeader", ModContent.ItemType<SecurityWallet>());

            //dubious
            AddToggle("DubiousHeader", ModContent.ItemType<DubiousCircuitry>());
            AddToggle("MasoLightningConfig", ModContent.ItemType<GroundStick>());
            AddToggle("MasoProbeConfig", ModContent.ItemType<GroundStick>());

            //pure heart
            AddToggle("PureHeartHeader", ModContent.ItemType<PureHeart>());
            AddToggle("MasoEaterConfig", ModContent.ItemType<CorruptHeart>());
            AddToggle("MasoBrainConfig", ModContent.ItemType<GuttedHeart>());

            //lump of flesh
            AddToggle("LumpofFleshHeader", ModContent.ItemType<LumpOfFlesh>());
            AddToggle("MasoPugentConfig", ModContent.ItemType<LumpOfFlesh>());

            //chalice
            AddToggle("ChaliceHeader", ModContent.ItemType<ChaliceoftheMoon>());
            AddToggle("MasoCultistConfig", ModContent.ItemType<ChaliceoftheMoon>());
            AddToggle("MasoPlantConfig", ModContent.ItemType<MagicalBulb>());
            AddToggle("MasoGolemConfig", ModContent.ItemType<LihzahrdTreasureBox>());
            AddToggle("MasoBoulderConfig", ModContent.ItemType<LihzahrdTreasureBox>());
            AddToggle("MasoCelestConfig", ModContent.ItemType<CelestialRune>());
            AddToggle("MasoVisionConfig", ModContent.ItemType<CelestialRune>());

            //heart of the masochist
            AddToggle("HeartHeader", ModContent.ItemType<HeartoftheMasochist>());
            AddToggle("MasoPump", ModContent.ItemType<PumpkingsCape>());
            AddToggle("MasoFlockoConfig", ModContent.ItemType<IceQueensCrown>());
            AddToggle("MasoUfoConfig", ModContent.ItemType<SaucerControlConsole>());
            AddToggle("MasoGravConfig", ModContent.ItemType<GalacticGlobe>());
            AddToggle("MasoGrav2Config", ModContent.ItemType<GalacticGlobe>());
            AddToggle("MasoTrueEyeConfig", ModContent.ItemType<GalacticGlobe>());

            //cyclonic fin
            AddToggle("CyclonicHeader", ModContent.ItemType<CyclonicFin>());
            AddToggle("MasoFishronConfig", ModContent.ItemType<CyclonicFin>());

            //mutant armor
            AddToggle("MutantArmorHeader", ModContent.ItemType<HeartoftheMasochist>());
            AddToggle("MasoAbomConfig", ModContent.ItemType<MutantMask>());
            AddToggle("MasoRingConfig", ModContent.ItemType<MutantMask>());
            AddToggle("MasoReviveDeathrayConfig", ModContent.ItemType<MutantMask>());

            #endregion masomode toggles

            #region soul toggles

            AddToggle("SoulHeader", ModContent.ItemType<UniverseSoul>());
            AddToggle("MeleeConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggle("MagmaStoneConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggle("YoyoBagConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggle("SniperConfig", ModContent.ItemType<SnipersSoul>());
            AddToggle("UniverseConfig", ModContent.ItemType<UniverseSoul>());
            AddToggle("MiningHuntConfig", ModContent.ItemType<MinerEnchant>());
            AddToggle("MiningDangerConfig", ModContent.ItemType<MinerEnchant>());
            AddToggle("MiningSpelunkConfig", ModContent.ItemType<MinerEnchant>());
            AddToggle("MiningShineConfig", ModContent.ItemType<MinerEnchant>());
            AddToggle("BuilderConfig", ModContent.ItemType<WorldShaperSoul>());
            AddToggle("DefenseSporeConfig", ModContent.ItemType<ColossusSoul>());
            AddToggle("DefenseStarConfig", ModContent.ItemType<ColossusSoul>());
            AddToggle("DefenseBeeConfig", ModContent.ItemType<ColossusSoul>());
            AddToggle("DefensePanicConfig", ModContent.ItemType<ColossusSoul>());
            AddToggle("RunSpeedConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("MomentumConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("SupersonicConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("SupersonicJumpsConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("SupersonicRocketBootsConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("SupersonicCarpetConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggle("TrawlerConfig", ModContent.ItemType<TrawlerSoul>());
            AddToggle("EternityConfig", ModContent.ItemType<EternitySoul>());

            #endregion soul toggles

            #region pet toggles

            AddToggle("PetHeader", 2420);
            AddToggle("PetCatConfig", 1810);
            AddToggle("PetCubeConfig", 3628);
            AddToggle("PetCurseSapConfig", 1837);
            AddToggle("PetDinoConfig", 1242);
            AddToggle("PetDragonConfig", 3857);
            AddToggle("PetEaterConfig", 994);
            AddToggle("PetEyeSpringConfig", 1311);
            AddToggle("PetFaceMonsterConfig", 3060);
            AddToggle("PetGatoConfig", 3855);
            AddToggle("PetHornetConfig", 1170);
            AddToggle("PetLizardConfig", 1172);
            AddToggle("PetMinitaurConfig", 2587);
            AddToggle("PetParrotConfig", 1180);
            AddToggle("PetPenguinConfig", 669);
            AddToggle("PetPupConfig", 1927);
            AddToggle("PetSeedConfig", 1182);
            AddToggle("PetDGConfig", 1169);
            AddToggle("PetSnowmanConfig", 1312);
            AddToggle("PetGrinchConfig", ItemID.BabyGrinchMischiefWhistle);
            AddToggle("PetSpiderConfig", 1798);
            AddToggle("PetSquashConfig", 1799);
            AddToggle("PetTikiConfig", 1171);
            AddToggle("PetShroomConfig", 1181);
            AddToggle("PetTurtleConfig", 753);
            AddToggle("PetZephyrConfig", 2420);
            AddToggle("PetHeartConfig", 3062);
            AddToggle("PetNaviConfig", 425);
            AddToggle("PetFlickerConfig", 3856);
            AddToggle("PetLanturnConfig", 3043);
            AddToggle("PetOrbConfig", 115);
            AddToggle("PetSuspEyeConfig", 3577);
            AddToggle("PetWispConfig", 1183);

            #endregion pet toggles

            #region patreon toggles

            AddToggle("PatreonHeader", ModContent.ItemType<RoombaPet>());
            AddToggle("PatreonRoomba", ModContent.ItemType<RoombaPet>());
            AddToggle("PatreonOrb", ModContent.ItemType<ComputationOrb>());
            AddToggle("PatreonFishingRod", ModContent.ItemType<MissDrakovisFishingPole>());
            AddToggle("PatreonDoor", ModContent.ItemType<SquidwardDoor>());
            AddToggle("PatreonWolf", ModContent.ItemType<ParadoxWolfSoul>());
            AddToggle("PatreonDove", ModContent.ItemType<FigBranch>());
            AddToggle("PatreonKingSlime", ModContent.ItemType<MedallionoftheFallenKing>());
            AddToggle("PatreonFishron", ModContent.ItemType<StaffOfUnleashedOcean>());
            AddToggle("PatreonPlant", ModContent.ItemType<PiranhaPlantVoodooDoll>());

            #endregion patreon toggles

            #endregion Toggles

            if (Main.netMode != NetmodeID.Server)
            {
                #region shaders

                //loading refs for shaders
                Ref<Effect> lcRef = new Ref<Effect>(GetEffect("Effects/LifeChampionShader"));
                Ref<Effect> wcRef = new Ref<Effect>(GetEffect("Effects/WillChampionShader"));
                Ref<Effect> gaiaRef = new Ref<Effect>(GetEffect("Effects/GaiaShader"));
                Ref<Effect> textRef = new Ref<Effect>(GetEffect("Effects/TextShader"));

                //loading shaders from refs
                GameShaders.Misc["LCWingShader"] = new MiscShaderData(lcRef, "LCWings");
                GameShaders.Armor.BindShader(ModContent.ItemType<LifeDye>(), new ArmorShaderData(lcRef, "LCArmor").UseColor(new Color(1f, 0.647f, 0.839f)).UseSecondaryColor(Color.Goldenrod));

                GameShaders.Misc["WCWingShader"] = new MiscShaderData(wcRef, "WCWings");
                GameShaders.Armor.BindShader(ModContent.ItemType<WillDye>(), new ArmorShaderData(wcRef, "WCArmor").UseColor(Color.DarkOrchid).UseSecondaryColor(Color.LightPink).UseImage("Images/Misc/Noise"));

                GameShaders.Misc["GaiaShader"] = new MiscShaderData(gaiaRef, "GaiaGlow");
                GameShaders.Armor.BindShader(ModContent.ItemType<GaiaDye>(), new ArmorShaderData(gaiaRef, "GaiaArmor").UseColor(new Color(0.44f, 1, 0.09f)).UseSecondaryColor(new Color(0.5f, 1f, 0.9f)));

                GameShaders.Misc["PulseUpwards"] = new MiscShaderData(textRef, "PulseUpwards");
                GameShaders.Misc["PulseDiagonal"] = new MiscShaderData(textRef, "PulseDiagonal");
                GameShaders.Misc["PulseCircle"] = new MiscShaderData(textRef, "PulseCircle");

                #endregion shaders
            }
        }

        public void AddToggle(string toggle, int item, string color = "ffffff")
        {
            ModTranslation text = CreateTranslation(toggle + "Toggle");
            text.SetDefault($"[i:{item}][c/{color}:{FargoLangHelper.GetToggleText(toggle)}]");
            AddTranslation(text);
        }

        public override void Unload()
        {
            if (DebuffIDs != null)
                DebuffIDs.Clear();

            //game will reload golem textures, this helps prevent the crash on reload
            Main.NPCLoaded[NPCID.Golem] = false;
            Main.NPCLoaded[NPCID.GolemFistLeft] = false;
            Main.NPCLoaded[NPCID.GolemFistRight] = false;
            Main.NPCLoaded[NPCID.GolemHead] = false;
            Main.NPCLoaded[NPCID.GolemHeadFree] = false;
        }

        public override object Call(params object[] args)
        {
            try
            {
                string code = args[0].ToString();

                switch (code)
                {
                    case "Masomode":
                        return FargoSoulsWorld.MasochistMode;

                    case "DownedMutant":
                        return FargoSoulsWorld.downedMutant;

                    case "DownedAbom":
                    case "DownedAbominationn":
                        return FargoSoulsWorld.downedAbom;

                    case "DownedDevi":
                    case "DownedDeviantt":
                        return FargoSoulsWorld.downedDevi;

                    case "DownedFishronEX":
                        return FargoSoulsWorld.downedFishronEX;

                    case "PureHeart":
                        return Main.LocalPlayer.GetModPlayer<FargoPlayer>().PureHeart;

                    case "MutantAntibodies":
                        return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantAntibodies;

                    case "SinisterIcon":
                        return Main.LocalPlayer.GetModPlayer<FargoPlayer>().SinisterIcon;

                    case "AbomAlive":
                        return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<AbomBoss>());

                    case "MutantAlive":
                        return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>());

                    case "DevianttAlive":
                        return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<DeviBoss>());

                    case "MutantPact":
                        return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantsPact;

                    case "MutantDiscountCard":
                        return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantsDiscountCard;

                    /*case "DevianttGifts":

                        Player player = Main.LocalPlayer;
                        FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

                        if (!fargoPlayer.ReceivedMasoGift)
                        {
                            fargoPlayer.ReceivedMasoGift = true;
                            if (Main.netMode == NetmodeID.SinglePlayer)
                            {
                                DropDevianttsGift(player);
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var netMessage = GetPacket(); // Broadcast item request to server
                                netMessage.Write((byte)14);
                                netMessage.Write((byte)player.whoAmI);
                                netMessage.Send();
                            }

                            return true;
                        }

                        break;*/

                    case "GiftsReceived":
                        Player player = Main.LocalPlayer;
                        FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
                        return fargoPlayer.ReceivedMasoGift;

                    case "GiveDevianttGifts":
                        Player player2 = Main.LocalPlayer;
                        FargoPlayer fargoPlayer2 = player2.GetModPlayer<FargoPlayer>();
                        fargoPlayer2.ReceivedMasoGift = true;
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            DropDevianttsGift(player2);
                        }
                        else if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            var netMessage = GetPacket(); // Broadcast item request to server
                            netMessage.Write((byte)14);
                            netMessage.Write((byte)player2.whoAmI);
                            netMessage.Send();
                        }

                        //Main.npcChatText = "This world looks tougher than usual, so you can have these on the house just this once! Talk to me if you need any tips, yeah?";

                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error("Call Error: " + e.StackTrace + e.Message);
            }

            return base.Call(args);
        }

        public static void DropDevianttsGift(Player player)
        {
            Item.NewItem(player.Center, ItemID.SilverPickaxe);
            Item.NewItem(player.Center, ItemID.SilverAxe);
            Item.NewItem(player.Center, ItemID.BugNet);
            Item.NewItem(player.Center, ItemID.LifeCrystal, 4);
            Item.NewItem(player.Center, ItemID.ManaCrystal, 4);
            Item.NewItem(player.Center, ItemID.RecallPotion, 15);
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                Item.NewItem(player.Center, ItemID.WormholePotion, 15);
            }
            Item.NewItem(player.Center, ModContent.ItemType<DevianttsSundial>());
            Item.NewItem(player.Center, ModContent.ItemType<AutoHouse>(), 3);
            Item.NewItem(player.Center, ModContent.ItemType<EurusSock>());
            Item.NewItem(player.Center, ModContent.ItemType<PuffInABottle>());

            //only give once per world
            if (ModLoader.GetMod("MagicStorage") != null && !FargoSoulsWorld.ReceivedTerraStorage)
            {
                Item.NewItem(player.Center, ModLoader.GetMod("MagicStorage").ItemType("StorageHeart"));
                Item.NewItem(player.Center, ModLoader.GetMod("MagicStorage").ItemType("CraftingAccess"));
                Item.NewItem(player.Center, ModLoader.GetMod("MagicStorage").ItemType("StorageUnit"), 16);

                FargoSoulsWorld.ReceivedTerraStorage = true;
                if (Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendData(MessageID.WorldData); //sync world in mp
            }
        }

        //bool sheet
        public override void PostSetupContent()
        {
            try
            {
                CalamityCompatibility = new CalamityCompatibility(this).TryLoad() as CalamityCompatibility;
                ThoriumCompatibility = new ThoriumCompatibility(this).TryLoad() as ThoriumCompatibility;
                SoACompatibility = new SoACompatibility(this).TryLoad() as SoACompatibility;
                MasomodeEXCompatibility = new MasomodeEXCompatibility(this).TryLoad() as MasomodeEXCompatibility;
                BossChecklistCompatibility = (BossChecklistCompatibility)new BossChecklistCompatibility(this).TryLoad();

                BossChecklistCompatibility.Initialize();

                DebuffIDs = new List<int> { 20, 22, 23, 24, 36, 39, 44, 46, 47, 67, 68, 69, 70, 80,
                    88, 94, 103, 137, 144, 145, 148, 149, 156, 160, 163, 164, 195, 196, 197, 199 };
                DebuffIDs.Add(BuffType("Antisocial"));
                DebuffIDs.Add(BuffType("Atrophied"));
                DebuffIDs.Add(BuffType("Berserked"));
                DebuffIDs.Add(BuffType("Bloodthirsty"));
                DebuffIDs.Add(BuffType("ClippedWings"));
                DebuffIDs.Add(BuffType("Crippled"));
                DebuffIDs.Add(BuffType("CurseoftheMoon"));
                DebuffIDs.Add(BuffType("Defenseless"));
                DebuffIDs.Add(BuffType("FlamesoftheUniverse"));
                DebuffIDs.Add(BuffType("Flipped"));
                DebuffIDs.Add(BuffType("FlippedHallow"));
                DebuffIDs.Add(BuffType("Fused"));
                DebuffIDs.Add(BuffType("GodEater"));
                DebuffIDs.Add(BuffType("Guilty"));
                DebuffIDs.Add(BuffType("Hexed"));
                DebuffIDs.Add(BuffType("Infested"));
                DebuffIDs.Add(BuffType("IvyVenom"));
                DebuffIDs.Add(BuffType("Jammed"));
                DebuffIDs.Add(BuffType("Lethargic"));
                DebuffIDs.Add(BuffType("LightningRod"));
                DebuffIDs.Add(BuffType("LivingWasteland"));
                DebuffIDs.Add(BuffType("Lovestruck"));
                DebuffIDs.Add(BuffType("MarkedforDeath"));
                DebuffIDs.Add(BuffType("Midas"));
                DebuffIDs.Add(BuffType("MutantNibble"));
                DebuffIDs.Add(BuffType("NullificationCurse"));
                DebuffIDs.Add(BuffType("Oiled"));
                DebuffIDs.Add(BuffType("OceanicMaul"));
                DebuffIDs.Add(BuffType("Purified"));
                DebuffIDs.Add(BuffType("Recovering"));
                DebuffIDs.Add(BuffType("ReverseManaFlow"));
                DebuffIDs.Add(BuffType("Rotting"));
                DebuffIDs.Add(BuffType("Shadowflame"));
                DebuffIDs.Add(BuffType("SqueakyToy"));
                DebuffIDs.Add(BuffType("Stunned"));
                DebuffIDs.Add(BuffType("Swarming"));
                DebuffIDs.Add(BuffType("Unstable"));

                DebuffIDs.Add(BuffType("MutantFang"));
                DebuffIDs.Add(BuffType("MutantPresence"));

                DebuffIDs.Add(BuffType("TimeFrozen"));

                Mod bossHealthBar = ModLoader.GetMod("FKBossHealthBar");
                if (bossHealthBar != null)
                {
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<BabyGuardian>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<TimberChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<TimberChampionHead>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<EarthChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<LifeChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<WillChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<ShadowChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<SpiritChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<TerraChampion>());
                    bossHealthBar.Call("RegisterHealthBarMini", ModContent.NPCType<NatureChampion>());
                }

                //mutant shop
                Mod fargos = ModLoader.GetMod("Fargowiltas");
                fargos.Call("AddSummon", 5.01f, "FargowiltasSouls", "DevisCurse", (Func<bool>)(() => FargoSoulsWorld.downedDevi), Item.buyPrice(0, 17, 50));
                fargos.Call("AddSummon", 14.01f, "FargowiltasSouls", "AbomsCurse", (Func<bool>)(() => FargoSoulsWorld.downedAbom), 10000000);
                fargos.Call("AddSummon", 14.02f, "FargowiltasSouls", "TruffleWormEX", (Func<bool>)(() => FargoSoulsWorld.downedFishronEX), 10000000);
                fargos.Call("AddSummon", 14.03f, "FargowiltasSouls", "MutantsCurse", (Func<bool>)(() => FargoSoulsWorld.downedMutant), 20000000);
            }
            catch (Exception e)
            {
                Logger.Warn("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }
        }

        private static float ColorTimer;

        public static Color EModeColor()
        {
            Color mutantColor = new Color(28, 222, 152);
            Color abomColor = new Color(255, 224, 53);
            Color deviColor = new Color(255, 51, 153);
            ColorTimer += 0.5f;
            if (ColorTimer >= 300)
            {
                ColorTimer = 0;
            }

            if (ColorTimer < 100)
                return Color.Lerp(mutantColor, abomColor, ColorTimer / 100);
            else if (ColorTimer < 200)
                return Color.Lerp(abomColor, deviColor, (ColorTimer - 100) / 100);
            else
                return Color.Lerp(deviColor, mutantColor, (ColorTimer - 200) / 100);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SoulofLight, 7);
            recipe.AddIngredient(ItemID.SoulofNight, 7);
            recipe.AddIngredient(ModContent.ItemType<Items.Misc.DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<JungleChest>());
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WizardHat);
            recipe.AddIngredient(ModContent.ItemType<Items.Misc.DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<RuneOrb>());
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ModContent.ItemType<Items.Misc.DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<HeartChocolate>());
            recipe.AddRecipe();

            /*recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBonesBanner", 2);
            recipe.AddIngredient(ModContent.ItemType<Items.Misc.DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ModContent.ItemType<InnocuousSkull>());
            recipe.AddRecipe();*/
        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", ItemID.Drax, ItemID.PickaxeAxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            //dungeon enemies
            group = new RecipeGroup(() => Lang.misc[37] + " Angry or Armored Bones Banner", ItemID.AngryBonesBanner, ItemID.BlueArmoredBonesBanner, ItemID.HellArmoredBonesBanner, ItemID.RustyArmoredBonesBanner);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBonesBanner", group);

            //cobalt
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

            //mythril
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

            //adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", ItemID.Ebonwood, ItemID.Shadewood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

            //any adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", group);

            //shroomite head
            group = new RecipeGroup(() => Lang.misc[37] + " Shroomite Head Piece", ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", group);

            //orichalcum head
            group = new RecipeGroup(() => Lang.misc[37] + " Orichalcum Head Piece", ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", group);

            //palladium head
            group = new RecipeGroup(() => Lang.misc[37] + " Palladium Head Piece", ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", group);

            //cobalt head
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Head Piece", ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", group);

            //mythril head
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Head Piece", ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", group);

            //titanium head
            group = new RecipeGroup(() => Lang.misc[37] + " Titanium Head Piece", ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", group);

            //hallowed head
            group = new RecipeGroup(() => Lang.misc[37] + " Hallowed Head Piece", ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", group);

            //adamantite head
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Head Piece", ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", group);

            //chloro head
            group = new RecipeGroup(() => Lang.misc[37] + " Chlorophyte Head Piece", ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", group);

            //spectre head
            group = new RecipeGroup(() => Lang.misc[37] + " Spectre Head Piece", ItemID.SpectreHood, ItemID.SpectreMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", ItemID.BeetleShell, ItemID.BeetleScaleMail);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

            //vanilla butterflies
            group = new RecipeGroup(() => Lang.misc[37] + " Butterfly", ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly,
                ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyButterfly", group);

            //vanilla squirrels
            group = new RecipeGroup(() => Lang.misc[37] + " Squirrel", ItemID.Squirrel, ItemID.SquirrelRed);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySquirrel", group);

            //vanilla squirrels
            group = new RecipeGroup(() => Lang.misc[37] + " Common Fish", ItemID.AtlanticCod, ItemID.Bass, ItemID.Trout, ItemID.RedSnapper, ItemID.Salmon, ItemID.Tuna);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCommonFish", group);

            //vanilla birds
            group = new RecipeGroup(() => Lang.misc[37] + " Bird", ItemID.Bird, ItemID.BlueJay, ItemID.Cardinal, ItemID.GoldBird, ItemID.Duck, ItemID.MallardDuck);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBird", group);

            //vanilla scorpions
            group = new RecipeGroup(() => Lang.misc[37] + " Scorpion", ItemID.Scorpion, ItemID.BlackScorpion);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyScorpion", group);

            //gold pick
            group = new RecipeGroup(() => Lang.misc[37] + " Gold Pickaxe", ItemID.GoldPickaxe, ItemID.PlatinumPickaxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldPickaxe", group);

            //fish trash
            group = new RecipeGroup(() => Lang.misc[37] + " Fishing Trash", ItemID.OldShoe, ItemID.TinCan, ItemID.FishingSeaweed);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyFishingTrash", group);
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: //server side spawning creepers
                    if (Main.netMode == NetmodeID.Server)
                    {
                        byte p = reader.ReadByte();
                        int multiplier = reader.ReadByte();
                        int n = NPC.NewNPC((int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, NPCType("CreeperGutted"), 0,
                            p, 0f, multiplier, 0f);
                        if (n != 200)
                        {
                            Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                        }
                    }
                    break;

                case 1: //server side synchronize pillar data request
                    if (Main.netMode == NetmodeID.Server)
                    {
                        byte pillar = reader.ReadByte();
                        if (!Main.npc[pillar].GetGlobalNPC<EModeGlobalNPC>().masoBool[1])
                        {
                            Main.npc[pillar].GetGlobalNPC<EModeGlobalNPC>().masoBool[1] = true;
                            Main.npc[pillar].GetGlobalNPC<EModeGlobalNPC>().SetDefaults(Main.npc[pillar]);
                            Main.npc[pillar].life = Main.npc[pillar].lifeMax;
                        }
                    }
                    break;

                case 2: //net updating maso
                    EModeGlobalNPC fargoNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>();
                    fargoNPC.masoBool[0] = reader.ReadBoolean();
                    fargoNPC.masoBool[1] = reader.ReadBoolean();
                    fargoNPC.masoBool[2] = reader.ReadBoolean();
                    fargoNPC.masoBool[3] = reader.ReadBoolean();
                    fargoNPC.Counter[0] = reader.ReadInt32();
                    fargoNPC.Counter[1] = reader.ReadInt32();
                    fargoNPC.Counter[2] = reader.ReadInt32();
                    fargoNPC.Counter[3] = reader.ReadInt32();
                    break;

                case 3: //rainbow slime/paladin, MP clients syncing to server
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        byte npc = reader.ReadByte();
                        Main.npc[npc].lifeMax = reader.ReadInt32();
                        float newScale = reader.ReadSingle();
                        Main.npc[npc].position = Main.npc[npc].Center;
                        Main.npc[npc].width = (int)(Main.npc[npc].width / Main.npc[npc].scale * newScale);
                        Main.npc[npc].height = (int)(Main.npc[npc].height / Main.npc[npc].scale * newScale);
                        Main.npc[npc].scale = newScale;
                        Main.npc[npc].Center = Main.npc[npc].position;
                    }
                    break;

                case 4: //moon lord vulnerability synchronization
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int ML = reader.ReadByte();
                        Main.npc[ML].GetGlobalNPC<EModeGlobalNPC>().Counter[0] = reader.ReadInt32();
                        EModeGlobalNPC.masoStateML = reader.ReadByte();
                    }
                    break;

                case 5: //retinazer laser MP sync
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int reti = reader.ReadByte();
                        Main.npc[reti].GetGlobalNPC<EModeGlobalNPC>().masoBool[2] = reader.ReadBoolean();
                        Main.npc[reti].GetGlobalNPC<EModeGlobalNPC>().Counter[0] = reader.ReadInt32();
                    }
                    break;

                case 6: //shark MP sync
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int shark = reader.ReadByte();
                        Main.npc[shark].GetGlobalNPC<EModeGlobalNPC>().SharkCount = reader.ReadByte();
                    }
                    break;

                case 7: //client to server activate dark caster family
                    if (Main.netMode == NetmodeID.Server)
                    {
                        int caster = reader.ReadByte();
                        if (Main.npc[caster].GetGlobalNPC<EModeGlobalNPC>().Counter[1] == 0)
                            Main.npc[caster].GetGlobalNPC<EModeGlobalNPC>().Counter[1] = reader.ReadInt32();
                    }
                    break;

                case 8: //server to clients reset counter
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int caster = reader.ReadByte();
                        Main.npc[caster].GetGlobalNPC<EModeGlobalNPC>().Counter[1] = 0;
                    }
                    break;

                case 9: //client to server, request heart spawn
                    if (Main.netMode == NetmodeID.Server)
                    {
                        int n = reader.ReadByte();
                        Item.NewItem(Main.npc[n].Hitbox, ItemID.Heart);
                    }
                    break;

                case 10: //client to server, sync cultist data
                    if (Main.netMode == NetmodeID.Server)
                    {
                        int cult = reader.ReadByte();
                        EModeGlobalNPC cultNPC = Main.npc[cult].GetGlobalNPC<EModeGlobalNPC>();
                        cultNPC.Counter[0] += reader.ReadInt32();
                        cultNPC.Counter[1] += reader.ReadInt32();
                        cultNPC.Counter[2] += reader.ReadInt32();
                        Main.npc[cult].localAI[3] += reader.ReadSingle();
                    }
                    break;

                case 11: //refresh creeper
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        byte player = reader.ReadByte();
                        NPC creeper = Main.npc[reader.ReadByte()];
                        if (creeper.active && creeper.type == NPCType("CreeperGutted") && creeper.ai[0] == player)
                        {
                            int damage = creeper.lifeMax - creeper.life;
                            creeper.life = creeper.lifeMax;
                            if (damage > 0)
                                CombatText.NewText(creeper.Hitbox, CombatText.HealLife, damage);
                            if (Main.netMode == NetmodeID.Server)
                                creeper.netUpdate = true;
                        }
                    }
                    break;

                case 12: //prime limbs spin
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int n = reader.ReadByte();
                        EModeGlobalNPC limb = Main.npc[n].GetGlobalNPC<EModeGlobalNPC>();
                        limb.masoBool[2] = reader.ReadBoolean();
                        limb.Counter[0] = reader.ReadInt32();
                        Main.npc[n].localAI[3] = reader.ReadSingle();
                    }
                    break;

                case 13: //prime limbs swipe
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int n = reader.ReadByte();
                        EModeGlobalNPC limb = Main.npc[n].GetGlobalNPC<EModeGlobalNPC>();
                        limb.Counter[0] = reader.ReadInt32();
                        limb.Counter[1] = reader.ReadInt32();
                    }
                    break;

                case 14: //devi gifts
                    if (Main.netMode == NetmodeID.Server)
                    {
                        Player player = Main.player[reader.ReadByte()];
                        DropDevianttsGift(player);
                    }
                    break;

                case 15: //sync npc counter array
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int n = reader.ReadByte();
                        EModeGlobalNPC eNPC = Main.npc[n].GetGlobalNPC<EModeGlobalNPC>();
                        for (int i = 0; i < eNPC.Counter.Length; i++)
                            eNPC.Counter[i] = reader.ReadInt32();
                    }
                    break;

                case 16: //client requesting a client side item from server
                    if (Main.netMode == NetmodeID.Server)
                    {
                        int p = reader.ReadInt32();
                        int type = reader.ReadInt32();
                        int netID = reader.ReadInt32();
                        byte prefix = reader.ReadByte();
                        int stack = reader.ReadInt32();

                        int i = Item.NewItem(Main.player[p].Hitbox, type, stack, true, prefix);
                        Main.itemLockoutTime[i] = 54000;

                        var netMessage = GetPacket();
                        netMessage.Write((byte)17);
                        netMessage.Write(p);
                        netMessage.Write(type);
                        netMessage.Write(netID);
                        netMessage.Write(prefix);
                        netMessage.Write(stack);
                        netMessage.Write(i);
                        netMessage.Send();

                        Main.item[i].active = false;
                    }
                    break;

                case 17: //client-server handshake, spawn client-side item
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int p = reader.ReadInt32();
                        int type = reader.ReadInt32();
                        int netID = reader.ReadInt32();
                        byte prefix = reader.ReadByte();
                        int stack = reader.ReadInt32();
                        int i = reader.ReadInt32();

                        if (Main.myPlayer == p)
                        {
                            Main.item[i].netDefaults(netID);

                            Main.item[i].active = true;
                            Main.item[i].spawnTime = 0;
                            Main.item[i].owner = p;

                            Main.item[i].Prefix(prefix);
                            Main.item[i].stack = stack;
                            Main.item[i].velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                            Main.item[i].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                            Main.item[i].noGrabDelay = 100;
                            Main.item[i].newAndShiny = false;

                            Main.item[i].position = Main.player[p].position;
                            Main.item[i].position.X += Main.rand.NextFloat(Main.player[p].Hitbox.Width);
                            Main.item[i].position.Y += Main.rand.NextFloat(Main.player[p].Hitbox.Height);
                        }
                    }
                    break;

                case 77: //server side spawning fishron EX
                    if (Main.netMode == NetmodeID.Server)
                    {
                        byte target = reader.ReadByte();
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        EModeGlobalNPC.spawnFishronEX = true;
                        NPC.NewNPC(x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                        EModeGlobalNPC.spawnFishronEX = false;
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron EX has awoken!"), new Color(50, 100, 255));
                    }
                    break;

                case 78: //confirming fish EX max life
                    {
                        int f = reader.ReadInt32();
                        Main.npc[f].lifeMax = reader.ReadInt32();
                    }
                    break;

                default:
                    break;
            }

            //BaseMod Stuff
            /*MsgType msg = (MsgType)reader.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = reader.ReadInt32();
                int projID = reader.ReadInt32();
                bool friendly = reader.ReadBoolean();
                bool hostile = reader.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == NetmodeID.Server) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = reader.ReadByte();
                int id = reader.ReadInt16();
                int aitype = reader.ReadByte();
                int arrayLength = reader.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = reader.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == NetmodeID.Server) BaseNet.SyncAI(classID, id, newAI, aitype);
            }*/
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.musicVolume != 0 && Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (MMWorld.MMArmy && priority <= MusicPriority.Environment)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MonsterMadhouse");
                    priority = MusicPriority.Event;
                }
                /*if (FargoSoulsGlobalNPC.BossIsAlive(ref FargoSoulsGlobalNPC.mutantBoss, ModContent.NPCType<NPCs.MutantBoss.MutantBoss>())
                    && Main.player[Main.myPlayer].Distance(Main.npc[FargoSoulsGlobalNPC.mutantBoss].Center) < 3000)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/SteelRed");
                    priority = (MusicPriority)12;
                }*/
            }
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                   (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }
    }

    internal enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}