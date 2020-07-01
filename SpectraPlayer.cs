﻿using Microsoft.Xna.Framework;
using SpectraMod.Items.Armor.Sets.Survivor;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpectraMod
{
    public class SpectraPlayer : ModPlayer
    {

        #region ACC_BOOLS
        public bool UnluckyTombEffect;
        public bool GreaterPygmyNecklaceEffect;
        public bool CharmoftheDeadEffect;
        #endregion

        #region MISC_EFFECTS
        public bool AllPassive;
        public bool Hated;
        public float CritDamage;
        #endregion

        #region ARMOR_BOOLS
        public bool DirtSetBonus;
        public bool AngerSetBonus;
        public bool SurvivorSetBonus;
        #endregion

        public SpectraEnums.HealthLevel PlayerLifeTier = SpectraEnums.HealthLevel.None;
        public SpectraEnums.ManaLevel PlayerManaTier = SpectraEnums.ManaLevel.None;

        public override void Initialize()
        {
            PlayerLifeTier = SpectraEnums.HealthLevel.None;
            PlayerManaTier = SpectraEnums.ManaLevel.None;
        }

        public override void ResetEffects()
        {
            UnluckyTombEffect = false;
            GreaterPygmyNecklaceEffect = false;
            Hated = false;
            DirtSetBonus = false;
            AngerSetBonus = false;
            SurvivorSetBonus = false;
            CharmoftheDeadEffect = false;
            CritDamage = 1f;

            AllPassive = false;
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            #region ACC_EFFECTS
            if (UnluckyTombEffect)
            {
                player.npcTypeNoAggro[NPCID.DemonEye] = true;
                player.npcTypeNoAggro[NPCID.CataractEye] = true;
                player.npcTypeNoAggro[NPCID.SleepyEye] = true;
                player.npcTypeNoAggro[NPCID.DialatedEye] = true;
                player.npcTypeNoAggro[NPCID.GreenEye] = true;
                player.npcTypeNoAggro[NPCID.PurpleEye] = true;
                player.npcTypeNoAggro[NPCID.DemonEyeOwl] = true;
                player.npcTypeNoAggro[NPCID.DemonEyeSpaceship] = true;

                player.npcTypeNoAggro[NPCID.Zombie] = true;
                player.npcTypeNoAggro[NPCID.ZombieDoctor] = true;
                player.npcTypeNoAggro[NPCID.ZombieElf] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfBeard] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
                player.npcTypeNoAggro[NPCID.ZombieEskimo] = true;
                player.npcTypeNoAggro[NPCID.ZombieMushroom] = true;
                player.npcTypeNoAggro[NPCID.ZombieMushroomHat] = true;
                player.npcTypeNoAggro[NPCID.ZombiePixie] = true;
                player.npcTypeNoAggro[NPCID.ZombieRaincoat] = true;
                player.npcTypeNoAggro[NPCID.ZombieSuperman] = true;
                player.npcTypeNoAggro[NPCID.ZombieXmas] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombie] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieCenx] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieEskimo] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombiePincussion] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSlimed] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSwamp] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieTwiggy] = true;
                player.npcTypeNoAggro[NPCID.BaldZombie] = true;
                player.npcTypeNoAggro[NPCID.DoctorBones] = true;
                player.npcTypeNoAggro[NPCID.BloodZombie] = true;
                player.npcTypeNoAggro[NPCID.TheGroom] = true;
                player.npcTypeNoAggro[NPCID.TheBride] = true;
            }

            if (CharmoftheDeadEffect)
            {
                player.npcTypeNoAggro[NPCID.Zombie] = true;
                player.npcTypeNoAggro[NPCID.ZombieDoctor] = true;
                player.npcTypeNoAggro[NPCID.ZombieElf] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfBeard] = true;
                player.npcTypeNoAggro[NPCID.ZombieElfGirl] = true;
                player.npcTypeNoAggro[NPCID.ZombiePixie] = true;
                player.npcTypeNoAggro[NPCID.ZombieSuperman] = true;
                player.npcTypeNoAggro[NPCID.ZombieXmas] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombie] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieCenx] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombiePincussion] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSlimed] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieSwamp] = true;
                player.npcTypeNoAggro[NPCID.ArmedZombieTwiggy] = true;
                player.npcTypeNoAggro[NPCID.BaldZombie] = true;
            }

            if (GreaterPygmyNecklaceEffect)
            {
                player.maxMinions += 3;
                player.minionKB += 5f;
                player.minionDamage += 0.25f;
            }
            #endregion

            #region MISC_EFFECTS
            if (AllPassive)
            {
                foreach (NPC npc in Main.npc)
                {
                    player.npcTypeNoAggro[npc.type] = true;
                }
            }
            #endregion

            #region POWERUP_STATBOOSTS
            switch (PlayerLifeTier)
            {
                case SpectraEnums.HealthLevel.CursedLife:
                    player.statLifeMax2 += 100;
                    break;
                default:
                    break;
            }

            switch (PlayerManaTier)
            {
                case SpectraEnums.ManaLevel.LavaMana:
                    player.statManaMax2 += 100;
                    break;
                default:
                    break;
            }
            #endregion
        }

        public override void PreUpdate()
        {
            #region POWERUP_STATCHECKS
            if (player.statLifeMax == 400 && PlayerLifeTier < SpectraEnums.HealthLevel.LifeCrystal)
            {
                PlayerLifeTier = SpectraEnums.HealthLevel.LifeCrystal;
            }
            if (player.statLifeMax == 500 && PlayerLifeTier < SpectraEnums.HealthLevel.LifeFruit)
            {
                PlayerLifeTier = SpectraEnums.HealthLevel.LifeFruit;
            }

            if (player.statManaMax == 200 && PlayerManaTier < SpectraEnums.ManaLevel.ManaCrystal)
            {
                PlayerManaTier = SpectraEnums.ManaLevel.ManaCrystal;
            }
            #endregion

            #region POWERUP_RESOURCETEXTURES
            if (Main.netMode != NetmodeID.Server)
            {
                switch (PlayerLifeTier)
                {
                    case SpectraEnums.HealthLevel.CursedLife:
                        Main.heart2Texture = ModContent.GetTexture("SpectraMod/ResourceTextures/CursedHeart");
                        break;
                    default:
                        break;
                }

                switch (PlayerManaTier)
                {
                    case SpectraEnums.ManaLevel.LavaMana:
                        Main.manaTexture = ModContent.GetTexture("SpectraMod/ResourceTextures/LavaMana");
                        break;
                    default:
                        break;

                }
            }
            #endregion

            if (SpectraWorld.professionalMode)
            {
                if (player.wet && !player.lavaWet && !player.honeyWet)
                {
                    player.AddBuff(BuffID.Wet, 30 * 60);
                }

                if (!Main.dayTime)
                {
                    player.AddBuff(BuffID.Darkness, 1);
                }
            }

            SkyManager.Instance.Activate("SpectraMod:DoomSlime");
        }

        public override void PostUpdateBuffs()
        {
            if (Hated)
            {
                player.moveSpeed -= 0.3f;
                player.maxRunSpeed -= 1.1f;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Hated)
            {
                player.lifeRegen = 0;
                player.lifeRegenTime = 0;
                player.lifeRegen -= 4;
            }
        }

        public override void UpdateDead()
        {
            Hated = false;
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            switch (npc.type)
            {
                case 1: //should cover blue slime + all net id slimes
                case 50:
                case 59:
                case 71:
                case 81:
                case 121:
                case 122:
                case 138:
                case 141:
                case 147:
                case 183:
                case 184:
                case 204:
                case 224:
                case 225:
                case 302:
                case 333:
                case 334:
                case 335:
                case 336:
                case 535:
                case 537:
                    if (SpectraWorld.professionalMode)
                        if (Main.rand.NextBool(3))
                            player.AddBuff(BuffID.Slimed, Main.rand.Next(3, 11) * 60);
                    break;
            }
            base.OnHitByNPC(npc, damage, crit);
        }

        public override TagCompound Save()
        {
            if (PlayerLifeTier > SpectraEnums.HealthLevel.LifeCrystal) Main.heart2Texture = ModContent.GetTexture("Terraria/Heart2");

            return new TagCompound() {
                { "LifeTier", (int)PlayerLifeTier },
                { "ManaTier", (int)PlayerManaTier }
            };
        }

        public override void Load(TagCompound tag)
        {
            PlayerLifeTier = (SpectraEnums.HealthLevel)tag.GetInt("LifeTier");
            PlayerManaTier = (SpectraEnums.ManaLevel)tag.GetInt("ManaTier");
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            survivorSetBonus(target.Center.X, target.Center.Y, target.type);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            survivorSetBonus(target.Center.X, target.Center.Y, target.type);
        }

        private void survivorSetBonus(float x, float y, int type)
        {
            if (SurvivorSetBonus)
            {
                if (type == NPCID.TargetDummy) return;
                if (Main.rand.NextBool(4)) Item.NewItem(new Rectangle((int)x, (int)y, 1, 1), ModContent.ItemType<SurvivorPickup>());
            }
        }
    }
}