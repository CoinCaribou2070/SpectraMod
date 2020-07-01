using Terraria;
using Terraria.ID;
using SpectraMod.Projectiles.Weapons.Prism.Harvest;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectraMod.Items.ProModeItems.HardMode
{
    public class HarvestCrystal : SpectraItem
    {
        public override bool professional() => true;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fire a beam of spooky energy");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.expert = true;

            item.value = Item.sellPrice(0, 25, 0, 0);
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

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (!Main.dayTime)
            {
                Texture2D texture = Main.itemTexture[item.type];
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);

                for (int i = 0; i < 4; i++)
                {
                    Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                    spriteBatch.Draw(texture, position + offsetPositon, null, new AnimatedColor(Color.Orange, Color.Yellow).GetColor(), rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }

        /*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);

            spriteBatch.Draw(texture, position, null, Color.White, rotation, texture.Size(), scale, SpriteEffects.None, 0f);
        }*/

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ProjectileType<HarvestHoldout>()] <= 0;
    }
}
