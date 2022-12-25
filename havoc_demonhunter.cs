using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class HavocDemonHunterSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleInterrupt = "Interrupt";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //Spells CD
        private int Metamorphosis = 191427;

     
        //Other Spells
        private int FelRush = 344865;
        private int VengefulRetreat = 344866;
        private int GlaiveTempest = 342817;
        private int ImmolationAura = 258920;
        private int BladeDance = 188499;
        private int DeathSweep = 210152;
        private int Felblade = 232893;
        private int EyeBeam = 198013;
        private int ChaosStrike = 344862;
        private int Annihilation = 201427;
        private int ThrowGlaive = 185123;
        private int DemonsBite = 344859;
        private int EssenceBreak = 258860;
        private int ChaosNova = 179057;
        private int FelBarrage = 258925;
        private int Blur = 198589;
        //Kick
        private int Disrupt = 183752;

        // Covenant Abilities
        private int TheHunt = 323639;
        private int SinfulBrand = 317009;
        private int ElysianDecree = 306830;



        //Buffs
        private int InnerDemons = 116014;
        private int Momentum = 333100;

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

        private string MetamorphosisMacro = "Metamorphosis Macro";

        private string AOERotationEnemyCount = "AOE Enemy Count";

        private string BlurHealth = "Blur Health";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Havoc Demon Hunter Shadowlands";
            CR.WriteLog("Havoc Demon Hunter by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.DemonHunter;


            

            CR.AddSpell(Metamorphosis, FelRush, VengefulRetreat, GlaiveTempest, ImmolationAura, BladeDance, DeathSweep, Felblade, FelBarrage, EyeBeam, ChaosNova, ChaosStrike, Annihilation, ThrowGlaive, DemonsBite, EssenceBreak, Disrupt, Blur, TheHunt, SinfulBrand, ElysianDecree, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, InnerDemons, Momentum, Metamorphosis, ImmolationAura, EssenceBreak, BuffCovenantFodderToTheFlameUp, BuffConduitExposedWound, BonusFelBombardment);
            //CR.AddDebuff(FrostNova);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
 

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to cast AOE spells", 3);
            CR.AddSettingsNumberField(BlurHealth, BlurHealth, "Use blur if health falls below  this value", 30);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(MetamorphosisMacro, @"#showtooltip Metamorphosis
/use [@cursor]Metamorphosis", Metamorphosis);
   
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {
    
        }
        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerBuffDuration(Metamorphosis) >= 250 || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage);
        bool meleeRange => CR.TargetRange <= 5;


        Random rand = new Random();
        public override void RotationInCombat()
        {

        
   
            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 50 || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }

            if (CR.PlayerHealthPercent < 30 && CR.CanCastSpell(Blur))
            {
                CR.CastSpell(Blur);
                return;
            }

            if (isBurst && !CR.PlayerIsMoving && CoolDowns()) return;

      
            if (Interrupts())  return;


            if (!meleeRange && CR.CanCastSpell(TheHunt))
            {
                CR.CastSpell(TheHunt);
                return;
            }


            if(!isBurst && CR.CanCastSpell(SinfulBrand) && meleeRange)
            {
                CR.CastSpell(SinfulBrand);
                return;
            }


            if (meleeRange && CR.IsTalentSelectedRetail(2,7))
            {
                if (CR.CanCastSpell(VengefulRetreat))
                {
                    CR.CastSpell(VengefulRetreat);
                    return;
                }
                if (CR.CanCastSpell(FelRush) && !CR.PlayerHasBuff(Momentum))
                {
                    CR.CastSpell(FelRush);
                    return;
                }

             
            }

            if (CR.TargetRange<=30 && CR.CanCastSpell(ThrowGlaive) && CR.PlayerHasBuff(BuffCovenantFodderToTheFlameUp))
            {
                CR.CastSpell(ThrowGlaive);
                return;
            }

            if (meleeRange)
            {
                if (CR.CanCastSpell(FelBarrage) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount))
                {
                    CR.CastSpell(FelBarrage);
                    return;
                }


                if (CR.CanCastSpell(EyeBeam) && CR.PlayerFury < 70 && CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(EyeBeam);
                    return;
                }

                if (CR.CanCastSpell(EyeBeam) && !CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(EyeBeam);
                    return;
                }
                if (CR.CanCastSpell(DeathSweep) && (CR.IsTalentSelectedRetail(2, 5) || (!CR.IsTalentSelectedRetail(2, 5) && CR.IsTalentSelectedRetail(1, 3))))
                {
                    CR.CastSpell(DeathSweep);
                    return;
                }
                if (CR.CanCastSpell(DeathSweep) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && !CR.IsTalentSelectedRetail(2, 5) && !CR.IsTalentSelectedRetail(1, 3))
                {
                    CR.CastSpell(DeathSweep);
                    return;
                }
                if (CR.CanCastSpell(BladeDance) && (CR.IsTalentSelectedRetail(2, 5) || (!CR.IsTalentSelectedRetail(2, 5) && CR.IsTalentSelectedRetail(1, 3))))
                {
                    CR.CastSpell(BladeDance);
                    return;
                }
                if (CR.CanCastSpell(BladeDance) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && !CR.IsTalentSelectedRetail(2, 5) && !CR.IsTalentSelectedRetail(1, 3))
                {
                    CR.CastSpell(BladeDance);
                    return;
                }

                if (CR.CanCastSpell(FelRush) && CR.PlayerHasBuff(BuffUnboundChaos))
                {
                    CR.CastSpell(FelRush);
                    return;
                }

                if (CR.CanCastSpell(GlaiveTempest))
                {
                    CR.CastSpell(GlaiveTempest);
                    return;
                }
                if (CR.CanCastSpell(EssenceBreak) && !CR.IsTalentSelectedRetail(3, 2) && CR.PlayerFury > 80)
                {
                    CR.CastSpell(EssenceBreak);
                    return;
                }
                if (CR.CanCastSpell(EssenceBreak) && CR.IsTalentSelectedRetail(3, 2))
                {
                    CR.CastSpell(EssenceBreak);
                    return;
                }

                if (CR.CanCastSpell(Annihilation)  && CR.PlayerFury >= 80 && CR.PlayerHasBuff(EssenceBreak) && CR.SpellCooldownDuration(EyeBeam) <= 200 && !CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5) && CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(Annihilation);
                    return;
                }
                if (CR.CanCastSpell(Annihilation) && CR.PlayerFury >= 80 && !CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5) && !CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(Annihilation);
                    return;
                }
                if (CR.CanCastSpell(Annihilation) && CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5) )
                {
                    CR.CastSpell(Annihilation);
                    return;
                }
                if (CR.CanCastSpell(Annihilation) && !CR.IsTalentSelectedRetail(3, 5))
                {
                    CR.CastSpell(Annihilation);
                    return;
                }

                if (CR.CanCastSpell(ElysianDecree))
                {
                    CR.CastSpell(ElysianDecree);
                    return;
                }


                if (CR.CanCastSpell(ChaosStrike) && CR.PlayerFury >= 80 && CR.PlayerHasBuff(EssenceBreak) && CR.SpellCooldownDuration(EyeBeam) <= 200 && !CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5) && CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(ChaosStrike);
                    return;
                }
                if (CR.CanCastSpell(ChaosStrike) && CR.PlayerFury >= 80 && !CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5) && !CR.IsTalentSelectedRetail(1, 1))
                {
                    CR.CastSpell(ChaosStrike);
                    return;
                }
                if (CR.CanCastSpell(ChaosStrike) && CR.IsTalentSelectedRetail(3, 2) && CR.IsTalentSelectedRetail(3, 5))
                {
                    CR.CastSpell(ChaosStrike);
                    return;
                }
                if (CR.CanCastSpell(ChaosStrike) && !CR.IsTalentSelectedRetail(3, 5))
                {
                    CR.CastSpell(ChaosStrike);
                    return;
                }

                if (CR.CanCastSpell(ImmolationAura) )
                {
                    CR.CastSpell(ImmolationAura);
                    return;
                }

                if (CR.CanCastSpell(FelRush) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount))
                {
                    CR.CastSpell(FelRush);
                    return;
                }

           
            }


            if (CR.TargetRange <= 15 && CR.CanCastSpell(Felblade))
            {
                CR.CastSpell(Felblade);
                return;
            }

            if (meleeRange)
            {
                if (CR.CanCastSpell(FelRush) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.SpellCurrentCharge(FelRush) == 2 && CR.IsTalentSelectedRetail(3, 2) && !CR.IsTalentSelectedRetail(2, 7))
                {
                    CR.CastSpell(FelRush);
                    return;
                }
            }

            if (CR.TargetRange <= 30 && CR.CanCastSpell(ThrowGlaive) && !CR.PlayerHasBuff(ImmolationAura) && CR.PlayerHasBuff(BonusFelBombardment))
            {
                CR.CastSpell(ThrowGlaive);
                return;
            }
            if (meleeRange)
            {
                if (CR.CanCastSpell(ChaosNova) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.IsTalentSelectedRetail(1, 6))
                {
                    CR.CastSpell(ChaosNova);
                    return;
                }


                if (CR.CanCastSpell(DemonsBite) &&  !CR.IsTalentSelectedRetail(3, 2))
                {
                    CR.CastSpell(DemonsBite);
                    return;
                }
            }
            if (CR.TargetRange<=30 && CR.CanCastSpell(ThrowGlaive) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount))
            {
                CR.CastSpell(ThrowGlaive);
                return;
            }




        }



        public override void RotationOutOfCombat()
        {
           
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
                else if (CR.SpellCooldownDuration(Disrupt) > 150)
                {

                    if (CR.CanCastSpell(ChaosNova))
                    {
                        CR.CastSpell(ChaosNova);
                        return true;
                    }

                }

            }

            return false;
        }


        private bool CoolDowns() { 


            if(meleeRange && CR.SpellCooldownDuration(EyeBeam) > 0 && (CR.SpellCooldownDuration(BladeDance) > 0 || (!CR.IsTalentSelectedRetail(1,3) && !CR.IsTalentSelectedRetail(2,5) && CR.AOEMeleeUnitCount < 3)))
            {
                if (CR.CanUseMacro(MetamorphosisMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(MetamorphosisMacro);
                    return true;
                }
                else if (CR.CanCastSpell(Metamorphosis))
                {
                    CR.CastSpell(Metamorphosis);
                    return true;
                }
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
