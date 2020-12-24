using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FargowiltasSouls
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // lol resharper :nerd:
    public class FargoSoulsWorld : ModWorld
    {
        public static bool SwarmActive => (bool)ModLoader.GetMod("Fargowiltas").Call("SwarmActive");

        public static bool[] downedChampions = new bool[9];
        public static bool DownedBetsy;
        public static bool DownedBoss;
        public const int MaxCountPreHM = 560; // Emode
        public const int MaxCountHM = 240;
        public static bool MasochistMode;
        public static bool DownedFishronEX;
        public static bool DownedDevi;
        public static bool DownedAbom;
        public static bool DownedMutant;
        public static bool AngryMutant;
        public static bool DownedMM;
        public static bool FirstGoblins;
        public static int SkipMutantP1;
        public static bool NoMasoBossScaling = true;
        public static bool ReceivedTerraStorage;

        public override void Initialize()
        {
            DownedBetsy = false;
            DownedBoss = false;
            DownedMM = false;
            MasochistMode = false;
            DownedFishronEX = false;
            DownedDevi = false;
            DownedAbom = false;
            DownedMutant = false;
            AngryMutant = false;
            FirstGoblins = true;
            SkipMutantP1 = 0;
            NoMasoBossScaling = true;
            ReceivedTerraStorage = false;

            for (int i = 0; i < downedChampions.Length; i++)
                downedChampions[i] = false;
        }

        public override TagCompound Save()
        {
            List<string> downed = new List<string>();

            if (DownedBetsy)
                downed.Add("betsy");

            if (DownedBoss)
                downed.Add("boss");

            if (MasochistMode)
                downed.Add("masochist");

            if (DownedFishronEX)
                downed.Add("downedFishronEX");

            if (DownedDevi)
                downed.Add("downedDevi");

            if (DownedAbom)
                downed.Add("downedAbom");

            if (DownedMutant)
                downed.Add("downedMutant");

            if (AngryMutant)
                downed.Add("AngryMutant");

            if (DownedMM)
                downed.Add("downedMadhouse");

            if (FirstGoblins)
                downed.Add("forceMeteor");

            if (NoMasoBossScaling)
                downed.Add("NoMasoBossScaling");

            if (ReceivedTerraStorage)
                downed.Add("ReceivedTerraStorage");

            for (int i = 0; i < downedChampions.Length; i++)
                if (downedChampions[i])
                    downed.Add("downedChampion" + i.ToString());

            return new TagCompound
            {
                { "downed", downed }, { "mutantP1", SkipMutantP1 }
            };
        }

        public override void Load(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>("downed");
            DownedBetsy = downed.Contains("betsy");
            DownedBoss = downed.Contains("boss");
            MasochistMode = downed.Contains("masochist");
            DownedFishronEX = downed.Contains("downedFishronEX");
            DownedDevi = downed.Contains("downedDevi");
            DownedAbom = downed.Contains("downedAbom");
            DownedMutant = downed.Contains("downedMutant");
            AngryMutant = downed.Contains("AngryMutant");
            DownedMM = downed.Contains("downedMadhouse");
            FirstGoblins = downed.Contains("forceMeteor");
            NoMasoBossScaling = downed.Contains("NoMasoBossScaling");
            ReceivedTerraStorage = downed.Contains("ReceivedTerraStorage");

            for (int i = 0; i < downedChampions.Length; i++)
                downedChampions[i] = downed.Contains("downedChampion" + i.ToString());

            if (tag.ContainsKey("mutantP1"))
                SkipMutantP1 = tag.GetAsInt("mutantP1");
        }

        public override void NetReceive(BinaryReader reader)
        {
            SkipMutantP1 = reader.ReadInt32();

            BitsByte flags = reader.ReadByte();
            DownedBetsy = flags[0];
            DownedBoss = flags[1];
            MasochistMode = flags[2];
            DownedFishronEX = flags[3];
            DownedDevi = flags[4];
            DownedAbom = flags[5];
            DownedMutant = flags[6];
            AngryMutant = flags[7];
            DownedMM = flags[8];
            FirstGoblins = flags[9];
            NoMasoBossScaling = flags[10];
            ReceivedTerraStorage = flags[11];

            for (int i = 0; i < downedChampions.Length; i++)
                downedChampions[i] = flags[i + 12];
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(SkipMutantP1);

            BitsByte flags = new BitsByte
            {
                [0] = DownedBetsy,
                [1] = DownedBoss,
                [2] = MasochistMode,
                [3] = DownedFishronEX,
                [4] = DownedDevi,
                [5] = DownedAbom,
                [6] = DownedMutant,
                [7] = AngryMutant,
                [8] = DownedMM,
                [9] = FirstGoblins,
                [10] = NoMasoBossScaling,
                [11] = ReceivedTerraStorage,
                [12] = downedChampions[0],
                [13] = downedChampions[1],
                [14] = downedChampions[2],
                [15] = downedChampions[3],
                [16] = downedChampions[4],
                [17] = downedChampions[5],
                [18] = downedChampions[6],
                [19] = downedChampions[7],
                [20] = downedChampions[8]
            };

            writer.Write(flags);
        }

        public override void PostUpdate()
        {
            if (MasochistMode)
            {
                if (!Main.expertMode)
                    MasochistMode = false;

                if (!NPC.downedSlimeKing && !NPC.downedBoss1 && !Main.hardMode //pre boss, disable rain and sandstorm
                    && !NPC.AnyNPCs(ModLoader.GetMod("Fargowiltas").NPCType("Abominationn")))
                {
                    Main.raining = false;
                    Sandstorm.Happening = false;
                    Sandstorm.TimeLeft = 0;
                }
            }

            //Main.NewText(BuilderMode);

            #region commented

            //right when day starts
            /*if(/*Main.time == 0 && Main.dayTime && !Main.eclipse && FargoSoulsWorld.masochistMode)
			{
					Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);

					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.eclipse = true;
						//Main.NewText(Lang.misc[20], 50, 255, 130, false);
					}
					else
					{
						//NetMessage.SendData(61, -1, -1, "", player.whoAmI, -6f, 0f, 0f, 0, 0, 0);
					}
			}

            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 361 && Main.CanStartInvasion(1, true))
            {
                this.itemTime = item.useTime;
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.invasionType == 0)
                    {
                        Main.invasionDelay = 0;
                        Main.StartInvasion(1);
                    }
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 602 && Main.CanStartInvasion(2, true))
            {
                this.itemTime = item.useTime;
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.invasionType == 0)
                    {
                        Main.invasionDelay = 0;
                        Main.StartInvasion(2);
                    }
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -2f, 0f, 0f, 0, 0, 0);
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1315 && Main.CanStartInvasion(3, true))
            {
                this.itemTime = item.useTime;
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.invasionType == 0)
                    {
                        Main.invasionDelay = 0;
                        Main.StartInvasion(3);
                    }
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -3f, 0f, 0f, 0, 0, 0);
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1844 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon && !DD2Event.Ongoing)
            {
                this.itemTime = item.useTime;
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.NewText(Lang.misc[31], 50, 255, 130, false);
                    Main.startPumpkinMoon();
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -4f, 0f, 0f, 0, 0, 0);
                }
            }

            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 3601 && NPC.downedGolemBoss && Main.hardMode && !NPC.AnyDanger() && !NPC.AnyoneNearCultists())
            {
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                this.itemTime = item.useTime;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    WorldGen.StartImpendingDoom();
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -8f, 0f, 0f, 0, 0, 0);
                }
            }
            if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1958 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon && !DD2Event.Ongoing)
            {
                this.itemTime = item.useTime;
                Main.PlaySound(SoundID.Roar, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.NewText(Lang.misc[34], 50, 255, 130, false);
                    Main.startSnowMoon();
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, "", this.whoAmI, -5f, 0f, 0f, 0, 0, 0);
                }
            }*/

            #endregion commented
        }

        public override void PostWorldGen()
        {
            /*WorldGen.PlaceTile(Main.spawnTileX - 1, Main.spawnTileY, TileID.GrayBrick, false, true);
            WorldGen.PlaceTile(Main.spawnTileX, Main.spawnTileY, TileID.GrayBrick, false, true);
            WorldGen.PlaceTile(Main.spawnTileX + 1, Main.spawnTileY, TileID.GrayBrick, false, true);
            Main.tile[Main.spawnTileX - 1, Main.spawnTileY].slope(0);
            Main.tile[Main.spawnTileX, Main.spawnTileY].slope(0);
            Main.tile[Main.spawnTileX + 1, Main.spawnTileY].slope(0);
            WorldGen.PlaceTile(Main.spawnTileX, Main.spawnTileY - 1, ModLoader.GetMod("Fargowiltas").TileType("RegalStatueSheet"), false, true);*/

            int positionX = Main.spawnTileX - 1; // Offset by dimensions of statue
            int positionY = Main.spawnTileY - 4;
            bool placed = false;
            List<int> legalBlocks = new List<int>
            {
                TileID.Stone,
                TileID.Grass,
                TileID.Dirt,
                TileID.SnowBlock,
                TileID.IceBlock,
                TileID.ClayBlock
            };

            for (int offsetX = -50; offsetX <= 50; offsetX++)
            {
                for (int offsetY = -30; offsetY <= 10; offsetY++)
                {
                    int baseCheckX = positionX + offsetX;
                    int baseCheckY = positionY + offsetY;
                    bool canPlaceStatueHere = true;

                    for (int i = 0; i < 3; i++) //check no obstructing blocks
                        for (int j = 0; j < 4; j++)
                        {
                            Tile tile = Framing.GetTileSafely(baseCheckX + i, baseCheckY + j);

                            if (WorldGen.SolidOrSlopedTile(tile))
                            {
                                canPlaceStatueHere = false;
                                break;
                            }
                        }

                    for (int i = 0; i < 3; i++) //check for solid foundation
                    {
                        Tile tile = Framing.GetTileSafely(baseCheckX + i, baseCheckY + 4);

                        if (!WorldGen.SolidTile(tile) || !legalBlocks.Contains(tile.type))
                        {
                            canPlaceStatueHere = false;
                            break;
                        }
                    }

                    if (canPlaceStatueHere)
                    {
                        for (int i = 0; i < 3; i++) //MAKE SURE nothing in the way
                            for (int j = 0; j < 4; j++)
                                WorldGen.KillTile(baseCheckX + i, baseCheckY + j);

                        WorldGen.PlaceTile(baseCheckX, baseCheckY + 4, TileID.GrayBrick, false, true);
                        WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 4, TileID.GrayBrick, false, true);
                        WorldGen.PlaceTile(baseCheckX + 2, baseCheckY + 4, TileID.GrayBrick, false, true);
                        Main.tile[baseCheckX, baseCheckY + 4].slope(0);
                        Main.tile[baseCheckX + 1, baseCheckY + 4].slope(0);
                        Main.tile[baseCheckX + 2, baseCheckY + 4].slope(0);
                        WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 3, mod.TileType("MutantStatueGift"), false, true);

                        placed = true;
                        break;
                    }
                }

                if (placed)
                    break;
            }
        }
    }
}