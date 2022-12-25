using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class FireMageSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleInterrupt = "Interrupt";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells
        private int FireBlast = 108853;
        private int FireBall = 133;
        private int Scorch = 2948;
        private int Meteor = 153561;
        private int PyroBlast = 11366;
        private int DragonsBreath = 31661;
        private int PhoenixFlames = 257541;
        private int FlameStrike = 2120;
        private int LivingBomb = 44457;

        private int PyroClasm = 269650;
       
        private int AlexstraszasFury = 235870;
        private int ArcaneExplosion = 1449;
        private int MirrorImage = 55342;


        private int FrostNova = 122;
        private int Counterspell = 2139;
        private int ArcaneIntellect = 1459;
        private int IceBlock = 45438;
        private int BlazingBarrier = 235313;

        private int Combustion = 190319;
        private int RuneofPower = 116011;

        private int Deathborne = 324220;
        private int MirrorsofTorment = 314793;
        private int RadiantSpark = 307443;
        private int ShiftingPower = 314791;




        private int HotStreak = 48108;
        private int HeatingUp = 48107;
        private int FlamePatch = 205037;
        private int Firestarter = 205026;

        private int RuneOfPowerBuff = 116014;

        private int BonusFirestorm = 333100;
        private int BonusSunKingsBlessingBuilding = 333314;
        //racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private string FlameStrikeMacro = "Flamestrike Macro";
        private string MeteorMacro = "Meteor Macro";
        private string AOERotationEnemyCount = "AOE Enemy Count";
        private string DisciplinaryCommand = "Disciplinary Command";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Fire Mage Shadowlands";
            CR.WriteLog("Fire Mage by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Mage;


            

            CR.AddSpell(Combustion, FireBlast, FireBall, Scorch, Meteor, PyroBlast, DragonsBreath, PhoenixFlames, ArcaneExplosion, FlameStrike, LivingBomb,  FrostNova, BlazingBarrier, Counterspell, ArcaneIntellect, IceBlock, MirrorImage, RuneofPower, Deathborne, MirrorsofTorment, RadiantSpark, ShiftingPower, BuffTimeWarp, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, PyroClasm, Combustion, RuneOfPowerBuff, HotStreak, HeatingUp, FlamePatch, Firestarter,  IceBlock, ArcaneIntellect,  BlazingBarrier, BonusFirestorm, BonusSunKingsBlessingBuilding);
            CR.AddDebuff(FrostNova);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
 

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to cast AOE spells", 3);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(FlameStrikeMacro, "@cursor", FlameStrike);
            CR.AddMacro(MeteorMacro, "@cursor", Meteor);
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {
           
        }


        Random rand = new Random();
        public override void RotationInCombat()
        {

        
   
            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 40 || CR.PlayerHasBuff(IceBlock) || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }


            if (isBurst && !CR.PlayerIsMoving && CoolDowns()) return;

      
            if (Interrupts())  return; 

            
            if(CR.TargetIsTargetingPlayer && CR.TargetRange <= 5 && CR.CanCastSpell(FrostNova) && !CR.TargetHasDebuff(FrostNova))
            {
                CR.CastSpell(FrostNova);
                return;
            }
            if (CR.TargetIsTargetingPlayer &&  !CR.PlayerHasBuff(BlazingBarrier) && CR.CanCastSpell(BlazingBarrier))
            {
                CR.CastSpell(BlazingBarrier);
                return;
            }





            if (CR.PlayerHasBuff(BonusFirestorm) && CR.ToggleAOE && CR.AOEUnitCount>=CR.GetSettingNumber(AOERotationEnemyCount) && !CR.PlayerIsMoving)
            {
                if (CR.CanUseMacro(FlameStrikeMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(FlameStrikeMacro);
                    return;
                }
                else if (CR.CanCastSpell(FlameStrike))
                {
                    CR.CastSpell(FlameStrike);
                    return;
                }
            }

            if (CR.PlayerHasBuff(BonusFirestorm) && CR.CanCastSpell(PyroBlast) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(PyroBlast);
                return;
            }

            if (!CR.PlayerHasBuff(Combustion) && CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.SecondLastCastSpellId == PyroBlast && CR.LastCastSpellId!= PyroBlast && CR.CanCastSpell(PhoenixFlames))
            {
                CR.CastSpell(PhoenixFlames);
                return;
            }

            if (CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount))
            {
                if (CR.CanUseMacro(MeteorMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(MeteorMacro);
                    return;
                }
                else if (CR.CanCastSpell(Meteor))
                {
                    CR.CastSpell(Meteor);
                    return;
                }
            }


            if (CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerHasBuff(HotStreak))
            {
                if (CR.CanUseMacro(FlameStrikeMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(FlameStrikeMacro);
                    return;
                }
                else if (CR.CanCastSpell(FlameStrike))
                {
                    CR.CastSpell(FlameStrike);
                    return;
                }
            }

            if (CR.PlayerHasBuff(HotStreak) && CR.CanCastSpell(PyroBlast))
            {
                CR.CastSpell(PyroBlast);
                return;
            }

            if (CR.PlayerHasBuff(Combustion) && CR.CanCastSpell(FireBlast) && CR.LastCastSpellId != FireBlast)
            {
                CR.CastSpell(FireBlast);
                return;
            }

            if (CR.PlayerHasBuff(BonusSunKingsBlessingBuilding)  && !CR.PlayerHasBuff(Combustion)  && CR.CanCastSpell(PyroBlast) )
            {
                CR.CastSpell(PyroBlast);
                return;
            }

            if (CR.PlayerHasBuff(PyroClasm) && !CR.PlayerHasBuff(Combustion) && CR.CanCastSpell(PyroBlast))
            {
                CR.CastSpell(PyroBlast);
                return;
            }

            if (CR.CanCastSpell(FireBlast) && CR.PlayerHasBuff(HeatingUp) && (CR.PlayerHasBuff(Combustion) || CR.PlayerHasBuff(RuneOfPowerBuff) || CR.SpellCurrentChargeFraction(FireBlast) * 8.4 < CR.SpellCooldownDuration(Combustion) / 100))
            {
                CR.CastSpell(FireBlast);
                return;
            }

            if(CR.CanCastSpell(DragonsBreath) && CR.IsTalentSelectedRetail(2,4) && CR.PlayerHasBuff(HeatingUp))
            {
                CR.CastSpell(DragonsBreath);
                return;
            }
            if (CR.CanCastSpell(PhoenixFlames)  && CR.PlayerHasBuff(Combustion))
            {
                CR.CastSpell(PhoenixFlames);
                return;
            }
            if (CR.CanCastSpell(LivingBomb) && CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && !CR.PlayerHasBuff(Combustion))
            { 
                CR.CastSpell(LivingBomb);
                return;
            }

            if (CR.CanCastSpell(DragonsBreath) && CR.ToggleAOE && CR.AOEUnitCount > 1 && CR.TargetRange<=12)
            {
                CR.CastSpell(DragonsBreath);
                return;
            }
            if (CR.CanCastSpell(Scorch) && CR.PlayerBuffDuration(Combustion)>130)
            {
                CR.CastSpell(Scorch);
                return;
            }
 

            if (CR.ToggleAOE && CR.AOEUnitCount>= CR.GetSettingNumber(AOERotationEnemyCount) && CR.TargetRange<=15 && CR.CanCastSpell(ShiftingPower))
            {
                CR.CastSpell(ShiftingPower);
                return;
            }

            if ( CR.CanCastSpell(PhoenixFlames) && CR.SpellCurrentCharge(PhoenixFlames) == CR.SpellMaxCharge(PhoenixFlames))
            {
                CR.CastSpell(PhoenixFlames);
                return;
            }

            if (CR.IsTalentSelectedRetail(1,6) && CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && !CR.PlayerIsMoving)
 
            {
                if (CR.CanUseMacro(FlameStrikeMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(FlameStrikeMacro);
                    return;
                }
                else if (CR.CanCastSpell(FlameStrike))
                {
                    CR.CastSpell(FlameStrike);
                    return;
                }
            }
            if (CR.CanCastSpell(Scorch) && CR.IsTalentSelectedRetail(3,1) && CR.TargetHealthPercent<30)
            {
                CR.CastSpell(Scorch);
                return;
            }
            if (CR.CanCastSpell(FireBall) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(FireBall);
                return;
            }
            if (CR.CanCastSpell(Scorch))
            {
                CR.CastSpell(Scorch);
                return;
            }
        }



        public override void RotationOutOfCombat()
        {
            if (CR.TargetRange<30 && CR.PlayerCanAttackTarget && !CR.PlayerHasBuff(BlazingBarrier) && CR.CanCastSpell(BlazingBarrier))
            {
                CR.CastSpell(BlazingBarrier);
                return;
            }

            if (!CR.PlayerHasBuff(ArcaneIntellect) && CR.CanCastSpell(ArcaneIntellect))
            {
                CR.CastSpell(ArcaneIntellect);
                return;
            }

        }


  


        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(Combustion) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp)  || CR.PlayerHasBuff(BuffPrimalRage);
 


        private bool Interrupts()
        {
            bool isInterrupt = CR.TargetCurrentCastInterruptible && CR.Toggle(ToggleInterrupt) && CR.TargetCurrentCastPercent > 10;


            if (isInterrupt)
            {

                if (CR.CanCastSpell(Counterspell))
                {
                    CR.CastSpell(Counterspell);
                    return true;
                }

            }

            return false;
        }


        private bool CoolDowns() { 


         

        

            if(CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.CanCastSpell(Deathborne))
            {
                CR.CastSpell(Deathborne);
                return true;
            }

            if (CR.SpellCooldownDuration(Combustion) > 2000 && !CR.PlayerHasBuff(RuneOfPowerBuff) && CR.CanCastSpell(RuneofPower))
            {
           
                CR.CastSpell(RuneofPower);
                return true;
                
            }

       
    


            if (CR.SpellCooldownDuration(Combustion)==0)
            {
                if (CR.CanUseMacro(MeteorMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(MeteorMacro);
                    return true;
                }
                else if (CR.CanCastSpell(Meteor))
                {
                    CR.CastSpell(Meteor);
                    return true;
                }
            }

            if (CR.CanCastSpell(RadiantSpark) && CR.SpellCooldownDuration(Combustion) == 0 && !CR.PlayerHasBuff(Combustion))
            {
                CR.CastSpell(RadiantSpark);
                return true;
            }

            if (CR.CanCastSpell(MirrorsofTorment) &&  !CR.PlayerHasBuff(Combustion))
            {
                CR.CastSpell(MirrorsofTorment);
                return true;
            }

            if (CR.CanCastSpell(Combustion))
            {
                CR.CastSpell(Combustion);
                return true;
            }




            if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Auto")
            {
                CR.UseMacro(Trinket1);
            }

            else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Auto")
            {
                CR.UseMacro(Trinket2);
            }

            if (CR.CanCastSpell(Fireblood))
            {
                CR.CastSpell(Fireblood);
                return true;
            }
            if (CR.CanCastSpell(Berserking))
            {
                CR.CastSpell(Berserking);
                return true;
            }
            if (CR.CanCastSpell(Berserking))
            {
                CR.CastSpell(Berserking);
                return true;
            }
            if (CR.CanCastSpell(BloodFury))
            {
                CR.CastSpell(BloodFury);
                return true;
            }

            if (CR.CanCastSpell(LightsJudgement))
            {
                CR.CastSpell(LightsJudgement);
                return true;
            }
            if (CR.CanCastSpell(AncestralCall))
            {
                CR.CastSpell(AncestralCall);
                return true;
            }


            return false;
        }

  

    }
}
