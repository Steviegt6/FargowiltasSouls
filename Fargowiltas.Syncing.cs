using FargowiltasSouls.NPCs;
using FargowiltasSouls.NPCs.EternityMode;
using FargowiltasSouls.Utilities;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public partial class Fargowiltas : Mod
    {
        public enum FargoPacket : byte
        {
            SpawnCreepers,
            SyncPillarData,
            NetUpdateEMode,
            AssortedSync,
            MLVulnerSync,
            RetinazerLaserSync,
            SharkSync,
            DarkCasterFamily,
            ResetCounter,
            RequestHeartSpawn,
            SyncCultistData,
            RefreshCreepers,
            PrimeLimbsSpin,
            PrimeLimbsSwipe,
            DeviGifts,
            SyncCounterArray,
            RequestItem,
            SpawnItem,

            SpawnFishEX = 77,
            ConfirmFishEXMaxLife = 78
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch ((FargoPacket)reader.ReadByte())
            {
                case FargoPacket.SpawnCreepers:
                    SpawnCreepers(reader);
                    break;

                case FargoPacket.SyncPillarData: // Server-side synchronize pillar data request.
                    SyncPillarData(reader);
                    break;

                case FargoPacket.NetUpdateEMode:
                    NetUpdateEMode(reader);
                    break;

                case FargoPacket.AssortedSync: // Rainbow Slime / Paladin, MP clients syncing to server
                    AssortedSync(reader);
                    break;

                case FargoPacket.MLVulnerSync:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>().Counter[0] = reader.ReadInt32(); // ReadByte: NPC's whoAmI
                        EModeGlobalNPC.masoStateML = reader.ReadByte();
                    }
                    break;

                case FargoPacket.RetinazerLaserSync:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NPC npc = Main.npc[reader.ReadByte()]; // NPC's whoAmI
                        npc.GetGlobalNPC<EModeGlobalNPC>().masoBool[2] = reader.ReadBoolean();
                        npc.GetGlobalNPC<EModeGlobalNPC>().Counter[0] = reader.ReadInt32();
                    }
                    break;

                case FargoPacket.SharkSync:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>().SharkCount = reader.ReadByte(); // First ReadByte is the NPC's whoAmI
                    break;

                case FargoPacket.DarkCasterFamily:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NPC npc = Main.npc[reader.ReadByte()]; // ReadByte: NPC's whoAmI

                        if (npc.GetGlobalNPC<EModeGlobalNPC>().Counter[1] == 0)
                            npc.GetGlobalNPC<EModeGlobalNPC>().Counter[1] = reader.ReadInt32();
                    }
                    break;

                case FargoPacket.ResetCounter:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>().Counter[1] = 0; // ReadByte: NPC's whoAmI
                    break;

                case FargoPacket.RequestHeartSpawn:
                    if (Main.netMode == NetmodeID.Server)
                        Item.NewItem(Main.npc[reader.ReadByte()].Hitbox, ItemID.Heart); // ReadByte: NPC's whoAmI
                    break;

                case FargoPacket.SyncCultistData:
                    SyncCultistData(reader);
                    break;

                case FargoPacket.RefreshCreepers:
                    RefreshCreepers(reader);
                    break;

                case FargoPacket.PrimeLimbsSpin:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NPC npc = Main.npc[reader.ReadByte()];
                        EModeGlobalNPC eModeNPC = npc.GetGlobalNPC<EModeGlobalNPC>();
                        eModeNPC.masoBool[2] = reader.ReadBoolean();
                        eModeNPC.Counter[0] = reader.ReadInt32();
                        npc.localAI[3] = reader.ReadSingle();
                    }
                    break;

                case FargoPacket.PrimeLimbsSwipe:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NPC npc = Main.npc[reader.ReadByte()];
                        EModeGlobalNPC eModeNPC = npc.GetGlobalNPC<EModeGlobalNPC>();
                        eModeNPC.Counter[0] = reader.ReadInt32();
                        eModeNPC.Counter[1] = reader.ReadInt32();
                    }
                    break;

                case FargoPacket.DeviGifts:
                    if (Main.netMode == NetmodeID.Server)
                        DropDevianttsGift(Main.player[reader.ReadByte()]);
                    break;

                case FargoPacket.SyncCounterArray:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        EModeGlobalNPC eModeNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>();

                        for (int i = 0; i < eModeNPC.Counter.Length; i++)
                            eModeNPC.Counter[i] = reader.ReadInt32();
                    }
                    break;

                case FargoPacket.RequestItem:
                    RequestItem(reader);
                    break;

                case FargoPacket.SpawnItem:
                    SpawnItem(reader);
                    break;

                case FargoPacket.SpawnFishEX:
                    SpawnFishEX(reader);
                    break;

                case FargoPacket.ConfirmFishEXMaxLife:
                    Main.npc[reader.ReadInt32()].lifeMax = reader.ReadInt32();
                    break;
            }
        }

        public static void SpawnCreepers(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                Player player = Main.player[reader.ReadByte()]; // ReadByte: player's whoAmI

                NPC npc = Main.npc[NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<CreeperGutted>(), 0,
                    player.whoAmI, 0f, reader.ReadByte(), 0f)]; // ReadByte: Multiplier

                if (npc.whoAmI < 200)
                {
                    npc.velocity = Vector2.UnitX.RotatedByRandom(2 * MathHelper.Pi) * 8;

                    NetMessage.SendData(MessageID.SyncNPC, number: npc.whoAmI);
                }
            }
        }

        public static void SyncPillarData(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NPC npc = Main.npc[reader.ReadByte()]; // ReadByte: NPC's whoAmI

                if (!npc.GetGlobalNPC<EModeGlobalNPC>().masoBool[1])
                {
                    npc.GetGlobalNPC<EModeGlobalNPC>().masoBool[1] = true;
                    npc.GetGlobalNPC<EModeGlobalNPC>().SetDefaults(npc);
                    npc.life = npc.lifeMax;
                }
            }
        }

        public static void NetUpdateEMode(BinaryReader reader)
        {
            // Needs commenting
            EModeGlobalNPC fargoNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<EModeGlobalNPC>();
            fargoNPC.masoBool[0] = reader.ReadBoolean();
            fargoNPC.masoBool[1] = reader.ReadBoolean();
            fargoNPC.masoBool[2] = reader.ReadBoolean();
            fargoNPC.masoBool[3] = reader.ReadBoolean();
            fargoNPC.Counter[0] = reader.ReadInt32();
            fargoNPC.Counter[1] = reader.ReadInt32();
            fargoNPC.Counter[2] = reader.ReadInt32();
            fargoNPC.Counter[3] = reader.ReadInt32();
        }

        public static void AssortedSync(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NPC npc = Main.npc[reader.ReadByte()]; // ReadByte: NPC's whoAmI

                npc.lifeMax = reader.ReadInt32();

                float newScale = reader.ReadSingle();

                npc.position = npc.Center;
                npc.width = (int)(npc.width / npc.scale * newScale);
                npc.height = (int)(npc.height / npc.scale * newScale);
                npc.scale = newScale;
                npc.Center = npc.position;
            }
        }

        public static void SyncCultistData(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NPC npc = Main.npc[reader.ReadByte()]; // ReadByte: NPC's whoAmI
                EModeGlobalNPC cultNPC = npc.GetGlobalNPC<EModeGlobalNPC>();
                cultNPC.Counter[0] += reader.ReadInt32();
                cultNPC.Counter[1] += reader.ReadInt32();
                cultNPC.Counter[2] += reader.ReadInt32();
                npc.localAI[3] += reader.ReadSingle();
            }
        }

        public static void RefreshCreepers(BinaryReader reader)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                byte playerWhoAmI = reader.ReadByte();
                NPC npc = Main.npc[reader.ReadByte()];

                if (npc.active && npc.type == ModContent.NPCType<CreeperGutted>() && npc.ai[0] == playerWhoAmI)
                {
                    int damage = npc.lifeMax - npc.life;
                    npc.life = npc.lifeMax;

                    if (damage > 0)
                        CombatText.NewText(npc.Hitbox, CombatText.HealLife, damage);

                    if (Main.netMode == NetmodeID.Server)
                        npc.netUpdate = true;
                }
            }
        }

        public static void RequestItem(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                int playerWhoAmI = reader.ReadInt32();
                int itemID = reader.ReadInt32();
                int netID = reader.ReadInt32();
                byte prefixID = reader.ReadByte();
                int itemStack = reader.ReadInt32();

                int itemWhoAmI = Item.NewItem(Main.player[playerWhoAmI].Hitbox, itemID, itemStack, true, prefixID);
                Main.itemLockoutTime[itemWhoAmI] = 54000;

                var netMessage = Instance.GetPacket();
                netMessage.Write((byte)17);
                netMessage.Write(playerWhoAmI);
                netMessage.Write(itemID);
                netMessage.Write(netID);
                netMessage.Write(prefixID);
                netMessage.Write(itemStack);
                netMessage.Write(itemWhoAmI);
                netMessage.Send();

                Main.item[itemWhoAmI].active = false;
            }
        }

        public static void SpawnItem(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                int playerWhoAmI = reader.ReadInt32();
                _ = reader.ReadInt32(); // Item type
                int netID = reader.ReadInt32();
                byte prefixID = reader.ReadByte();
                int itemStack = reader.ReadInt32();
                Item item = Main.item[reader.ReadInt32()];

                if (Main.myPlayer == playerWhoAmI)
                {
                    item.netDefaults(netID);

                    item.active = true;
                    item.spawnTime = 0;
                    item.owner = playerWhoAmI;

                    item.Prefix(prefixID);
                    item.stack = itemStack;
                    item.velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                    item.velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                    item.noGrabDelay = 100;
                    item.newAndShiny = false;

                    item.position = Main.player[playerWhoAmI].position;
                    item.position.X += Main.rand.NextFloat(Main.player[playerWhoAmI].Hitbox.Width);
                    item.position.Y += Main.rand.NextFloat(Main.player[playerWhoAmI].Hitbox.Height);
                }
            }
        }

        public static void SpawnFishEX(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                byte target = reader.ReadByte();
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();

                EModeGlobalNPC.spawnFishronEX = true;
                NPC.NewNPC(x, y, NPCID.DukeFishron, Target: target);
                EModeGlobalNPC.spawnFishronEX = false;

                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(FargoUtilities.GetFargoTranslation("SpawnAnnouncements.DukeEX")), new Color(50, 100, 255));
            }
        }
    }
}