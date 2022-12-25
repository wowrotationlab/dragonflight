using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class DemonologyWarlockSL : RotationLab
    {
        string[] TrinketSettings = { "Never", "Auto" };
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string ToggleBurst = "Burst";
        private string ToggleCurseOfWeakness = "Weakness";
        private string ToggleCurseOfExhaustion = "Exhaustion";
        private string TrinketSetting1 => CR.GetSettingList(Trinket1);
        private string TrinketSetting2 => CR.GetSettingList(Trinket2);



        //spells
        private int SummonDemonicTyrant = 265187;
        private int GrimoireFelguard = 111898;
        private int SummonVilefiend = 264119;
        private int NetherPortal = 267217;
        private int DemonicStrength = 267171;
        private int BilescourgeBombers  = 267211;
        private int CallDreadstalkers = 104316;
        private int HandofGuldan = 105174;
        private int Implosion = 196277;
        private int Doom = 603;
        private int Demonbolt = 264178;
        private int PowerSiphon  = 264130;
        private int ShadowBolt = 686;
        private int SoulStrike = 264057;
        private int ScouringTithe = 312321;
        private int DecimatingBolt = 325289;
        private int SoulRot = 325640;
        private int ImpendingCatastrophe = 321792;


        private int RainOfFire = 5740;
        private int DrainLife = 234153;
        private int UnendingResolve = 104773;
        private int CurseOfWeakness = 702;
        private int CurseOfExhaustion = 334275;

        //racials
        private int Fireblood = 265221;
        private int Berserking = 26297;
        private int BloodFury = 33697;
        private int LightsJudgement = 255647;
        private int AncestralCall = 274738;

        private int BuffBloodlust = 2825;
        private int BuffHeroism = 32182;
        private int BuffDemonicCore = 267102;
        private int BuffNetherPortal = 267218;
        private int BuffMadnessOfTheAzjAqir = 337169;
        private int BuffFromTheShadows = 270569;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private string OdrShawlOfTheYmirjar = "OdrShawl Of The Ymirjar";
        private string MadnessOfTheAzjAqir = "Madness Of The AzjAqir";



        private string BilescourgeBombersMacro = "Bilescourge Bombers Macro";
        private string RainOfFireMacro = "Rain Of Fire Macro";
        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Demonology Warlock Shadowlands";
            CR.WriteLog("Demonology Warlock Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Warlock;




            CR.AddSpell(SummonDemonicTyrant, GrimoireFelguard, SummonVilefiend, DemonicStrength, BilescourgeBombers, CallDreadstalkers, HandofGuldan, Implosion, Doom, Demonbolt, SoulStrike, PowerSiphon, ShadowBolt, DrainLife, UnendingResolve, CurseOfExhaustion, CurseOfWeakness, ScouringTithe, DecimatingBolt, SoulRot, ImpendingCatastrophe, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);


            CR.AddBuff(BuffBloodlust, BuffHeroism,  BuffTimeWarp, BuffPrimalRage, BuffDemonicCore, BuffNetherPortal, BuffFromTheShadows);
            CR.AddDebuff(CurseOfExhaustion, CurseOfWeakness, Doom);


            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

            CR.AddSettingsBooleanField(OdrShawlOfTheYmirjar, OdrShawlOfTheYmirjar, OdrShawlOfTheYmirjar, false);
            CR.AddSettingsBooleanField(MadnessOfTheAzjAqir, MadnessOfTheAzjAqir, MadnessOfTheAzjAqir, false);


            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleCurseOfWeakness);
            CR.AddToggle(ToggleCurseOfExhaustion);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(BilescourgeBombersMacro, @"#showtooltip Bilescourge Bombers 
/use [@cursor]Summon Infernal", BilescourgeBombers);
            CR.AddMacro(RainOfFireMacro, @"#showtooltip Rain of Fire
/use [@cursor]Rain of Fire", RainOfFire);
            sw = new Stopwatch();
            sw.Start();

        }

        public override void Rotation()
        {



        }


        Random rand = new Random();
        public override void RotationInCombat()
        {

  

            bool BurstPhase = CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) || CR.PlayerHasBuff(BuffBloodlust) || (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10);


            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 40 || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }

            if(!CR.TargetIsInCombat && CR.CanCastSpell(Demonbolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Demonbolt);
                return;
            }

            if (CR.Toggle(ToggleCurseOfExhaustion) && CR.TargetDebuffDuration(CurseOfExhaustion) < 100)
            {
                if (CR.CanCastSpell(CurseOfExhaustion))
                {
                    CR.CastSpell(CurseOfExhaustion);
                    return;

                }
            }


            if (CR.Toggle(ToggleCurseOfWeakness) && CR.TargetDebuffDuration(CurseOfWeakness) < 100)
            {
                if (CR.CanCastSpell(CurseOfWeakness))
                {
                    CR.CastSpell(CurseOfWeakness);
                    return;

                }
            }

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




            if (CR.ToggleAOE && CR.AOEUnitCount >= 2 && CR.CanCastSpell(ImpendingCatastrophe) && !CR.PlayerIsMoving)
            {

                CR.CastSpell(ImpendingCatastrophe);
                return;


            }
            if (CR.ToggleAOE && CR.AOEUnitCount >= 2 && CR.CanCastSpell(SoulRot) && !CR.PlayerIsMoving)
            {

                CR.CastSpell(SoulRot);
                return;


            }



            if (CR.PlayerHealthPercent < 20)
            {
                if (CR.CanCastSpell(UnendingResolve) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(UnendingResolve);
                    return;

                }
                if (CR.CanCastSpell(DrainLife) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(DrainLife);
                    return;

                }

            }



            if (CR.CanCastSpell(Doom) && !CR.TargetHasDebuff(Doom))
            {

                CR.CastSpell(Doom);
                return;


            }

            if (CR.IsTalentSelectedRetail(3, 7))
            {
                if (CR.CanCastSpell(NetherPortal) && CR.PlayerSoulShards == 5 && (CR.SpellCooldownDuration(SummonDemonicTyrant) < 1200 || CR.EnemyMaxTTD < 30) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(NetherPortal);
                    return;
                }

                if (CR.CanCastSpell(SummonDemonicTyrant) && CR.PlayerHasBuff(BuffNetherPortal) && CR.PlayerBuffDuration(BuffNetherPortal) <= 400 && BurstPhase && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(SummonDemonicTyrant);
                    return;
                }

                if(CR.CanCastSpell(Demonbolt) && !CR.SpellIsOnCooldown(NetherPortal) && CR.PlayerHasBuff(BuffDemonicCore) && CR.PlayerSoulShards < 4 && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(Demonbolt);
                    return;
                }

                if (CR.CanCastSpell(SoulStrike) && !CR.SpellIsOnCooldown(NetherPortal) && CR.PlayerSoulShards < 5)
                {
                    CR.CastSpell(SoulStrike);
                    return;
                }

                if (CR.CanCastSpell(ShadowBolt) && !CR.SpellIsOnCooldown(ShadowBolt) && CR.PlayerSoulShards < 5)
                {
                    CR.CastSpell(SoulStrike);
                    return;
                }
            }

            if (CR.CanCastSpell(SummonVilefiend) && !CR.IsTalentSelectedRetail(2,7) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SummonVilefiend);
                return;
            }
            if (CR.CanCastSpell(SummonVilefiend) && CR.IsTalentSelectedRetail(2, 7) && CR.SpellCooldownDuration(SummonDemonicTyrant)> 3000 && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SummonVilefiend);
                return;
            }
            if (CR.CanCastSpell(GrimoireFelguard) && !CR.IsTalentSelectedRetail(2, 7) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(GrimoireFelguard);
                return;
            }

            if (CR.CanCastSpell(GrimoireFelguard) && CR.IsTalentSelectedRetail(2, 7) && CR.SpellCooldownDuration(SummonDemonicTyrant)> CR.SpellCooldownDuration(CallDreadstalkers) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(GrimoireFelguard);
                return;
            }

            if (CR.CanCastSpell(CallDreadstalkers) && !CR.IsTalentSelectedRetail(2, 7) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(CallDreadstalkers);
                return;
            }

            if (CR.CanCastSpell(CallDreadstalkers) && CR.IsTalentSelectedRetail(2, 7) && CR.SpellCooldownDuration(SummonDemonicTyrant) > CR.SpellCooldownDuration(CallDreadstalkers) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(CallDreadstalkers);
                return;
            }

            if(CR.ToggleAOE && CR.AOEUnitCount >=2 )
            {
                if (CR.CanUseMacro(BilescourgeBombersMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(BilescourgeBombersMacro);
                    return;
                }
                else if (CR.CanCastSpell(BilescourgeBombers))
                {
                    CR.CastSpell(BilescourgeBombers);
                    return;
                }
            }

            if (CR.CanCastSpell(DemonicStrength) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(DemonicStrength);
                return;
            }

            if (CR.CanCastSpell(HandofGuldan) && CR.PlayerHasBuff(BuffNetherPortal) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(HandofGuldan);
                return;
            }

            if (CR.CanCastSpell(HandofGuldan) && CR.PlayerSoulShards >= 3 && !CR.PlayerIsMoving)
            {
                CR.CastSpell(HandofGuldan);
                return;
            }

            if (CR.CanCastSpell(PowerSiphon) && CR.PlayerHasBuff(BuffFromTheShadows) && !CR.PlayerHasBuff(BuffDemonicCore))
            {
                CR.CastSpell(PowerSiphon);
                return;
            }

            if (CR.CanCastSpell(DecimatingBolt) && CR.PlayerHasBuff(BuffDemonicCore) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(DecimatingBolt);
                return;
            }

            if (CR.CanCastSpell(Demonbolt) && CR.PlayerHasBuff(BuffDemonicCore) && CR.PlayerSoulShards<4 && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Demonbolt);
                return;
            }

            if (BurstPhase && CR.CanCastSpell(SummonDemonicTyrant) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SummonDemonicTyrant);
                return;
            }

            if (CR.CanCastSpell(SoulStrike))
            {
                CR.CastSpell(SoulStrike);
                return;
            }


            if (CR.CanCastSpell(ScouringTithe) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(ScouringTithe);
                return;
            }

            if (CR.CanCastSpell(DecimatingBolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(DecimatingBolt);
                return;
            }

            if (CR.CanCastSpell(SoulRot) && CR.ToggleAOE && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SoulRot);
                return;
            }

            if (CR.CanCastSpell(ImpendingCatastrophe) && CR.ToggleAOE && !CR.PlayerIsMoving)
            {
                CR.CastSpell(ImpendingCatastrophe);
                return;
            }

            if (CR.AOEUnitCount >= 2 && CR.LastCastSpellId != HandofGuldan && CR.PlayerHasBuff(BuffFromTheShadows) && CR.SpellCurrentCharge(Implosion) >= 3){
                CR.CastSpell(Implosion);
                return;
            }

            if (CR.CanCastSpell(ShadowBolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(ShadowBolt);
                return;
            }
        }
        public override void RotationOutOfCombat()
        {





        }






    }
}
