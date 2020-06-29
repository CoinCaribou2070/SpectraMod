using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Projectiles.Weapons.Prism.Death
{
    public class DeathBeam : PrismBeam
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrismLaser;

        protected override float BeamColorHue()
        {
            string name = Main.player[projectile.owner].name;
            switch (name)
            {
                case "Oreo":
                case "OreoV2":
                    return 0.7f;

                case "Diagnostic":
                case "DiagnosticLord":
                    return Main.rgbToHsl(new AnimatedColor(Color.Green, Color.GreenYellow).GetColor()).X;

                case "pbone":
                    //TD: Decide colour (mixture?)
                    //return Main.rgbToHsl(new AnimatedColor(Color.SeaGreen, Color.Green).GetColor()).X;
                    return Main.rgbToHsl(Main.DiscoColor).X;

                case "Stevie":
                    if (Main.rgbToHsl(new AnimatedColor(Color.Red, Color.Orange).GetColor()).X < 0.005f)
                        return 0.005f;
                    return Main.rgbToHsl(new AnimatedColor(Color.Red, Color.Orange).GetColor()).X;

                case "Martin":
                    return Main.rgbToHsl(new AnimatedColor(new Color(46, 43, 226), new Color(255, 0, 0)).GetColor()).X;

                case "CoolDoge":
                    Main.NewText("D");
                    return Main.rgbToHsl(Color.Black).X;

                default:
                    return 0f;
            }
        }

        protected override float BeamColorLightness() => BeamColorHue() == 0f ? 0f : 0.3f;

        protected override float BeamColorSaturation() => 0.5f;

        protected override float BeamHitboxCollisionWidth() => 25f;

        protected override float BeamHueVariance() => 0.01f;

        protected override float BeamLightBrightness() => 2f;

        protected override int HitCD() => 0;

        protected override float MaxBeamScale() => 2f;

        protected override float MaxBeamSpread() => 1.5f;

        protected override float MaxDamageMultiplier() => 50f;

        protected override PrismHoldout Parent() => new DeathHoldout();

        protected override int ParentType() => ModContent.ProjectileType<DeathHoldout>();

        public override Color GetInnerBeamColor()
        {
            float h = BeamColorHue();
            return h == 0f && Main.player[projectile.owner].name != "CoolDoge" ? Color.Red : h == Main.rgbToHsl(Color.Black).X ? Color.Black : Color.White;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }

        public override float PerformBeamHitscan(Projectile prism, bool fullCharge)
        {
            return 2400f;
        }
    }
}
