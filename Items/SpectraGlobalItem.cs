using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpectraMod.Items.Currency;
using SpectraMod.Items.Tools.Sets.Dirt;
using SpectraMod.Items.Weapons.Sets.Dirt;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpectraMod.Items
{
    public class SpectraGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public static bool DirtPick;

        public bool givenAutoswing;

        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<Tools.Sets.Dirt.DirtPickaxe>()) DirtPick = true;  
            else DirtPick = false;  
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            if (item.type == ModContent.ItemType<DirtSword>() || item.type == ModContent.ItemType<DirtPickaxe>() && spectraPlayer.DirtSetBonus)
                mult = 2;
            //base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
        }

        public override bool ConsumeAmmo(Item item, Player player)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            if (spectraPlayer.AngerSetBonus && Main.rand.NextBool(10))
                return false;

            if (Main.rand.NextFloat() > spectraPlayer.NoAmmoChance)
                return false;
            return base.ConsumeAmmo(item, player);
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            SpectraPlayer spectraPlayer = player.GetModPlayer<SpectraPlayer>();

            if (crit) damage = (int)(damage * spectraPlayer.CritDamage);
        }

        public override bool UseItem(Item item, Player player)
        {
            if (player.GetModPlayer<SpectraPlayer>().AutoswingMinions && item.summon && !item.autoReuse)
            {
                Main.NewText("C");
                item.autoReuse = true;
                givenAutoswing = true;
                return true;
            }
            else if (givenAutoswing && item.summon && !player.GetModPlayer<SpectraPlayer>().AutoswingMinions)
            {
                Main.NewText("D");
                item.autoReuse = false;
                return true;
            }
            return base.UseItem(item, player);
        }
    }
}
