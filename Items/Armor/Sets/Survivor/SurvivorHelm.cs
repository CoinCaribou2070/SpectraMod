using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    [AutoloadEquip(EquipType.Head)]
    public class SurvivorHelm : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'I will survive!'" +
                             "\n6% increased melee damage" +
                             "\n3% increased melee crit chance" +
                             "\n12% increased melee speed" +
                             "\nMelee attacks inflict On Fire!");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.06f;
            player.meleeCrit += 3;
            player.meleeSpeed += 0.12f;
            player.magmaStone = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SurvivorBreastplate>() && legs.type == ModContent.ItemType<SurvivorLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "6% increased melee damage" +
                            "\nHitting enemies has a chance to drop blood pellets that restore health" +
                            "\nWhen under half health, blood pellets are attracted to you";

            player.meleeDamage += 0.06f;
            player.GetModPlayer<SpectraPlayer>().SurvivorSetBonus = true;
        }
    }
}
