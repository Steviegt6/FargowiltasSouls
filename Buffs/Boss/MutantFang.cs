using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Buffs.Boss
{
    public class MutantFang : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mutant Fang");
            Description.SetDefault("The power of Eternity Mode compels you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            player.poisoned = true;
            player.venom = true;
            player.ichor = true;
            player.onFire2 = true;
            player.electrified = true;
            //fargoPlayer.OceanicMaul = true;
            fargoPlayer.CurseoftheMoon = true;
            if (fargoPlayer.FirstInfection)
            {
                fargoPlayer.MaxInfestTime = player.buffTime[buffIndex];
                fargoPlayer.FirstInfection = false;
            }
            fargoPlayer.Infested = true;
            fargoPlayer.Rotting = true;
            fargoPlayer.MutantNibble = true;
            fargoPlayer.noDodge = true;
            fargoPlayer.noSupersonic = true;
            fargoPlayer.MutantPresence = true;
            player.moonLeech = true;
            player.potionDelay = player.buffTime[buffIndex];
            if (Fargowiltas.Instance.MasomodeEXLoaded && !FargoSoulsWorld.DownedFishronEX && player.buffTime[buffIndex] > 1
                && EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.mutantBoss, mod.NPCType("MutantBoss")))
            {
                player.AddBuff(ModLoader.GetMod("MasomodeEX").BuffType("MutantJudgement"), player.buffTime[buffIndex]);
                player.buffTime[buffIndex] = 1;
            }
        }
    }
}