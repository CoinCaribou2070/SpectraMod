using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items.Armor.Sets.Survivor
{
    [AutoloadEquip(EquipType.Head)]
    public class SurvivorCowl : SpectraItem
    {
        public override string Texture => "SpectraMod/Items/Armor/PlaceholderHelmet";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Survivor's Cowl");
            Tooltip.SetDefault("'I will survive!'" +
                             "\nIncreases your maximum number of minions" +
                             "\n9% increased minion damage" +
                             "\nIncreases minion knockback" +
                             "\nAll summoner weapons have autoswing");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
            player.minionDamage += 0.9f;
            player.minionKB += 0.1f;
            player.GetModPlayer<SpectraPlayer>().AutoswingMinions = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SurvivorBreastplate>() && legs.type == ModContent.ItemType<SurvivorLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases your maximum number of minions" +
                            "\n10% increased minion damage" +
                            "\nHitting enemies has a chance to drop blood pellets that restore health" +
                            "\nWhen under half health, blood pellets are attracted to you";

            player.minionDamage += 0.1f;
            player.maxMinions++;
            player.GetModPlayer<SpectraPlayer>().SurvivorSetBonus = true;
        }
    }
}
