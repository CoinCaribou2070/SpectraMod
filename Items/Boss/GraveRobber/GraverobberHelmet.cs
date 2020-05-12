﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.Boss.GraveRobber
{
    [AutoloadEquip(EquipType.Head)]
    public class GraverobberHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grave Robber's HelmS");
            Tooltip.SetDefault("33% more damage and crit chance" +
                               "\nYou are hated");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(26, 24);
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.defense = 5;
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.33f;
            player.meleeCrit += 33;
            player.magicCrit += 33;
            player.rangedCrit += 33;
            player.thrownCrit += 33;
        }
    }
}
