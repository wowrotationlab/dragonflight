using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class EnhanceShamanDF : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto", "CD" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);

        private string Build = "Build";
        private string Elementalist = "Elementalist";
        private string Physical = "Physical";
        private string AOEEnemyCount = "AOE Enemy Count";

        //spells
        int WindfuryTotem = 8512;
        int FeralSpirit = 51533;
        int LavaLash = 60103;
        int FlameShock = 188389;
        int LightningBolt = 188196;
        int PrimordialWave = 375982;
        int DoomWinds = 384352;
        int CrashLightning = 187874;
        int ElementalBlast = 117014;
        int IceStrike = 342240;
        int FrostShock = 196840;
        int StormStrike = 17364;
        int ChainLightning = 188443;
        int Ascendance = 114051;
        int WindStrike = 115356;
        int Sundering = 197214;
        int LavaBurst = 51505;
        int FireNova = 333974;
        int LightningShield = 192106;
        // talent

        int TalentAlphaWolf = 198434;
        int TalentElementalBlast = 117014;
        int TalentThorinsInvocation = 384444;
        int TalentWindfuryTotem = 8512;
        int TalentAscendence = 114051;
        int TalentFeralSpirit = 51533;
        int TalentDoomWinds = 384352;
        int TalentPrimordialWave = 375982;
        int TalentCrashLightning = 187874;
        int TalentSundering = 197214;
        int TalentLashingFlames = 334046;
        int TalentIceStrike = 342240;
        int TalentHailStorm = 334195;
        int TalentFireNova = 333974;
        int TalentDeeplyRootedElements = 378270;
        int TalentOverflowingMaelstrom = 384149;
        int TalentElementalSpirits = 262624;
        //buffs
        int BuffWindfury = 327942;
        int BuffHotHand = 201900;
        int BuffAshenCatalyst = 390371;
        int BuffPrimordialWave = 375982;
        int BuffMaelstromWeapon = 187880;
        int BuffDoomWinds = 384352;
        int BuffHailStorm = 334185;
        int BuffAscendance = 114051;
        int BuffStormBringer = 201846;
        int BuffBloodlust = 2825;
        int BuffHeroism = 32182;
        int BuffTimeWarp = 80353;
        int BuffPrimalRage = 264667;
        private bool IsBurst => CR.Toggle(ToggleBurst) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffPrimalRage) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffHeroism);

        public override void Initialize()
        {
            CR.RotationName = "Enhancement Shaman Dragonflight";
            CR.WriteLog("Enhancement Shaman Dragonflight by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Shaman;

            CR.AddSpell(WindfuryTotem, FeralSpirit, LavaLash, FlameShock, LightningBolt, PrimordialWave, DoomWinds, CrashLightning, ElementalBlast, IceStrike, FrostShock, StormStrike, ChainLightning, Ascendance, WindStrike, Sundering, LavaBurst, FireNova, LightningShield);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffPrimalRage, BuffTimeWarp, BuffWindfury, BuffHotHand, BuffAshenCatalyst, BuffPrimordialWave, BuffMaelstromWeapon, BuffDoomWinds, BuffHailStorm, BuffAscendance, BuffStormBringer, LightningShield);
            CR.AddDebuff(FlameShock);
            
            CR.AddTalent(TalentElementalSpirits, TalentOverflowingMaelstrom, TalentAlphaWolf, TalentElementalBlast, TalentThorinsInvocation, TalentWindfuryTotem, TalentAscendence, TalentFeralSpirit, TalentDoomWinds, TalentPrimordialWave, TalentCrashLightning, TalentSundering, TalentLashingFlames, TalentIceStrike, TalentHailStorm, TalentFireNova, TalentDeeplyRootedElements);

            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);
            //CR.AddSettingsListField(Build, Build, Build, new string[]{ Elementalist, Physical}, Elementalist);
            CR.AddSettingsNumberField(AOEEnemyCount, AOEEnemyCount, AOEEnemyCount, 3);
            CR.AddToggle(ToggleBurst);

            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            
        }

        public override void Rotation()
        {

            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead)
            {
                return;
            }


            if(CR.TargetRange<10 && !CR.PlayerHasBuff(BuffWindfury) && CR.CanCastSpell(WindfuryTotem)){
                CR.CastSpell(WindfuryTotem);
                return;

            }

        }

        public override void RotationInCombat()
        {

        


            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead)
            {
                return;
            }

            if(CR.TargetRange == 0)
            {
                if (IsBurst && !CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Auto")
                {
                    CR.UseMacro(Trinket1);
                }
                else if (IsBurst && !CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Auto")
                {
                    CR.UseMacro(Trinket2);
                }

                if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "CD" && CR.TargetTTD>10)
                {
                    CR.UseMacro(Trinket1);
                }
                else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "CD" && CR.TargetTTD > 10)
                {
                    CR.UseMacro(Trinket2);
                }
            }

            if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOEEnemyCount))
            {
                if (CR.TargetRange <= 5 && CR.CanCastSpell(FeralSpirit) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(FeralSpirit);
                    return;
                }
                if (CR.CanCastSpell(LavaLash, -1, true) && CR.TargetHasDebuff(FlameShock, true) && CR.EnemyDebuffCount(FlameShock) < 6 && CR.EnemyDebuffCount(FlameShock) < CR.AOEMeleeUnitCount)
                {
                    CR.CastSpell(LavaLash);
                    return;
                }

                if (CR.TargetRange <= 5 && CR.CanCastSpell(Ascendance) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(Ascendance);
                    return;
                }
             
                if (CR.CanCastSpell(LightningBolt, -1, true) && CR.PlayerHasBuff(BuffPrimordialWave) && ((CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && !CR.IsTalentSelected(TalentOverflowingMaelstrom)) || (CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10 && CR.IsTalentSelected(TalentOverflowingMaelstrom))))
                {
                    CR.CastSpell(LightningBolt);
                    return;
                }
                if (CR.CanCastSpell(ChainLightning, -1, true) && CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10)
                {
                    CR.CastSpell(ChainLightning);
                    return;
                }

                if (CR.CanCastSpell(DoomWinds, -1, true) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(DoomWinds);
                    return;
                }

                if (CR.PlayerHasBuff(BuffDoomWinds) && CR.CanCastSpell(CrashLightning) && CR.TargetRange == 0)
                {
                    CR.CastSpell(CrashLightning);
                    return;
                   
                }
                if (CR.PlayerHasBuff(BuffDoomWinds) && CR.CanCastSpell(Sundering) && CR.TargetRange==0)
                {
                    CR.CastSpell(Sundering);
                    return;

                }

                if (CR.CanCastSpell(FireNova) && CR.TargetHasDebuff(FlameShock) && CR.EnemyDebuffCount(FlameShock)>=6)
                {
                    CR.CastSpell(FireNova);
                    return;
                }

                if (CR.CanCastSpell(CrashLightning) && CR.TargetRange==0)
                {
                    CR.CastSpell(CrashLightning);
                    return;
                }

                if (CR.CanCastSpell(IceStrike, -1, true))
                {
                    CR.CastSpell(IceStrike);
                    return;
                }

                if (CR.CanCastSpell(FrostShock, -1, true) && CR.PlayerHasBuff(BuffHailStorm))
                {
                    CR.CastSpell(FrostShock);
                    return;
                }

                if (CR.PlayerHasBuff(BuffAscendance) && CR.CanCastSpell(WindStrike, -1, true))
                {
                    CR.CastSpell(WindStrike);
                    return;
                }
                if (CR.CanCastSpell(PrimordialWave, -1, true))
                {
                    CR.CastSpell(PrimordialWave);
                    return;
                }
                if (CR.CanCastSpell(FlameShock, -1, true) && !CR.TargetHasDebuff(FlameShock, true))
                {
                    CR.CastSpell(FlameShock);
                    return;
                }
                if (CR.CanCastSpell(WindStrike, -1, true))
                {
                    CR.CastSpell(WindStrike);
                    return;
                }
                if (CR.CanCastSpell(StormStrike) && CR.TargetRange == 0)
                {
                    CR.CastSpell(StormStrike);
                    return;
                }
            
                if (CR.CanCastSpell(Sundering) && CR.TargetRange == 0)
                {
                    CR.CastSpell(Sundering);
                    return;
                }
                if (CR.CanCastSpell(FireNova) && CR.TargetHasDebuff(FlameShock) && CR.EnemyDebuffCount(FlameShock) >= 4)
                {
                    CR.CastSpell(FireNova);
                    return;
                }
                if (CR.CanCastSpell(LavaLash, -1, true) && CR.TargetHasDebuff(FlameShock))
                {
                    CR.CastSpell(LavaLash);
                    return;
                }
            }
            else
            {


                if (CR.TargetRange <= 5 && CR.CanCastSpell(FeralSpirit) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(FeralSpirit);
                    return;
                }

                if (CR.TargetRange <= 5 && CR.CanCastSpell(Ascendance) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(Ascendance);
                    return;
                }

                if (CR.CanCastSpell(DoomWinds, -1, true) && IsBurst && CR.TargetTTD > 10)
                {
                    CR.CastSpell(DoomWinds);
                    return;
                }

                if (CR.PlayerHasBuff(BuffAscendance) && CR.CanCastSpell(WindStrike, -1, true))
                {
                    CR.CastSpell(WindStrike);
                    return;
                }

                if (CR.PlayerHasBuff(BuffDoomWinds))
                {
                    if (CR.CanCastSpell(IceStrike, -1, true))
                    {
                        CR.CastSpell(IceStrike);
                        return;
                    }
                    if (CR.CanCastSpell(StormStrike) && CR.TargetRange == 0)
                    {
                        CR.CastSpell(StormStrike);
                        return;
                    }
                    if (CR.CanCastSpell(CrashLightning, -1, true) && CR.TargetRange == 0)
                    {
                        CR.CastSpell(CrashLightning);
                        return;
                    }
                    if (CR.CanCastSpell(Sundering) && CR.TargetRange == 0)
                    {
                        CR.CastSpell(Sundering);
                        return;
                    }
                }




                if (CR.CanCastSpell(LavaLash, -1, true) && (CR.PlayerHasBuff(BuffHotHand) || CR.PlayerBuffStacks(BuffAshenCatalyst) >= 6))
                {
                    CR.CastSpell(LavaLash);
                    return;
                }

                if (CR.CanCastSpell(CrashLightning) && CR.PlayerHasBuff(BuffDoomWinds) && CR.TargetRange == 0)
                {
                    CR.CastSpell(CrashLightning);
                    return;
                }

                if (CR.CanCastSpell(FlameShock, -1, true) && !CR.TargetHasDebuff(FlameShock, true))
                {
                    CR.CastSpell(FlameShock);
                    return;
                }

                if (CR.CanCastSpell(LightningBolt, -1, true) && CR.PlayerHasBuff(BuffPrimordialWave) && ((CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && !CR.IsTalentSelected(TalentOverflowingMaelstrom)) || (CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10 && CR.IsTalentSelected(TalentOverflowingMaelstrom))))
                {
                    CR.CastSpell(LightningBolt);
                    return;
                }

                if (CR.CanCastSpell(PrimordialWave, -1, true))
                {
                    CR.CastSpell(PrimordialWave);
                    return;
                }

                if (CR.CanCastSpell(ElementalBlast, -1, true) && CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && CR.IsTalentSelected(TalentElementalSpirits) && CR.SpellCooldownDuration(FeralSpirit) > 750)
                {
                    CR.CastSpell(ElementalBlast);
                    return;
                }

                if (CR.CanCastSpell(LavaBurst, -1, true) && CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && CR.IsTalentSelected(TalentElementalSpirits) && CR.SpellCooldownDuration(FeralSpirit) > 750)
                {
                    CR.CastSpell(LavaBurst);
                    return;
                }


                if (CR.CanCastSpell(IceStrike, -1, true))
                {
                    CR.CastSpell(IceStrike);
                    return;
                }

                if (CR.CanCastSpell(FrostShock, -1, true) && CR.PlayerHasBuff(BuffHailStorm))
                {
                    CR.CastSpell(FrostShock);
                    return;
                }

                if (CR.CanCastSpell(StormStrike) && CR.TargetRange == 0  && CR.PlayerHasBuff(BuffStormBringer))
                {
                    CR.CastSpell(StormStrike);
                    return;
                }

                if (CR.CanCastSpell(LavaLash, -1, true) && CR.TargetHasDebuff(FlameShock, true) && CR.TargetDebuffDuration(FlameShock, true) < 400)
                {
                    CR.CastSpell(LavaLash);
                    return;
                }

                if (CR.CanCastSpell(ElementalBlast, -1, true) && CR.SpellCurrentCharge(ElementalBlast) == 2 && ((CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && !CR.IsTalentSelected(TalentOverflowingMaelstrom)) || (CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10 && CR.IsTalentSelected(TalentOverflowingMaelstrom))))
                {
                    CR.CastSpell(ElementalBlast);
                    return;
                }

                if (CR.CanCastSpell(LavaBurst, -1, true) && ((CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5 && !CR.IsTalentSelected(TalentOverflowingMaelstrom)) || (CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10 && CR.IsTalentSelected(TalentOverflowingMaelstrom))))
                {
                    CR.CastSpell(LavaBurst);
                    return;
                }

                if (CR.CanCastSpell(LightningBolt, -1, true) && CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 10)
                {
                    CR.CastSpell(LightningBolt);
                    return;
                }

                if (CR.CanCastSpell(StormStrike) && CR.TargetRange == 0)
                {
                    CR.CastSpell(StormStrike);
                    return;
                }
               

                if (CR.CanCastSpell(LavaLash, -1, true))
                {
                    CR.CastSpell(LavaLash);
                    return;
                }

                if (CR.CanCastSpell(LightningBolt, -1, true) && CR.PlayerBuffStacks(BuffMaelstromWeapon) >= 5)
                {
                    CR.CastSpell(LightningBolt);
                    return;
                }

                if (CR.CanCastSpell(Sundering) && CR.TargetRange == 0)
                {
                    CR.CastSpell(Sundering);
                    return;
                }


                if (CR.CanCastSpell(FrostShock, -1, true))
                {
                    CR.CastSpell(FrostShock);
                    return;
                }

                if (CR.CanCastSpell(CrashLightning) && CR.TargetRange == 0)
                {
                    CR.CastSpell(CrashLightning);
                    return;
                }
            }



        }

        public override void RotationOutOfCombat()
        {


            if (!CR.PlayerHasBuff(LightningShield) && CR.CanCastSpell(LightningShield))
            {
                CR.CastSpell(LightningShield);
                return;
            }


        }






    }
}
