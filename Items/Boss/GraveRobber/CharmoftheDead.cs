using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class CharmoftheDead : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of the Undead");
            Tooltip.SetDefault("'Anything but charming.'" +
                               "\nCommon zombies become passive");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.accessory = true;
            item.rare = ItemRarityID.White;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SpectraPlayer>().CharmoftheDeadEffect = true;
        }
    }
}