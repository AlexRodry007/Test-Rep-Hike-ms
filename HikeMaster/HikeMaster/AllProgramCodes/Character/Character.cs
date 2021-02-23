using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace HikeMaster
{
    class ICharacter
    {
        public static string[] attrnames = new string[9] { "Strength", "Dexterity", "Constitution", "Intelligence", "Memory", "Perception", "Charisma", "Leadership", "Epathy" };
        public static string[][] talentnames = new string[9][]
        {
            new string[]{"Heavy Weapone","Resource Gather","Carrying","Hand to Hand","Brute force"},//Strength 0
            new string[]{"Light Weapone","Ranged Weapone","Sneak","Dodge","Agility"},//Dexterity 1
            new string[]{"Shield","Armour","Stamina","Health","Balance"},//Constitution 2
            new string[]{"Magick Power","Medicine","Cooking","Science","Craft"},//Intelligence 3
            new string[]{"Willpower","Magick Level","Languages","Learning","Logistics"},//Memory 4
            new string[]{"Hunting","Traps","Foraging","Spotting","Pathfinding"},//Perception 5
            new string[]{"Persuation","Intimidation","Trade","Music","Discussion"},//Charisma 6
            new string[]{"Command","Tactic","Strategy","Survival","Adjutant"},//Leadership 7
            new string[]{"Lie","Lie Detector","Psycology","Pacification","Animal Handling"},//Empathy 8
        };
        public static string[][] xmltalentnames = new string[9][]
{
            new string[]{"HeavyWeapone","ResourceGather","Carrying","HandToHand","Bruteforce"},//Strength
            new string[]{"LightWeapone","RangedWeapone","Sneak","Dodge","Agility"},//Dexterity
            new string[]{"Shield","Armour","Stamina","Health","Balance"},//Constitution
            new string[]{"MagickPower","Medicine","Cooking","Science","Craft"},//Intelligence
            new string[]{"Willpower","MagickLevel","Languages","Learning","Logistics"},//Memory
            new string[]{"Hunting","Traps","Foraging","Spotting","Pathfinding"},//Perception
            new string[]{"Persuation","Intimidation","Trade","Music","Discussion"},//Charisma
            new string[]{"Command","Tactic","Strategy","Survival","Adjutant"},//Leadership
            new string[]{"Lie","LieDetector","Psycology","Pacification","AnimalHandling"},//Empathy
};
    }
    public class Talent
    {
        int value = 0;
        int advantage = 0;
        int disadvantage = 0;
        int status = 1;
        int expirience = 0;

        bool isTraining = false;
        public Talent(int v)
        {
            value = v;
        }
        public int GetValue()
        {
            return value + advantage - disadvantage;
        }
        public void Show(int attr, int tal)
        {
            Console.WriteLine("     {0}: {1}", ICharacter.talentnames[attr][tal], GetValue());
        }
    }
    public class Attribute
    {
        int value = 0;
        int advantage = 0;
        int disadvantage = 0;
        int status = 1;
        int expirience = 0;
        bool isTraining = false;
        List<Talent> talents = new List<Talent> { };
        public Attribute(int v, int[] tals)
        {
            value = v;
            for (int i = 0; i < 5; i++)
            {
                talents.Add(new Talent(tals[i]));
            }
        }
        public int GetValue()
        {
            return value + advantage - disadvantage;
        }
        public int GetTalentValue(int t)
        {
            return talents[t].GetValue();
        }
        public List<Talent> GetTalents()
        {
            return talents;
        }
        public void ShowAll(int attr)
        {
            Console.WriteLine(" {0}: {1}", ICharacter.attrnames[attr], GetValue());
            int i = 0;
            foreach (Talent talent in GetTalents())
            {
                talent.Show(attr, i);
                i++;
            }
        }
    }
    public class Character
    {
        string name = "###";
        int side = -1;
        int id;
        int sanity;
        int maxSanity;
        int health;
        int maxHealth;
        int energy;
        int maxEnergy;
        int satiety;
        int maxSatiety;
        int status = 5;     //5 - здоров, 4 - легкораненый, 3 - тяжелораненый, 2 - критически раненый (без сознания), 1 - присмерти (без сознания), 0 - мёртв
        static int maxId = 0;
        List<Attribute> attributes = new List<Attribute> { };
        Item weapone = ReadLoadItem.ReadItemById(1);
        public Character(string n, int s, int[] attrs, int[][] tals, int h, int e, int hu, int sa)
        {
            Item weapone = ReadLoadItem.ReadItemById(1);
            name = n;
            side = s;
            id = maxId;
            maxHealth   = h;
            health      = maxHealth;
            maxEnergy   = e;
            energy      = maxEnergy;
            maxSatiety  = hu;
            satiety     = maxSatiety;
            maxSanity   = sa;
            sanity      = maxSanity;
            maxId++;
            for (int i = 0; i < 9; i++)
            {
                    attributes.Add(new Attribute(attrs[i], tals[i]));
            }
        }
        public Character(string n, int s, int[] attrs, int[][] tals, int h, int e, int hu, int sa,Item weapon)
        {
            Item weapone = weapon;
            name = n;
            side = s;
            id = maxId;
            maxHealth = h;
            health = maxHealth;
            maxEnergy = e;
            energy = maxEnergy;
            maxSatiety = hu;
            satiety = maxSatiety;
            maxSanity = sa;
            sanity = maxSanity;
            maxId++;
            for (int i = 0; i < 9; i++)
            {
                attributes.Add(new Attribute(attrs[i], tals[i]));
            }
        }
        public Character(string n, int s, int idd, int[] attrs, int[][] tals, int mh, int h, int me, int e, int mhu, int hu, int msa, int sa, int sta)
        {
            Item weapone = ReadLoadItem.ReadItemById(1);
            name = n;
            side = s;
            id = idd;
            maxHealth = mh;
            health = h;
            maxEnergy = me;
            energy = e;
            maxSatiety = mhu;
            satiety = hu;
            maxSanity = msa;
            sanity = sa;
            status = sta;
            if (maxId < idd)
                maxId = idd;
            for (int i = 0; i < 9; i++)
            {
                attributes.Add(new Attribute(attrs[i], tals[i]));
            }
        }
            public int CheckStatus()
        {
            if (health >= maxHealth * 0.9)
                return 5;
            else
                if (health >= maxHealth * 0.75)
                return 4;
            else
                if (health >= maxHealth * 0.3)
                return 3;
            else
                if (health >= maxHealth * 0.15)
                return 2;
            else
                if (health > 0)
                return 1;
            if (health <= 0)
                return 0;
            else
                return -1;

        }

        public int DamageCharacter(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Console.WriteLine("Dead");
                status = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} got {1} damage\nremaining health: {2}", name, damage, health);
                Console.WriteLine("Current status: {0}", status = CheckStatus());
                Console.ResetColor();
            }
            return status;
        }
        public void Heal(int h)
        {
            health += h;
            if (health > maxHealth)
                health = maxHealth;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} healed and regained {1} health\nremaining health: {2}", name, h, satiety);
            Console.WriteLine("Current status: {0}", status = CheckStatus());
            Console.ResetColor();
        }
        public void ExhaustCharacter(int exhaust)
        {
            energy -= exhaust;
            if (health <= 0)
            {
                Console.WriteLine("Exhausted");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0} got {1} exhausting\nremaining energy: {2}", name, exhaust, energy);
                Console.ResetColor();
            }
        }
        public void Rest(int r)
        {
            energy += r;
            if (energy > maxEnergy)
                energy = maxEnergy;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} rested and regained {1} energy\nremaining energy: {2}", name, r, satiety);
            Console.ResetColor();
        }
        public void HungerCharacter(int hunger)
        {
            satiety -= hunger;
            if (satiety <= 0)
            {
                Console.WriteLine("Starving to death!!!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{0} got {1} hunger\nremaining satiety: {2}", name, hunger, energy);
                Console.ResetColor();
            }
        }
        public void Eat(int sat)
        {
            satiety += sat;
            if (satiety > maxSatiety)
                satiety = maxSatiety;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} eated and regained {1} satiety\nremaining satiety: {2}", name, sat, satiety);
            Console.ResetColor();
        }
        public void StressCharacter(int str)
        {
            sanity -= str;
            if (sanity <= 0)
            {
                Console.WriteLine("Driven Mad");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("{0} stressed and lost {1} sanity\nremaining sanity: {2}", name, str, sanity);
                Console.ResetColor();
            }
        }
        public void RelaxCharacter(int rlx)
        {
            sanity += rlx;
            if (sanity > maxSanity)
                sanity = maxSanity;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} relaxed and regained {1} sanity\nremaining sanity: {2}", name, rlx, sanity);
            Console.ResetColor();

        }
        public int GetStatus()
        {
            return status;
        }
        public int GetMaxHealth()
        {
            return maxHealth;
        }
        public int GetHealth()
        {
            return health;
        }
        public int GetMaxEnergy()
        {
            return maxEnergy;
        }
        public int GetEnergy()
        {
            return energy;
        }
        public int GetMaxSatiety()
        {
            return maxSatiety;
        }
        public int GetSatiety()
        {
            return satiety;
        }
        public int GetMaxSanity()
        {
            return maxSanity;
        }
        public int GetSanity()
        {
            return sanity;
        }
        public List<Attribute> GetAttributes()
        {
            return attributes;
        }
        public int GetTalentValue(int a, int t)
        {
            return attributes[a].GetTalentValue(t);
        }
        public int GetAttributeValue(int a)
        {
            return attributes[a].GetValue();

        }
        public string GetName()
        {
            return name;
        }
        public int GetWeaponeAttribute()
        {
            return weapone.GetWeaponeAttribute();
        }
        public int GetWeaponeTalent()
        {
            return weapone.GetWeaponeTalent();
        }
        public int GetWeaponeDamage()
        {
            return weapone.GetWeaponeDamage();
        }
        public int GetSide()
        {
            return side;
        }
        public int GetId()
        {
            return id;
        }
        public int GetWeaponeId()
        {
            return weapone.GetId();
        }
        public static void SetMaxId(int characterMaxId)
        {
            maxId = characterMaxId;
        }
        public void ShowAll()
        {
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Side: {0}", side);
            Console.WriteLine("Id: {0}", id);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Health: {0}/{1}", health, maxHealth);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Sanity: {0}/{1}", sanity, maxSanity);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Energy: {0}/{1}", energy, maxEnergy);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Satiety: {0}/{1}", satiety, maxSatiety);
            Console.ResetColor();
            Console.WriteLine("Character health status: {0}", status);
            int j = 0;
            foreach(Attribute attribute in GetAttributes())
            {
                attribute.ShowAll(j);
                j++;
            }
        }
    }
}