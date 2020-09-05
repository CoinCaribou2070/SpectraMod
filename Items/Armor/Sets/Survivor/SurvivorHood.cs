using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    [AutoloadEquip(EquipType.Head)]
    public class SurvivorHood : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Hood");
            Tooltip.SetDefault("'I will survive!'" +
                             "\n8% increased magic damage" +
                             "\n5% increased melee critical strike chance" +
                             "\nIncreases your maximum mana by 30" +
                             "\nIncreases your mana regeneration rate");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.08f;
            player.meleeCrit += 5;
            player.statManaMax2 += 30;
            player.manaRegenDelayBonus += 2;
            player.manaRegenBonus += 18;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SurvivorBreastplate>() && legs.type == ModContent.ItemType<SurvivorLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased magic damage" +
                            "\nHitting enemies has a chance to drop blood pellets that restore health" +
                            "\nWhen under half health, blood pellets are attracted to you";

            player.magicDamage += 0.1f;
            player.GetModPlayer<SpectraPlayer>().SurvivorSetBonus = true;
        }
    }
}
