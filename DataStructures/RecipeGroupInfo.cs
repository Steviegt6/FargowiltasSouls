using FargowiltasSouls.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.DataStructures
{
    public struct RecipeGroupInfo
    {
        public RecipeGroup group;
        public bool wasAlreadyRegistered;

        public RecipeGroupInfo(Mod mod, string localizationKey, bool autoRegister, params int[] itemIDs)
        {
            string text = mod == Fargowiltas.Instance ? FargoLangHelper.GetRecipeGroupText(localizationKey) : Language.GetTextValue(localizationKey);

            group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {text}", itemIDs);

            if (autoRegister)
            {
                string[] splitKey = localizationKey.Split('.');

                if (splitKey.Length > 1)
                    localizationKey = splitKey[splitKey.Length];

                RecipeGroup.RegisterGroup($"{mod.Name}:{localizationKey}", group);
                wasAlreadyRegistered = true;
            }
            else
                wasAlreadyRegistered = false;
        }
    }
}