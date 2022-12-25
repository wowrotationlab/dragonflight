using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class WarriorArmsSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells


        private int MortalStrike = 12294;
        private int Overpower = 7384;
        private int Slam = 1464;
        private int Ravager = 152277;
        private int BladeStorm = 46924;
        private int Warbreaker = 262161;
        private int ColossusSmash = 167105;
        private int Cleave = 845;
        private int Avatar = 107574;
        private int DeadlyCalm = 262228;
        private int Whirlwind = 190411;
        private int Execute = 5308;
        private int Charge = 100;
        private int Condemn = 317349;
        private int AncientAftershock = 325886;
        private int SpearOfBastion = 307865;
        private int BattleShout = 6673;
        private int ConquerorsBanner = 324143;
        private int StormBolt = 107570;
        private int VictoryRush = 34428;
        private int ImpendingVictory = 202168;
        private int DieByTheSword = 118038;
        private int BerserkerRage = 18499;
        private int Pummel = 6552;
        private int SweepingStrikes = 260708;
        private int SkullSplitter = 260643;
        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffEnrage = 184361;
        private int BuffSuddenDeath = 280721;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;
        private string MacroSpearOfBastion = "Spear Of Bastion Macro";
        private int DeepWounds = 262115;
        private int DebuffColossusSmash = 208086;
        private int BuffExploiter = 335452;
        private int BuffCollateralDamage = 334783;
        private int BuffConquerersFrenzy = 343672;

        public override void Initialize()
        {
            CR.RotationName = "Arms Warrior Shadowlands";
            CR.WriteLog("Arms Warrior Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Warrior;

            CR.AddSpell(MortalStrike, Overpower, Slam, Ravager, BladeStorm, SkullSplitter, SweepingStrikes, Warbreaker, ColossusSmash, Cleave, Avatar, DeadlyCalm, Whirlwind, Execute, Charge, Condemn, AncientAftershock, SpearOfBastion, BattleShout, ConquerorsBanner, StormBolt, VictoryRush, ImpendingVictory, DieByTheSword, BerserkerRage, Pummel);



            CR.AddBuff(BattleShout);
            CR.AddBuff(BuffEnrage);
            CR.AddBuff(BuffSuddenDeath);
            CR.AddBuff(Avatar);
            CR.AddBuff(Whirlwind, SweepingStrikes, BuffExploiter, BuffConquerersFrenzy);
            CR.AddBuff(BuffHeroism);
            CR.AddBuff(BuffBloodlust, BuffTimeWarp, BuffPrimalRage);
            CR.AddDebuff(DeepWounds, DebuffColossusSmash);
            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);
            CR.AddToggle(ToggleBurst);

            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(MacroSpearOfBastion, "@cursor", SpearOfBastion);
        }

        public override void Rotation()
        {

            if (!CR.PlayerHasBuff(BattleShout))
            {
                CR.CastSpell(BattleShout);
                return;
            }

        }

        bool HasCleaveTalent => CR.IsTalentSelectedRetail(3, 5);
        public override void RotationInCombat()
        {


            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 20)
            {
                return;
            }


            int Rage = CR.PlayerRage;

            bool BurstPhase = CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) || (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(Avatar);

            if (CR.LossOfControlType == LossOfControlType.FEAR || CR.LossOfControlType == LossOfControlType.CHARM)
            {
                if (CR.CanCastSpell(BerserkerRage))
                {
                    CR.CastSpell(BerserkerRage);
                    return;
                }

            }

            if (CR.PlayerHealthPercent <= 30)
            {
                if (CR.CanCastSpell(DieByTheSword))
                {
                    CR.CastSpell(DieByTheSword);
                    return;
                }
            }

            if (!CR.PlayerCanAttackTarget)
            {
                return;
            }

            if (CR.TargetRange <= 25 && CR.TargetRange >= 8 && CR.CanCastSpell(Charge))
            {
                CR.CastSpell(Charge);
                return;
            }

            if (BurstPhase)
            {
                if (CR.CanCastSpell(ConquerorsBanner))
                {
                    CR.CastSpell(ConquerorsBanner);
                    return;
                }
            }


            bool MeleeRange = CR.TargetRange <= 7;

            if (MeleeRange)
            {
                if (BurstPhase)
                {

                    if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Auto")
                    {
                        CR.UseMacro(Trinket1);
                    }
                    else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Auto")
                    {
                        CR.UseMacro(Trinket2);
                    }

                }


                if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= 2 && !CR.PlayerHasBuff(Whirlwind))
                {

                    if (CR.CanCastSpell(Whirlwind))
                    {
                        CR.CastSpell(Whirlwind);
                        return;
                    }

                }

                if (BurstPhase)
                {
                    if (CR.CanCastSpell(Avatar))
                    {
                        CR.CastSpell(Avatar);
                        return;
                    }

                    if (CR.ToggleAOE && CR.CanCastSpell(Warbreaker))
                    {
                        CR.CastSpell(Warbreaker);
                        return;
                    }

                    if (CR.ToggleAOE && CR.CanCastSpell(ColossusSmash))
                    {
                        CR.CastSpell(ColossusSmash);
                        return;
                    }
                }


                if (CR.PlayerHealthPercent < 30)
                {
                    if (CR.CanCastSpell(ImpendingVictory))
                    {
                        CR.CastSpell(ImpendingVictory);
                        return;
                    }
                    else if (CR.CanCastSpell(VictoryRush))
                    {
                        CR.CastSpell(VictoryRush);
                        return;
                    }
                }

                if(BurstPhase && HasCleaveTalent && ((CR.EnemyDebuffCount(DeepWounds)>=CR.AOEMeleeUnitCount) || CR.AOEMeleeUnitCount< 2) && CR.ToggleAOE && CR.CanCastSpell(AncientAftershock))
                {
                    CR.CastSpell(AncientAftershock);
                    return;
                }

                if (BurstPhase && !HasCleaveTalent &&  CR.AOEMeleeUnitCount >= 2 && CR.ToggleAOE && CR.CanCastSpell(AncientAftershock))
                {
                    CR.CastSpell(AncientAftershock);
                    return;
                }

                if (BurstPhase && HasCleaveTalent && ((CR.EnemyDebuffCount(DeepWounds) >= CR.AOEMeleeUnitCount) || CR.AOEMeleeUnitCount < 2) && CR.ToggleAOE && CR.CanCastSpell(SpearOfBastion))
                {
                    if (CR.PlayerCanAttackMouseover && CR.CanUseMacro(MacroSpearOfBastion))
                    {
                        CR.UseMacro(MacroSpearOfBastion);
                        return;
                    }
                    else if (CR.CanCastSpell(SpearOfBastion))
                    {
                        CR.CastSpell(SpearOfBastion);
                        return;
                    }
                }

                if (BurstPhase && !HasCleaveTalent && CR.AOEMeleeUnitCount >= 2 && CR.ToggleAOE && CR.CanCastSpell(SpearOfBastion))
                {
                    if (CR.PlayerCanAttackMouseover && CR.CanUseMacro(MacroSpearOfBastion))
                    {
                        CR.UseMacro(MacroSpearOfBastion);
                        return;
                    }
                    else if (CR.CanCastSpell(SpearOfBastion))
                    {
                        CR.CastSpell(SpearOfBastion);
                        return;
                    }
                }

                if(CR.ToggleAOE && CR.AOEMeleeUnitCount >=3 && !CR.PlayerHasBuff(SweepingStrikes) && CR.CanCastSpell(BladeStorm))
                {
                    CR.CastSpell(BladeStorm);
                    return;
                }

                if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= 3 &&  CR.CanCastSpell(Ravager))
                {
                    CR.CastSpell(Ravager);
                    return;
                }

                if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= 2 && CR.CanCastSpell(SweepingStrikes))
                {
                    CR.CastSpell(SweepingStrikes);
                    return;
                }

                if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= 2 && CR.PlayerHasBuff(SweepingStrikes) && CR.CanCastSpell(ColossusSmash))
                {
                    CR.CastSpell(ColossusSmash);
                    return;
                }

                if(CR.TargetDebuffDuration(DeepWounds)<100 && CR.CanCastSpell(MortalStrike))
                {
                    CR.CastSpell(MortalStrike);
                    return;
                }

                if(CR.CanCastSpell(Condemn) && (!CR.ToggleAOE || CR.AOEMeleeUnitCount < 2) && (CR.PlayerRage>=40 || CR.TargetHasDebuff(ColossusSmash)))
                {
                    CR.CastSpell(Condemn);
                    return;
                }


                if (CR.CanCastSpell(Execute) && (!CR.ToggleAOE || CR.AOEMeleeUnitCount < 2) && (CR.PlayerRage >= 40 || CR.TargetHasDebuff(ColossusSmash)))
                {
                    CR.CastSpell(Execute);
                    return;
                }


                if (CR.CanCastSpell(Overpower))
                {
                    CR.CastSpell(Overpower);
                    return;
                }

                if (CR.CanCastSpell(Cleave) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= 2 && (!CR.PlayerHasBuff(SweepingStrikes) || CR.AOEMeleeUnitCount >= 3))
                {
                    CR.CastSpell(Cleave);
                    return;
                }

                if (CR.PlayerBuffStacks(BuffExploiter) ==2 && CR.CanCastSpell(MortalStrike))
                {
                    CR.CastSpell(MortalStrike);
                    return;
                }


            
                if (CR.CanCastSpell(Condemn))
                {
                    CR.CastSpell(Condemn);
                    return;
                }


                if (CR.CanCastSpell(Whirlwind) && CR.EnemyDebuffCount(DeepWounds)>=5 && CR.ToggleAOE && CR.AOEMeleeUnitCount>=5)
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }
                if (CR.CanCastSpell(Execute))
                {
                    CR.CastSpell(Execute);
                    return;
                }


                if (CR.CanCastSpell(Whirlwind) && CR.EnemyDebuffCount(DeepWounds) >= 3 && CR.ToggleAOE && CR.AOEMeleeUnitCount >= 3)
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }

                if (CR.CanCastSpell(MortalStrike))
                {
                    CR.CastSpell(MortalStrike);
                    return;
                }

                if (CR.CanCastSpell(SkullSplitter))
                {
                    CR.CastSpell(SkullSplitter);
                    return;
                }

                if(CR.ToggleAOE && !CR.PlayerHasBuff(SweepingStrikes) && !CR.SpellIsKnown(Ravager) && CR.AOEMeleeUnitCount>=3 && CR.CanCastSpell(BladeStorm))
                {
                    CR.CastSpell(BladeStorm);
                    return;

                }

                if(CR.PlayerHasBuff(BuffCollateralDamage) && CR.ToggleAOE && CR.CanCastSpell(Whirlwind))
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }

                if (CR.AOEMeleeUnitCount>=3 && CR.ToggleAOE && CR.CanCastSpell(Whirlwind))
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }

                if (CR.IsTalentSelectedRetail(2,3) && CR.ToggleAOE && CR.CanCastSpell(Whirlwind))
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }

                if (!CR.IsTalentSelectedRetail(2, 3) && CR.CanCastSpell(Slam) && (!CR.ToggleAOE || CR.AOEMeleeUnitCount<3))
                {
                    CR.CastSpell(Slam);
                    return;
                }


                if (CR.CanCastSpell(DeadlyCalm) && !CR.PlayerHasBuff(BuffConquerersFrenzy))
                {
                    CR.CastSpell(DeadlyCalm);
                    return;
                }
                  



            }

        }

        public override void RotationOutOfCombat()
        {




        }






    }
}
