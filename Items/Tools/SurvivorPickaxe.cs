using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Tools
{
    public class SurvivorPickaxe : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Pickaxe");
            Tooltip.SetDefault("The pickaxe of doom" +
                             "\nMakes you immune to lava while held");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 16;
            item.melee = true;
            item.knockBack = 2;
            item.useTime = 9;
            item.useAnimation = 9;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.pick = 105;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem == item)
            {
                player.lavaImmune = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Bars.SurviveBar>());
            recipe.AddRecipeGroup("Spectra:EvilPick");
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddIngredient(ItemID.BonePickaxe);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
