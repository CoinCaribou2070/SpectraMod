using Microsoft.Xna.Framework;
using System;
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
                    return Main.rgbToHsl(Main.DiscoColor).X;

                case "Stevie":
                    return Main.rgbToHsl(
                        new AnimatedColor(
                            new AnimatedColor(Color.Yellow, Color.Pink).GetColor(),
                            new AnimatedColor(
                                new AnimatedColor(Color.Red, Color.Blue).GetColor(), Color.Transparent).GetColor()).GetColor()
                    ).X;

                case "Martin":
                    return Main.rgbToHsl((Main.GameUpdateCount % 100) > 50 ? new Color(184, 56, 59) : new Color(88, 133, 162)).X;

                case "CoolDoge":
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
