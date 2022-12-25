using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class WarriorFurySL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst"; 
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells

        private int Recklessness = 1719;
        private int CrushingBlow = 335097;
        private int BloodBath = 113344;
        private int SiegeBreaker = 280772;
        private int Rampage = 184367;
        private int BladeStorm = 46924;
        private int Bloodthirst = 23881;
        private int Ragingblow = 85288;
        private int DragonRoar = 118000;
        private int Onslaught = 315720;
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
        private int EnragedRegeneration = 184364;
        private int BerserkerRage = 18499;
        private int Pummel = 6552;

        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffEnrage = 184361;
        private int BuffSuddenDeath = 280721;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;
        private string MacroSpearOfBastion = "Spear Of Bastion Macro";





        public override void Initialize()
        {
            CR.RotationName = "Fury Warrior Shadowlands";
            CR.WriteLog("Fury Warrior Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Warrior;

            CR.AddSpell(Recklessness);
            CR.AddSpell(CrushingBlow);
            CR.AddSpell(BloodBath);
            CR.AddSpell(SiegeBreaker);
            CR.AddSpell(Rampage);
            CR.AddSpell(BladeStorm);
            CR.AddSpell(Bloodthirst);
            CR.AddSpell(Ragingblow);
            CR.AddSpell(DragonRoar);
            CR.AddSpell(Onslaught);
            CR.AddSpell(Whirlwind);
            CR.AddSpell(Execute);
            CR.AddSpell(Charge);
            CR.AddSpell(Condemn);
            CR.AddSpell(AncientAftershock);
            CR.AddSpell(SpearOfBastion);
            CR.AddSpell(ConquerorsBanner);
            CR.AddSpell(BattleShout);
            CR.AddSpell(EnragedRegeneration);
            CR.AddSpell(BerserkerRage);
            CR.AddSpell(Pummel);
            CR.AddSpell(StormBolt);
            CR.AddSpell(VictoryRush);
            CR.AddSpell(ImpendingVictory);

            CR.AddBuff(BattleShout);
            CR.AddBuff(BuffEnrage);
            CR.AddBuff(BuffSuddenDeath);
            CR.AddBuff(Recklessness);
            CR.AddBuff(Whirlwind);
            CR.AddBuff(BuffHeroism);
            CR.AddBuff(BuffBloodlust, BuffTimeWarp, BuffPrimalRage);

            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);
            CR.AddToggle(ToggleBurst);

            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(MacroSpearOfBastion, "#showtooltip\r\n/use[@cursor]Spear Of Bastion");
        }

        public override void Rotation()
        {

            if (!CR.PlayerHasBuff(BattleShout))
            {
                CR.CastSpell(BattleShout);
                return;
            }

        }

        public override void RotationInCombat()
        {


            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 20)
            {
                return;
            }


            int Rage = CR.PlayerRage;

            bool BurstPhase = CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffBloodlust)  || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) || (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(Recklessness);

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
                if (CR.CanCastSpell(EnragedRegeneration))
                {
                    CR.CastSpell(EnragedRegeneration);
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
                    if (CR.CanCastSpell(Recklessness))
                    {
                        CR.CastSpell(Recklessness);
                        return;
                    }
                }


                if (CR.PlayerHealthPercent < 70)
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


                if (CR.CanCastSpell(SiegeBreaker))
                {
                    CR.CastSpell(SiegeBreaker);
                    return;
                }

                if ((!CR.PlayerHasBuff(BuffEnrage) || Rage >= 90) && CR.CanCastSpell(Rampage))
                {
                    CR.CastSpell(Rampage);
                    return;
                }

                if (CR.CanCastSpell(Condemn))
                {
                    CR.CastSpell(Condemn);
                    return;
                }


                if (CR.PlayerHasBuff(Recklessness))
                {
                    if (CR.CanCastSpell(AncientAftershock))
                    {
                        CR.CastSpell(AncientAftershock);
                        return;
                    }
                }



                if (CR.PlayerHasBuff(BuffEnrage))
                {

                    if (CR.PlayerCanAttackMouseover && CR.CanUseMacro(MacroSpearOfBastion))
                    {
                        CR.UseMacro(MacroSpearOfBastion);
                        return;
                    }else if (CR.CanCastSpell(SpearOfBastion))
                    {
                        CR.CastSpell(SpearOfBastion);
                        return;
                    }

                }
                if (CR.CanCastSpell(Execute))
                {
                    CR.CastSpell(Execute);
                    return;
                }

                if (CR.PlayerHasBuff(BuffEnrage))
                {

                    if (CR.CanCastSpell(BladeStorm))
                    {
                        CR.CastSpell(BladeStorm);
                        return;
                    }

                }


                if (!CR.PlayerHasBuff(BuffEnrage))
                {

                    if (CR.CanCastSpell(BloodBath))
                    {
                        CR.CastSpell(BloodBath);
                        return;
                    }
                    else if (CR.CanCastSpell(Bloodthirst))
                    {
                        CR.CastSpell(Bloodthirst);
                        return;
                    }

                }

                if (CR.CanCastSpell(CrushingBlow))
                {
                    CR.CastSpell(CrushingBlow);
                    return;
                }
                else if (CR.CanCastSpell(Ragingblow))
                {
                    CR.CastSpell(Ragingblow);
                    return;
                }


                if (CR.PlayerHasBuff(BuffEnrage))
                {

                    if (CR.CanCastSpell(DragonRoar))
                    {
                        CR.CastSpell(DragonRoar);
                        return;
                    }

                    if (CR.CanCastSpell(Onslaught))
                    {
                        CR.CastSpell(Onslaught);
                        return;
                    }
                }

                if (CR.CanCastSpell(BloodBath))
                {
                    CR.CastSpell(BloodBath);
                    return;
                }
                else if (CR.CanCastSpell(Bloodthirst))
                {
                    CR.CastSpell(Bloodthirst);
                    return;
                }
                if (CR.CanCastSpell(StormBolt))
                {
                    CR.CastSpell(StormBolt);
                    return;
                }
                if (CR.CanCastSpell(Whirlwind) && CR.ToggleAOE)
                {
                    CR.CastSpell(Whirlwind);
                    return;
                }



            }

        }

        public override void RotationOutOfCombat()
        {




        }






    }
}
