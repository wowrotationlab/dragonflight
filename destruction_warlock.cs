using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationLabEngine
{
    public class DestructionWarlockSL : RotationLab
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

        private int Immolate = 348;
        private int ChaosBolt = 116858;
        private int Cataclysm = 152108;
        private int ChannelDemonfire  = 196447;
        private int SoulFire = 6353;
        private int Conflagrate = 17962;
        private int Incinerate = 29722;
        private int Shadowburn = 17877;
        private int DarkSoulInstability = 113858;
        private int ScouringTithe = 312321;
        private int DecimatingBolt = 325289;
        private int SoulRot = 325640;
        private int ImpendingCatastrophe = 321792;
        private int SummonInfernal = 1122;
        private int Havoc = 80240;
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
        private int BuffHeroism  = 32182;
        private int BuffBackdraft = 196406;
        private int DebuffEradication = 196414;
        private int BuffMadnessOfTheAzjAqir = 337169;
        private int BuffRoaringBlaze = 205184;
        private int BuffTimeWarp = 80353;
        private int BuffPrimalRage = 264667;

        private string OdrShawlOfTheYmirjar = "OdrShawl Of The Ymirjar";
        private string MadnessOfTheAzjAqir = "Madness Of The AzjAqir";



        private string SummonInfernalMacro = "Summon Infernal Macro";
        private string RainOfFireMacro = "Rain Of Fire Macro";
        Stopwatch sw;
        public override void Initialize()
        {
            CR.RotationName = "Destruction Warlock Shadowlands";
            CR.WriteLog("Destruction Warlock Shadowlands by Merciless");
            CR.TBCRotation = false;
            CR.ProfileClass = ClassType.Warlock;


          

            CR.AddSpell(Immolate, ChaosBolt, Cataclysm, ChannelDemonfire, SoulFire, Conflagrate, Incinerate, Shadowburn, DarkSoulInstability, ScouringTithe, DecimatingBolt, SoulRot, ImpendingCatastrophe, RainOfFire, SummonInfernal, Havoc, DrainLife,  UnendingResolve, CurseOfExhaustion, CurseOfWeakness, Fireblood, Berserking, BloodFury, LightsJudgement, AncestralCall);
            

            CR.AddBuff(BuffBloodlust, BuffHeroism, BuffTimeWarp, BuffPrimalRage, DarkSoulInstability, BuffBackdraft, BuffRoaringBlaze);
            CR.AddDebuff(DebuffEradication, Immolate, CurseOfExhaustion, CurseOfWeakness);
            

            CR.AddSettingsListField(Trinket1, Trinket1, "Use Trinket 1", TrinketSettings, TrinketSettings[0]);
            CR.AddSettingsListField(Trinket2, Trinket2, "Use Trinket 2", TrinketSettings, TrinketSettings[0]);

            CR.AddSettingsBooleanField(OdrShawlOfTheYmirjar, OdrShawlOfTheYmirjar, OdrShawlOfTheYmirjar, false);
            CR.AddSettingsBooleanField(MadnessOfTheAzjAqir, MadnessOfTheAzjAqir, MadnessOfTheAzjAqir, false);


            CR.AddToggle(ToggleBurst);
            CR.AddToggle(ToggleCurseOfWeakness);
            CR.AddToggle(ToggleCurseOfExhaustion);



            CR.AddMacro(Trinket1, "/use 13");
            CR.AddMacro(Trinket2, "/use 14");
            CR.AddMacro(SummonInfernalMacro, @"#showtooltip Summon Infernal
/use [@cursor]Summon Infernal", SummonInfernal);
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
            if (!CR.PlayerCanAttackTarget || CR.TargetIsDead || CR.TargetRange > 40 || CR.PlayerCurrentCastIsChannelling)
            {
                return;
            }

            bool BurstPhase = CR.PlayerHasBuff(BuffHeroism) || CR.PlayerHasBuff(BuffBloodlust) || CR.PlayerHasBuff(BuffTimeWarp) || CR.PlayerHasBuff(BuffPrimalRage) ||  (CR.Toggle(ToggleBurst) && CR.EnemyMaxTTD > 10) || CR.PlayerHasBuff(DarkSoulInstability);


            if (!CR.PlayerCanAttackTarget || CR.TargetRange > 40)
            {
                return;
            }


            if (CR.Toggle(ToggleCurseOfExhaustion) && CR.TargetDebuffDuration(CurseOfExhaustion)<100) 
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
                if (CR.CanCastSpell(DarkSoulInstability))
                {
                    CR.CastSpell(DarkSoulInstability);
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



                if (CR.CanCastSpell(DarkSoulInstability) && CR.AOEUnitCount >= 2 && CR.ToggleAOE && CR.EnemyMaxTTD > 20 )
                {
                    if (CR.CanCastSpell(SummonInfernal))
                    {
                        if (CR.CanUseMacro(SummonInfernalMacro) && CR.PlayerCanAttackMouseover)
                        {
                            CR.UseMacro(SummonInfernalMacro);
                            return;
                        }
                        else if (CR.CanCastSpell(SummonInfernal))
                        {
                            CR.CastSpell(SummonInfernal);
                            return;
                        }

                    }
                }

                if (!CR.IsTalentSelectedRetail(3, 7))
                {
                    if (CR.CanCastSpell(SummonInfernal) && CR.AOEUnitCount >= 2 && CR.ToggleAOE && CR.EnemyMaxTTD > 20)
                    {
                        if (CR.CanUseMacro(SummonInfernalMacro) && CR.PlayerCanAttackMouseover)
                        {
                            CR.UseMacro(SummonInfernalMacro);
                            return;
                        }
                        else if (CR.CanCastSpell(SummonInfernal))
                        {
                            CR.CastSpell(SummonInfernal);
                            return;
                        }

                    }
                }




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


            if (CR.CanCastSpell(SoulRot) && CR.AOEUnitCount >= 2 && CR.ToggleAOE && CR.EnemyMaxTTD > 10 && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SoulRot);
                return;

            }

            if (CR.CanCastSpell(Cataclysm) &CR.EnemyMaxTTD > 6 && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Cataclysm);
                return;

            }

            if (CR.AOEUnitCount >= 3 && CR.ToggleAOE && !CR.PlayerIsMoving)
            {
                if (CR.CanUseMacro(RainOfFireMacro) && CR.PlayerCanAttackMouseover)
                {
                    CR.UseMacro(RainOfFireMacro);
                    return;
                }
                else if (CR.CanCastSpell(RainOfFire))
                {
                    CR.CastSpell(RainOfFire);
                    return;
                }
            }

            if(CR.PlayerDebuffDuration(Immolate)>300 && CR.CanCastSpell(ChannelDemonfire) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(ChannelDemonfire);
                return;
            }


            if (CR.GetSettingBoolean(OdrShawlOfTheYmirjar))
            {
                if (CR.CanCastSpell(Havoc))
                {
                    CR.CastSpell(Havoc);
                    return;

                }
            }
        

            if(CR.ToggleAOE && CR.AOEUnitCount >= 2 && CR.EnemyMinTTD < 15 && !CR.PlayerIsMoving)
            {
                if (CR.CanCastSpell(ScouringTithe))
                {
                    CR.CastSpell(ScouringTithe);
                    return;

                }
            }

            if (CR.TargetDebuffDuration(Immolate) <= 300 && !CR.PlayerIsMoving)
            {
                if (CR.CanCastSpell(Immolate))
                {
                    CR.CastSpell(Immolate);
                    return;

                }
            }

            if (CR.CanCastSpell(DecimatingBolt) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(DecimatingBolt);
                return;

            }
            if(!CR.PlayerHasBuff(BuffBackdraft) && CR.PlayerSoulShards >=1.5 && CR.CanCastSpell(Conflagrate))
            {
                CR.CastSpell(Conflagrate);
                return;
            }

            if (CR.GetSettingBoolean(MadnessOfTheAzjAqir) && !CR.PlayerIsMoving)
            {
                if(CR.PlayerSoulShards >= 4 || CR.PlayerHasBuff(BuffMadnessOfTheAzjAqir) && CR.CanCastSpell(ChaosBolt))
                {
                    CR.CastSpell(ChaosBolt);
                    return;
                }
            }
            else
            {
                if((!CR.ToggleAOE || CR.AOEUnitCount < 3)  && CR.CanCastSpell(ChaosBolt) && !CR.PlayerIsMoving)
                {
                    CR.CastSpell(ChaosBolt);
                    return;
                }
            }

            if (CR.CanCastSpell(SoulFire) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(SoulFire);
                return;

            }


       
            if (CR.EnemyMinTTD < 15 && CR.CanCastSpell(ScouringTithe) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(ScouringTithe);
                return;

            }

            if (CR.EnemyMinTTD < 5 && CR.CanCastSpell(Shadowburn))
            {
                CR.CastSpell(Shadowburn);
                return;

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

            if(CR.IsTalentSelectedRetail(1,6) && CR.PlayerBuffDuration(BuffRoaringBlaze)<150 && CR.CanCastSpell(Conflagrate))
            {
                CR.CastSpell(Conflagrate);
                return;

            }


            if (CR.CanCastSpell(Incinerate) && !CR.PlayerIsMoving)
            {
                CR.CastSpell(Incinerate);
                return;

            }



        }
        public override void RotationOutOfCombat()
        {

           

        

        }






    }
}
