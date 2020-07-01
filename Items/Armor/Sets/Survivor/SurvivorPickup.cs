using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    public class SurvivorPickup : SpectraItem
    {
        public override string Texture => "SpectraMod/Projectiles/Blank";

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            for (int i = 0; i < 2; i++) Dust.NewDust(item.position, 1, 1, DustID.Blood);
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            if (player.statLife == player.statLifeMax2)
            {
                grabRange = 0;
                return;
            }

            if (player.GetModPlayer<SpectraPlayer>().SurvivorSetBonus)
            {
                grabRange += 28;

                if (player.statLife < player.statLifeMax2 / 2)
                {
                    grabRange += 32;
                }
            }
        }

        public override bool OnPickup(Player player)
        {
            player.HealEffect(Main.rand.Next(4, 6));
            return false;
        }
    }
}
