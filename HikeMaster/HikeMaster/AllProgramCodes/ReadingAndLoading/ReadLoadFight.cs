using System;
using System.Collections.Generic;
using System.Xml;

namespace HikeMaster
{
    class ReadLoadFight
    {
        const string AllListPlayerActionPath = @"..\..\..\XMLFiles\Fight\AllPlayerActionLists.xml";
        const string AllListPathsPath = @"..\..\..\XMLFiles\Fight\AllActionListPathsPath.xml";
        const string DummyTactic = @"..\..\..\XMLFiles\Fight\DummyTactic.xml";
        const string AllTacticsPath = @"..\..\..\XMLFiles\Fight\AllTacticsPath.xml";

        public static List<Tactic> ReadTactic(int id)
        {
            XmlDocument allTacticsPathPath = new XmlDocument();
            allTacticsPathPath.Load(AllTacticsPath);
            XmlNode tacticRoot = allTacticsPathPath.DocumentElement;
            string path = tacticRoot.ChildNodes[id].InnerText;
            Character character;
            List<IAction> actions1;
            List<Tactic> tactics = new List<Tactic> { };
            XmlDocument tacticDocument = new XmlDocument();
            tacticDocument.Load(path);
            XmlNode dummyRoot = tacticDocument.DocumentElement;


            foreach (XmlNode tacticNode in dummyRoot.ChildNodes)
            {
                character = ReadLoadCharacter.ReadCharacterBySideAndId(Convert.ToInt32(tacticNode.ChildNodes[0].InnerText), Convert.ToInt32(tacticNode.ChildNodes[1].InnerText));
                actions1 = ReadActionList(Convert.ToInt32(tacticNode.ChildNodes[2].InnerText));
                tactics.Add(new Tactic(actions1, character));
            }
            return tactics;
        }
        public static List<List<IAction>> ReadPlayerActionList()
        {
            List<List<IAction>> actions = new List<List<IAction>> { };
            XmlDocument allPlayerActionListsDocument = new XmlDocument();
            allPlayerActionListsDocument.Load(AllListPlayerActionPath);
            XmlNode fightRoot = allPlayerActionListsDocument.DocumentElement;

            foreach (XmlNode roott in fightRoot.ChildNodes)
            {
                actions.Add(ReadActionList(roott));
            }
            return actions;
        }

        public static List<IAction> ReadActionList(int id)
        {
            List<IAction> actions = new List<IAction> { };
            XmlDocument allPathFightDocument = new XmlDocument();
            allPathFightDocument.Load(AllListPathsPath);
            XmlNode fightRoot = allPathFightDocument.DocumentElement;
            string path = fightRoot.ChildNodes[id].InnerText;

            XmlDocument fightDocument = new XmlDocument();
            fightDocument.Load(path);
            XmlNode root = fightDocument.DocumentElement;
            foreach (XmlNode roott in root.ChildNodes)
            {
                actions.Add(GetAction(roott));
            }
            return actions;
        }
        public static List<IAction> ReadActionList(XmlNode root)
        {
            List<IAction> actions = new List<IAction> { };
            foreach (XmlNode roott in root.ChildNodes)
            {
                actions.Add(GetAction(roott));
            }
            return actions;
        }

        public static IAction GetAction(XmlNode attackNode)
        {

            switch (attackNode.Name)
            {
                case "Attack":
                    {
                        string name = attackNode.ChildNodes[0].InnerText;
                        int result = Convert.ToInt32(attackNode.ChildNodes[1].InnerText);
                        double result1 = Double.Parse(attackNode.ChildNodes[2].InnerText);
                        double resurt2 = Double.Parse(attackNode.ChildNodes[3].InnerText);
                        return new Attack(name, result, result1, resurt2);
                    }
                case "Skip":
                    {
                        return new Skip(Convert.ToInt32(attackNode.ChildNodes[0].InnerText));
                    }
                default:
                    {
                        return null;
                    }
            }

        }
    };

}