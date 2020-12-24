using FargowiltasSouls.NPCs;
using FargowiltasSouls.NPCs.AbomBoss;
using FargowiltasSouls.NPCs.DeviBoss;
using FargowiltasSouls.NPCs.MutantBoss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public partial class Fargowiltas : Mod
    {
        public override object Call(params object[] args)
        {
            // Convert everything to lowercase so we only have to check for singular keys
            string code = args[0].ToString().ToLower();

            switch (code)
            {
                case "masomode":
                case "eternitymode":
                case "emode":
                    return FargoSoulsWorld.MasochistMode;

                case "downedmutant":
                    return FargoSoulsWorld.DownedMutant;

                case "downedabom":
                case "downedabomination":
                case "downedabominationn":
                    return FargoSoulsWorld.DownedAbom;

                case "downeddevi":
                case "downeddeviant":
                case "downeddeviantt":
                    return FargoSoulsWorld.DownedDevi;

                case "downedfishronex":
                case "downedukeex":
                case "downeddukefishronex":
                    return FargoSoulsWorld.DownedFishronEX;

                case "pureheart":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().PureHeart;

                case "mutantantibodies":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantAntibodies;

                case "sinistericon":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().SinisterIcon;

                case "abomalive":
                case "abominationalive":
                case "abominationnalive":
                    return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<AbomBoss>());

                case "mutantalive":
                    return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>());

                case "devialive":
                case "deviantalive":
                case "devianttalive":
                    return EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<DeviBoss>());

                case "mutantpact":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantsPact;

                case "mutantdiscountcard":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().MutantsDiscountCard;

                /*case "DevianttGifts":

                    Player player = Main.LocalPlayer;
                    FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

                    if (!fargoPlayer.ReceivedMasoGift)
                    {
                        fargoPlayer.ReceivedMasoGift = true;
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            DropDevianttsGift(player);
                        }
                        else if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            var netMessage = GetPacket(); // Broadcast item request to server
                            netMessage.Write((byte)14);
                            netMessage.Write((byte)player.whoAmI);
                            netMessage.Send();
                        }

                        return true;
                    }

                    break;*/

                case "giftsreceived":
                    return Main.LocalPlayer.GetModPlayer<FargoPlayer>().ReceivedMasoGift;

                case "givedevianttgifts":
                    Player player = Main.LocalPlayer;

                    player.GetModPlayer<FargoPlayer>().ReceivedMasoGift = true;

                    if (Main.netMode == NetmodeID.SinglePlayer)
                        DropDevianttsGift(player);
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket netMessage = GetPacket(); // Broadcast item request to server.
                        netMessage.Write((byte)14);
                        netMessage.Write((byte)player.whoAmI);
                        netMessage.Send();
                    }

                    // Main.npcChatText = "This world looks tougher than usual, so you can have these on the house just this once! Talk to me if you need any tips, yeah?";
                    break;
            }

            return base.Call(args);
        }
    }
}