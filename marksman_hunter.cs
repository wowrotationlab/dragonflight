using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class MarksmanHunterSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells

        private int TrueShot = 288613;
        private int MultiShot = 2643;
        private int SteadyShot = 56641;
        private int ArcaneShot = 185358;
        private int DoubleTap = 260402;
        private int RapidFire = 257044;
        private int AimedShot = 19434;
        private int ExplosiveShot = 212431;
        private int Volley = 260243;
        private int SerpentSting = 271788;
        private int Barrage = 120360;
        private int FreezingTrap = 187650;




        private int TarTrap = 187698;
        private int Flare = 1543;
      
        private int WildSpirits = 328231;
        private int FlayedShot = 324149;
        private int ResonatingArrow = 308491;
        private int KillShot = 53351;
        private int WailingArrow = 355589;
        private int DeathChakram = 325028;
        private int Stampede = 201430;
        private int AMurderofCrows = 131894;
        private int ChimaeraShot = 53209;
   
        private int BagofTricks = 312411;


        private int CounterShot = 147362;
        private int MendPet = 136;

        //racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffSteadyFocus = 193533;
        private int FeignDeath = 5384;
        private int BuffTrickShots = 257621;
        private int BuffPreciseShots = 260242;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private int DebuffTarTrap = 135299;
        private int DebuffFreezingTrap = 3355;

        private string NesingwaryTrappingApparatus = "Nesingwary's Trapping Apparatus";

        private string SerpentstalkersTrickery = "Serpentstalker's Trickery";
        private string RaeshalareDeathWhispers = "Rae'shalare, Death's Whispers";
        private string SoulforgeEmbers = "Soulforge Embers";
        private string EagletalonsTrueFocus = "Eagletalon's True Focus";
        private string SurgingShots = "Surging Shots";
        private string PC4Bonus = "4PC Tier Set Bonus + Vigil Legendary";
        private string PC2Bonus = "2PC Tier Set Bonus";

        private string PetAttackMacro = "Pet Attack";
        private string PetStopAttackMacro = "Pet Stop Attack";
        private string VolleyMacro = "Volley Macro";
        private string WildSpiritsMacro = "Wild Spirit Macro";
        private string ResonatingArrowMacro = "Resonating Arrow Macro";
        private string TarTrapMacro = "Tar Trap Macro";
        private string FreezingTrapMacro = "Freezing Trap Macro";
        private string FlareMacro = "Flare Macro";
        private string PetSkillMacro = "Pet Skill Macro";
        private string TogglePetAttack = "Pet Attack";
        private string ToggleInterrupt = "Interrupt";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Marksman Hunter Shadowlands";
            CR.WriteLog("Marksman Hunter Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Hunter;

                 
            CR.AddSpell(TrueShot, ExplosiveShot, Volley, ArcaneShot, SerpentSting, Barrage, MultiShot, SteadyShot, DoubleTap, RapidFire, CounterShot, AimedShot, MendPet, FeignDeath, TarTrap, FreezingTrap, Flare, WildSpirits, FlayedShot, ResonatingArrow, KillShot, WailingArrow, DeathChakram, Stampede, AMurderofCrows, ChimaeraShot, BagofTricks, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, BuffSteadyFocus, BuffTrickShots, FeignDeath, BuffPreciseShots, MendPet, TrueShot, DoubleTap);
            CR.AddDebuff(DebuffTarTrap, DebuffFreezingTrap, SerpentSting);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);
            
            CR.AddSettingsBooleanField(NesingwaryTrappingApparatus, NesingwaryTrappingApparatus, NesingwaryTrappingApparatus, false);
            CR.AddSettingsBooleanField(SerpentstalkersTrickery, SerpentstalkersTrickery, SerpentstalkersTrickery, false);
            CR.AddSettingsBooleanField(RaeshalareDeathWhispers, RaeshalareDeathWhispers, RaeshalareDeathWhispers, false);
            CR.AddSettingsBooleanField(SoulforgeEmbers, SoulforgeEmbers, SoulforgeEmbers, false);
            CR.AddSettingsBooleanField(SurgingShots, SurgingShots, SurgingShots, false);
            CR.AddSettingsBooleanField(EagletalonsTrueFocus, EagletalonsTrueFocus, EagletalonsTrueFocus, false);
            CR.AddSettingsBooleanField(PC4Bonus, PC4Bonus, PC4Bonus, false);
            CR.AddSettingsBooleanField(PC2Bonus, PC2Bonus, PC2Bonus, false);
         


            CR.AddToggle(ToggleBurst);
            CR.AddToggle(TogglePetAttack);
            CR.AddToggle(ToggleInterrupt);

            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(PetAttackMacro, "/petattack [@mouseover,harm][]");
            CR.AddMacro(PetStopAttackMacro, "/petpassive\r\n/petfollow"); 
            
            CR.AddMacro(VolleyMacro, @"#showtooltip Volley
/use [@cursor]Volley", Volley);
            CR.AddMacro(WildSpiritsMacro, @"#showtooltip Wild Spirits
/use [@cursor]Wild Spirits", WildSpirits);
            CR.AddMacro(ResonatingArrowMacro, @"#showtooltip Resonating Arrow
/use [@cursor]Resonating Arrow", ResonatingArrow);
            CR.AddMacro(TarTrapMacro, @"#showtooltip
/use [@cursor]Tar Trap", TarTrap);
            CR.AddMacro(FreezingTrapMacro, @"#showtooltip
/use [@cursor]Freezing Trap", FreezingTrap);
            CR.AddMacro(FlareMacro, @"#showtooltip
/use [@cursor]Flare", Flare);
            CR.AddMacro(PetSkillMacro, @"/use [@pettarget]Claw
/use [@pettarget]Bite
/use [@pettarget]Smack");
            sw = new Stopwatch();
            sw.Start();

        }
        private bool Interrupts()
        {
            bool isInterrupt = CR.TargetCurrentCastInterruptible && CR.Toggle(ToggleInterrupt) && CR.TargetCurrentCastPercent > 10;


            if (isInterrupt)
            {

                if (CR.CanCastSpell(CounterShot))
                {
                    CR.CastSpell(CounterShot);
                    return true;
                }

            }

            return false;
        }
        public override void Rotation()
        {



        }

        private bool FlareOnTrap = false;
        private int PetTarget;
        private long LastPetMacro = 0;
        Random rand = new Random();
        public override void RotationInCombat()
        {
           
            int Focus = CR.PlayerFocus;
            int FocusToMax = CR.PlayerFocusMax - CR.PlayerFocus;
            int TargetGUIDNPCID = CR.TargetNPCID;
            bool BurstPhase = CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) ||  (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(TrueShot) ;


            if (CR.PlayerHasBuff(FeignDeath) || !CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 40)
            {
                return;
            }

            if (CR.LastCastSpellId == Flare)
            {
                FlareOnTrap = false;
            }

            if (FlareOnTrap)
            {
                if (CR.CanUseMacro(FlareMacro))
                {
                    CR.UseMacro(FlareMacro);
                    return;
                } 
                else if (CR.CanCastSpell(Flare))
                {
                    CR.CastSpell(Flare);
                    return;
                }
            }

            if (Interrupts()) return;

            if (CR.PlayerHasPet && CR.PetHealthPercent < 75 && CR.PetHealthPercent > 1)
            {
                if (!CR.PetHasBuff(MendPet))
                {
                    if (CR.CanCastSpell(MendPet))
                    {
                        CR.CastSpell(MendPet);
                        return;
                    }
                }

            }

            if(!CR.TargetIsInCombat && CR.CanCastSpell(AimedShot) && !CR.PlayerIsMoving && CR.PlayerCurrentCastID==0 && CR.LastCastSpellId!=AimedShot)
            {
                CR.CastSpell(AimedShot);
                return;
            }


            if (CR.Toggle(TogglePetAttack))
            {

                if (CR.PlayerHasPet && !CR.PetIsDead)
                {
                    if (CR.PetRange > 10 && CR.PetHealthPercent < 60)
                    {
                        CR.UseMacro(PetStopAttackMacro);
                    }
                    else if (((CR.PetRange <= 6 && CR.PetRange > 0) || (TargetGUIDNPCID > 0 && PetTarget != TargetGUIDNPCID)) && CR.PetHealthPercent >= 60)
                    {
                        CR.UseMacro(PetAttackMacro);
                        PetTarget = TargetGUIDNPCID;

                    }
                   
                    if (CR.PetIsAutoAttacking && CR.PetHealthPercent > 60)
                    {
                        if (CR.CanUseMacro(PetSkillMacro) && sw.ElapsedMilliseconds - LastPetMacro > (rand.NextDouble() * 3000) + 1000)
                        {
                            CR.UseMacro(PetSkillMacro);
                            LastPetMacro = sw.ElapsedMilliseconds;
                        }
                    }
                }


            }
            else
            {

                if (CR.PetRange > 6)
                {
                    if (CR.PetIsInCombat && CR.PetRange > 10)
                    {
                        CR.UseMacro(PetStopAttackMacro);

                    }
                }


            }

        

            if (BurstPhase)
            {
                if (CR.CanCastSpell(TrueShot))
                {
                    CR.CastSpell(TrueShot);
                    return;
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
            }






     

            if (CR.ToggleAOE && CR.AOEUnitCount >= 3)
            {
                if (BurstPhase)
                {
                    if (CR.CanUseMacro(WildSpiritsMacro) && CR.PlayerCanAttackMouseover)
                    {
                        CR.UseMacro(WildSpiritsMacro);
                        return;
                    }
                    else if (CR.CanCastSpell(WildSpirits))
                    {
                        CR.CastSpell(WildSpirits);
                        return;
                    }

                    if (CR.CanUseMacro(ResonatingArrowMacro) && CR.PlayerCanAttackMouseover)
                    {
                        CR.UseMacro(ResonatingArrowMacro);
                        return;
                    }
                    else if (CR.CanCastSpell(ResonatingArrow))
                    {
                        CR.CastSpell(ResonatingArrow);
                        return;
                    }
                }
               


                if (CR.CanUseMacro(VolleyMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(VolleyMacro);
                    return;
                }
                else if (CR.CanCastSpell(Volley))
                {
                    CR.CastSpell(Volley);
                    return;
                }


            }



         /*   if (CR.CanCastSpell(KillShot))
            {
                CR.CastSpell(KillShot);
                return;
            }
*/

   
            // trueshot rotation
            if (CR.PlayerHasBuff(TrueShot))
            {
                if (CR.ToggleAOE && CR.AOEUnitCount >= 2 && !CR.PlayerHasBuff(BuffTrickShots) && CR.CanCastSpell(MultiShot))
                {
                 
                    CR.CastSpell(MultiShot);
                    return;
                    
                }
                if(CR.SpellCooldownDuration(RapidFire)<100 && CR.CanCastSpell(DoubleTap))
                {
                    CR.CastSpell(DoubleTap);
                    return;
                }
                if (CR.CanCastSpell(RapidFire))
                {
                    CR.CastSpell(RapidFire);
                    return;
                }
                if (CR.CanCastSpell(AimedShot) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(AimedShot);
                    return;
                }
                if (CR.CanCastSpell(KillShot))
                {
                    CR.CastSpell(KillShot);
                    return;
                }
                if(CR.PlayerHasBuff(BuffPreciseShots) && CR.AOEUnitCount<3  || !CR.ToggleAOE)
                {
                    if (CR.SpellIsKnown(ChimaeraShot))
                    {
                        if (CR.CanCastSpell(ChimaeraShot))
                        {
                            CR.CastSpell(ChimaeraShot);
                            return;
                        }
                    }
                    else
                    {
                        if (CR.CanCastSpell(ArcaneShot))
                        {
                            CR.CastSpell(ArcaneShot);
                            return;
                        }
                    }

                 
                
                }
                if (CR.CanCastSpell(SteadyShot))
                {
                    CR.CastSpell(SteadyShot);
                    return;
                }
            }





         
            if (CR.IsTalentSelectedRetail(1, 4) && (!CR.PlayerHasBuff(BuffSteadyFocus) || CR.PlayerBuffDuration(BuffSteadyFocus)<5) && CR.LastCastSpellId == SteadyShot)
            {
                if (CR.CanCastSpell(SteadyShot))
                {
                    CR.CastSpell(SteadyShot);
                      
                }
                return;
            }
            

            if (CR.GetSettingBoolean(SoulforgeEmbers))
            {
                if (CR.CanUseMacro(TarTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffTarTrap))
                {
                    CR.UseMacro(TarTrapMacro);
                    FlareOnTrap = true;
                    return;
                }
                else if (CR.CanCastSpell(TarTrap))
                {
                    CR.CastSpell(TarTrap);
                    FlareOnTrap = true;
                    return;
                }

            }

            if(CR.GetSettingBoolean(RaeshalareDeathWhispers) && CR.ToggleAOE && CR.AOEUnitCount >= 2 && CR.CanCastSpell(WailingArrow))
            {
                CR.CastSpell(WailingArrow);
                return;
            }

            //aoe rotation
            if (CR.ToggleAOE && CR.AOEUnitCount >= 3)
            {
                if (CR.CanCastSpell(DoubleTap))
                {
                    CR.CastSpell(DoubleTap);
                    return;
                }


                if (CR.CanUseMacro(VolleyMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(VolleyMacro);
                    return;
                }
                else if (CR.CanCastSpell(Volley))
                {
                    CR.CastSpell(Volley);
                    return;
                }

                if (!CR.PlayerHasBuff(BuffTrickShots))
                {
                    if (CR.CanCastSpell(MultiShot))
                    {
                        CR.CastSpell(MultiShot);
                        return;
                    }
                }
                if (CR.CanCastSpell(RapidFire))
                {
                    CR.CastSpell(RapidFire);
                    return;
                }
                if (CR.CanCastSpell(Barrage))
                {
                    CR.CastSpell(Barrage);
                    return;
                }
                if (CR.CanCastSpell(AimedShot) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(AimedShot);
                    return;
                }
                if (CR.CanCastSpell(ExplosiveShot))
                {
                    CR.CastSpell(ExplosiveShot);
                    return;
                }

                if (CR.TargetTTD<60 && CR.CanCastSpell(AMurderofCrows))
                {
                    CR.CastSpell(AMurderofCrows);
                    return;
                }

                if((CR.PlayerHasBuff(BuffPreciseShots) || Focus > 60) && CR.CanCastSpell(MultiShot))
                {
                    CR.CastSpell(MultiShot);
                    return;
                }

                if (FocusToMax > 25 && CR.CanCastSpell(DeathChakram))
                {
                    CR.CastSpell(DeathChakram);
                    return;
                }

                if (CR.CanCastSpell(KillShot))
                {
                    CR.CastSpell(KillShot);
                    return;
                }

                if (CR.CanCastSpell(FlayedShot))
                {
                    CR.CastSpell(FlayedShot);
                    return;
                }


                if (CR.GetSettingBoolean(NesingwaryTrappingApparatus))
                {

                    if (FocusToMax > 45)
                    {
                        if (CR.CanUseMacro(TarTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffTarTrap))
                        {
                            CR.UseMacro(TarTrapMacro);
           
                            return;
                        }
                        else if (CR.CanCastSpell(TarTrap))
                        {
                            CR.CastSpell(TarTrap);
                 
                            return;
                        }
                    }
                    else
                    {
                        if (CR.CanUseMacro(FreezingTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffFreezingTrap))
                        {
                            CR.UseMacro(FreezingTrapMacro);
                           
                            return;
                        }
                        else if (CR.CanCastSpell(FreezingTrap))
                        {
                            CR.CastSpell(FreezingTrap);
                      
                            return;
                        }
                    }

                }



                if (CR.CanCastSpell(SteadyShot))
                {
                    CR.CastSpell(SteadyShot);
                    return;
                }


            }



            if (CR.TargetTTD < 60 && CR.CanCastSpell(AMurderofCrows))
            {
                CR.CastSpell(AMurderofCrows);
                return;
            }

            if (CR.ToggleAOE && CR.AOEUnitCount>=3 && CR.SpellCooldownDuration(RapidFire) < 100 && CR.CanCastSpell(DoubleTap))
            {
                CR.CastSpell(DoubleTap);
                return;
            }

            if (Focus < 75 && CR.CanCastSpell(DeathChakram))
            {
                CR.CastSpell(DeathChakram);
                return;
            }

            if (CR.CanCastSpell(RapidFire) && Focus<75)
            {
                CR.CastSpell(RapidFire);
                return;
            }


            if (CR.IsTalentSelectedRetail(1, 4) && !CR.PlayerHasBuff(BuffSteadyFocus) && FocusToMax > 30)
            {
                if (CR.CanCastSpell(SteadyShot))
                {
                    CR.CastSpell(SteadyShot);
                    return;
                }
              
            }

            if (CR.CanCastSpell(KillShot))
            {
                CR.CastSpell(KillShot);
                return;
            }

            if (CR.PlayerHasBuff(BuffPreciseShots))
            {
                if (CR.SpellIsKnown(ChimaeraShot))
                {
                    if (CR.CanCastSpell(ChimaeraShot))
                    {
                        CR.CastSpell(ChimaeraShot);
                        return;
                    }
                }
                else
                {
                    if (CR.CanCastSpell(ArcaneShot))
                    {
                        CR.CastSpell(ArcaneShot);
                        return;
                    }
                }
           

              

            }


            if (CR.CanCastSpell(AimedShot) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(AimedShot);
                return;
            }
            if (CR.CanCastSpell(FlayedShot))
            {
                CR.CastSpell(FlayedShot);
                return;
            }

            if (CR.ToggleAOE && CR.AOEUnitCount>=2 && CR.CanCastSpell(Barrage))
            {
                CR.CastSpell(Barrage);
                return;
            }


            if (CR.ToggleAOE && CR.AOEUnitCount >= 3 && CR.CanCastSpell(ExplosiveShot))
            {
                CR.CastSpell(ExplosiveShot);
                return;
            }

            if(CR.CanCastSpell(SerpentSting) && CR.TargetDebuffDuration(SerpentSting, true)< 360)
            {
                CR.CastSpell(SerpentSting);
                return;
            }


            if (CR.SpellIsKnown(ChimaeraShot))
            {
                if (CR.CanCastSpell(ChimaeraShot) && Focus > 50)
                {
                    CR.CastSpell(ChimaeraShot);
                    return;
                }
            }
            else
            {
                if (CR.CanCastSpell(ArcaneShot) && Focus > 50)
                {
                    CR.CastSpell(ArcaneShot);
                    return;
                }
            }

            if (CR.GetSettingBoolean(NesingwaryTrappingApparatus))
            {

                if (FocusToMax > 45)
                {
                    if (CR.CanUseMacro(TarTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffTarTrap))
                    {
                        CR.UseMacro(TarTrapMacro);

                        return;
                    }
                    else if (CR.CanCastSpell(TarTrap))
                    {
                        CR.CastSpell(TarTrap);

                        return;
                    }
                }
                else
                {
                    if (CR.CanUseMacro(FreezingTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffFreezingTrap))
                    {
                        CR.UseMacro(FreezingTrapMacro);

                        return;
                    }
                    else if (CR.CanCastSpell(FreezingTrap))
                    {
                        CR.CastSpell(FreezingTrap);

                        return;
                    }
                }

            }



            if (CR.CanCastSpell(SteadyShot) )
            {
                CR.CastSpell(SteadyShot);
                return;
            }


        }
        public override void RotationOutOfCombat()
        {

            if (CR.PlayerHasBuff(FeignDeath))
            {
                return;
            }


            if (CR.PlayerHasPet && CR.PetHealthPercent < 71 && CR.PetHealthPercent > 1)
            {
                if (!CR.PetHasBuff(MendPet))
                {
                    if (CR.CanCastSpell(MendPet))
                    {
                        //CR.WriteLog(CR.PetHealthPercent.ToString());
                        CR.CastSpell(MendPet);
                        return;
                    }
                }

            }


        }






    }
}
