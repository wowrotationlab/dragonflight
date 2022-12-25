using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class SurvivalHunterSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst"; 
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells
        private int CoordinatedAssault = 266779;
        private int Harpoon = 190925;
        private int SerpentSting = 259491;
        private int AspectoftheEagle = 186289;
        private int WildfireBomb = 259495;
        private int ShrapnelBomb = 270335;
        private int VolatileBomb = 271045;
        private int PheromonBomb = 270323;
        private int Chakram = 259391;
        private int Muzzle = 187707;
        private int FlankingStrike = 269751;
        private int SteelTrap = 162488;
        private int MongooseBite = 259387;
        private int RaptorStrike = 186270;
        private int Carve = 187708;
        private int Butchery = 212436;
        private int KillShot = 53351;
        private int TarTrap = 187698;
        private int Flare = 1543;
      
        private int WildSpirits  =328231 ;
        private int FlayedShot = 324149;
        private int ResonatingArrow  = 308491;


     
        private int WailingArrow  = 355589;
        private int DeathChakram = 325028;
    
        private int AMurderofCrows = 131894;

        private int KillCommand = 34026;
        private int BagofTricks = 312411;
     

        private int MendPet = 136;
        private int FreezingTrap = 187650;


        //racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        private int BuffBloodlust = 2825;
        private int BuffHeroism  = 32182;
        private int BuffVipersVenom = 268552;
        private int FeignDeath = 5384;
        private int BuffCoordinatedAssault = 266779;
        private int BuffTipoftheSpear = 260285;
        private int BuffMongooseFury = 259388;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private int DebuffTarTrap = 135299;
        private int DebuffFreezingTrap = 3355;
        private int DebuffSteelTrap = 162487;
        private int DebuffInteralBleeding = 270343;
        private int DebuffShrapnelBomb = 270339;

        private string NesingwaryTrappingApparatus = "Nesingwary's Trapping Apparatus";
        private string RaeshalareDeathWhispers = "Rae'shalare, Death's Whispers";
        private string SoulforgeEmbers = "Soulforge Embers";

        private string PetAttackMacro = "Pet Attack";
        private string PetStopAttackMacro = "Pet Stop Attack";
        private string WildSpiritsMacro = "Wild Spirit Macro";
        private string ResonatingArrowMacro = "Resonating Arrow Macro";
        private string TarTrapMacro = "Tar Trap Macro";
        private string FreezingTrapMacro = "Freezing Trap Macro";
        private string SteelTrapMacro = "Steel Trap Macro";
        private string FlareMacro = "Flare Macro";
        private string PetSkillMacro = "Pet Skill Macro";
        private string TogglePetAttack = "Pet Attack";
        private string ToggleInterrupt = "Interrupt";
        private string StartAttack = "Start Attack";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Survival Hunter Shadowlands";
            CR.WriteLog("Survival Hunter Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Hunter;


          

            CR.AddSpell(CoordinatedAssault, Harpoon, AspectoftheEagle, WildfireBomb, ShrapnelBomb, VolatileBomb, PheromonBomb, Chakram, SerpentSting, FlankingStrike, KillCommand, Carve, Butchery, MongooseBite, RaptorStrike, KillShot, MendPet, FeignDeath, Muzzle, TarTrap, FreezingTrap, SteelTrap, Flare,  WildSpirits, FlayedShot, ResonatingArrow, WailingArrow, DeathChakram,  AMurderofCrows,  BagofTricks, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);
            

            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, BuffVipersVenom, BuffCoordinatedAssault, FeignDeath, BuffTipoftheSpear, MendPet, BuffMongooseFury, AspectoftheEagle);
            CR.AddDebuff(DebuffTarTrap, DebuffFreezingTrap, DebuffSteelTrap, SerpentSting, DebuffInteralBleeding, DebuffShrapnelBomb);
            

            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

            CR.AddSettingsBooleanField(NesingwaryTrappingApparatus, NesingwaryTrappingApparatus, NesingwaryTrappingApparatus, false);
            CR.AddSettingsBooleanField(RaeshalareDeathWhispers, RaeshalareDeathWhispers, RaeshalareDeathWhispers, false);
            CR.AddSettingsBooleanField(SoulforgeEmbers, SoulforgeEmbers, SoulforgeEmbers, false);

            CR.AddToggle(ToggleBurst);
            CR.AddToggle(TogglePetAttack);
            CR.AddToggle(ToggleInterrupt);


            CR.AddMacro(StartAttack, "/startattack [@mouseover, harm][harm][@targettarget, harm][]");
            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(PetAttackMacro, "/petattack [@mouseover,harm][]");
            CR.AddMacro(PetStopAttackMacro, "/petpassive\r\n/petfollow");

            CR.AddMacro(WildSpiritsMacro, @"#showtooltip Wild Spirits
/use [@cursor]Wild Spirits",WildSpirits);
            CR.AddMacro(ResonatingArrowMacro, @"#showtooltip Resonating Arrow
/use [@cursor]Resonating Arrow",ResonatingArrow);
            CR.AddMacro(TarTrapMacro, @"#showtooltip
/use [@cursor]Tar Trap",TarTrap);
            CR.AddMacro(SteelTrapMacro, @"#showtooltip
/use [@cursor]Steel Trap", TarTrap);
            CR.AddMacro(FreezingTrapMacro, @"#showtooltip
/use [@cursor]Freezing Trap", FreezingTrap);
            CR.AddMacro(FlareMacro, @"#showtooltip
/use [@cursor]Flare",Flare);

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

                if (CR.CanCastSpell(Muzzle))
                {
                    CR.CastSpell(Muzzle);
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
            bool BurstPhase =CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) || (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(CoordinatedAssault);

            bool MeleeRange = CR.TargetRange <= 5;
            bool MongooseBiteRange = CR.TargetRange <= 5 || (CR.PlayerHasBuff(AspectoftheEagle) && CR.TargetRange < 40);
                


            if (CR.PlayerHasBuff(FeignDeath) || !CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 30)
            {
                return;
            }

            if(CR.LastCastSpellId == Flare)
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


            if (CR.Toggle(TogglePetAttack))
            {

                if (CR.PlayerHasPet && !CR.PetIsDead)
                {
                   if (!CR.PetIsAutoAttacking || (TargetGUIDNPCID > 0 && PetTarget != TargetGUIDNPCID))
                    {
                        CR.UseMacro(PetAttackMacro);
                        PetTarget = TargetGUIDNPCID;

                    }


                    if (CR.PetIsAutoAttacking)
                    {
                        if (CR.CanUseMacro(PetSkillMacro) && sw.ElapsedMilliseconds - LastPetMacro > (rand.NextDouble()*3000)+1000)
                        {
                            CR.UseMacro(PetSkillMacro);
                            LastPetMacro = sw.ElapsedMilliseconds;
                        }

                        if (CR.CanCastSpell(KillCommand))
                        {
                            CR.CastSpell(KillCommand);
                        }
                    }
                }


            }
            else
            {

                if (CR.PetIsAutoAttacking)
                {
                    CR.UseMacro(PetStopAttackMacro);

                }
                


            }


            if(CR.TargetRange>5 && CR.TargetRange < 30 && CR.CanCastSpell(Harpoon))
            {
                CR.CastSpell(Harpoon);
                return;
            }

            if (MeleeRange && !CR.PlayerIsAutoAttacking)
            {
                CR.UseMacro(StartAttack);
            }



            if (BurstPhase)
            {
                if (CR.CanCastSpell(CoordinatedAssault))
                {
                    CR.CastSpell(CoordinatedAssault);
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



            if(CR.ToggleAOE && CR.AOEMeleeUnitCount>=3 )
            {
                // aoe

                //1,2
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

                //3,4
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

                
                //6,7
                if (CR.IsTalentSelectedRetail(1, 7) && CR.PlayerHasBuff(CoordinatedAssault) && CR.PlayerBuffDuration(CoordinatedAssault) < 150)
                {
                    if (CR.CanCastSpell(MongooseBite))
                    {
                        CR.CastSpell(MongooseBite);
                        return;
                    }
                    else if (CR.CanCastSpell(RaptorStrike))
                    {
                        CR.CastSpell(RaptorStrike);
                        return;
                    }
                }

                if (CR.CanCastSpell(WildfireBomb))
                {
                    CR.CastSpell(WildfireBomb);
                    return;
                }
                if (CR.CanCastSpell(PheromonBomb))
                {
                    CR.CastSpell(PheromonBomb);
                    return;
                }
               
                if (CR.CanCastSpell(ShrapnelBomb))
                {
                    CR.CastSpell(ShrapnelBomb);
                    return;
                }
                if (CR.CanCastSpell(Chakram))
                {
                    CR.CastSpell(Chakram);
                    return;
                }
                if (CR.CanCastSpell(Butchery))
                {
                    CR.CastSpell(Butchery);
                    return;
                }
                if (CR.CanCastSpell(Carve))
                {
                    CR.CastSpell(Carve);
                    return;
                }
                if (CR.CanCastSpell(DeathChakram) && BurstPhase)
                {
                    CR.CastSpell(DeathChakram);
                    return;
                }



                if (CR.CanCastSpell(AMurderofCrows) && CR.TargetTTD < 60)
                {
                    CR.CastSpell(AMurderofCrows);
                    return;
                }

                if (Focus > 50 && CR.TargetDebuffDuration(SerpentSting) < 300 && CR.CanCastSpell(SerpentSting) && CR.TargetTTD>10)
                {
                    CR.CastSpell(SerpentSting);

                    return;
                }

                if (CR.CanCastSpell(VolatileBomb))
                {
                    CR.CastSpell(VolatileBomb);
                    return;
                }

                if (CR.CanCastSpell(FlayedShot))
                {
                    CR.CastSpell(FlayedShot);
                    return;
                }
                if (CR.CanCastSpell(KillShot))
                {
                    CR.CastSpell(KillShot);
                    return;
                }
                if (CR.CanUseMacro(SteelTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffSteelTrap))
                {
                    CR.UseMacro(SteelTrapMacro);

                    return;
                }
                else if (CR.CanCastSpell(SteelTrap))
                {
                    CR.CastSpell(SteelTrap);

                    return;
                }
                if (FocusToMax > 25 && CR.CanCastSpell(KillCommand))
                {
                    CR.CastSpell(KillCommand);
                    return;
                }

                if (FocusToMax > 40 && CR.CanCastSpell(FlankingStrike))
                {
                    CR.CastSpell(FlankingStrike);
                    return;
                }

                if (Focus > 50)
                {
                    if (CR.CanCastSpell(MongooseBite))
                    {
                        CR.CastSpell(MongooseBite);
                        return;
                    }
                    else if (CR.CanCastSpell(RaptorStrike))
                    {
                        CR.CastSpell(RaptorStrike);
                        return;
                    }
                }
            }
            else
            {
                // single target 



                if (CR.CanCastSpell(AMurderofCrows) && CR.TargetTTD < 60)
                {
                    CR.CastSpell(AMurderofCrows);
                    return;
                }

                if(CR.IsTalentSelectedRetail(1,7) && CR.PlayerHasBuff(CoordinatedAssault) && CR.PlayerBuffDuration(CoordinatedAssault) < 150)
                {
                    if (CR.CanCastSpell(MongooseBite))
                    {
                        CR.CastSpell(MongooseBite);
                        return;
                    }
                    else if (CR.CanCastSpell(RaptorStrike))
                    {
                        CR.CastSpell(RaptorStrike);
                        return;
                    }
                }

                if (CR.CanCastSpell(KillShot))
                {
                    CR.CastSpell(KillShot);
                    return;
                }

                if (FocusToMax > 25 && CR.CanCastSpell(KillCommand))
                {
                    CR.CastSpell(KillCommand);
                    return;
                }


               
                if (CR.CanCastSpell(FlayedShot))
                {
                    CR.CastSpell(FlayedShot);
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


                if(!CR.IsTalentSelectedRetail(1,2) && !CR.IsTalentSelectedRetail(2, 7))
                {
                    if (CR.CanCastSpell(WildfireBomb))
                    {
                        CR.CastSpell(WildfireBomb);
                        return;
                    }
                }

                if (CR.IsTalentSelectedRetail(1, 2) && !CR.IsTalentSelectedRetail(2, 7))
                {
                    if (CR.CanCastSpell(WildfireBomb) && CR.SpellCurrentCharge(WildfireBomb)==2)
                    {
                        CR.CastSpell(WildfireBomb);
                        return;
                    }
                }

                if (FocusToMax > 25 && CR.CanCastSpell(DeathChakram))
                {
                    CR.CastSpell(DeathChakram);
                    return;
                }

                if (CR.CanUseMacro(SteelTrapMacro) && CR.PlayerCanAttackMouseover && !CR.MouseoverHasDebuff(DebuffSteelTrap))
                {
                    CR.UseMacro(SteelTrapMacro);

                    return;
                }
                else if (CR.CanCastSpell(SteelTrap))
                {
                    CR.CastSpell(SteelTrap);

                    return;
                }

                if(CR.PlayerHasBuff(BuffVipersVenom) && CR.TargetDebuffDuration(SerpentSting) < 300 && CR.CanCastSpell(SerpentSting))
                {
                    CR.CastSpell(SerpentSting);

                    return;
                }

                if(CR.IsTalentSelectedRetail(2, 7) && (Focus > 60 || CR.TargetDebuffStacks(DebuffInteralBleeding) >= 3) && CR.CanCastSpell(ShrapnelBomb))
                {
                    CR.CastSpell(ShrapnelBomb);
                    return;
                }

                if(CR.IsTalentSelectedRetail(2, 7) && CR.TargetDebuffDuration(SerpentSting)<100 && CR.SpellIsUsable(VolatileBomb) && CR.CanCastSpell(SerpentSting))
                {
                    CR.CastSpell(SerpentSting);
                    return;
                }

                if (CR.TargetHasDebuff(DebuffShrapnelBomb))
                {
                    if (CR.CanCastSpell(MongooseBite))
                    {
                        CR.CastSpell(MongooseBite);
                        return;
                    }
                    else if (CR.CanCastSpell(RaptorStrike))
                    {
                        CR.CastSpell(RaptorStrike);
                        return;
                    }
                }

                if (CR.CanCastSpell(KillShot))
                {
                    CR.CastSpell(KillShot);
                    return;
                }
                if (CR.CanCastSpell(VolatileBomb))
                {
                    CR.CastSpell(VolatileBomb);
                    return;
                }
                if (CR.CanCastSpell(MongooseBite) &&( CR.PlayerHasBuff(BuffMongooseFury) || Focus > 60 ))
                {
                    CR.CastSpell(MongooseBite);
                    return;
                }
                if (CR.IsTalentSelectedRetail(1, 2) && !CR.IsTalentSelectedRetail(2, 7))
                {
                    if (CR.CanCastSpell(WildfireBomb))
                    {
                        CR.CastSpell(WildfireBomb);
                        return;
                    }
                }

                if (CR.IsTalentSelectedRetail(2, 7) &&  CR.CanCastSpell(PheromonBomb))
                {
                    CR.CastSpell(PheromonBomb);
                    return;
                }


                if(CR.IsTalentSelectedRetail(1, 7))
                {
                    if (!CR.PlayerHasBuff(CoordinatedAssault) && CR.TargetDebuffDuration(SerpentSting)<300 && CR.CanCastSpell(SerpentSting))
                    {
                        CR.CastSpell(SerpentSting);
                        return;
                    }
                }
                else
                {
                    if (CR.TargetDebuffDuration(SerpentSting) < 300 && CR.CanCastSpell(SerpentSting))
                    {
                        CR.CastSpell(SerpentSting);
                        return;
                    }
                }

                if(FocusToMax > 40 && CR.CanCastSpell(FlankingStrike))
                {
                    CR.CastSpell(FlankingStrike);
                    return;
                }

                if (CR.CanCastSpell(MongooseBite))
                {
                    CR.CastSpell(MongooseBite);
                    return;
                }
                else if (CR.CanCastSpell(RaptorStrike))
                {
                    CR.CastSpell(RaptorStrike);
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
