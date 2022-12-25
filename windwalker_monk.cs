using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class WindWalkerMonkSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleInterrupt = "Interrupt";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //Spells CD
        private int StormEarthAndFire = 137639;
        private int StormEarthAndFireFixate = 221771;
        private int Serenity = 152173;
        //Other Spells
        private int BlackoutKick = 100784;
        private int CracklingJadeLightning = 172724;
        private int LegSweep = 119381;
        private int MarkoftheCrane = 228287;
        private int Paralysis = 344359;
        private int Roll = 109132;
        private int TigerPalm = 100780;
        private int TouchofDeath = 322109;
        private int FistsofFury = 113656;
        private int InvokeXuen = 123904;
        private int RisingSunKick = 107428;
        private int TouchofKarma = 122470;
        private int SpinningCraneKick = 101546;
        private int FistoftheWhiteTiger = 261947;
        private int ChiWave = 115098;
        private int WhirlingDragonPunch = 152175;
        private int EnergizingElixir = 115288;
        private int RushingJadeWind = 116847;
        private int ChiBurst = 123986;
   
        //Kick
        private int SpearHandStrike = 116705;

        // Covenant Abilities
        private int BonedustBrew = 325216;
        private int FaelineStomp = 327104;
        private int FallenOrder = 326860;
        private int WeaponsofOrder = 310454;
        


        //Buffs


        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

    
        private int DanceofChiJi = 325201;
        private int WeaponsofOrderWW = 311054;
        private int BonusLastEmperorsCapacitor = 337291;
        private int CycloneStrikes = 220358;
        private int BonusPressurePoint = 337482;
        private int ComboBreaker = 116768;
        //Debuffs

        //Racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        //Macros

        private string BonedustBrewMacro = "Bonedust Brew Macro";

        private string AOERotationEnemyCount = "AOE Enemy Count";


        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Windwalker Monk Shadowlands";
            CR.WriteLog("Windwalker Monk by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Monk;

            
            

            CR.AddSpell(StormEarthAndFire, StormEarthAndFireFixate, Serenity, BlackoutKick, CracklingJadeLightning, LegSweep, MarkoftheCrane, Paralysis, Roll, TigerPalm, TouchofDeath, FistsofFury, InvokeXuen, RisingSunKick, TouchofKarma, SpinningCraneKick, FistoftheWhiteTiger, ChiWave, WhirlingDragonPunch, EnergizingElixir, RushingJadeWind, ChiBurst, SpearHandStrike, BonedustBrew, FaelineStomp, FallenOrder, WeaponsofOrder, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, BonedustBrew, StormEarthAndFireFixate, DanceofChiJi, WeaponsofOrderWW, BonusLastEmperorsCapacitor, CycloneStrikes, BonusPressurePoint, ComboBreaker);
            CR.AddDebuff(BonedustBrew);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
 

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to cast AOE spells", 3);
        

            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(BonedustBrewMacro, @"#showtooltip Bonedust Brew
/use [@player]Bonedust Brew", BonedustBrew);
   
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {
    
        }
        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerBuffDuration(StormEarthAndFire) >= 250 || CR.PlayerBuffDuration(Serenity) >= 250 || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage);
        bool inMelee => CR.TargetRange <= 5;
        bool hasSerenity => CR.IsTalentSelectedRetail(3, 7);
        bool hasWhirlingDragonPunch => CR.IsTalentSelectedRetail(2, 7);
        int ChiToMax => CR.PlayerChiMax - CR.PlayerChi;
        int ChiDeficit => CR.PlayerChiMax - CR.PlayerChi;

        Random rand = new Random();
        public override void RotationInCombat()
        {

        
   
            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 50 || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }

      
        
           


            if (inMelee)
            {


                if (Interrupts())  return;

                if(CR.CanCastSpell(StormEarthAndFireFixate) && CR.AOEUnitCount<3 && !CR.PlayerHasBuff(StormEarthAndFireFixate) && CR.PlayerHasBuff(StormEarthAndFire))
                {
                    CR.CastSpell(StormEarthAndFireFixate);
                    return;
                 
                }


                if (isBurst)
                {

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
                        return;
                    }
                    if (CR.CanCastSpell(Berserking))
                    {
                        CR.CastSpell(Berserking);
                        return;
                    }
                    if (CR.CanCastSpell(Berserking))
                    {
                        CR.CastSpell(Berserking);
                        return;
                    }
                    if (CR.CanCastSpell(BloodFury))
                    {
                        CR.CastSpell(BloodFury);
                        return;
                    }

                    if (CR.CanCastSpell(LightsJudgement))
                    {
                        CR.CastSpell(LightsJudgement);
                        return;
                    }
                    if (CR.CanCastSpell(AncestralCall))
                    {
                        CR.CastSpell(AncestralCall);
                        return;
                    }


                    if (CR.CanCastSpell(FallenOrder))
                    {
                        CR.CastSpell(FallenOrder);
                        return;
                    }

                    if (CR.CanCastSpell(WeaponsofOrder))
                    {
                        CR.CastSpell(FallenOrder);
                        return;
                    }

                    if (CR.CanCastSpell(InvokeXuen))
                    {
                        CR.CastSpell(InvokeXuen);
                        return;
                    }

                    if (!CR.PlayerHasBuff(BonedustBrew))
                    {
                        if (CR.CanUseMacro(BonedustBrewMacro))
                        {
                            CR.UseMacro(BonedustBrewMacro);
                            return;
                        }
                        else if (CR.CanCastSpell(BonedustBrew))
                        {
                            CR.CastSpell(BonedustBrew);
                            return;
                        }

                    }



                }




                if ( CR.CanCastSpell(FaelineStomp))
                {
                    CR.CastSpell(FaelineStomp);
                    return;
                }

                if(CR.CanCastSpell(TigerPalm) && CR.LastCastSpellId!=TigerPalm && ChiToMax >=2 && hasSerenity && !CR.PlayerHasBuff(Serenity))
                {
                    CR.CastSpell(TigerPalm);
                    return;

                }

                if (CR.CanCastSpell(TigerPalm) && CR.LastCastSpellId != TigerPalm && ChiToMax >= 2)
                {
                    CR.CastSpell(TigerPalm);
                    return;

                }

                if(isBurst && CR.CanCastSpell(TouchofDeath))
                {
                    CR.CastSpell(TouchofDeath);
                    return;

                }

                if (isBurst && CR.CanCastSpell(Serenity))
                {
                    CR.CastSpell(Serenity);
                    return;

                }

                if (isBurst && CR.CanCastSpell(StormEarthAndFire))
                {
                    CR.CastSpell(StormEarthAndFire);
                    return;

                }

                if (isBurst && CR.CanCastSpell(StormEarthAndFire))
                {
                    CR.CastSpell(StormEarthAndFire);
                    return;

                }

                if (CR.CanCastSpell(WhirlingDragonPunch))
                {
                    CR.CastSpell(WhirlingDragonPunch);
                    return;

                }
                if (CR.CanCastSpell(CracklingJadeLightning) && CR.PlayerBuffStacks(BonusLastEmperorsCapacitor) ==20)
                {
                    CR.CastSpell(CracklingJadeLightning);
                    return;

                }
                if (CR.PlayerChi>=2 && CR.ToggleAOE && CR.AOEMeleeUnitCount>= CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerBuffStacks(CycloneStrikes) >= 3 && !CR.PlayerHasBuff(BonedustBrew))
                {

                    if (CR.CanUseMacro(BonedustBrewMacro))
                    {
                        CR.UseMacro(BonedustBrewMacro);
                        return;
                    }
                    else if (CR.CanCastSpell(BonedustBrew))
                    {
                        CR.CastSpell(BonedustBrew);
                        return;
                    }


                }


                if(CR.CanCastSpell(SpinningCraneKick) && CR.LastCastSpellId!= SpinningCraneKick && CR.ToggleAOE && CR.AOEMeleeUnitCount>=CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerBuffStacks(CycloneStrikes) >= 3 && CR.PlayerHasBuff(BonedustBrew))
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }

                if(CR.CanCastSpell(RisingSunKick) && CR.PlayerHasBuff(BonusPressurePoint))
                {
                    CR.CastSpell(RisingSunKick);
                    return;
                }

                if (CR.CanCastSpell(SpinningCraneKick) && CR.LastCastSpellId != SpinningCraneKick && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerHasBuff(Serenity))
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }

                if (CR.CanCastSpell(SpinningCraneKick) && CR.LastCastSpellId != SpinningCraneKick &&  CR.PlayerHasBuff(DanceofChiJi))
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }

                if (CR.CanCastSpell(FistsofFury))
                {
                    CR.CastSpell(FistsofFury);
                    return;
                }

                if (CR.CanCastSpell(SpinningCraneKick) && hasWhirlingDragonPunch && CR.LastCastSpellId != SpinningCraneKick && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerBuffStacks(CycloneStrikes) >= 3 && CR.SpellCooldownDuration(FistsofFury)>300 && CR.SpellCooldownDuration(WhirlingDragonPunch) > 300) 
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }


                if (CR.CanCastSpell(SpinningCraneKick) && !hasWhirlingDragonPunch && CR.LastCastSpellId != SpinningCraneKick && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount) && CR.PlayerBuffStacks(CycloneStrikes) >= 3 && CR.SpellCooldownDuration(FistsofFury) > 300)
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }

                if (CR.CanCastSpell(RisingSunKick) && hasWhirlingDragonPunch && (CR.PlayerBuffStacks(CycloneStrikes) < 3 || CR.SpellCooldownDuration(WhirlingDragonPunch) < 300 || CR.PlayerHasBuff(WeaponsofOrderWW)))
                {
                    CR.CastSpell(RisingSunKick);
                    return;
                }
                if (CR.CanCastSpell(RisingSunKick) && !hasWhirlingDragonPunch)
                {
                    CR.CastSpell(RisingSunKick);
                    return;
                }

                if (CR.CanCastSpell(FistoftheWhiteTiger) && ChiToMax >= 3)
                {
                    CR.CastSpell(FistoftheWhiteTiger);
                    return;
                }
                if (CR.CanCastSpell(RushingJadeWind) && CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount))
                {
                    CR.CastSpell(RushingJadeWind);
                    return;
                }

                if (CR.CanCastSpell(BlackoutKick) && CR.PlayerHasBuff(Serenity) && CR.LastCastSpellId!= BlackoutKick)
                {
                    CR.CastSpell(BlackoutKick);
                    return;
                }

                if (CR.CanCastSpell(BlackoutKick) &&  CR.LastCastSpellId != BlackoutKick && (CR.PlayerHasBuff(ComboBreaker) || CR.PlayerChi >= 4 || (CR.PlayerChi >= 2 && CR.SpellCooldownDuration(FistsofFury) > 400 && CR.SpellCooldownDuration(RisingSunKick) > 300)))
                {
                    CR.CastSpell(BlackoutKick);
                    return;
                }

                if (CR.CanCastSpell(SpinningCraneKick) && CR.PlayerHasBuff(Serenity) && CR.LastCastSpellId != SpinningCraneKick)
                {
                    CR.CastSpell(SpinningCraneKick);
                    return;
                }

              
            }

            if (CR.CanCastSpell(ChiBurst) && CR.TargetRange <= 40 && CR.ToggleAOE && CR.AOEUnitCount >= 2)
            {
                CR.CastSpell(ChiBurst);
                return;
            }

            if (CR.CanCastSpell(ChiWave) && CR.TargetRange <= 40 )
            {
                CR.CastSpell(ChiWave);
                return;
            }

            if (inMelee)
            {

                if (CR.CanCastSpell(TigerPalm) && CR.LastCastSpellId!=TigerPalm)
                {
                    CR.CastSpell(ChiWave);
                    return;
                }

                if (CR.CanCastSpell(EnergizingElixir) && ChiDeficit >= 2)
                {
                    CR.CastSpell(EnergizingElixir);
                    return;
                }

                if (CR.CanCastSpell(BlackoutKick) && CR.LastCastSpellId != TigerPalm && CR.LastCastSpellId != TigerPalm)
                {
                    CR.CastSpell(BlackoutKick);
                    return;
                }
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

                if (CR.CanCastSpell(SpearHandStrike))
                {
                    CR.CastSpell(SpearHandStrike);
                    return true;
                }

            }

            return false;
        }


  


        int[] ExtraComboSpells = { 322101, 101545, 310454 };


        private bool ComboStrike(int spellID) {
            if (CR.LastCastSpellId!=spellID) {
                return true;
            }else{
                foreach(int cs in ExtraComboSpells)
                {
                    if (cs == CR.LastCastSpellId) {
                        return true;
                    }
                }
            }
            return false;
        }



    }
}
