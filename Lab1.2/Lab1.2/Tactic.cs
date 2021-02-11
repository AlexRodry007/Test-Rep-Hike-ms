using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Tactic
    {
        public Tactic()
        {
            Actions = new HashSet<Action>();
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string TacticName { get; set; }
        public string TacticValue { get; set; }
        public int? Fight { get; set; }

        public virtual Fight FightNavigation { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
