using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class VengeanceDemonHunterSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Offensive", "Defensive" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleInterrupt = "Interrupt";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //Spells CD
        private int Metamorphosis = 191427;

     
        //Other Spells
        private int FelDevastation = 212084;
        private int SpiritBomb = 247454;
        private int Fracture = 263642;
        private int Shear = 203782;
        private int SoulCleave = 228477;
        private int SigilofFlame = 204596;
        private int InfernalStrike = 189110;
        private int FieryBrand = 204021;
        private int DemonSpikes = 203720;
        private int Felblade = 232893;

        private int ThrowGlaive = 185123;
        private int ImmolationAura = 258920;
        private int BulkExtraction = 320341;
        private int SoulBarrier = 263648;
        private int Torment = 185245;
        //Kick
        private int Disrupt = 183752;

        // Covenant Abilities
        private int TheHunt = 323639;
        private int SinfulBrand = 317009;
        private int ElysianDecree = 306830;
        private int Fleshcraft = 324631;


        //Buffs
        private int InnerDemons = 116014;
        private int Momentum = 333100;
        private int Frailty = 224509;
        private int SoulFragment = 203981;
        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private int BuffCovenantFodderToTheFlameUp = 329554;
        private int BuffConduitExposedWound = 339229;
        private int BuffUnboundChaos = 347462;
        private int BonusFelBombardment = 337849;
        //Debuffs

        //Racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        //Macros

        private string SigilofFlameMacro = "Sigil of Flame Macro";
        private string InfernoStrikeMacro = "Infernal Strike Macro";
        private string AOERotationEnemyCount = "AOE Enemy Count";

        private string DefensiveHealth = "Defensive Health";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Vengeance Demon Hunter Shadowlands";
            CR.WriteLog("Vengeance Demon Hunter by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.DemonHunter;


            

            CR.AddSpell(Metamorphosis, FelDevastation, SpiritBomb, Fracture, Shear, SoulCleave, SigilofFlame, Felblade, InfernalStrike, FieryBrand, DemonSpikes, ThrowGlaive, ImmolationAura, BulkExtraction, SoulBarrier, Disrupt, Torment, TheHunt, SinfulBrand, ElysianDecree, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, InnerDemons, Momentum, Metamorphosis, ImmolationAura, Frailty, BuffCovenantFodderToTheFlameUp, BuffConduitExposedWound, BonusFelBombardment, SoulFragment);
            //CR.AddDebuff(FrostNova);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
 

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to cast AOE spells", 3);
            CR.AddSettingsNumberField(DefensiveHealth, DefensiveHealth, "Use defensives cooldowns if health falls below  this value", 50);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(SigilofFlameMacro, @"#showtooltip Sigil of Flame
/use [@player]Sigil of Flame", SigilofFlame);
            CR.AddMacro(InfernoStrikeMacro, @"#showtooltip Infernal Strike
/use [@cursor]Infernal Strike", InfernalStrike);
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {
    
        }
        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerBuffDuration(Metamorphosis) >= 2500 || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage);
        bool meleeRange => CR.TargetRange <= 5;


        Random rand = new Random();
        public override void RotationInCombat()
        {

        
   
            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 50 || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }

            if (Defensives()) return;

            if (isBurst &&  CoolDowns()) return;

      
            if (Interrupts())  return;


           
   

            if (CR.TargetRange<=30 && CR.CanCastSpell(ThrowGlaive) && CR.PlayerHasBuff(BuffCovenantFodderToTheFlameUp))
            {
                CR.CastSpell(ThrowGlaive);
                return;
            }

            if (!meleeRange && CR.CanCastSpell(TheHunt))
            {
                CR.CastSpell(TheHunt);
                return;
            }


            if (!isBurst && CR.CanCastSpell(SinfulBrand) && meleeRange)
            {
                CR.CastSpell(SinfulBrand);
                return;
            }

            if (CR.CanCastSpell(BulkExtraction) && CR.PlayerHealthPercent <= CR.GetSettingNumber(DefensiveHealth) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && meleeRange)
            {
                CR.CastSpell(BulkExtraction);
                return;
            }

            if (CR.CanCastSpell(FelDevastation) && CR.PlayerHealthPercent <= 75 && CR.ToggleAOE && CR.AOEMeleeUnitCount >= 2 && CR.TargetRange<=15)
            {
                CR.CastSpell(FelDevastation);
                return;
            }

            if (CR.CanCastSpell(ElysianDecree) && CR.TargetRange <= 30)
            {
                CR.CastSpell(ElysianDecree);
                return;
            }


            if (meleeRange)
            {
                if (CR.CanUseMacro(SigilofFlameMacro))
                {
                    CR.UseMacro(SigilofFlameMacro);
                    return;
                }
                else if (CR.CanCastSpell(SigilofFlame))
                {
                    CR.CastSpell(SigilofFlame);
                    return;
                }


                if (CR.CanCastSpell(SoulCleave) && CR.PlayerHealth <= CR.GetSettingNumber(DefensiveHealth) && !CR.IsTalentSelectedRetail(3, 3))
                {
                    CR.CastSpell(SoulCleave);
                    return;
                }

                if (CR.CanCastSpell(SoulCleave) && CR.PlayerHealth <= CR.GetSettingNumber(DefensiveHealth) && CR.IsTalentSelectedRetail(3, 3) && CR.AOEMeleeUnitCount < 3)
                {
                    CR.CastSpell(SoulCleave);
                    return;
                }

                if (CR.CanCastSpell(SpiritBomb) && ((CR.PlayerHealth <= CR.GetSettingNumber(DefensiveHealth) && CR.AOEMeleeUnitCount >= 3) || CR.PlayerBuffStacks(SoulFragment)>=4) )
                {
                    CR.CastSpell(SpiritBomb);
                    return;
                }
                if(CR.CanCastSpell(SoulCleave) &&  CR.PlayerFuryMax - CR.PlayerFury < 30 &&(! CR.PlayerHasBuff(SoulFragment) || CR.AOEMeleeUnitCount < 2))
                {
                    CR.CastSpell(SoulCleave);
                    return;
                }


                if (CR.CanCastSpell(Fracture) && CR.SpellCurrentCharge(Fracture)==2)
                {
                    CR.CastSpell(Fracture);
                    return;
                }
              
           
            }
            if (CR.TargetRange <= 30 && CR.CanCastSpell(ThrowGlaive) && CR.PlayerBuffStacks(BonusFelBombardment)>=3)
            {
                CR.CastSpell(ThrowGlaive);
                return;
            }

            if (CR.CanCastSpell(ImmolationAura))
            {
                CR.CastSpell(ImmolationAura);
                return;
            }

            if (meleeRange)
            {
                if (CR.CanCastSpell(SoulCleave) && CR.PlayerFuryMax - CR.PlayerFury < 30 )
                {
                    CR.CastSpell(SoulCleave);
                    return;
                }

                if (CR.CanCastSpell(Fracture) )
                {
                    CR.CastSpell(Fracture);
                    return;
                }

              
            }

            if (CR.CanCastSpell(Felblade) && CR.PlayerFuryMax - CR.PlayerFury >= 40)
            {
                CR.CastSpell(Felblade);
                return;
            }

            if (CR.SpellCurrentCharge(InfernalStrike) == 2)
            {
                if (CR.CanUseMacro(InfernoStrikeMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(InfernoStrikeMacro);
                    return;
                }
                else if (CR.CanCastSpell(InfernalStrike))
                {
                    CR.CastSpell(InfernalStrike);
                    return;
                }

            }
            if (CR.CanCastSpell(Shear) )
            {
                CR.CastSpell(Shear);
                return;
            }



        }



        public override void RotationOutOfCombat()
        {
           
        }


        private bool Defensives()
        {


            if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Defensive")
            {
                CR.UseMacro(Trinket1);
            }

            else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Defensive")
            {
                CR.UseMacro(Trinket2);
            }

            if (CR.CanCastSpell(DemonSpikes) && CR.SpellCurrentCharge(DemonSpikes) == 2 && CR.PlayerHealthPercent < 60 && !CR.PlayerHasBuff(Metamorphosis) && !CR.PlayerHasBuff(FieryBrand) && !CR.PlayerHasBuff(DemonSpikes)) {
                CR.CastSpell(DemonSpikes);
                return true;
            }

            if (CR.CanCastSpell(FieryBrand) &&  !CR.PlayerHasBuff(Metamorphosis) && !CR.PlayerHasBuff(DemonSpikes) && CR.TargetTTD > 10)
            {
                CR.CastSpell(FieryBrand);
                return true;
            }

            if (CR.CanCastSpell(Metamorphosis) && CR.PlayerHealthPercent < 30 && !CR.PlayerHasBuff(Metamorphosis) && !CR.PlayerHasBuff(FieryBrand) && !CR.PlayerHasBuff(DemonSpikes))
            {
                CR.CastSpell(DemonSpikes);
                return true;
            }


            if (CR.CanCastSpell(DemonSpikes) && CR.PlayerHealthPercent < CR.GetSettingNumber(DefensiveHealth)-10 &&  !CR.PlayerHasBuff(DemonSpikes) && !CR.PlayerHasBuff(Metamorphosis) && !CR.PlayerHasBuff(FieryBrand) && !CR.PlayerHasBuff(DemonSpikes))
            {
                CR.CastSpell(DemonSpikes);
                return true;
            }


            if (CR.CanCastSpell(SoulBarrier) && CR.PlayerHealthPercent < CR.GetSettingNumber(DefensiveHealth)+10)
            {
                CR.CastSpell(SoulBarrier);
                return true;
            }

            if (CR.CanCastSpell(Fleshcraft) && CR.PlayerHealthPercent < CR.GetSettingNumber(DefensiveHealth))
            {
                CR.CastSpell(Fleshcraft);
                return true;
            }
            return false;
        }





        private bool Interrupts()
        {
            bool isInterrupt = CR.TargetCurrentCastInterruptible && CR.Toggle(ToggleInterrupt) && CR.TargetCurrentCastPercent > 10;


            if (isInterrupt)
            {

                if (CR.CanCastSpell(Disrupt))
                {
                    CR.CastSpell(Disrupt);
                    return true;
                }

            }

            return false;
        }


        private bool CoolDowns() { 



            if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Offensive")
            {
                CR.UseMacro(Trinket1);
            }

            else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Offensive")
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
