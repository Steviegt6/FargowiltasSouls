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
using System.Reflection;
using Terraria.ModLoader.Core;
using System.Linq;
using System.Text;
using FargowiltasSouls.DataStructures;
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

            TmodFile tModFile = typeof(Fargowiltas).GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Instance) as TmodFile;

            foreach (TmodFile.FileEntry translationFile in tModFile.Where(entry => Path.GetExtension(entry.Name) == ".fargolang"))
            {
                var modTranslationDictionary = new Dictionary<string, ModTranslation>();
                string translationFileContents = Encoding.UTF8.GetString(tModFile.GetBytes(translationFile));
                GameCulture culture = GameCulture.FromName(Path.GetFileNameWithoutExtension(translationFile.Name));

                using (StringReader reader = new StringReader(translationFileContents))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        int split = line.IndexOf('=');

                        if (split < 0)
                            continue;

                        string key = line.Substring(0, split).Trim().Replace(' ', '_');
                        string value = line.Substring(split + 1);

                        if (value.Length == 0)
                            continue;

                        value = value.Replace("\\n", "\n");

                        if (!modTranslationDictionary.TryGetValue(key, out ModTranslation translation))
                            modTranslationDictionary[key] = translation = CreateTranslation(key);
                        Logger.Debug(value);
                        translation.AddTranslation(culture, value);
                    }
                }

                foreach (ModTranslation translation in modTranslationDictionary.Values)
                    AddTranslation(translation);
            }

            #region Toggles

            AddToggleTags(new CultureLocalizationInfo(english: "All Toggles On"), "AllTogglesOn", ItemID.None);
            AddToggleTags(new CultureLocalizationInfo(english: "All Toggles Off"), "AllTogglesOff", ItemID.None);
            AddToggleTags(new CultureLocalizationInfo(english: "Minimal Effects Only"), "MinimalEffectsOnly", ItemID.None);

            AddToggleTags(new CultureLocalizationInfo(english: "Preset Configurations"), "PresetHeader", ModContent.ItemType<Masochist>());

            #region enchants

            AddToggleTags(new CultureLocalizationInfo(english: "Patreon Items"), "PatreonHeader", ModContent.ItemType<RoombaPet>());
            AddToggleTags(new CultureLocalizationInfo(english: "Roomba"), "PatreonRoomba", ModContent.ItemType<RoombaPet>());
            AddToggleTags(new CultureLocalizationInfo(english: "Computation Orb"), "PatreonOrb", ModContent.ItemType<ComputationOrb>());
            AddToggleTags(new CultureLocalizationInfo(english: "Miss Drakovi's Fishing Pole"), "PatreonFishingRod", ModContent.ItemType<MissDrakovisFishingPole>());
            AddToggleTags(new CultureLocalizationInfo(english: "Squidward Door"), "PatreonDoor", ModContent.ItemType<SquidwardDoor>());
            AddToggleTags(new CultureLocalizationInfo(english: "Paradox Wolf Soul"), "PatreonWolf", ModContent.ItemType<ParadoxWolfSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Fig Branch"), "PatreonDove", ModContent.ItemType<FigBranch>());
            AddToggleTags(new CultureLocalizationInfo(english: "Medallion of the Fallen King"), "PatreonKingSlime", ModContent.ItemType<MedallionoftheFallenKing>());
            AddToggleTags(new CultureLocalizationInfo(english: "Staff Of Unleashed Ocean"), "PatreonFishron", ModContent.ItemType<StaffOfUnleashedOcean>());
            AddToggleTags(new CultureLocalizationInfo(english: "Piranha Plant Voodoo Doll"), "PatreonPlant", ModContent.ItemType<PiranhaPlantVoodooDoll>());

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Timber"), "WoodHeader", ModContent.ItemType<TimberForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Boreal Snowballs"), "BorealConfig", ModContent.ItemType<BorealWoodEnchant>(), "8B7464");
            AddToggleTags(new CultureLocalizationInfo(english: "Mahogany Hook Speed"), "MahoganyConfig", ModContent.ItemType<RichMahoganyEnchant>(), "b56c64");
            AddToggleTags(new CultureLocalizationInfo(english: "Ebonwood Shadowflame"), "EbonConfig", ModContent.ItemType<EbonwoodEnchant>(), "645a8d");
            AddToggleTags(new CultureLocalizationInfo(english: "Blood Geyser On Hit"), "ShadeConfig", ModContent.ItemType<ShadewoodEnchant>(), "586876");
            AddToggleTags(new CultureLocalizationInfo(english: "Proximity Triggers On Hit Effects"), "ShadeOnHitConfig", ModContent.ItemType<ShadewoodEnchant>(), "586876");
            AddToggleTags(new CultureLocalizationInfo(english: "Palmwood Sentry"), "PalmConfig", ModContent.ItemType<PalmWoodEnchant>(), "b78d56");
            AddToggleTags(new CultureLocalizationInfo(english: "Pearlwood Rain"), "PearlConfig", ModContent.ItemType<PearlwoodEnchant>(), "ad9a5f");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Earth"), "EarthHeader", ModContent.ItemType<EarthForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Adamantite Projectile Splitting"), "AdamantiteConfig", ModContent.ItemType<AdamantiteEnchant>(), "dd557d");
            AddToggleTags(new CultureLocalizationInfo(english: "Cobalt Shards"), "CobaltConfig", ModContent.ItemType<CobaltEnchant>(), "3da4c4");
            AddToggleTags(new CultureLocalizationInfo(english: "Ancient Cobalt Stingers"), "AncientCobaltConfig", ModContent.ItemType<AncientCobaltEnchant>(), "354c74");
            AddToggleTags(new CultureLocalizationInfo(english: "Mythril Weapon Speed"), "MythrilConfig", ModContent.ItemType<MythrilEnchant>(), "9dd290");
            AddToggleTags(new CultureLocalizationInfo(english: "Orichalcum Petals"), "OrichalcumConfig", ModContent.ItemType<OrichalcumEnchant>(), "eb3291");
            AddToggleTags(new CultureLocalizationInfo(english: "Palladium Healing"), "PalladiumConfig", ModContent.ItemType<PalladiumEnchant>(), "f5ac28");
            AddToggleTags(new CultureLocalizationInfo(english: "Palladium Orbs"), "PalladiumOrbConfig", ModContent.ItemType<PalladiumEnchant>(), "f5ac28");
            AddToggleTags(new CultureLocalizationInfo(english: "Titanium Shadow Dodge"), "TitaniumConfig", ModContent.ItemType<TitaniumEnchant>(), "828c88");

            AddToggleTags(new CultureLocalizationInfo(english: "Terra Force"), "TerraHeader", ModContent.ItemType<TerraForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Copper Lightning"), "CopperConfig", ModContent.ItemType<CopperEnchant>(), "d56617");
            AddToggleTags(new CultureLocalizationInfo(english: "Iron Magnet"), "IronMConfig", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggleTags(new CultureLocalizationInfo(english: "Iron Shield"), "IronSConfig", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggleTags(new CultureLocalizationInfo(english: "Shield of Cthulhu"), "CthulhuShield", ModContent.ItemType<IronEnchant>(), "988e83");
            AddToggleTags(new CultureLocalizationInfo(english: "Tin Crits"), "TinConfig", ModContent.ItemType<TinEnchant>(), "a28b4e");
            AddToggleTags(new CultureLocalizationInfo(english: "Tungsten Item Effect"), "TungstenConfig", ModContent.ItemType<TungstenEnchant>(), "b0d2b2");
            AddToggleTags(new CultureLocalizationInfo(english: "Tungsten Projectile Effect"), "TungstenProjConfig", ModContent.ItemType<TungstenEnchant>(), "b0d2b2");
            AddToggleTags(new CultureLocalizationInfo(english: "Obsidian Explosions"), "ObsidianConfig", ModContent.ItemType<ObsidianEnchant>(), "453e73");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Will"), "WillHeader", ModContent.ItemType<WillForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Gladiator Rain"), "GladiatorConfig", ModContent.ItemType<GladiatorEnchant>(), "9c924e");
            AddToggleTags(new CultureLocalizationInfo(english: "Gold Lucky Coin"), "GoldConfig", ModContent.ItemType<GoldEnchant>(), "e7b21c");
            AddToggleTags(new CultureLocalizationInfo(english: "Huntress Ability"), "HuntressConfig", ModContent.ItemType<HuntressEnchant>(), "7ac04c");
            AddToggleTags(new CultureLocalizationInfo(english: "Squire/Valhalla Effect"), "ValhallaConfig", ModContent.ItemType<ValhallaKnightEnchant>(), "93651e");
            AddToggleTags(new CultureLocalizationInfo(english: "Ballista Panic On Hit"), "SquirePanicConfig", ModContent.ItemType<SquireEnchant>(), "948f8c");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Life"), "LifeHeader", ModContent.ItemType<LifeForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Bees"), "BeeConfig", ModContent.ItemType<BeeEnchant>(), "FEF625");
            AddToggleTags(new CultureLocalizationInfo(english: "Beetles"), "BeetleConfig", ModContent.ItemType<BeetleEnchant>(), "6D5C85");
            AddToggleTags(new CultureLocalizationInfo(english: "Cactus Needles"), "CactusConfig", ModContent.ItemType<CactusEnchant>(), "799e1d");
            AddToggleTags(new CultureLocalizationInfo(english: "Grow Pumpkins"), "PumpkinConfig", ModContent.ItemType<PumpkinEnchant>(), "e3651c");
            AddToggleTags(new CultureLocalizationInfo(english: "Spider Swarm"), "SpiderConfig", ModContent.ItemType<SpiderEnchant>(), "6d4e45");
            AddToggleTags(new CultureLocalizationInfo(english: "Turtle Shell Buff"), "TurtleConfig", ModContent.ItemType<TurtleEnchant>(), "f89c5c");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Nature"), "NatureHeader", ModContent.ItemType<NatureForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Chlorophyte Leaf Crystal"), "ChlorophyteConfig", ModContent.ItemType<ChlorophyteEnchant>(), "248900");
            AddToggleTags(new CultureLocalizationInfo(english: "Flower Boots"), "ChlorophyteFlowerConfig", ModContent.ItemType<ChlorophyteEnchant>(), "248900");
            AddToggleTags(new CultureLocalizationInfo(english: "Crimson Regen"), "CrimsonConfig", ModContent.ItemType<CrimsonEnchant>(), "C8364B");
            AddToggleTags(new CultureLocalizationInfo(english: "Rain Clouds"), "RainConfig", ModContent.ItemType<RainEnchant>(), "ffec00");
            AddToggleTags(new CultureLocalizationInfo(english: "Frost Icicles"), "FrostConfig", ModContent.ItemType<FrostEnchant>(), "7abdb9");
            AddToggleTags(new CultureLocalizationInfo(english: "Snowstorm"), "SnowConfig", ModContent.ItemType<SnowEnchant>(), "25c3f2");
            AddToggleTags(new CultureLocalizationInfo(english: "Jungle Spores"), "JungleConfig", ModContent.ItemType<JungleEnchant>(), "71971f");
            AddToggleTags(new CultureLocalizationInfo(english: "Plant Fiber Cordage"), "CordageConfig", ModContent.ItemType<JungleEnchant>(), "71971f");
            AddToggleTags(new CultureLocalizationInfo(english: "Molten Inferno Buff"), "MoltenConfig", ModContent.ItemType<MoltenEnchant>(), "c12b2b");
            AddToggleTags(new CultureLocalizationInfo(english: "Molten Explosion On Hit"), "MoltenEConfig", ModContent.ItemType<MoltenEnchant>(), "c12b2b");
            AddToggleTags(new CultureLocalizationInfo(english: "Shroomite Stealth"), "ShroomiteConfig", ModContent.ItemType<ShroomiteEnchant>(), "008cf4");
            AddToggleTags(new CultureLocalizationInfo(english: "Shroomite Mushrooms"), "ShroomiteShroomConfig", ModContent.ItemType<ShroomiteEnchant>(), "008cf4");

            AddToggleTags(new CultureLocalizationInfo(english: "Shadow Force"), "ShadowHeader", ModContent.ItemType<ShadowForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Flameburst Minion"), "DarkArtConfig", ModContent.ItemType<DarkArtistEnchant>(), "9b5cb0");
            AddToggleTags(new CultureLocalizationInfo(english: "Apprentice Effect"), "ApprenticeConfig", ModContent.ItemType<ApprenticeEnchant>(), "5d86a6");
            AddToggleTags(new CultureLocalizationInfo(english: "Necro Graves"), "NecroConfig", ModContent.ItemType<NecroEnchant>(), "565643");
            AddToggleTags(new CultureLocalizationInfo(english: "Shadow Darkness"), "ShadowConfig", ModContent.ItemType<ShadowEnchant>(), "42356f");
            AddToggleTags(new CultureLocalizationInfo(english: "Ancient Shadow Orbs"), "AncientShadowConfig", ModContent.ItemType<AncientShadowEnchant>(), "42356f");
            AddToggleTags(new CultureLocalizationInfo(english: "Monk Dash"), "MonkConfig", ModContent.ItemType<MonkEnchant>(), "920520");
            AddToggleTags(new CultureLocalizationInfo(english: "Shinobi Through Walls"), "ShinobiConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggleTags(new CultureLocalizationInfo(english: "Tabi Dash"), "ShinobiTabiConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggleTags(new CultureLocalizationInfo(english: "Tiger Climbing Gear"), "ShinobiClimbingConfig", ModContent.ItemType<ShinobiEnchant>(), "935b18");
            AddToggleTags(new CultureLocalizationInfo(english: "Spooky Scythes"), "SpookyConfig", ModContent.ItemType<SpookyEnchant>(), "644e74");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Spirit"), "SpiritHeader", ModContent.ItemType<SpiritForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Forbidden Storm"), "ForbiddenConfig", ModContent.ItemType<ForbiddenEnchant>(), "e7b21c");
            AddToggleTags(new CultureLocalizationInfo(english: "Hallowed Enchanted Sword Familiar"), "HallowedConfig", ModContent.ItemType<HallowEnchant>(), "968564");
            AddToggleTags(new CultureLocalizationInfo(english: "Hallowed Shield"), "HallowSConfig", ModContent.ItemType<HallowEnchant>(), "968564");
            AddToggleTags(new CultureLocalizationInfo(english: "Silver Sword Familiar"), "SilverConfig", ModContent.ItemType<SilverEnchant>(), "b4b4cc");
            AddToggleTags(new CultureLocalizationInfo(english: "Spectre Orbs"), "SpectreConfig", ModContent.ItemType<SpectreEnchant>(), "accdfc");
            AddToggleTags(new CultureLocalizationInfo(english: "Tiki Minions"), "TikiConfig", ModContent.ItemType<TikiEnchant>(), "56A52B");

            AddToggleTags(new CultureLocalizationInfo(english: "Force of Cosmos"), "CosmoHeader", ModContent.ItemType<CosmoForce>());
            AddToggleTags(new CultureLocalizationInfo(english: "Meteor Shower"), "MeteorConfig", ModContent.ItemType<MeteorEnchant>(), "5f4752");
            AddToggleTags(new CultureLocalizationInfo(english: "Nebula Boosters"), "NebulaConfig", ModContent.ItemType<NebulaEnchant>(), "fe7ee5");
            AddToggleTags(new CultureLocalizationInfo(english: "Solar Shield"), "SolarConfig", ModContent.ItemType<SolarEnchant>(), "fe9e23");
            AddToggleTags(new CultureLocalizationInfo(english: "Inflict Solar Flare"), "SolarFlareConfig", ModContent.ItemType<SolarEnchant>(), "fe9e23");
            AddToggleTags(new CultureLocalizationInfo(english: "Stardust Guardian"), "StardustConfig", ModContent.ItemType<StardustEnchant>(), "00aeee");
            AddToggleTags(new CultureLocalizationInfo(english: "Vortex Stealth"), "VortexSConfig", ModContent.ItemType<VortexEnchant>(), "00f2aa");
            AddToggleTags(new CultureLocalizationInfo(english: "Vortex Voids"), "VortexVConfig", ModContent.ItemType<VortexEnchant>(), "00f2aa");

            #endregion enchants

            #region masomode toggles

            //Masomode Header
            AddToggleTags(new CultureLocalizationInfo(english: "Eternity Mode"), "MasoHeader", ModContent.ItemType<MutantStatue>());
            //AddToggleTags(new CultureLocalizationInfo(english: "Mutant Bright Background"), "MasoBossBG", ModContent.ItemType<Masochist>());
            AddToggleTags(new CultureLocalizationInfo(english: "Boss Recolors (Restart to use)"), "MasoBossRecolors", ModContent.ItemType<Masochist>());
            AddToggleTags(new CultureLocalizationInfo(english: "Sinister Icon"), "MasoIconConfig", ModContent.ItemType<SinisterIcon>());
            AddToggleTags(new CultureLocalizationInfo(english: "Sinister Icon Drops"), "MasoIconDropsConfig", ModContent.ItemType<SinisterIcon>());
            AddToggleTags(new CultureLocalizationInfo(english: "Graze"), "MasoGrazeConfig", ModContent.ItemType<SparklingAdoration>());
            AddToggleTags(new CultureLocalizationInfo(english: "Attacks Spawn Homing Hearts"), "MasoDevianttHeartsConfig", ModContent.ItemType<SparklingAdoration>());

            //supreme death fairy header
            AddToggleTags(new CultureLocalizationInfo(english: "Supreme Deathbringer Fairy"), "SupremeFairyHeader", ModContent.ItemType<SupremeDeathbringerFairy>());
            AddToggleTags(new CultureLocalizationInfo(english: "Slimy Balls"), "MasoSlimeConfig", ModContent.ItemType<SlimyShield>());
            AddToggleTags(new CultureLocalizationInfo(english: "Increased Fall Speed"), "SlimeFallingConfig", ModContent.ItemType<SlimyShield>());
            AddToggleTags(new CultureLocalizationInfo(english: "Scythes When Dashing"), "MasoEyeConfig", ModContent.ItemType<AgitatingLens>());
            AddToggleTags(new CultureLocalizationInfo(english: "Honey When Hitting Enemies"), "MasoHoneyConfig", ModContent.ItemType<QueenStinger>());
            AddToggleTags(new CultureLocalizationInfo(english: "Skeletron Arms Minion"), "MasoSkeleConfig", ModContent.ItemType<NecromanticBrew>());

            //bionomic
            AddToggleTags(new CultureLocalizationInfo(english: "Bionomic Cluster"), "BionomicHeader", ModContent.ItemType<BionomicCluster>());
            AddToggleTags(new CultureLocalizationInfo(english: "Tim's Concoction"), "MasoConcoctionConfig", ModContent.ItemType<TimsConcoction>());
            AddToggleTags(new CultureLocalizationInfo(english: "Carrot View"), "MasoCarrotConfig", ModContent.ItemType<OrdinaryCarrot>());
            AddToggleTags(new CultureLocalizationInfo(english: "Rainbow Slime Minion"), "MasoRainbowConfig", ModContent.ItemType<ConcentratedRainbowMatter>());
            AddToggleTags(new CultureLocalizationInfo(english: "Frostfireballs"), "MasoFrigidConfig", ModContent.ItemType<FrigidGemstone>());
            AddToggleTags(new CultureLocalizationInfo(english: "Attacks Spawn Hearts"), "MasoNymphConfig", ModContent.ItemType<NymphsPerfume>());
            AddToggleTags(new CultureLocalizationInfo(english: "Squeaky Toy On Hit"), "MasoSqueakConfig", ModContent.ItemType<SqueakyToy>());
            AddToggleTags(new CultureLocalizationInfo(english: "Tentacles On Hit"), "MasoPouchConfig", ModContent.ItemType<WretchedPouch>());
            AddToggleTags(new CultureLocalizationInfo(english: "Inflict Clipped Wings"), "MasoClippedConfig", ModContent.ItemType<WyvernFeather>());
            AddToggleTags(new CultureLocalizationInfo(english: "Tribal Charm Auto Swing"), "TribalCharmConfig", ModContent.ItemType<TribalCharm>());
            //AddToggleTags(new CultureLocalizationInfo(english: "Security Wallet"), "WalletHeader", ModContent.ItemType<SecurityWallet>());

            //dubious
            AddToggleTags(new CultureLocalizationInfo(english: "Dubious Circuitry"), "DubiousHeader", ModContent.ItemType<DubiousCircuitry>());
            AddToggleTags(new CultureLocalizationInfo(english: "Inflict Lightning Rod"), "MasoLightningConfig", ModContent.ItemType<GroundStick>());
            AddToggleTags(new CultureLocalizationInfo(english: "Probes Minion"), "MasoProbeConfig", ModContent.ItemType<GroundStick>());

            //pure heart
            AddToggleTags(new CultureLocalizationInfo(english: "Pure Heart"), "PureHeartHeader", ModContent.ItemType<PureHeart>());
            AddToggleTags(new CultureLocalizationInfo(english: "Tiny Eaters"), "MasoEaterConfig", ModContent.ItemType<CorruptHeart>());
            AddToggleTags(new CultureLocalizationInfo(english: "Creeper Shield"), "MasoBrainConfig", ModContent.ItemType<GuttedHeart>());

            //lump of flesh
            AddToggleTags(new CultureLocalizationInfo(english: "Lump of Flesh"), "LumpofFleshHeader", ModContent.ItemType<LumpOfFlesh>());
            AddToggleTags(new CultureLocalizationInfo(english: "Pungent Eye Minion"), "MasoPugentConfig", ModContent.ItemType<LumpOfFlesh>());

            //chalice
            AddToggleTags(new CultureLocalizationInfo(english: "Chalice of the Moon"), "ChaliceHeader", ModContent.ItemType<ChaliceoftheMoon>());
            AddToggleTags(new CultureLocalizationInfo(english: "Cultist Minion"), "MasoCultistConfig", ModContent.ItemType<ChaliceoftheMoon>());
            AddToggleTags(new CultureLocalizationInfo(english: "Plantera Minion"), "MasoPlantConfig", ModContent.ItemType<MagicalBulb>());
            AddToggleTags(new CultureLocalizationInfo(english: "Lihzahrd Ground Pound"), "MasoGolemConfig", ModContent.ItemType<LihzahrdTreasureBox>());
            AddToggleTags(new CultureLocalizationInfo(english: "Boulder Spray"), "MasoBoulderConfig", ModContent.ItemType<LihzahrdTreasureBox>());
            AddToggleTags(new CultureLocalizationInfo(english: "Celestial Rune Support"), "MasoCelestConfig", ModContent.ItemType<CelestialRune>());
            AddToggleTags(new CultureLocalizationInfo(english: "Ancient Visions On Hit"), "MasoVisionConfig", ModContent.ItemType<CelestialRune>());

            //heart of the masochist
            AddToggleTags(new CultureLocalizationInfo(english: "Heart of the Eternal"), "HeartHeader", ModContent.ItemType<HeartoftheMasochist>());
            AddToggleTags(new CultureLocalizationInfo(english: "Pumpking's Cape Support"), "MasoPump", ModContent.ItemType<PumpkingsCape>());
            AddToggleTags(new CultureLocalizationInfo(english: "Flocko Minion"), "MasoFlockoConfig", ModContent.ItemType<IceQueensCrown>());
            AddToggleTags(new CultureLocalizationInfo(english: "Saucer Minion"), "MasoUfoConfig", ModContent.ItemType<SaucerControlConsole>());
            AddToggleTags(new CultureLocalizationInfo(english: "Gravity Control"), "MasoGravConfig", ModContent.ItemType<GalacticGlobe>());
            AddToggleTags(new CultureLocalizationInfo(english: "Stabilized Gravity"), "MasoGrav2Config", ModContent.ItemType<GalacticGlobe>());
            AddToggleTags(new CultureLocalizationInfo(english: "True Eyes Minion"), "MasoTrueEyeConfig", ModContent.ItemType<GalacticGlobe>());

            //cyclonic fin
            AddToggleTags(new CultureLocalizationInfo(english: "Abominable Wand"), "CyclonicHeader", ModContent.ItemType<CyclonicFin>());
            AddToggleTags(new CultureLocalizationInfo(english: "Spectral Abominationn"), "MasoFishronConfig", ModContent.ItemType<CyclonicFin>());

            //mutant armor
            AddToggleTags(new CultureLocalizationInfo(english: "True Mutant Armor"), "MutantArmorHeader", ModContent.ItemType<HeartoftheMasochist>());
            AddToggleTags(new CultureLocalizationInfo(english: "Abominationn Minion"), "MasoAbomConfig", ModContent.ItemType<MutantMask>());
            AddToggleTags(new CultureLocalizationInfo(english: "Phantasmal Ring Minion"), "MasoRingConfig", ModContent.ItemType<MutantMask>());
            AddToggleTags(new CultureLocalizationInfo(english: "Deathray When Revived"), "MasoReviveDeathrayConfig", ModContent.ItemType<MutantMask>());

            #endregion masomode toggles

            #region soul toggles

            AddToggleTags(new CultureLocalizationInfo(english: "Souls"), "SoulHeader", ModContent.ItemType<UniverseSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Melee Speed"), "MeleeConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Magma Stone"), "MagmaStoneConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Yoyo Bag"), "YoyoBagConfig", ModContent.ItemType<GladiatorsSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Sniper Scope"), "SniperConfig", ModContent.ItemType<SnipersSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Universe Attack Speed"), "UniverseConfig", ModContent.ItemType<UniverseSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Mining Hunter Buff"), "MiningHuntConfig", ModContent.ItemType<MinerEnchant>());
            AddToggleTags(new CultureLocalizationInfo(english: "Mining Dangersense Buff"), "MiningDangerConfig", ModContent.ItemType<MinerEnchant>());
            AddToggleTags(new CultureLocalizationInfo(english: "Mining Spelunker Buff"), "MiningSpelunkConfig", ModContent.ItemType<MinerEnchant>());
            AddToggleTags(new CultureLocalizationInfo(english: "Mining Shine Buff"), "MiningShineConfig", ModContent.ItemType<MinerEnchant>());
            AddToggleTags(new CultureLocalizationInfo(english: "Builder Mode"), "BuilderConfig", ModContent.ItemType<WorldShaperSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Spore Sac"), "DefenseSporeConfig", ModContent.ItemType<ColossusSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Stars On Hit"), "DefenseStarConfig", ModContent.ItemType<ColossusSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Bees On Hit"), "DefenseBeeConfig", ModContent.ItemType<ColossusSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Panic On Hit"), "DefensePanicConfig", ModContent.ItemType<ColossusSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Higher Base Run Speed"), "RunSpeedConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "No Momentum"), "MomentumConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Supersonic Speed Boosts"), "SupersonicConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Supersonic Jumps"), "SupersonicJumpsConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Supersonic Rocket Boots"), "SupersonicRocketBootsConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Supersonic Carpet"), "SupersonicCarpetConfig", ModContent.ItemType<SupersonicSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Trawler Extra Lures"), "TrawlerConfig", ModContent.ItemType<TrawlerSoul>());
            AddToggleTags(new CultureLocalizationInfo(english: "Eternity Stacking"), "EternityConfig", ModContent.ItemType<EternitySoul>());

            #endregion soul toggles

            #region pet toggles

            AddToggleTags(new CultureLocalizationInfo(english: "Pets"), "PetHeader", 2420);
            AddToggleTags(new CultureLocalizationInfo(english: "Black Cat Pet"), "PetCatConfig", 1810);
            AddToggleTags(new CultureLocalizationInfo(english: "Companion Cube Pet"), "PetCubeConfig", 3628);
            AddToggleTags(new CultureLocalizationInfo(english: "Cursed Sapling Pet"), "PetCurseSapConfig", 1837);
            AddToggleTags(new CultureLocalizationInfo(english: "Dino Pet"), "PetDinoConfig", 1242);
            AddToggleTags(new CultureLocalizationInfo(english: "Dragon Pet"), "PetDragonConfig", 3857);
            AddToggleTags(new CultureLocalizationInfo(english: "Eater Pet"), "PetEaterConfig", 994);
            AddToggleTags(new CultureLocalizationInfo(english: "Eye Spring Pet"), "PetEyeSpringConfig", 1311);
            AddToggleTags(new CultureLocalizationInfo(english: "Face Monster Pet"), "PetFaceMonsterConfig", 3060);
            AddToggleTags(new CultureLocalizationInfo(english: "Gato Pet"), "PetGatoConfig", 3855);
            AddToggleTags(new CultureLocalizationInfo(english: "Hornet Pet"), "PetHornetConfig", 1170);
            AddToggleTags(new CultureLocalizationInfo(english: "Lizard Pet"), "PetLizardConfig", 1172);
            AddToggleTags(new CultureLocalizationInfo(english: "Mini Minotaur Pet"), "PetMinitaurConfig", 2587);
            AddToggleTags(new CultureLocalizationInfo(english: "Parrot Pet"), "PetParrotConfig", 1180);
            AddToggleTags(new CultureLocalizationInfo(english: "Penguin Pet"), "PetPenguinConfig", 669);
            AddToggleTags(new CultureLocalizationInfo(english: "Puppy Pet"), "PetPupConfig", 1927);
            AddToggleTags(new CultureLocalizationInfo(english: "Seedling Pet"), "PetSeedConfig", 1182);
            AddToggleTags(new CultureLocalizationInfo(english: "Skeletron Pet"), "PetDGConfig", 1169);
            AddToggleTags(new CultureLocalizationInfo(english: "Snowman Pet"), "PetSnowmanConfig", 1312);
            AddToggleTags(new CultureLocalizationInfo(english: "Grinch Pet"), "PetGrinchConfig", ItemID.BabyGrinchMischiefWhistle);
            AddToggleTags(new CultureLocalizationInfo(english: "Spider Pet"), "PetSpiderConfig", 1798);
            AddToggleTags(new CultureLocalizationInfo(english: "Squashling Pet"), "PetSquashConfig", 1799);
            AddToggleTags(new CultureLocalizationInfo(english: "Tiki Pet"), "PetTikiConfig", 1171);
            AddToggleTags(new CultureLocalizationInfo(english: "Truffle Pet"), "PetShroomConfig", 1181);
            AddToggleTags(new CultureLocalizationInfo(english: "Turtle Pet"), "PetTurtleConfig", 753);
            AddToggleTags(new CultureLocalizationInfo(english: "Zephyr Fish Pet"), "PetZephyrConfig", 2420);
            AddToggleTags(new CultureLocalizationInfo(english: "Crimson Heart Pet"), "PetHeartConfig", 3062);
            AddToggleTags(new CultureLocalizationInfo(english: "Fairy Pet"), "PetNaviConfig", 425);
            AddToggleTags(new CultureLocalizationInfo(english: "Flickerwick Pet"), "PetFlickerConfig", 3856);
            AddToggleTags(new CultureLocalizationInfo(english: "Magic Lantern Pet"), "PetLanturnConfig", 3043);
            AddToggleTags(new CultureLocalizationInfo(english: "Shadow Orb Pet"), "PetOrbConfig", 115);
            AddToggleTags(new CultureLocalizationInfo(english: "Suspicious Eye Pet"), "PetSuspEyeConfig", 3577);
            AddToggleTags(new CultureLocalizationInfo(english: "Wisp Pet"), "PetWispConfig", 1183);

            #endregion pet toggles

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

        public void AddToggleTags(CultureLocalizationInfo localizationInfo, string toggle, int item, string color = "ffffff", Mod mod = default)
        {
            if (mod == default)
                mod = Instance;

            localizationInfo.PopulateModTranslation(CreateTranslation($"Toggles.{toggle}"), item, mod, color);
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
                BossChecklistCompatibility = new BossChecklistCompatibility(this).TryLoad() as BossChecklistCompatibility;

                BossChecklistCompatibility?.Initialize();

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
            // the ctors for these guys autoload the recipegroups
            //drax
            new RecipeGroupInfo(Instance, "AnyDrax", true, ItemID.Drax, ItemID.PickaxeAxe);

            //dungeon enemies
            new RecipeGroupInfo(Instance, "AnyBonesBanner", true, ItemID.AngryBonesBanner, ItemID.BlueArmoredBonesBanner, ItemID.HellArmoredBonesBanner, ItemID.RustyArmoredBonesBanner);

            //cobalt
            new RecipeGroupInfo(Instance, "AnyCobaltRepeater", true, ItemID.CobaltRepeater, ItemID.PalladiumRepeater);

            //mythril
            new RecipeGroupInfo(Instance, "AnyMythrilRepeater", true, ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);

            //adamantite
            new RecipeGroupInfo(Instance, "AnyAdamantiteRepeater", true, ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);

            //evil wood
            new RecipeGroupInfo(Instance, "AnyEvilWood", true, ItemID.Ebonwood, ItemID.Shadewood);

            //any adamantite
            new RecipeGroupInfo(Instance, "AnyAdamantite", true, ItemID.AdamantiteBar, ItemID.TitaniumBar);

            //shroomite head
            new RecipeGroupInfo(Instance, "AnyShroomHead", true, ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);

            //orichalcum head
            new RecipeGroupInfo(Instance, "AnyOriHead", true, ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);

            //palladium head
            new RecipeGroupInfo(Instance, "AnyPallaHead", true, ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);

            //cobalt head
            new RecipeGroupInfo(Instance, "AnyCobaltHead", true, ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);

            //mythril head
            new RecipeGroupInfo(Instance, "AnyMythrilHead", true, ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);

            //titanium head
            new RecipeGroupInfo(Instance, "AnyTitaHead", true, ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);

            //hallowed head
            new RecipeGroupInfo(Instance, "AnyHallowHead", true, ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);

            //adamantite head
            new RecipeGroupInfo(Instance, "AnyAdamHead", true, ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);

            //chloro head
            new RecipeGroupInfo(Instance, "AnyChloroHead", true, ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);

            //spectre head
            new RecipeGroupInfo(Instance, "AnySpectreHead", true, ItemID.SpectreHood, ItemID.SpectreMask);

            //beetle body
            new RecipeGroupInfo(Instance, "AnyBeetle", true, ItemID.BeetleShell, ItemID.BeetleScaleMail);

            //phasesabers
            new RecipeGroupInfo(Instance, "AnyPhasesaber", true, ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber, ItemID.YellowPhasesaber);

            //vanilla butterflies
            new RecipeGroupInfo(Instance, "AnyButterfly", true, ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly, ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);

            //vanilla squirrels
            new RecipeGroupInfo(Instance, "AnySquirrel", true, ItemID.Squirrel, ItemID.SquirrelRed);

            //vanilla squirrels
            new RecipeGroupInfo(Instance, "AnyCommonFish", true, ItemID.AtlanticCod, ItemID.Bass, ItemID.Trout, ItemID.RedSnapper, ItemID.Salmon, ItemID.Tuna);

            //vanilla birds
            new RecipeGroupInfo(Instance, "AnyBird", true, ItemID.Bird, ItemID.BlueJay, ItemID.Cardinal, ItemID.GoldBird, ItemID.Duck, ItemID.MallardDuck);

            //vanilla scorpions
            new RecipeGroupInfo(Instance, "AnyScorpion", true, ItemID.Scorpion, ItemID.BlackScorpion);

            //gold pick
            new RecipeGroupInfo(Instance, "AnyGoldPickaxe", true, ItemID.GoldPickaxe, ItemID.PlatinumPickaxe);

            //fish trash
            new RecipeGroupInfo(Instance, "AnyFishingTrash", true, ItemID.OldShoe, ItemID.TinCan, ItemID.FishingSeaweed);
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