using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Utilities
{
    public static class FargoUtilities
    {
        /// <summary>
        /// Returns the return value of <see cref="Language.GetTextValue(string)"/> with <paramref name="key"/> appended to <c>"Mods.FargowiltasSouls."</c>.
        /// </summary>
        public static string GetFargoTranslation(string key) => Language.GetTextValue("Mods.FargowiltasSouls." + key);

        /// <summary>
        /// Automatically creates a translation for the use of config "toggles". <br />
        /// Set <paramref name="isFargo"/> to <c>false</c> if you don't want to use a Fargo-added translation.
        /// </summary>
        public static ModTranslation AddToggle(string toggleKey, int itemID, Mod mod, Color color = default, bool isFargo = true)
        {
            // If color is unspecified, set it to white.
            color = color == default ? Color.White : color;

            ModTranslation translation = mod.CreateTranslation(toggleKey);
            translation.SetDefault($"[i:{itemID}][c/{color}:{(isFargo ? GetFargoTranslation(toggleKey) : Language.GetTextValue(toggleKey))}]");
            return translation;
        }

        /// <summary>
        /// Creates a recipe group. <br />
        /// Returns the created group.
        /// <para>
        /// If <paramref name="isFargo"/> is set to <c>true</c>, <see cref="GetFargoTranslation(string)"/> will be used instead of <see cref="Language.GetTextValue(string)"/>. <br/>
        /// The registered group's name is set to <c><see cref="Mod.Name"/>:<see cref="string.Split(char[])"/></c> with either the 1st or 3rd array index as the latter part of the name depending on if <paramref name="isFargo"/> is set to <c>true</c> or <c>false</c>.
        /// </para>
        /// </summary>
        public static RecipeGroup CreateRecipeGroup(string langKey, Mod mod, bool isFargo = true, params int[] itemIDs)
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("Misc.37") + " " + (isFargo ? GetFargoTranslation(langKey) : Language.GetTextValue(langKey)), itemIDs);
            RecipeGroup.RegisterGroup($"{mod.Name}:{langKey.Split('.')[isFargo ? 1 : 3]}", group);
            return group;
        }

        public static Color GetEmodeRarityColor()
        {
            Color mutantColor = new Color(28, 222, 152);
            Color abomColor = new Color(255, 224, 53);
            Color deviColor = new Color(255, 51, 153);

            if (Fargowiltas.ColorTimer < 100)
                return Color.Lerp(mutantColor, abomColor, Fargowiltas.ColorTimer / 100);
            else if (Fargowiltas.ColorTimer < 200)
                return Color.Lerp(abomColor, deviColor, (Fargowiltas.ColorTimer - 100) / 100);
            else
                return Color.Lerp(deviColor, mutantColor, (Fargowiltas.ColorTimer - 200) / 100);
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo) => !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                   (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;

            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo) => !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;

        public static bool NoZone(NPCSpawnInfo spawnInfo) => NoZoneAllowWater(spawnInfo) && !spawnInfo.water;

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo) => !spawnInfo.playerInTown && NoInvasion(spawnInfo);

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo) => NormalSpawn(spawnInfo) && NoZone(spawnInfo);

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo) => NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo) => NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
    }
}