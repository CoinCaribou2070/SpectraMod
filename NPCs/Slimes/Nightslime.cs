﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpectraMod.Items.Banner;

namespace SpectraMod.NPCs.Slimes
{
    public class Nightslime : SpectraNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night Slime");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SafeSetDefaults()
        {
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = ModContent.ItemType<NightslimeBanner>();

            npc.damage = Main.hardMode ? 16 : 48;

            if (!Main.hardMode) npc.lifeMax = 62;
            else npc.lifeMax = NPC.downedPlantBoss ? 248 : 124;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void NPCLoot()
        {
            int amount = Main.expertMode ? 2 : 1;
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.Gel.BlackGel>(), amount);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (SpawnCondition.OverworldNightMonster.Chance > 0f) ? SpawnCondition.OverworldNightMonster.Chance / 2.75f : 0f;
        }
    }
}
