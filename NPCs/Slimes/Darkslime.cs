using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class Darkslime : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Slime");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SafeSetDefaults()
        {
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<DarkslimeBanner>();

            npc.damage = 96;
            npc.lifeMax = 1200;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void NPCLoot()
        {
            int amount = Main.expertMode && Main.rand.NextBool(2) ? 2 : 1;
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.Gel.DoomGel>(), amount);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (SpectraWorld.professionalMode)
            {
                if (Main.rand.NextBool(3))
                    target.AddBuff(BuffID.Slimed, Main.rand.Next(3, 11) * 60);
                if (Main.rand.NextBool(5))
                    target.AddBuff(BuffID.Darkness, Main.rand.Next(2, 4) * 60);
            }
            base.OnHitPlayer(target, damage, crit);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (SpawnCondition.OverworldNightMonster.Chance > 0f && NPC.downedMoonlord) ? SpawnCondition.OverworldNightMonster.Chance / 2.75f : 0f;
        }
    }
}
