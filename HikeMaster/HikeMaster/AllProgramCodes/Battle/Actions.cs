using System;
using System.Collections.Generic;


namespace HikeMaster
{
    interface IAction
    {
        public int Play(Character character, List<Tactic> tactics);
        public string GetName();

    }

    class Skip : IAction
    {
        int apLoss;
        public Skip(int al)
        {
            apLoss = al;
        }
        public int Play(Character character, List<Tactic> tactics)
        {
            return apLoss;
        }
        public string GetName()
        {
            string name = "Skip";
            return name;
        }


    }
    class Attack : IAction
    {
        int damage;
        int weaponeDamage;
        int aattribute;
        int ttalent;
        int result;
        int k;
        const int valueAttackForTalentMultiplier = 3;
        const int valueDefenseForTalentMultiplier = 3;
        const int divisionActionPoints = 15;
        const int minRandom = 0;
        const int maxRandom = 10;
        const int dodgeIdTalent = 3;
        const int shieldIdTalent = 0;
        const int armourIdTalent = 1;
        const int dexterityIdAttribute = 1;
        const int constitutionIdAttribute = 2;
        double pureMultiplierDamage;
        double talentMultiplierDamage;
        string name;
        Character defenseSubject;

        Random rnd = new Random();
        public Attack(string n, int re, double pmd, double tmd)
        {
            name = n;
            pureMultiplierDamage = pmd;
            talentMultiplierDamage = tmd;
            result = re;

        }
        public string GetName()
        {
            return name;
        }

        public int AttackRating(Character character)
        {

            int talant = character.GetTalentValue(aattribute, ttalent);
            int atribute = character.GetAttributeValue(aattribute);
            int pureAttRat;
            int randAttRat = 0;
            int rating;

            pureAttRat = (atribute + (valueAttackForTalentMultiplier * talant));

            for (int i = 0; i < divisionActionPoints - pureAttRat % divisionActionPoints + 1; ++i)
            {
                randAttRat += rnd.Next(minRandom, maxRandom);
            }
            rating = randAttRat + pureAttRat;
            return rating;
        }
        public int DefenseRating(int aRating)
        {
            int dodge;
            int armour;
            int shield;
            int dexterity;
            int constitution;
            int randDodge = 0;
            int randArmour = 0;
            int randShield = 0;
            int dRating;
            int arRating;
            int sRating;
            dodge = defenseSubject.GetTalentValue(dexterityIdAttribute, dodgeIdTalent);
            armour = defenseSubject.GetTalentValue(constitutionIdAttribute, armourIdTalent);
            shield = defenseSubject.GetTalentValue(constitutionIdAttribute, shieldIdTalent);
            dexterity = defenseSubject.GetAttributeValue(dexterityIdAttribute);
            constitution = defenseSubject.GetAttributeValue(constitutionIdAttribute);
            int pureDodge = (dexterity + valueDefenseForTalentMultiplier * dodge);
            int pureArmour = (constitution + valueDefenseForTalentMultiplier * armour);
            int pureShield = (constitution + valueDefenseForTalentMultiplier * shield);
            for (int i = 0; i < divisionActionPoints - pureDodge % divisionActionPoints + 1; ++i)
            {
                randDodge += rnd.Next(minRandom, maxRandom);
            }
            for (int i = 0; i < divisionActionPoints - pureArmour % divisionActionPoints + 1; ++i)
            {
                randArmour += rnd.Next(minRandom, maxRandom);
            }
            for (int i = 0; i < divisionActionPoints - pureShield % divisionActionPoints + 1; ++i)
            {
                randShield += rnd.Next(minRandom, maxRandom);
            }
            dRating = randDodge + pureDodge;
            arRating = randArmour + pureArmour;
            sRating = randShield + pureShield;
            if (aRating >= dRating)
            {
                if (aRating >= sRating)
                {
                    if (aRating >= arRating)
                    {
                        damage = Convert.ToInt32(weaponeDamage * pureMultiplierDamage + weaponeDamage * talentMultiplierDamage * (aattribute + valueAttackForTalentMultiplier * ttalent) / 40);
                        return damage;
                    }
                }
            }
            return 0;
        }

        int a;
        public int Play(Character character, List<Tactic> tactics)
        {
            k = rnd.Next(0, tactics.Count);
            defenseSubject = tactics[k].GetCharacter();
            aattribute = character.GetWeaponeAttribute();
            ttalent = character.GetWeaponeTalent();
            weaponeDamage = character.GetWeaponeDamage();
            a = DefenseRating(AttackRating(character));
            Console.WriteLine("{0} attacked {1} with {2}", character.GetName(), defenseSubject.GetName(), name);
            defenseSubject.DamageCharacter(a);
            return result;
        }
    }
}