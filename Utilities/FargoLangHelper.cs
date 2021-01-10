using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Utilities
{
    public static class FargoLangHelper
    {
        internal enum LangCategory
        {
            Hotkey,
            Toggles,
            RecipeGroups,
            Announcements
        }

        internal static string GetHotkeyText(string key) => GetFargoTextByCategory(LangCategory.Hotkey, key);

        internal static string GetToggleText(string key) => GetFargoTextByCategory(LangCategory.Toggles, key);

        internal static string GetRecipeGroupText(string key) => GetFargoTextByCategory(LangCategory.RecipeGroups, key);

        internal static string GetAnnouncementsText(string key) => GetFargoTextByCategory(LangCategory.Announcements, key);

        /// <summary>
        /// Returns <see cref="GetModText(Mod, string)"/> with the mod being <see cref="Fargowiltas.Instance"/> and the key being <c>$"{category}.{remainderKey}"</c>.
        /// </summary>
        internal static string GetFargoTextByCategory(LangCategory category, string remainderKey) => GetFargoText($"{category}.{remainderKey}");

        internal static string GetFargoText(string key) => GetModText(Fargowiltas.Instance, key);

        /// <summary>
        /// Returns <see cref="Language.GetTextValue(string)"/> with the string as <c>$"Mods.{mod.Name}.{key}"</c>.
        /// </summary>
        public static string GetModText(Mod mod, string key) => Language.GetTextValue($"Mods.{mod.Name}.{key}");
    }
}