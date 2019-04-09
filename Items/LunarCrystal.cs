using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class LunarCrystal : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Crystal");
            Tooltip.SetDefault("A fragment of the moon's power");
        }

        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = 10;
            item.width = 12;
            item.height = 12;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}