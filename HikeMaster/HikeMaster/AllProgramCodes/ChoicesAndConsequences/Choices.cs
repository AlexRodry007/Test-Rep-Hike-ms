using System;

namespace HikeMaster
{
    class StartBattleChoice : IChoice
    {
        string description;
        int idd;
        public StartBattleChoice(string desc,int id)
        {
            idd = id;
            description = desc;
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
        public void play(Hike nowHike)
        {
            Console.WriteLine("Battle has started");
            PlayerInteractions.StartBattle(nowHike, ReadLoadFight.ReadPlayerActionList(), ReadLoadFight.ReadTactic(idd));
        }
    }
    class ReturnToTheTown : IChoice
    {
        string description;
        public ReturnToTheTown(string descr)
        {
            description = descr;
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
        public void play(Hike nowHike)
        {
            foreach(Character character in nowHike.GetAllCharacters())
            {
                Town.AddFreeCharacter(character);
            }
            foreach(Item item in nowHike.GetStorage().GetAllItems())
            {
                Town.AddStorageItem(item);
            }
            nowHike.EndOfTheHike();
        }
    }
    class SkipChoice : IChoice
    {
        string description;
        string skip;
        public SkipChoice(string des, string ski)
        {
            description = des;
            skip = ski;
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
        public void play(Hike nowHike)
        {
            Console.WriteLine(skip);
        }
    }
    class NextEventUseItem : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        string passiveUseName;
        int requiredLevel;
        Eventt nextEventt;

