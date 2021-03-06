﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FargowiltasSouls.Buffs.Minions
{
    public class JungleMimicSummonBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Jungle Mimic");
            Description.SetDefault("The Jungle Mimic will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("JungleMimicSummon")] > 0)
            {
                player.buffTime[buffIndex] = 2;
            }
        }
    }
}