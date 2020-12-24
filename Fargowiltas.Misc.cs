using Fargowiltas.Items.Explosives;
using Fargowiltas.Items.Misc;
using FargowiltasSouls.Items.Accessories.Masomode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public partial class Fargowiltas : Mod
    {
        public override void PostUpdateEverything()
        {
            ColorTimer += 0.5f;

            if (ColorTimer > 300f)
                ColorTimer = 0f;
        }

        public static void DropDevianttsGift(Player player)
        {
            player.QuickSpawnItem(ItemID.SilverPickaxe);
            player.QuickSpawnItem(ItemID.SilverAxe);
            player.QuickSpawnItem(ItemID.BugNet);
            player.QuickSpawnItem(ItemID.LifeCrystal, 4);
            player.QuickSpawnItem(ItemID.ManaCrystal, 4);
            player.QuickSpawnItem(ItemID.RecallPotion, 15);

            if (Main.netMode != NetmodeID.SinglePlayer)
                player.QuickSpawnItem(ItemID.WormholePotion, 15);

            player.QuickSpawnItem(ModContent.ItemType<DevianttsSundial>());
            player.QuickSpawnItem(ModContent.ItemType<AutoHouse>(), 3);
            player.QuickSpawnItem(ModContent.ItemType<EurusSock>());
            player.QuickSpawnItem(ModContent.ItemType<PuffInABottle>());

            Mod magicStorage = ModLoader.GetMod("MagicStorage");

            // Should only be given once per world
            if (magicStorage != null && !FargoSoulsWorld.ReceivedTerraStorage)
            {
                player.QuickSpawnItem(magicStorage.ItemType("StorageHeart"));
                player.QuickSpawnItem(magicStorage.ItemType("CraftingAccess"));
                player.QuickSpawnItem(magicStorage.ItemType("StorageUnit"), 16);

                FargoSoulsWorld.ReceivedTerraStorage = true;

                if (Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendData(MessageID.WorldData); // Sync world in multiplayer
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.musicVolume != 0 && Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (MMWorld.MMArmy && priority <= MusicPriority.Environment)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MonsterMadhouse");
                    priority = MusicPriority.Event;
                }

                /*if (FargoSoulsGlobalNPC.BossIsAlive(ref FargoSoulsGlobalNPC.mutantBoss, ModContent.NPCType<NPCs.MutantBoss.MutantBoss>())
                    && Main.player[Main.myPlayer].Distance(Main.npc[FargoSoulsGlobalNPC.mutantBoss].Center) < 3000)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/SteelRed");
                    priority = (MusicPriority)12;
                }*/
            }
        }
    }
}