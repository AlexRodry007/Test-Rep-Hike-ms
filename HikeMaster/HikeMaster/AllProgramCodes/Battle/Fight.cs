using System;
using System.Collections.Generic;

namespace HikeMaster
{

    class Tactic
    {
        Character character;
        List<IAction> actions;

        public Tactic(List<IAction> aactions, Character ccharacter)
        {
            actions = aactions;
            character = ccharacter;
        }

        int i = 0;
        int current = 20;
        public List<IAction> GetActions()
        {
            return actions;
        }
        public Character GetCharacter()
        {
            return character;
        }

        public void ResetTactic()
        {
            i = 0;
            current = 20;
        }

        public int Sequence(List<Tactic> tactics)
        {

            current -= actions[i].Play(character, tactics);

            i++;
            Console.WriteLine("{1}: {0}", current, character.GetName());
            return current;
        }

    }

    class Fight
    {
        int countTactic = 0;
        List<Tactic> attackTacticList;
        List<int> attackActionPointsList = new List<int> { };

        List<Tactic> defenseTacticList;
        List<int> defenseActionPointsList = new List<int> { };

        const int actionPoints = 20;
        const int criticalStatus = 2;
        const int minRand = 0;
        const int maxRand = 0;
        public Fight(List<Tactic> atacticList, List<Tactic> dtacticList)
        {
            attackTacticList = atacticList;
            countTactic = attackTacticList.Count;
            for (int i = 0; i < countTactic; i++)
            {
                attackActionPointsList.Add(actionPoints);
            }
            defenseTacticList = dtacticList;
            countTactic = defenseTacticList.Count;
            for (int i = 0; i < countTactic; i++)
            {
                defenseActionPointsList.Add(actionPoints);
            }
        }
        public int Ffight()
        {
            while (attackTacticList.Count != 0 && defenseTacticList.Count != 0)
            {
                Round();
            }
            return 0;
        }
        public int StatusCheck()
        {
            int q;
            q = 0;
            List<int> dropouted = new List<int> { };

            foreach (Tactic tactic in attackTacticList)
            {
                if (tactic.GetCharacter().GetStatus() <= criticalStatus)
                {
                    dropouted.Add(q);
                }
                q++;
            }

            foreach (int drp in dropouted)
            {
                attackTacticList.RemoveAt(drp);
                attackActionPointsList.RemoveAt(drp);
            }
            q = 0;
            dropouted.Clear();

            foreach (Tactic tactic in defenseTacticList)
            {
                if (tactic.GetCharacter().GetStatus() <= criticalStatus)
                {
                    dropouted.Add(q);
                }
                q++;
            }

            foreach (int drp in dropouted)
            {
                defenseTacticList.RemoveAt(drp);
                defenseActionPointsList.RemoveAt(drp);
            }
            dropouted.Clear();
            return 0;
        }

        public int Round()
        {

            Random rnd = new Random();
            int maxAPID = 0;
            int maxAP = attackActionPointsList[0];
            int side = 0;

            foreach (Tactic tactic in attackTacticList)
            {
                tactic.ResetTactic();
            }

            foreach (Tactic tactic in defenseTacticList)
            {
                tactic.ResetTactic();
            }
            countTactic = attackTacticList.Count;

            for (int i = 0; i < countTactic; i++)
            {
                attackActionPointsList[i] = actionPoints;
            }
            countTactic = defenseTacticList.Count;

            for (int i = 0; i < countTactic; i++)
            {
                defenseActionPointsList[i] = actionPoints;
            }

            do
            {
                maxAP = 0;
                for (int i = 0; i < attackActionPointsList.Count; i++)
                {

                    if (maxAP < attackActionPointsList[i])
                    {
                        maxAPID = i;
                        maxAP = attackActionPointsList[maxAPID];
                        side = 0;
                    }

                    else
                    {

                        if (maxAP == attackActionPointsList[i])
                        {

                            if (rnd.Next(minRand, maxRand) == 1)
                            {
                                maxAPID = i;
                                maxAP = attackActionPointsList[maxAPID];
                                side = 0;
                            }
                        }
                    }
                }

                for (int i = 0; i < defenseActionPointsList.Count; i++)
                {

                    if (maxAP < defenseActionPointsList[i])
                    {
                        maxAPID = i;
                        maxAP = defenseActionPointsList[maxAPID];
                        side = 1;
                    }

                    else
                    {

                        if (maxAP == defenseActionPointsList[i])
                        {

                            if (rnd.Next(minRand, maxRand) == 1)
                            {
                                maxAPID = i;
                                maxAP = defenseActionPointsList[maxAPID];
                                side = 1;
                            }
                        }
                    }
                }

                if (maxAP == 0)
                {
                    break;
                }

                if (side == 0)
                {
                    attackActionPointsList[maxAPID] = attackTacticList[maxAPID].Sequence(defenseTacticList);
                }

                else
                {
                    defenseActionPointsList[maxAPID] = defenseTacticList[maxAPID].Sequence(attackTacticList);
                }
                StatusCheck();
            }
            while (attackTacticList.Count != 0 && defenseTacticList.Count != 0);
            return 0;
        }

    }
}