using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Character
    {
        public int TrueId { get; set; }
        public string Name { get; set; }
        public int? Side { get; set; }
        public int? Id { get; set; }
        public int? MaxHealth { get; set; }
        public int? NowHealth { get; set; }
        public int? MaxEnergy { get; set; }
        public int? NowEnergy { get; set; }
        public int? MaxSatiety { get; set; }
        public int? NowSatiety { get; set; }
        public int? MaxSanity { get; set; }
        public int? NowSanity { get; set; }
        public int? Status { get; set; }
        public int? Strength { get; set; }
        public int? HeavyWeapone { get; set; }
        public int? ResourceGather { get; set; }
        public int? Carrying { get; set; }
        public int? HandToHand { get; set; }
        public int? Bruteforce { get; set; }
        public int? Dexterity { get; set; }
        public int? LightWeapone { get; set; }
        public int? RangedWeapone { get; set; }
        public int? Sneak { get; set; }
        public int? Dodge { get; set; }
        public int? Agility { get; set; }
        public int? Constitution { get; set; }
        public int? Shield { get; set; }
        public int? Armour { get; set; }
        public int? Stamina { get; set; }
        public int? Health { get; set; }
        public int? Balance { get; set; }
        public int? Intelligence { get; set; }
        public int? MagickPower { get; set; }
        public int? Medicine { get; set; }
        public int? Cooking { get; set; }
        public int? Science { get; set; }
        public int? Craft { get; set; }
        public int? Memory { get; set; }
        public int? Willpower { get; set; }
        public int? MagickLevel { get; set; }
        public int? Languages { get; set; }
        public int? Learning { get; set; }
        public int? Logistics { get; set; }
        public int? Perception { get; set; }
        public int? Hunting { get; set; }
        public int? Traps { get; set; }
        public int? Foraging { get; set; }
        public int? Spotting { get; set; }
        public int? Pathfinding { get; set; }
        public int? Charisma { get; set; }
        public int? Persuation { get; set; }
        public int? Intimidation { get; set; }
        public int? Trade { get; set; }
        public int? Music { get; set; }
        public int? Discussion { get; set; }
        public int? Leadership { get; set; }
        public int? Command { get; set; }
        public int? Tactic { get; set; }
        public int? Strategy { get; set; }
        public int? Survival { get; set; }
        public int? Adjutant { get; set; }
        public int? Epathy { get; set; }
        public int? Lie { get; set; }
        public int? LieDetector { get; set; }
        public int? Psycology { get; set; }
        public int? Pacification { get; set; }
        public int? AnimalHandling { get; set; }
        public int? Weapone { get; set; }
        public int? Hike { get; set; }
        public int? BattleTactic { get; set; }

        public virtual Tactic BattleTacticNavigation { get; set; }
        public virtual Hike HikeNavigation { get; set; }
        public virtual Item WeaponeNavigation { get; set; }
    }
}
