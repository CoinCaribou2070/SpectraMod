using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpectraMod.Items.Boss.GraveRobber;

namespace SpectraMod.Items.Weapons.Sets.Hatred
{
    public class HatredBow : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Bow");
            Tooltip.SetDefault("Strike with the bow of vengeance!");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 5, 60, 0);
            item.rare = ItemRarityID.Orange;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 19;
            item.ranged = true;
            item.knockBack = 6;
            item.useTime = 17;
            item.useAnimation = 17;
            item.autoReuse = true;
            item.UseSound = SoundID.Item5;
            item.useAmmo = AmmoID.Arrow;
            item.noMelee = true;
            item.shoot = 10;
            item.shootSpeed = 9f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<Projectiles.Ammo.HatredArrowPro>();
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<HatredBar>(), 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
