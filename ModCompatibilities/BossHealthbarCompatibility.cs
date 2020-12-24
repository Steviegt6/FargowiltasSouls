using FargowiltasSouls.NPCs.Champions;
using FargowiltasSouls.NPCs.EternityMode;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public class BossHealthbarCompatibility : ModCompatibility
    {
        public List<int> MiniBarNPCs = new List<int>
        {
            ModContent.NPCType<BabyGuardian>(),
            ModContent.NPCType<TimberChampion>(),
            ModContent.NPCType<EarthChampion>(),
            ModContent.NPCType<LifeChampion>(),
            ModContent.NPCType<WillChampion>(),
            ModContent.NPCType<ShadowChampion>(),
            ModContent.NPCType<SpiritChampion>(),
            ModContent.NPCType<TerraChampion>(),
            ModContent.NPCType<NatureChampion>()
        };

        public BossHealthbarCompatibility(Mod callerMod) : base(callerMod, "FKBossHealthBar")
        {
        }

        public override void Load()
        {
            if (ModInstance != null)
                foreach (int npc in MiniBarNPCs)
                    RegisterHealthMinibar(npc);
        }

        public void RegisterHealthMinibar(int npc) => ModInstance.Call("RegisterHealthBarMini", npc);
    }
}