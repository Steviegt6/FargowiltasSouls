using System;
using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public abstract class ModCompatibility
    {
        public Mod CallerMod { get; }

        public string ModName { get; }

        public Mod ModInstance { get; private set; }

        public bool IsLoaded => ModInstance != null;

        public ModCompatibility(Mod callerMod, string modName)
        {
            CallerMod = callerMod;
            ModName = modName;
        }

        public ModCompatibility TryLoad()
        {
            ModInstance = ModLoader.GetMod(ModName);

            try
            {
                Load();
            }
            catch (Exception e)
            {
                CallerMod.Logger.Error($"Error while loading \"{ModInstance.Name}\" for mod \"{CallerMod.Name}\".", e);
            }

            return ModInstance == null ? null : this;
        }

        public virtual void Load()
        {
        }

        public void TryAddRecipes()
        {
            try
            {
                AddRecipes();
            }
            catch (Exception e)
            {
                CallerMod.Logger.Error($"Error while adding recipes from \"{ModInstance.Name}\" for mod \"{CallerMod.Name}\".", e);
            }
        }

        protected virtual void AddRecipes()
        {
        }

        public void TryAddRecipeGroups()
        {
            try
            {
                AddRecipeGroups();
            }
            catch (Exception e)
            {
                CallerMod.Logger.Error($"Error while adding recipe groups from \"{ModInstance.Name}\" for mod \"{CallerMod.Name}\".", e);
            }
        }

        protected virtual void AddRecipeGroups()
        {
        }
    }
}