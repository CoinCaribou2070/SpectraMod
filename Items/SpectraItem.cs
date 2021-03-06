using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria;
using System.Security.Authentication.ExtendedProtection;

namespace SpectraMod.Items
{
    public abstract class SpectraItem : ModItem
    {
        public virtual CustomRarity CustomRare => CustomRarity.None;

        /// <summary>
        /// Use for animated items
        /// </summary>
        public virtual bool ignoreAutoSize() => false;

        public virtual bool professional() => false;

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();

            //Texture2D texture = ModContent.GetTexture(item.modItem.Texture);
            Texture2D texture = Main.itemTexture[item.type];

            if (SpectraMod.SizeFix && !ignoreAutoSize()) item.Size = texture.Size();

            if (professional())
            {
                item.rare = -13;
                item.expert = false; //expert messes with my rarity code
            }
        }

        public virtual void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
        }

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SafeModifyTooltips(tooltips);

            TooltipLine name = tooltips.FirstOrDefault((TooltipLine t) => t.Name == "ItemName" && t.mod == "Terraria");
            if (name != null)
            {
                Color? customColor = new Color();
                switch (CustomRare)
                {
                    case CustomRarity.None:
                        customColor = null;
                        break;
                    case CustomRarity.Professional:
                        customColor = new AnimatedColor(Color.Red, Color.Yellow).GetColor();
                        break;
                }
                name.overrideColor = customColor;
            }

            if (professional())
                tooltips.Add(new TooltipLine(mod, "Spectra:Professional", "Professional"));
        }
    }

    public enum CustomRarity : byte
    {
        None,
        Professional
    }
}
