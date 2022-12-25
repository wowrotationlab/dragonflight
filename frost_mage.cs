using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class FrostMageSL : RotationLab
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
        private int FrostBolt = 116;
        private int IceLance = 30455;
        private int Blizzard = 190356;
        private int Flurry = 44614;
        private int FrostNova = 122;
        private int FrozenOrb = 84714;
        private int ArcaneExplosion = 1449;
        private int RayofFrost = 205021;
        private int GlacialSpike = 199786;
        private int CometStorm = 153595;
        private int IceNova = 157997;
        private int Ebonbolt = 257537;
        private int Counterspell = 2139;
        private int ArcaneIntellect = 1459;
        private int IceBlock = 45438;

        private int IceBarrier = 11426;
        private int IcyVeins = 12472;
        private int RuneofPower = 116011;

        private int Deathborne = 324220;
        private int MirrorsofTorment = 314793;
        private int RadiantSpark = 307443;
    
        private int ShiftingPower = 314791;



        private int FingersofFrost = 112965;
        private int BrainFreeze = 190447;
        private int WintersChill = 228358;
        private int RuneOfPowerBuff = 116014;
        private int Icicles = 205473;
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

        private string BlizzardMacro = "Blizzard Macro";
        private string AOERotationEnemyCount = "AOE Enemy Count";
        private string DisciplinaryCommand = "Disciplinary Command";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Frost Mage Shadowlands";
            CR.WriteLog("Frost Mage by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Mage;


            

            CR.AddSpell(FireBlast, FrostBolt, IceLance, Blizzard, Flurry, FrostNova, FrozenOrb, ArcaneExplosion, RayofFrost, GlacialSpike, CometStorm, IceNova, Ebonbolt, Counterspell, ArcaneIntellect, IceBlock, IceBarrier, IcyVeins, RuneofPower, Deathborne, MirrorsofTorment, RadiantSpark, ShiftingPower, BuffTimeWarp, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, IcyVeins, RuneOfPowerBuff, BrainFreeze, WintersChill, Icicles, FingersofFrost,  IceBlock, ArcaneIntellect, IceBarrier);
            CR.AddDebuff(FrostNova);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
 

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to cast AOE spells", 3);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(BlizzardMacro, @"#showtooltip Blizzard
/use [@cursor]Blizzard", Blizzard);
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
            if (CR.TargetIsTargetingPlayer &&  !CR.PlayerHasBuff(IceBarrier) && CR.CanCastSpell(IceBarrier))
            {
                CR.CastSpell(IceBarrier);
                return;
            }

            if (CR.LastCastSpellId == Flurry && CR.CanCastSpell(RayofFrost) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(RayofFrost);
                return;
            }

            if ((CR.LastCastSpellId == Flurry || CR.PlayerHasBuff(WintersChill)) && CR.CanCastSpell(IceLance))
            {
                CR.CastSpell(IceLance);
                return;
            }

            if (CR.LastCastSpellId == Ebonbolt && CR.CanCastSpell(Flurry) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Flurry);
                return;
            }

            if (CR.LastCastSpellId == GlacialSpike && CR.PlayerHasBuff(BrainFreeze) && CR.CanCastSpell(Flurry))
            {
                CR.CastSpell(Flurry);
                return;
            }

           
            if (CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) &&CR.CanCastSpell(FrozenOrb) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(FrozenOrb);
                return;
            }

            if (CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.CanCastSpell(CometStorm))
            {
                CR.CastSpell(CometStorm);
                return;
            }

            if (CR.ToggleAOE && CR.AOEUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && !CR.PlayerIsMoving)
            {
                if (CR.CanUseMacro(BlizzardMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(BlizzardMacro);
                    return;
                }
                else if (CR.CanCastSpell(Blizzard))
                {
                    CR.CastSpell(Blizzard);
                    return;
                }
            }

            if(CR.PlayerHasBuff(BrainFreeze) && CR.IsTalentSelectedRetail(3,7) && CR.LastCastSpellId== FrostBolt && CR.PlayerBuffStacks(Icicles) < 4 && CR.CanCastSpell(Flurry))
            {
                CR.CastSpell(Flurry);
                return;
            }

            if (CR.PlayerHasBuff(BrainFreeze) && !CR.IsTalentSelectedRetail(3, 7) && CR.LastCastSpellId == FrostBolt && CR.CanCastSpell(Flurry) )
            {
                CR.CastSpell(Flurry);
                return;
            }

            if(CR.PlayerHasBuff(FingersofFrost) && CR.CanCastSpell(IceLance))
            {
                CR.CastSpell(IceLance);
                return;
            }

            if (CR.CanCastSpell(Ebonbolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Ebonbolt);
                return;
            }

            if (CR.PlayerHasBuff(BrainFreeze) && CR.CanCastSpell(GlacialSpike) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(GlacialSpike);
                return;
            }

            if (CR.ToggleAOE  && CR.CanCastSpell(IceNova) )
            {
                CR.CastSpell(IceNova);
                return;
            }


            if (CR.ToggleAOE && CR.AOEUnitCount>= CR.GetSettingNumber(AOERotationEnemyCount) && CR.TargetRange<=15 && CR.CanCastSpell(ShiftingPower))
            {
                CR.CastSpell(ShiftingPower);
                return;
            }

            if ( CR.CanCastSpell(FrostBolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(FrostBolt);
                return;
            }

            if (CR.CanCastSpell(IceLance))
            {
                CR.CastSpell(IceLance);
                return;
            }


        }



        public override void RotationOutOfCombat()
        {
            if (CR.TargetRange<30 && CR.PlayerCanAttackTarget && !CR.PlayerHasBuff(IceBarrier) && CR.CanCastSpell(IceBarrier))
            {
                CR.CastSpell(IceBarrier);
                return;
            }

            if (!CR.PlayerHasBuff(ArcaneIntellect) && CR.CanCastSpell(ArcaneIntellect))
            {
                CR.CastSpell(ArcaneIntellect);
                return;
            }

        }


  


        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(IcyVeins) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp)  || CR.PlayerHasBuff(BuffPrimalRage);
 


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

            if (CR.SpellCooldownDuration(IcyVeins) > 1500 && !CR.PlayerHasBuff(RuneOfPowerBuff) && CR.CanCastSpell(RuneofPower))
            {
           
                CR.CastSpell(RuneofPower);
                return true;
                
            }

            if (CR.CanCastSpell(MirrorsofTorment))
            {
                CR.CastSpell(MirrorsofTorment);
                return true;
            }

            if (CR.CanCastSpell(RadiantSpark))
            {
                CR.CastSpell(RadiantSpark);
                return true;
            }

        


            if (CR.CanCastSpell(IcyVeins))
            {
                CR.CastSpell(IcyVeins);
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


            if (CR.CanCastSpell(FrozenOrb))
            {
                CR.CastSpell(FrozenOrb);
                return true;
            }

            if (CR.CanCastSpell(CometStorm))
            {
                CR.CastSpell(CometStorm);
                return true;
            }
            return false;
        }

  

    }
}
