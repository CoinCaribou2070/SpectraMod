﻿using Terraria;
using Terraria.ID;
using SpectraMod.Projectiles.Weapons.Prism.Harvest;
using static Terraria.ModLoader.ModContent;

namespace SpectraMod.Items.ProModeItems.HardMode
{
    public class HarvestCrystal : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fire a beam of spooky energy");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.expert = true;
            professional = true;

            item.CloneDefaults(ItemID.LastPrism);
            item.magic = true;
            item.mana = 14;
            item.damage = 63;
            item.crit = 3;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 0;
            item.shoot = ProjectileType<HarvestHoldout>();
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ProjectileType<HarvestHoldout>()] <= 0;
    }
}
