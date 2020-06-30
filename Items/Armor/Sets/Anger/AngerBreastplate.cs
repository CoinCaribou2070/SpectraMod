using SpectraMod.Items.Boss.GraveRobber;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Anger
{
    [AutoloadEquip(EquipType.Body)]
    public class AngerBreastplate : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderBreastplate";

        public override void SetStaticDefaults() => Tooltip.SetDefault("3% increased ranged damage");

        public override void SafeSetDefaults()
        {
            item.value = (15 *(40 * 100) +  15 *(1 * 100)) * 5;
            item.defense = 6;
            base.SafeSetDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.03f;
            base.UpdateEquip(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ModContent.ItemType<HatredBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}
