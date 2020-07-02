using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    [AutoloadEquip(EquipType.Legs)]
    public class SurvivorLeggings : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderLeggings";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Leggings");
            Tooltip.SetDefault("'I will survive!'" +
                             "\n4% increased critical strike chance" +
                             "\n20% increased movement speed");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 4;
            player.meleeCrit += 4;
            player.rangedCrit += 4;
            player.thrownCrit += 4;

            player.moveSpeed += 0.2f;
        }
    }
}
