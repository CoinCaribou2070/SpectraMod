using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    public class SurvivorHeadgear : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Headgear");
            Tooltip.SetDefault("'I will survive!'" +
                             "\n7% increased ranged damage" +
                             "\n5% increased ranged critical strike chance" +
                             "\n2% increased critical strike damage" +
                             "\n8% chance to not consume ammo");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            player.rangedDamage += 0.07f;
            player.rangedCrit += 5;
            spectraPlayer.CritDamage += 0.02f;
            spectraPlayer.NoAmmoChance += 0.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SurvivorBreastplate>() && legs.type == ModContent.ItemType<SurvivorLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased ranged damage" +
                            "\nHitting enemies has a chance to drop blood pellets that restore health" +
                            "\nWhen under half health, blood pellets are attracted to you";

            player.meleeDamage += 0.1f;
            player.GetModPlayer<SpectraPlayer>().SurvivorSetBonus = true;
        }
    }
}
