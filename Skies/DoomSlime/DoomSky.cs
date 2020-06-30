using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SpectraMod.Skies.DoomSlime
{
    public class DoomSky : CustomSky
    {
        public bool active;
        private float fadeIn;

        public override void Activate(Vector2 position, params object[] args)
        {
            active = true;
        }

        public override void Deactivate(params object[] args)
        {
            //texture.Dispose();
            //overlay.Dispose();
            active = false;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black);
            }

        }

        public override bool IsActive()
        {
            return active || fadeIn > 0f;
        }

        public override void Reset()
        {
            active = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (active && fadeIn < 1f)
                fadeIn += 0.05f;
            if (!active && fadeIn > 0f)
                fadeIn -= 0.1f;
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override Color OnTileColor(Color inColor)
        {
            return Color.Lerp(Color.Black, inColor, 0.75f);
        }
    }
}
