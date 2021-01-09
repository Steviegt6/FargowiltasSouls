using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.DataStructures
{
    public struct CultureLocalizationInfo
    {
        public string english;
        public string german;
        public string italian;
        public string french;
        public string spanish;
        public string russian;
        public string chinese;
        public string portuguese;
        public string polish;

        public CultureLocalizationInfo(string english, string german = "", string italian = "", string french = "", string spanish = "", string russian = "", string chinese = "", string portuguese = "", string polish = "")
        {
            this.english = english;
            this.german = german;
            this.italian = italian;
            this.french = french;
            this.spanish = spanish;
            this.russian = russian;
            this.chinese = chinese;
            this.portuguese = portuguese;
            this.polish = polish;
        }

        public void PopulateModTranslation(ModTranslation translation, int item, Mod mod, string color = "ffffff")
        {
            translation.SetDefault($"[i:{item}] [c/{color}:{english}]");

            if (!string.IsNullOrEmpty(german))
                translation.AddTranslation(GameCulture.German, $"[i:{item}] [c/{color}:{german}]");

            if (!string.IsNullOrEmpty(italian))
                translation.AddTranslation(GameCulture.Italian, $"[i:{item}] [c/{color}:{italian}]");

            if (!string.IsNullOrEmpty(french))
                translation.AddTranslation(GameCulture.French, $"[i:{item}] [c/{color}:{french}]");

            if (!string.IsNullOrEmpty(spanish))
                translation.AddTranslation(GameCulture.Spanish, $"[i:{item}] [c/{color}:{spanish}]");

            if (!string.IsNullOrEmpty(russian))
                translation.AddTranslation(GameCulture.Russian, $"[i:{item}] [c/{color}:{russian}]");

            if (!string.IsNullOrEmpty(chinese))
                translation.AddTranslation(GameCulture.Chinese, $"[i:{item}] [c/{color}:{chinese}]");

            if (!string.IsNullOrEmpty(portuguese))
                translation.AddTranslation(GameCulture.Portuguese, $"[i:{item}] [c/{color}:{portuguese}]");

            if (!string.IsNullOrEmpty(polish))
                translation.AddTranslation(GameCulture.Polish, $"[i:{item}] [c/{color}:{polish}]");

            mod.AddTranslation(translation);
        }
    }
}