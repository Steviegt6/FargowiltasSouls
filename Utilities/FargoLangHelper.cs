using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Utilities
{
    public static class FargoLangHelper
    {
        internal enum LangCategory
        {
            Hotkey,
            Toggles // This shouldn't be directly used outside of ModifyToggleTranslation
        }

        internal static string GetHotkeyText(string key) => GetFargoTextByCategory(LangCategory.Hotkey, key);

        internal static string GetToggleText(string key) => GetFargoTextByCategory(LangCategory.Toggles, key);

        /// <summary>
        /// Returns <see cref="GetModText(Mod, string)"/> with the mod being <see cref="Fargowiltas.Instance"/> and the key being <c>$"{category}.{remainderKey}"</c>.
        /// </summary>
        internal static string GetFargoTextByCategory(LangCategory category, string remainderKey) => GetModText(Fargowiltas.Instance, $"{category}.{remainderKey}");

        /// <summary>
        /// Returns <see cref="Language.GetTextValue(string)"/> with the string as <c>$"Mods.{mod.Name}.{key}"</c>.
        /// </summary>
        public static string GetModText(Mod mod, string key) => Language.GetTextValue($"Mods.{mod.Name}.{key}");
    }
}