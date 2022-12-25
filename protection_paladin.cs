using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class ProtectionPaladinSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Offensive", "Defensive" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleBigDefensives = "Defensives";
        private string ToggleInterrupt = "Interrupt";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells
        private int Consecration = 26573;
        private int Judgement = 275779;
        private int HammerofWrath = 24275;
        private int AvengersShield = 31935;
        private int HammeroftheRighteous = 53595;
        private int BlessedHammer = 204019;
        private int Seraphim = 152262;
        private int AvengingWrath = 31884;
        private int WordofGlory = 85673;
        private int ShieldoftheRighteous = 53600;
        private int HandofReckoning = 62124;
        private int GuardianofAncientKings  = 86659;
        private int ArdentDefender  = 31850;
        private int HolyAvenger = 105809;
        private int DevotionAura = 465;
        private int DivineShield  = 642;
        private int Rebuke = 96231;
        private int MomentofGlory = 327193;
        private int HammerofJustice = 853;
        private int LayOnHands = 633;
        private int AshenHallow = 316958;
        private int DivineToll = 304971;
        private int VanquishersHammer = 328204;
        private int Fleshcraft = 324631;
        private int PurifySoul = 323436;
        private int Redemption = 7328;
        private int BlessingOfProtection = 1022;
        private int BlessingOfFreedom = 1044;
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
        private int BuffShieldoftheRighteous = 132403;
        private int BuffRoyalDecree = 340147;
        private int BuffShiningLight = 327510;
        private int DebuffForbearance = 25771;
        private int BuffDivinePurpose = 223819;
        private int BuffFinalStand = 204079;
        private string AOERotationEnemyCount = "AOE Enemy Count";
        private string FinalStandHealth = "Final Stand Health";
        private string LayonHandsHealth = "Lay On Hands Health";
        private string GuardianOfAncientKingsHealth = "Guardian Of Ancient Kings Health";
        private string ArdentDefenderHealth = "Ardent Defender Health";
        private string WordOfGloryHealth = "Word of Glory Health";
        private string WordOfGloryHealthShiningLight = "Word of Glory Health w SL"; 
        private string BlessingOfProtectionFriendly = "Blessing of Protection Friendly Health";

        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Protection Paladin Shadowlands";
            CR.WriteLog("Protection Paladin Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Paladin;




            CR.AddSpell(Consecration, Judgement, HammerofWrath, AvengersShield, HammeroftheRighteous, BlessedHammer, Seraphim, AvengingWrath, WordofGlory, ShieldoftheRighteous, HandofReckoning, GuardianofAncientKings, ArdentDefender, HolyAvenger, HammerofJustice, DevotionAura, DivineShield, LayOnHands, Rebuke, MomentofGlory, Redemption, BlessingOfProtection, BlessingOfFreedom, AshenHallow, DivineToll, VanquishersHammer, Fleshcraft, PurifySoul ,Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, MomentofGlory, BuffRoyalDecree, BuffShieldoftheRighteous, ArdentDefender, Consecration, BuffShiningLight, BuffDivinePurpose, DivineShield, BuffFinalStand);
            CR.AddDebuff(DebuffForbearance);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

       
            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleInterrupt);
            CR.AddToggle(ToggleBigDefensives);

            
            CR.AddSettingsNumberField(AOERotationEnemyCount, AOERotationEnemyCount, "Number of enemies to perform AOE rotation", 3);
            CR.AddSettingsNumberField(GuardianOfAncientKingsHealth, GuardianOfAncientKingsHealth, "Health percent to cast Guardian of Ancient Kings", 25);
            CR.AddSettingsNumberField(ArdentDefenderHealth, ArdentDefenderHealth, "Health percent to cast Ardent Defender", 20);
            CR.AddSettingsNumberField(LayonHandsHealth, LayonHandsHealth, "Health percent to cast Lay on Hands", 15);
            CR.AddSettingsNumberField(FinalStandHealth, FinalStandHealth, "Health percent to cast Divine Shield", 10);
            CR.AddSettingsNumberField(WordOfGloryHealth, WordOfGloryHealth, "Health percent to cast Word of Glory", 50);
            CR.AddSettingsNumberField(WordOfGloryHealthShiningLight, WordOfGloryHealthShiningLight, "Health percent to cast Word of Glory with Shining Light buff", 50);
            CR.AddSettingsNumberField(BlessingOfProtectionFriendly, BlessingOfProtectionFriendly, "Friendly unit health percent to cast BOP", 15);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
           
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {



        }


        Random rand = new Random();
        public override void RotationInCombat()
        {

            if (FriendlyRotation()) return;

            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 30)
            {
                return;
            }


            if (isBurst && CoolDowns()) return;

            if (Mitigation())  return; 

            if (Interrupts())  return; 


            if (CR.ToggleAOE && CR.AOEMeleeUnitCount >= CR.GetSettingNumber(AOERotationEnemyCount)) {
                if (MultiTarget())  return; 
            }
            else {
                if (SingleTarget())  return; 
            }

  

        }

      

        public override void RotationOutOfCombat()
        {
            if (FriendlyRotation()) return;

        }


  


        bool isBurst => (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(AvengingWrath) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) || CR.PlayerHasBuff(BuffHeroism) ;
        bool inMelee => CR.TargetRange <= 5;


        private bool Interrupts()
        {
            bool isInterrupt = CR.TargetCurrentCastInterruptible && CR.Toggle(ToggleInterrupt) && CR.TargetCurrentCastPercent > 10;


            if (isInterrupt)
            {

                if (CR.CanCastSpell(AvengersShield))
                {
                    CR.CastSpell(AvengersShield);
                    return true;
                }

                if (CR.CanCastSpell(Rebuke))
                {
                    CR.CastSpell(Rebuke);
                    return true;
                }
            }

            return false;
        }

        private bool FriendlyRotation() { 

            if(CR.TargetIsFriend && CR.TargetIsPlayer && !CR.UnitIsTarget("player") )
            {
                if (!CR.PlayerIsInCombat)
                {

                    if(CR.TargetIsDead && !CR.PlayerIsMoving && CR.CanCastSpell(Redemption)){
                        CR.CastSpell(Redemption);
                        return true;
                    }

                }

                if (!CR.TargetIsDead)
                {
                    if(CR.CanCastSpell(WordofGlory) && CR.TargetHealthPercent <= CR.GetSettingNumber(WordOfGloryHealth))
                    {
                        CR.CastSpell(WordofGlory);
                        return true;
                    }

                    if (CR.CanCastSpell(BlessingOfProtection) && CR.TargetHealthPercent <= CR.GetSettingNumber(BlessingOfProtectionFriendly) && CR.TargetHasDebuff(DebuffForbearance))
                    {
                        CR.CastSpell(WordofGlory);
                        return true;
                    }

                }

           

            }

            return false;
        }
        private bool shouldCastDivineShield() {

            if (CR.PlayerHealthPercent < CR.GetSettingNumber(FinalStandHealth) && !CR.PlayerHasDebuff(DebuffForbearance) && CR.CanCastSpell(DivineShield))
            {
                if (CR.PlayerIsInGroup || CR.PlayerIsInRaid)
                {
                    if(CR.IsTalentSelected(3))
                    {
                        CR.CastSpell(DivineShield);
                        return true;
                    }
                }
                else
                {
                    CR.CastSpell(DivineShield);
                    return true;

                }
            }
            return false;
        }

        private bool castAshenHallow() {

            if (CR.CanCastSpell(AshenHallow))
            {
                CR.CastSpell(AshenHallow);
                return true;
            }
            return false;
        }

        private bool shouldCastDivineToll() {

            if (CR.CanCastSpell(DivineToll) && inMelee)
            {
                CR.CastSpell(DivineToll);
                return true;
            }
            return false;

        }

        private bool shouldCastWordOfGlory() {

            if (CR.CanCastSpell(WordofGlory))
            {
                if(CR.PlayerHasBuff(BuffShiningLight) && CR.PlayerHealthPercent <= CR.GetSettingNumber(WordOfGloryHealthShiningLight))
                {
                    CR.CastSpell(WordofGlory);
                    return true;
                }
                else if (CR.PlayerHealthPercent <= CR.GetSettingNumber(WordOfGloryHealth))
                {

                    CR.CastSpell(WordofGlory);
                    return true;

                }
            }
            return false;
        
        }

        private bool Mitigation() {

            CR.WriteLog(CR.PlayerHolyPower.ToString());
            if (shouldCastWordOfGlory()) {
                return true;
            }


            if(CR.PlayerHealthPercent< 30)
            {
                if (!CR.MacroIsDisabled(Trinket1) && CR.Trinket1Usable && CR.Trinket1CooldownRemaining == 0 && TrinketSetting1 == "Defensive")
                {
                    CR.UseMacro(Trinket1);
                }

                else if (!CR.MacroIsDisabled(Trinket2) && CR.Trinket2Usable && CR.Trinket2CooldownRemaining == 0 && TrinketSetting2 == "Defensive")
                {
                    CR.UseMacro(Trinket2);
                }
            }

       

            if (CR.Toggle(ToggleBigDefensives))
            {

                if (!CR.PlayerHasBuff(DivineShield) && !CR.PlayerHasBuff(BuffFinalStand))
                {
                    if (CR.CanCastSpell(PurifySoul) && CR.PlayerHealthPercent <= 75)
                    {
                        CR.CastSpell(PurifySoul);
                        return true;
                    }

                    if (CR.CanCastSpell(ArdentDefender) && CR.PlayerHealthPercent <= CR.GetSettingNumber(ArdentDefenderHealth))
                    {
                        CR.CastSpell(ArdentDefender);
                        return true;
                    }

                    if (CR.CanCastSpell(GuardianofAncientKings) && CR.PlayerHealthPercent <= CR.GetSettingNumber(GuardianOfAncientKingsHealth))
                    {
                        CR.CastSpell(GuardianofAncientKings);
                        return true;
                    }


                    if (CR.CanCastSpell(LayOnHands) && !CR.PlayerHasDebuff(DebuffForbearance) && !CR.PlayerHasDebuff(ArdentDefender) && CR.PlayerHealthPercent <= CR.GetSettingNumber(LayonHandsHealth))
                    {
                        CR.CastSpell(LayOnHands);
                        return true;
                    }

                }


                if (shouldCastDivineShield()) {
                    return true;
                }

            }


            if (CR.CanCastSpell(ShieldoftheRighteous) && CR.PlayerHasBuff(BuffDivinePurpose))
            {
                CR.CastSpell(ShieldoftheRighteous);
                return true;
            }

            if (CR.CanCastSpell(ShieldoftheRighteous) && CR.PlayerBuffDuration(BuffDivinePurpose) < 100 && CR.PlayerHolyPower>=3)
            {
                CR.CastSpell(ShieldoftheRighteous);
                return true;
            }

            if (CR.CanCastSpell(ShieldoftheRighteous) && CR.PlayerBuffDuration(BuffDivinePurpose) < 100 && CR.PlayerHolyPower >= 3)
            {
                CR.CastSpell(ShieldoftheRighteous);
                return true;
            }

            if (CR.CanCastSpell(ShieldoftheRighteous) && CR.PlayerHolyPower >= 3)
            {
                CR.CastSpell(ShieldoftheRighteous);
                return true;
            }

            return false;
        }

        private bool CoolDowns() { 


            if (CR.CanCastSpell(AvengingWrath) && CR.TargetRange<=5)
            {
                CR.CastSpell(AvengingWrath);
                return true;
            }

            if (CR.CanCastSpell(Seraphim))
            {
                CR.CastSpell(Seraphim);
                return true;
            }


            if (shouldCastDivineToll()) {
                return true;
            }

            if (castAshenHallow()) {
                return true;
            }


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

        private bool SingleTarget() { 

            if(CR.CanCastSpell(Consecration) && !CR.PlayerHasBuff(Consecration) && CR.TargetRange <= 5)
            {
                CR.CastSpell(Consecration);
                return true;
            }

   

            if (CR.CanCastSpell(Judgement))
            {
                CR.CastSpell(Judgement);
                return true;
            }

            if (CR.CanCastSpell(HammerofWrath))
            {
                CR.CastSpell(HammerofWrath);
                return true;
            }

            if (CR.CanCastSpell(AvengersShield))
            {
                CR.CastSpell(AvengersShield);
                return true;
            }


            if (CR.CanCastSpell(HammeroftheRighteous) && CR.TargetRange<=8)
            {
                CR.CastSpell(HammeroftheRighteous);
                return true;
            }

            if (CR.CanCastSpell(BlessedHammer) && CR.TargetRange <= 8)
            {
                CR.CastSpell(BlessedHammer);
                return true;
            }


            if (CR.CanCastSpell(Consecration)  && CR.TargetRange <= 5)
            {
                CR.CastSpell(Consecration);
                return true;
            }

            return false;
        }

        private bool MultiTarget() {

            if (CR.CanCastSpell(Consecration) && !CR.PlayerHasBuff(Consecration) && CR.TargetRange <= 5)
            {
                CR.CastSpell(Consecration);
                return true;
            }

            if (CR.CanCastSpell(AvengersShield))
            {
                CR.CastSpell(AvengersShield);
                return true;
            }


            if (CR.CanCastSpell(Judgement))
            {
                CR.CastSpell(Judgement);
                return true;
            }


            if (CR.CanCastSpell(HammerofWrath))
            {
                CR.CastSpell(HammerofWrath);
                return true;
            }
            if (CR.CanCastSpell(HammeroftheRighteous) && CR.TargetRange <= 8)
            {
                CR.CastSpell(HammeroftheRighteous);
                return true;
            }

            if (CR.CanCastSpell(BlessedHammer) && CR.TargetRange <= 8)
            {
                CR.CastSpell(BlessedHammer);
                return true;
            }



            if (CR.CanCastSpell(Consecration) && CR.TargetRange <= 5)
            {
                CR.CastSpell(Consecration);
                return true;
            }
            return false;
        }


    }
}
