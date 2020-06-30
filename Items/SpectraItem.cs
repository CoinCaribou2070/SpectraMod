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
        CustomRarity CustomRare;

        /// <summary>
        /// Use for animated items
        /// </summary>
        public bool ignoreAutoSize = false;

        public bool professional = false;

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            //Texture2D texture = ModContent.GetTexture(item.modItem.Texture);
            Texture2D texture = Main.itemTexture[item.type];

            CustomRare = CustomRarity.None;
            if (SpectraMod.SizeFix && !ignoreAutoSize) item.Size = texture.Size();
            SafeSetDefaults();
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
                }
                name.overrideColor = customColor;
            }

            Main.NewText(professional);
            TooltipLine expert = tooltips.FirstOrDefault((TooltipLine t) => t.Name == "Expert" && t.mod == "Terraria");
            if (expert != null)
            {
                expert.text = "Professional";
            }
        }
    }

    public enum CustomRarity : byte
    {
        None
    }
}
