﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class HappyHat : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovely Hat");
            Tooltip.SetDefault("Increases movement speed" +
                               "\nGives the happy buff, even when in vanity");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.defense += 3;
            item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Sunflower, 60);
            player.moveSpeed += 0.20f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.Bars.DelightedBar>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}