using Terraria;
using Terraria.ID;

namespace SpectraMod.Items.Boss.GraveRobber
{
    public class UnluckyTomb : SpectraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unlucky Gravestone");
            Tooltip.SetDefault("'The robber's downfall...'" +
                               "\nZombies and demon eyes become friendly");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 6, 6, 6);
            item.accessory = true;
            item.rare = ItemRarityID.White;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SpectraPlayer>().UnluckyTombEffect = true;
        }
    }
}