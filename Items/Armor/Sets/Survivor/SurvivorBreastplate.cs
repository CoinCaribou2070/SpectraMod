﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    [AutoloadEquip(EquipType.Body)]
    public class SurvivorBreastplate : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderBreastplate";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'I will survive!'" +
                             "\nCrit damage increased by 10%" +
                             "\nYou take 5% less damage");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            spectraPlayer.CritDamage += 0.1f;
            player.endurance += 0.05f;
        }
    }
}
