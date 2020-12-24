using Fargowiltas.Items.Summons.Deviantt;
using FargowiltasSouls.Items.Misc;
using FargowiltasSouls.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public partial class Fargowiltas : Mod
    {
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SoulofLight, 7);
            recipe.AddIngredient(ItemID.SoulofNight, 7);
            recipe.AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<JungleChest>());
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WizardHat);
            recipe.AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<RuneOrb>());
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<HeartChocolate>());
            recipe.AddRecipe();

            /*recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBonesBanner", 2);
            recipe.AddIngredient(ModContent.ItemType<Items.Misc.DeviatingEnergy>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ModContent.ItemType<InnocuousSkull>());
            recipe.AddRecipe();*/
        }

        public override void AddRecipeGroups()
        {
            // Drax
            FargoUtilities.CreateRecipeGroup("AnyDrax", this, true, ItemID.Drax, ItemID.PickaxeAxe);

            // Dungeon enemies
            FargoUtilities.CreateRecipeGroup("AnyBonesBanner", this, true, ItemID.AngryBonesBanner, ItemID.BlueArmoredBonesBanner, ItemID.HellArmoredBonesBanner, ItemID.RustyArmoredBonesBanner);

            // Cobalt
            FargoUtilities.CreateRecipeGroup("AnyCobaltRepeater", this, true, ItemID.CobaltRepeater, ItemID.PalladiumRepeater);

            // Mythril
            FargoUtilities.CreateRecipeGroup("AnyMythrilRepeater", this, true, ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);

            // Adamantite
            FargoUtilities.CreateRecipeGroup("AnyAdamantiteRepeater", this, true, ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);

            // Evil wood
            FargoUtilities.CreateRecipeGroup("AnyEvilWood", this, true, ItemID.Ebonwood, ItemID.Shadewood);

            // Any adamantite
            FargoUtilities.CreateRecipeGroup("AnyAdamantite", this, true, ItemID.AdamantiteBar, ItemID.TitaniumBar);

            // Shroomite head
            FargoUtilities.CreateRecipeGroup("AnyShroomHead", this, true, ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);

            // Orichalcum head
            FargoUtilities.CreateRecipeGroup("AnyOriHead", this, true, ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);

            // Palladium head
            FargoUtilities.CreateRecipeGroup("AnyPallaHead", this, true, ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);

            // Cobalt head
            FargoUtilities.CreateRecipeGroup("AnyCobaltHead", this, true, ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);

            // Mythril head
            FargoUtilities.CreateRecipeGroup("AnyMythrilHead", this, true, ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);

            // Titanium head
            FargoUtilities.CreateRecipeGroup("AnyTitaHead", this, true, ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);

            // Hallowed head
            FargoUtilities.CreateRecipeGroup("AnyHallowHead", this, true, ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);

            // Adamantite head
            FargoUtilities.CreateRecipeGroup("AnyAdamHead", this, true, ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);

            // Chloro head
            FargoUtilities.CreateRecipeGroup("AnyChloroHead", this, true, ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);

            // Spectre head
            FargoUtilities.CreateRecipeGroup("AnySpectreHead", this, true, ItemID.SpectreHood, ItemID.SpectreMask);

            // Beetle body
            FargoUtilities.CreateRecipeGroup("AnyBeetle", this, true, ItemID.BeetleShell, ItemID.BeetleScaleMail);

            // Phasesabers
            FargoUtilities.CreateRecipeGroup("AnyPhasesaber", this, true, ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber);

            // Vanilla butterflies
            FargoUtilities.CreateRecipeGroup("AnyButterfly", this, true, ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly,
                ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);

            // Vanilla squirrels
            FargoUtilities.CreateRecipeGroup("AnySquirrel", this, true, ItemID.Squirrel, ItemID.SquirrelRed);

            // Vanilla squirrels
            FargoUtilities.CreateRecipeGroup("AnyCommonFish", this, true, ItemID.AtlanticCod, ItemID.Bass, ItemID.Trout, ItemID.RedSnapper, ItemID.Salmon, ItemID.Tuna);

            // Vanilla birds
            FargoUtilities.CreateRecipeGroup("AnyBird", this, true, ItemID.Bird, ItemID.BlueJay, ItemID.Cardinal, ItemID.GoldBird, ItemID.Duck, ItemID.MallardDuck);

            // Vanilla scorpions
            FargoUtilities.CreateRecipeGroup("AnyScorpion", this, true, ItemID.Scorpion, ItemID.BlackScorpion);

            // Gold pickaxe
            FargoUtilities.CreateRecipeGroup("AnyGoldPickaxe", this, true, ItemID.GoldPickaxe, ItemID.PlatinumPickaxe);

            // Fishing trash
            FargoUtilities.CreateRecipeGroup("AnyFishingTrash", this, true, ItemID.OldShoe, ItemID.TinCan, ItemID.FishingSeaweed);
        }
    }
}