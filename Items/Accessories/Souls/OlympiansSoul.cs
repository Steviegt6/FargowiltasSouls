using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class OlympiansSoul : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olympian's Soul");

            string tooltip =
@"30% increased throwing damage
20% increased throwing speed
15% increased throwing critical chance and velocity
'Strike with deadly precision'";
            string tooltip_ch =
@"'致命的精准打击'
增加30%投掷伤害
增加20%投掷速度
增加15%投掷暴击率和抛射物速度";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "奥林匹斯之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(85, 5, 230));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //throw speed
            player.GetModPlayer<FargoPlayer>().ThrowSoul = true;
            //player.thrownDamage += 0.3f;
            //player.thrownCrit += 15;
            //player.thrownVelocity += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SlingersEssence");

            /*
            else
            {
                recipe.AddIngredient(fargos.ItemType("MagicDaggerThrown"));
                recipe.AddIngredient(fargos.ItemType("BananarangThrown"), 5);
                recipe.AddIngredient(fargos.ItemType("AmarokThrown"));
                recipe.AddIngredient(fargos.ItemType("ShadowFlameKnifeThrown"));
                recipe.AddIngredient(fargos.ItemType("FlyingKnifeThrown"));
                recipe.AddIngredient(fargos.ItemType("LightDiscThrown"), 5);
                recipe.AddIngredient(fargos.ItemType("FlowerPowThrown"));
                recipe.AddIngredient(fargos.ItemType("ToxicFlaskThrown"));
                recipe.AddIngredient(fargos.ItemType("VampireKnivesThrown"));
                recipe.AddIngredient(fargos.ItemType("PaladinsHammerThrown"));
                recipe.AddIngredient(fargos.ItemType("PossessedHatchetThrown"));
                recipe.AddIngredient(fargos.ItemType("TheEyeOfCthulhuThrown"));
                recipe.AddIngredient(fargos.ItemType("TerrarianThrown"));
            }*/

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
