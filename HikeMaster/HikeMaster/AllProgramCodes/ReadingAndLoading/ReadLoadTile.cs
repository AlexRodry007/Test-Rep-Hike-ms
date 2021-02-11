using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;



namespace HikeMaster
{
    class ReadLoadTile
    {
        const string AllTilePathsPath = @"..\..\..\XMLFiles\Tiles\AllTilePaths.xml";
        public static void CleanEverything()
        {
            t = 0;
            tMax = 0;
            ichoices.Clear();
        }
        public static void ShowAll()
        {
            foreach (List<IChoice> lich in ichoices)
            {
                foreach (IChoice cho in lich)
                {
                    cho.writeDescription();
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static List<List<IChoice>> ichoices = new List<List<IChoice>> { };

        static int t = 0;
        static int tMax = 0;

        public static void tCheck()
        {
            if (t < tMax)
                t = tMax;
            t++;
        }
        public static Tile ReadTileById(int id)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(AllTilePathsPath);
            string path = xDoc.DocumentElement.ChildNodes[id].InnerText;
            return ReadXMLTilebyPath(path);
        }
        public static Tile ReadXMLTilebyPath(string path)
        {
            List<Eventt> safeEvents = new List<Eventt> { };
            List<Eventt> dangerousEvents = new List<Eventt> { };
            List<Eventt> quests = new List<Eventt> { };
            string name;
            string description;
            int safeness;
            int id;
            int tileLength;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList rootChilds = xRoot.ChildNodes;
            name = rootChilds[0].InnerText;
            description = rootChilds[1].InnerText;
            safeness = Convert.ToInt32(rootChilds[2].InnerText);
            id = Convert.ToInt32(rootChilds[3].InnerText);
            tileLength = Convert.ToInt32(rootChilds[4].InnerText);
            foreach (XmlNode safeEventsNodes in rootChilds[5])
            {
                safeEvents.Add(GetEventt(safeEventsNodes));
                tCheck();
            }
            foreach (XmlNode dangerousEventsNodes in rootChilds[6])
            {
                dangerousEvents.Add(GetEventt(dangerousEventsNodes));
                tCheck();

            }
            foreach (XmlNode questEventsNodes in rootChilds[7])
            {
                quests.Add(GetEventt(questEventsNodes));
                tCheck();

            }
            CleanEverything();
            return new Tile(safeEvents, dangerousEvents, name, description, safeness, id, tileLength, quests);
        }
        public static Eventt GetEventt(XmlNode eventNode)
        {
            ichoices.Add(new List<IChoice> { });
            string ename = eventNode.ChildNodes[0].InnerText;
            foreach (XmlNode choiceNode in eventNode.ChildNodes[1])
            {
                ichoices[t].Add(GetIchoice(choiceNode));
            }
            return new Eventt(ename, ichoices[t]);
        }
        public static XmlDocument GetTileDocumentById(int id)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(AllTilePathsPath);
            string path = xDoc.DocumentElement.ChildNodes[id].InnerText;
            XmlDocument tileDocument = new XmlDocument();
            tileDocument.Load(path);
            return tileDocument;
        }
        public static IConsequence GetCharacterConsequence(XmlNode consequenceNode)
        {
            switch (consequenceNode.Name)
            {
                case "goodConsequence":
                    {
                        return GetCharacterConsequence(consequenceNode.FirstChild);
                    }
                case "badConsequence":
                    {
                        return GetCharacterConsequence(consequenceNode.FirstChild);
                    }
                case "dcc":
                    {
                        return new DamageCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "ndcc":
                    {
                        return new NextDamageCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "ecc":
                    {
                        return new ExhaustCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "necc":
                    {
                        return new NextExhaustCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "hcc":
                    {
                        return new HungerCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "nhcc":
                    {
                        return new NextHungerCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "scc":
                    {
                        return new StressCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "nscc":
                    {
                        return new NextStressCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "rtcc":
                    {
                        return new RestCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "nrtcc":
                    {
                        return new NextRestCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "hlcc":
                    {
                        return new HealCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "nhlcc":
                    {
                        return new NextHealCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "etcc":
                    {
                        return new EatCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "netcc":
                    {
                        return new NextEatCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "rxcc":
                    {
                        return new RelaxCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "nrxcc":
                    {
                        return new NextRelaxCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "gicc":
                    {
                        return new GetItemCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "ngicc":
                    {
                        return new NextGetItemCharacterConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                case "cpc":
                    {
                        return new ChangeProgressConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText));
                    }
                case "ncpc":
                    {
                        return new NextChangeProgressConsequence(Convert.ToInt32(consequenceNode.ChildNodes[0].InnerText), GetCharacterConsequence(consequenceNode.ChildNodes[1]));
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        public static IChoice GetIchoice(XmlNode choiceNode)
        {
            switch (choiceNode.Name)
            {
                case "sbc":
                    {
                        return new StartBattleChoice(choiceNode.ChildNodes[0].InnerText, Convert.ToInt32(choiceNode.ChildNodes[1].InnerText));
                    }
                case "rttt":
                    {
                        return new ReturnToTheTown(choiceNode.ChildNodes[0].InnerText);
                    }
                case "gst":
                    {
                        return new GoodStrongTest(choiceNode.ChildNodes[0].InnerText, Convert.ToInt32(choiceNode.ChildNodes[1].InnerText), Convert.ToInt32(choiceNode.ChildNodes[2].InnerText), Convert.ToInt32(choiceNode.ChildNodes[3].InnerText));
                    }
                case "ne":
                    {
                        int jj = t;
                        tCheck();

                        if (t > tMax)
                            tMax = t;
                        string ename = choiceNode.ChildNodes[0].InnerText;
                        NextEvent nextEvent = new NextEvent(ename, GetEventt(choiceNode.ChildNodes[1]));
                        t = jj;
                        return nextEvent;
                    }
                case "nect":
                    {
                        int jj = t;
                        tCheck();

                        if (t > tMax)
                            tMax = t;
                        string ename = choiceNode.ChildNodes[0].InnerText;
                        int difficulty = Convert.ToInt32(choiceNode.ChildNodes[1].InnerText);
                        int attribute = Convert.ToInt32(choiceNode.ChildNodes[2].InnerText);
                        int talent = Convert.ToInt32(choiceNode.ChildNodes[3].InnerText);
                        Eventt gEvent = GetEventt(choiceNode.ChildNodes[4]);
                        t++;
                        if (t > tMax)
                            tMax = t;
                        NextEventCharacterTest nextEvent = new NextEventCharacterTest(ename, difficulty, attribute, talent, gEvent, GetEventt(choiceNode.ChildNodes[5]));
                        t = jj;
                        return nextEvent;
                    }
                case "neui":
                    {
                        int jj = t;
                        tCheck();
                        if (t > tMax)
                            tMax = t;
                        string eName = choiceNode.ChildNodes[0].InnerText;
                        string gDescr = choiceNode.ChildNodes[1].InnerText;
                        string bDescr = choiceNode.ChildNodes[2].InnerText;
                        string passiveUseName = choiceNode.ChildNodes[3].InnerText;
                        int reqLvl = Convert.ToInt32(choiceNode.ChildNodes[4].InnerText);
                        NextEventUseItem nextEvent = new NextEventUseItem(eName, gDescr, bDescr, passiveUseName, reqLvl, GetEventt(choiceNode.ChildNodes[5]));
                        t = jj;
                        return nextEvent;
                    }
                case "ist":
                    {
                        return new ItemStrongTest(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText, choiceNode.ChildNodes[2].InnerText, Convert.ToInt32(choiceNode.ChildNodes[3].InnerText), Convert.ToInt32(choiceNode.ChildNodes[4].InnerText), Convert.ToInt32(choiceNode.ChildNodes[5].InnerText), Convert.ToInt32(choiceNode.ChildNodes[6].InnerText));
                    }
                case "dct":
                    {
                        return new DamageCharacterTest(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText, choiceNode.ChildNodes[2].InnerText, Convert.ToInt32(choiceNode.ChildNodes[3].InnerText), Convert.ToInt32(choiceNode.ChildNodes[4].InnerText), Convert.ToInt32(choiceNode.ChildNodes[5].InnerText), Convert.ToInt32(choiceNode.ChildNodes[6].InnerText));
                    }
                case "ncc":
                    {
                        return new NextCharacterConsequence(choiceNode.ChildNodes[0].InnerText, GetCharacterConsequence(choiceNode.ChildNodes[1]));
                    }
                case "tcct":
                    {
                        return new TwoConsequencesCharacterTest(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText, choiceNode.ChildNodes[2].InnerText, Convert.ToInt32(choiceNode.ChildNodes[3].InnerText), Convert.ToInt32(choiceNode.ChildNodes[4].InnerText), Convert.ToInt32(choiceNode.ChildNodes[5].InnerText), GetCharacterConsequence(choiceNode.ChildNodes[6]), GetCharacterConsequence(choiceNode.ChildNodes[7]));
                    }
                case "tscct":
                    {
                        return new TwoSquadConsequencesCharacterTest(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText, choiceNode.ChildNodes[2].InnerText, Convert.ToInt32(choiceNode.ChildNodes[3].InnerText), Convert.ToInt32(choiceNode.ChildNodes[4].InnerText), Convert.ToInt32(choiceNode.ChildNodes[5].InnerText), GetCharacterConsequence(choiceNode.ChildNodes[6]), GetCharacterConsequence(choiceNode.ChildNodes[7]));
                    }
                case "tcact":
                    {
                        return new TwoConsequencesAllCharacterTest(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText, choiceNode.ChildNodes[2].InnerText, Convert.ToInt32(choiceNode.ChildNodes[3].InnerText), Convert.ToInt32(choiceNode.ChildNodes[4].InnerText), Convert.ToInt32(choiceNode.ChildNodes[5].InnerText), GetCharacterConsequence(choiceNode.ChildNodes[6]), GetCharacterConsequence(choiceNode.ChildNodes[7]));
                    }
                case "sc":
                    {
                        return new SkipChoice(choiceNode.ChildNodes[0].InnerText, choiceNode.ChildNodes[1].InnerText);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}