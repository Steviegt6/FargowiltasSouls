using System;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public class FargowiltasCompatibility : ModCompatibility
    {
        public FargowiltasCompatibility(Mod callerMod) : base(callerMod, "Fargowiltas")
        {
        }

        public override void Load()
        {
            // No need to check if ModInstance is null since it'll always be enabled
            AddSummon(5.01f, "DevisCurse", () => FargoSoulsWorld.downedDevi, Item.buyPrice(gold: 17, silver: 50));
            AddSummon(14.01f, "AbomsCurse", () => FargoSoulsWorld.downedAbom, 10000000);
            AddSummon(14.02f, "TruffleWormEX", () => FargoSoulsWorld.downedFishronEX, 10000000);
            AddSummon(14.03f, "MutantsCurse", () => FargoSoulsWorld.downedMutant, 20000000);
        }

        public void AddSummon(float value, string itemName, Func<bool> condition, int sellPrice) => ModInstance.Call("AddSummon", value, "FargowiltasSouls", itemName, condition, sellPrice);
    }
}