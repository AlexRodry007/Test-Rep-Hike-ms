namespace HikeMaster
{

    class ChangeProgressConsequence : IConsequence
    {
        int progressChange;
        public ChangeProgressConsequence(int pc)
        {
            progressChange = pc;
        }
        public void Play(Hike nowHike, Character subject)
        {
            int prg = nowHike.GetProgressChanger();
            nowHike.SetProgressChanger(prg + progressChange);
        }
    }
    class NextChangeProgressConsequence : IConsequence
    {
        int progressChange;
        IConsequence consequence;
        public NextChangeProgressConsequence(int pc, IConsequence cons)
        {
            progressChange = pc;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            int prg = nowHike.GetProgressChanger();
            nowHike.SetProgressChanger(prg + progressChange);
            consequence.Play(nowHike, subject);
        }
    }
    class NextExhaustCharacterConsequence : IConsequence
    {
        int exhaust;
        IConsequence consequence;
        public NextExhaustCharacterConsequence(int e, IConsequence cons)
        {
            exhaust = e;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.ExhaustCharacter(exhaust);
            consequence.Play(nowHike, subject);
        }
    }
    class ExhaustCharacterConsequence : IConsequence
    {
        int exhaust;
        public ExhaustCharacterConsequence(int e)
        {
            exhaust = e;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.ExhaustCharacter(exhaust);
        }
    }
    class NextRestCharacterConsequence : IConsequence
    {
        int rest;
        IConsequence consequence;
        public NextRestCharacterConsequence(int e, IConsequence cons)
        {
            rest = e;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Rest(rest);
            consequence.Play(nowHike, subject);
        }
    }
    class RestCharacterConsequence : IConsequence
    {
        int rest;
        public RestCharacterConsequence(int e)
        {
            rest = e;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Rest(rest);
        }
    }
    class NextDamageCharacterConsequence : IConsequence
    {
        int damage;
        IConsequence consequence;
        public NextDamageCharacterConsequence(int d, IConsequence cons)
        {
            damage = d;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.DamageCharacter(damage);
            consequence.Play(nowHike, subject);
        }
    }
    class DamageCharacterConsequence : IConsequence
    {
        int damage;
        public DamageCharacterConsequence(int d)
        {
            damage = d;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.DamageCharacter(damage);
        }

    }
    class NextHealCharacterConsequence : IConsequence
    {
        int heal;
        IConsequence consequence;
        public NextHealCharacterConsequence(int d, IConsequence cons)
        {
            heal = d;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Heal(heal);
            consequence.Play(nowHike, subject);
        }
    }
    class HealCharacterConsequence : IConsequence
    {
        int heal;
        public HealCharacterConsequence(int d)
        {
            heal = d;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Heal(heal);
        }

    }
    class HungerCharacterConsequence : IConsequence
    {
        int hunger;
        public HungerCharacterConsequence(int h)
        {
            hunger = h;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.HungerCharacter(hunger);
        }
    }
    class NextHungerCharacterConsequence : IConsequence
    {
        int hunger;
        IConsequence consequence;
        public NextHungerCharacterConsequence(int h, IConsequence cons)
        {
            hunger = h;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.HungerCharacter(hunger);
            consequence.Play(nowHike, subject);
        }
    }
    class EatCharacterConsequence : IConsequence
    {
        int eat;
        public EatCharacterConsequence(int h)
        {
            eat = h;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Eat(eat);
        }
    }
    class NextEatCharacterConsequence : IConsequence
    {
        int eat;
        IConsequence consequence;
        public NextEatCharacterConsequence(int h, IConsequence cons)
        {
            eat = h;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.Eat(eat);
            consequence.Play(nowHike, subject);
        }
    }
    class GetItemCharacterConsequence : IConsequence
    {
        int id;
        public GetItemCharacterConsequence(int i)
        {
            id = i;
        }
        public void Play(Hike nowHike, Character subject)
        {
            nowHike.AddItem(new Item(id));
        }
    }
    class NextGetItemCharacterConsequence : IConsequence
    {
        int id;
        IConsequence consequence;
        public NextGetItemCharacterConsequence(int i, IConsequence cons)
        {
            id = i;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            nowHike.AddItem(new Item(id));
            consequence.Play(nowHike, subject);
        }
    }
    class RelaxCharacterConsequence : IConsequence
    {
        int rlx;
        public RelaxCharacterConsequence(int r)
        {
            rlx = r;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.RelaxCharacter(rlx);
        }
    }
    class NextRelaxCharacterConsequence : IConsequence
    {
        int rlx;
        IConsequence consequence;
        public NextRelaxCharacterConsequence(int r, IConsequence cons)
        {
            rlx = r;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.RelaxCharacter(rlx);
            consequence.Play(nowHike, subject);
        }
    }
    class StressCharacterConsequence : IConsequence
    {
        int rlx;
        public StressCharacterConsequence(int r)
        {
            rlx = r;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.StressCharacter(rlx);
        }
    }
    class NextStressCharacterConsequence : IConsequence
    {
        int rlx;
        IConsequence consequence;
        public NextStressCharacterConsequence(int r, IConsequence cons)
        {
            rlx = r;
            consequence = cons;
        }
        public void Play(Hike nowHike, Character subject)
        {
            subject.StressCharacter(rlx);
            consequence.Play(nowHike, subject);
        }
    }
    public interface IConsequence
    {
        public void Play(Hike nowHike, Character subject);
    }
}