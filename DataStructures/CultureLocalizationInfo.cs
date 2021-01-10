using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.DataStructures
{
    public struct CultureLocalizationInfo
    {
        public Dictionary<int, string> Translations;

        public CultureLocalizationInfo(string english, string german = "", string italian = "", string french = "", string spanish = "", string russian = "", string chinese = "", string portuguese = "", string polish = "")
        {
            Translations = new Dictionary<int, string>()
            {
                { GameCulture.English.LegacyId, english },
                { GameCulture.German.LegacyId, german },
                { GameCulture.Italian.LegacyId, italian },
                { GameCulture.French.LegacyId, french },
                { GameCulture.Spanish.LegacyId, spanish },
                { GameCulture.Russian.LegacyId, russian },
                { GameCulture.Chinese.LegacyId, chinese },
                { GameCulture.Portuguese.LegacyId, portuguese },
                { GameCulture.Polish.LegacyId, polish }
            };
        }

        public void PopulateModTranslation(ModTranslation translation, int item, Mod mod, string color = "ffffff")
        {
            string enText = $"[c/{color}:{Translations[GameCulture.English.LegacyId]}]";

            if (item != ItemID.None)
                enText = $"[i:{item}] " + enText;

            foreach (int culture in Translations.Keys)
                translation.AddTranslation(culture, string.IsNullOrEmpty(Translations[culture]) ? enText : (item != ItemID.None ? $"[i:{item}] " : "") + $"[c/{color}:{Translations[culture]}]");

            mod.AddTranslation(translation);
        }
    }
}