        public NextEventUseItem(string descr, string gDesc, string bDesc, string passiveUseNam, int reqLevel, Eventt nEvent)
        {
            description = descr;
            gDescription = gDesc;
            bDescription = bDesc;
            passiveUseName = passiveUseNam;
            requiredLevel = reqLevel;
            nextEventt = nEvent;
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
            Console.WriteLine("Need: {0}", passiveUseName);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
            Console.WriteLine("Need: {0}", passiveUseName);
        }
        public void play(Hike nowHike)
        {
            if (PlayerInteractions.ChoosePassiveUse(passiveUseName, nowHike) >= requiredLevel)
            {
                Console.WriteLine(gDescription);
                nextEventt.Play(nowHike);
            }
            else
            {
                Console.WriteLine(bDescription);
            }

        }
    }
    class TwoConsequencesAllCharacterTest : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        int difficulty;
        int attribute;
        int skill;
        IConsequence gConsequence;
        IConsequence bConsequence;
        Random rnd = new Random();
        public TwoConsequencesAllCharacterTest(string descr, string goodDescription, string badDescription, int dif, int att, int tal, IConsequence goodConsequence, IConsequence badConsequence)
        {
            description = descr;
            gDescription = goodDescription;
            bDescription = badDescription;
            difficulty = dif;
            attribute = att;
            skill = tal;
            gConsequence = goodConsequence;
            bConsequence = badConsequence;
        }
        public void play(Hike nowHike)
        {
            foreach (Character subject in nowHike.GetAllCharacters())
            {
                if (Functions.Test(subject.GetAttributeValue(attribute), subject.GetTalentValue(attribute, skill), difficulty))
                {
                    Console.WriteLine("{0} {1}", subject.GetName(), gDescription);
                    gConsequence.Play(nowHike, subject);
                }
                else
                {
                    Console.WriteLine("{0} {1}", subject.GetName(), bDescription);
                    bConsequence.Play(nowHike, subject);
                }
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class TwoConsequencesCharacterTest : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        Character subject;
        int difficulty;
        int attribute;
        int skill;
        IConsequence gConsequence;
        IConsequence bConsequence;
        Random rnd = new Random();
        public TwoConsequencesCharacterTest(string descr, string goodDescription, string badDescription, int dif, int att, int tal, IConsequence goodConsequence, IConsequence badConsequence)
        {
            description = descr;
            gDescription = goodDescription;
            bDescription = badDescription;
            difficulty = dif;
            attribute = att;
            skill = tal;
            gConsequence = goodConsequence;
            bConsequence = badConsequence;
        }
        public void play(Hike nowHike)
        {
            Character subject;
            subject = PlayerInteractions.ChooseCharacter(nowHike);
            if (Functions.Test(subject.GetAttributeValue(attribute), subject.GetTalentValue(attribute, skill), difficulty))
            {
                Console.WriteLine(gDescription);
                gConsequence.Play(nowHike, subject);
            }
            else
            {
                Console.WriteLine(bDescription);
                bConsequence.Play(nowHike, subject);
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class TwoSquadConsequencesCharacterTest : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        Character subject;
        int difficulty;
        int attribute;
        int skill;
        IConsequence gConsequence;
        IConsequence bConsequence;
        Random rnd = new Random();
        public TwoSquadConsequencesCharacterTest(string descr, string goodDescription, string badDescription, int dif, int att, int tal, IConsequence goodConsequence, IConsequence badConsequence)
        {
            description = descr;
            gDescription = goodDescription;
            bDescription = badDescription;
            difficulty = dif;
            attribute = att;
            skill = tal;
            gConsequence = goodConsequence;
            bConsequence = badConsequence;
        }
        public void play(Hike nowHike)
        {
            Character subject;
            subject = PlayerInteractions.ChooseCharacter(nowHike);
            if (Functions.Test(subject.GetAttributeValue(attribute), subject.GetTalentValue(attribute, skill), difficulty))
            {
                Console.WriteLine(gDescription);
                foreach (Character subj in nowHike.GetAllCharacters())
                {
                    gConsequence.Play(nowHike, subj);
                }
            }
            else
            {
                Console.WriteLine(bDescription);
                foreach (Character subj in nowHike.GetAllCharacters())
                {
                    bConsequence.Play(nowHike, subj);
                }
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class DamageCharacterTest : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        Character subject;
        int difficulty;
        int attribute;
        int skill;
        int damage;
        Random rnd = new Random();
        public DamageCharacterTest(string descr, string goodDescription, string badDescription, int dif, int att, int tal, int d)
        {
            description = descr;
            gDescription = goodDescription;
            bDescription = badDescription;
            difficulty = dif;
            attribute = att;
            skill = tal;
            damage = d;
        }
        public void play(Hike nowHike)
        {
            Character subject;
            subject = PlayerInteractions.ChooseCharacter(nowHike);
            if (Functions.Test(subject.GetAttributeValue(attribute), subject.GetTalentValue(attribute, skill), difficulty))
            {
                Console.WriteLine(gDescription);
            }
            else
            {
                subject.DamageCharacter(damage);
                Console.WriteLine(bDescription);
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class ItemStrongTest : IChoice
    {
        string description;
        string gDescription;
        string bDescription;
        int difficulty;
        int attribute;
        int skill;
        int item;
        Random rnd = new Random();
        public ItemStrongTest(string descr, string goodDescription, string badDescription, int dif, int att, int tal, int itm)
        {
            description = descr;
            gDescription = goodDescription;
            bDescription = badDescription;
            difficulty = dif;
            attribute = att;
            skill = tal;
            item = itm;
        }
        public void play(Hike nowHike)
        {
            if (Functions.Test(nowHike.GetStrongAttr(attribute), nowHike.GetStrongTalent(attribute, skill), difficulty))
            {
                Console.WriteLine(gDescription);
                nowHike.AddItem(new Item(item));
            }
            else
            {
                Console.WriteLine(bDescription);
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class NextCharacterConsequence : IChoice
    {
        string description;
        IConsequence consequence;
        public NextCharacterConsequence(string descr, IConsequence cons)
        {
            description = descr;
            consequence = cons;
        }
        public void play(Hike nowHike)
        {
            Character subject;
            subject = PlayerInteractions.ChooseCharacter(nowHike);
            consequence.Play(nowHike, subject);

        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    class NextEvent : IChoice
    {
        string description;
        Eventt nextEventt;

        Random rnd = new Random();
        public NextEvent(string descr, Eventt nevent)
        {
            description = descr;
            nextEventt = nevent;
        }

        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
        public void play(Hike nowHike)
        {
            nextEventt.Play(nowHike);
        }
    }
    class NextEventCharacterTest : IChoice
    {
        string description;
        int difficulty;
        int attribute;
        int talent;
        Eventt nextGoodEventt;
        Eventt nextBadEventt;

        Random rnd = new Random();
        public NextEventCharacterTest(string descr, int diff, int att, int tal, Eventt ngevent, Eventt nbeventt)
        {
            description = descr;
            difficulty = diff;
            attribute = att;
            talent = tal;
            nextGoodEventt = ngevent;
            nextBadEventt = nbeventt;
        }

        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
        public void play(Hike nowHike)
        {
            Character subject;
            subject = PlayerInteractions.ChooseCharacter(nowHike);
            if (Functions.TestCharacter(attribute, talent, difficulty, subject))
            {
                nextGoodEventt.Play(nowHike);
            }
            else
            {
                nextBadEventt.Play(nowHike);
            }
        }
    }
    class GoodStrongTest : IChoice
    {
        string description;
        int difficulty;
        int attribute;
        int skill;
        Random rnd = new Random();
        public GoodStrongTest(string descr, int dif, int att, int tal)
        {
            description = descr;
            difficulty = dif;
            attribute = att;
            skill = tal;
        }
        public void play(Hike nowHike)
        {
            if (Functions.Test(nowHike.GetStrongAttr(attribute), nowHike.GetStrongTalent(attribute, skill), difficulty))
            {
                Console.WriteLine("Good");
            }
            else
            {
                Console.WriteLine("Bad");
            }
        }
        public void writeDescription()
        {
            Console.WriteLine(description);
        }
        public void writeDescription(int n)
        {
            Console.WriteLine("{0}. {1}", n, description);
        }
    }
    public interface IChoice
    {
        public void writeDescription();
        public void writeDescription(int n);
        public void play(Hike nowHike);
    }
}