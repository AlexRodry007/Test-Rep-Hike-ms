using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace HikeMaster
{
    class PlayerInput
    {
        public static int Input(int min, int max)
        {
            string line;
            int result;
            do
            {
                do
                {
                    line = Console.ReadLine();
                }
                while (!Int32.TryParse(line, out result));
            }
            while (!(result >= min && result < max));
            return result;
        }
    }
    class PlayerInteractions
    {
        public static Character ChooseCharacter(Hike nowHike)
        {

            Character subject = null;
            int i = 0;
            Console.WriteLine("Choose character:");
            foreach (Character s in nowHike.GetAllCharacters())
            {
                Console.WriteLine("{0} {1}", i, s.GetName());
                i++;
            }
            subject = nowHike.GetAllCharacters()[PlayerInput.Input(0, nowHike.GetAllCharacters().Count())];
            return subject;
        }
        public static Eventt GetQuestByTile(Tile tile)
        {
            int i = 0;
            List<Eventt> quests = tile.GetQuests();
            Eventt choosenQuest = null;
            Console.WriteLine("Choose the quest in {0}", tile.GetName());
            foreach (Eventt quest in quests)
            {
                Console.WriteLine("{0}. {1}", i, quest.GetName());
            }

            choosenQuest = quests[PlayerInput.Input(0, quests.Count())];

            return choosenQuest;
        }
        public static Hike StartHike()
        {
            List<Coords> path;
            List<Character> characters = new List<Character> { };
            Eventt quest = null;
            List<Eventt> choosenQuests;
            bool continuee = true;
            string name;
            int i;
            int cho;
            Console.WriteLine("Enter hike name");
            name = Console.ReadLine();
            Console.WriteLine("Choose characters");
            while (continuee)
            {
                i = 0;
                Console.WriteLine("Choosen Characters:");
                foreach (Character character1 in characters)
                {
                    Console.WriteLine(character1.GetName());
                }
                Console.WriteLine("Free Characters");
                foreach (Character character in Town.GetFreeCharacters())
                {
                    Console.WriteLine("{0}. {1}", i, character.GetName());
                    i++;
                }
                Console.WriteLine("{0}. Remove characters from hike", i);
                i++;
                Console.WriteLine("{0}. That's all, continue", i);
                cho = PlayerInput.Input(0, Town.GetFreeCharacters().Count() + 2);
                Console.Write("\n");
                if (cho >= 0 && cho < Town.GetFreeCharacters().Count())
                {
                    characters.Add(Town.GetFreeCharacters()[cho]);
                    Town.RemoveFreeCharacter(Town.GetFreeCharacters()[cho]);
                }
                else
                {
                    if(cho == Town.GetFreeCharacters().Count())
                    {
                        do
                        {
                            i = 0;
                            Console.WriteLine("Free Characters");
                            foreach (Character character in Town.GetFreeCharacters())
                            {
                                Console.WriteLine(character.GetName());
                            }
                            Console.WriteLine("Choosen Characters:");
                            foreach (Character character1 in characters)
                            {
                                Console.WriteLine("{0}. {1}", i, character1.GetName());
                                i++;
                            }
                            Console.WriteLine("{0}. Add characters to the hike", i);
                            i++;
                            cho = PlayerInput.Input(0, characters.Count() + 1);
                            if (cho >= 0 && cho < characters.Count())
                            {
                                Town.AddFreeCharacter(characters[cho]);
                                characters.Remove(characters[cho]);
                            }
                            else
                            {
                                if(cho== characters.Count())
                                {
                                    continuee = false;
                                }
                            }
                        } while (continuee);
                            cho = 0;
                        continuee = true;
                    }

                    if (cho == Town.GetFreeCharacters().Count()+1)
                    {
                        if (characters.Count() != 0)
                        {
                            continuee = false;
                        }
                        else
                        {
                            Console.WriteLine("Choose at least one");
                        }
                    }


                }
            }
            Console.WriteLine("Choose quest");
            TileWithCoords choosenTile = Map.ChooseTileOnMap(Map.GetTownCoords());
            choosenQuests = ReadLoadTile.ReadTileById(choosenTile.GetTile()).GetQuests();
            Coords destination = choosenTile.GetCoords();
                i = 0;
                foreach (Eventt qst in choosenQuests)
                {
                    Console.WriteLine("{0}. {1}", i, qst.GetName());
                    i++;
                }
                cho = PlayerInput.Input(0, choosenQuests.Count());
                    quest = choosenQuests[cho];
            
            Console.WriteLine("Choose Path");
            path = Map.BuildPath(Map.GetTownCoords(), destination);
            return new Hike(name, characters, path, quest, new List<Item> { }, 100, 100);
        }
        public static Hike ContinueHike(Hike nowHike)
        {
            string name;
            Console.WriteLine("Enter hike name");
            name = Console.ReadLine();
            List <Character> characters = nowHike.GetAllCharacters();
            Storage storage = nowHike.GetStorage();
            Console.WriteLine("Choose quest");
            TileWithCoords choosenTile = Map.ChooseTileOnMap(nowHike.GetHikePlacement());
            List<Eventt> choosenQuests = ReadLoadTile.ReadTileById(choosenTile.GetTile()).GetQuests();
            Coords destination = choosenTile.GetCoords();
            int i = 0;
            foreach (Eventt qst in choosenQuests)
            {
                Console.WriteLine("{0}. {1}", i, qst.GetName());
                i++;
            }
            int cho = PlayerInput.Input(0, choosenQuests.Count());
            Eventt quest = choosenQuests[cho];

            Console.WriteLine("Choose Path");
            List<Coords> path = Map.BuildPath(nowHike.GetHikePlacement(), destination);
            return new Hike(name, characters, path, quest, storage);
        }
        public static void ChooseItem(Hike nowHike, Character subject)
        {
            int i = 0;
            int cho;
            if (nowHike.GetStorage().GetAllItems().Count() != 0)
            {
                do
                {
                    Console.WriteLine("Choose an item:");
                    foreach (Item itm in nowHike.GetStorage().GetAllItems())
                    {
                        Console.WriteLine("{0}. {1}", i, itm.GetName());
                        i++;
                    }
                    Console.WriteLine("{0}. Return", i);
                    i = 0;
                    cho = PlayerInput.Input(0, nowHike.GetStorage().GetAllItems().Count() + 1);
                    Console.Write("\n");
                    if (cho >= 0 && cho < nowHike.GetStorage().GetAllItems().Count())
                    {
                        if (ChooseUseOfItem(nowHike.GetStorage().GetAllItems()[cho], nowHike, subject))
                            nowHike.GetStorage().GetAllItems().RemoveAt(cho);
                    }
                } while (cho != nowHike.GetStorage().GetAllItems().Count());

            }
        }
        public static int ChoosePassiveUse(string passiveUseName, Hike nowHike)
        {
            List<Item> items = nowHike.GetStorage().FindPassiveUses(passiveUseName);
            if (items.Count() == 0)
                return 0;
            else
            {
                int i = 0;
                int cho;
                Console.WriteLine("Choose Item:");
                foreach (Item itm in items)
                {
                    Console.WriteLine("{0}. {1}", i, itm.GetName());
                    Console.WriteLine("Level: {0}", itm.FindPassiveUses(passiveUseName));
                    i++;
                }
                Console.WriteLine("{0}. Don't use any", i);
                do
                {
                    cho = Convert.ToInt32(Console.ReadKey().KeyChar) - 48;
                    if (cho >= 0 && cho < items.Count())
                    {
                        return items[cho].FindPassiveUses(passiveUseName);
                    }
                    else
                    {
                        if (cho == items.Count())
                            return 0;
                    }
                } while (true);
            }
        }
        public static bool ChooseUseOfItem(Item itm, Hike nowHike, Character subject)
        {
            int i = 0;
            int cho;
            bool consume = false;
            bool continuee = true;
            if (itm.GetAllUseItems().Count() != 0)
            {
                Console.WriteLine("Choose the use of the item:");
                foreach (UseItem use in itm.GetAllUseItems())
                {
                    Console.WriteLine("{0}. {1}", i, use.GetDescription());
                    i++;
                }
                Console.WriteLine("{0}. Return", i);
                do
                {
                    cho = Console.ReadKey().KeyChar - 48;
                    if (cho >= 0 && cho < itm.GetAllUseItems().Count())
                    {
                        if (itm.UseItem(cho, nowHike, subject))
                        {
                            consume = true;
                            continuee = false;
                        }
                    }
                    else
                        if (cho == itm.GetAllUseItems().Count())
                    {
                        continuee = false;
                    }
                } while (continuee);

            }
            return consume;
        }
        public static void StartBattle(Hike hike, List<List<IAction>> listOfActionLists, List<Tactic> enemyCharactersTactics)
        {
            bool continuue = true;
            int i;
            int j;
            int w;
            int cho;
            List<Tactic> characterTactics = new List<Tactic> { };

            foreach (Character character in hike.GetAllCharacters())
            {
                characterTactics.Add(new Tactic(listOfActionLists[0], character));
            }
            while (continuue)
            {
                Console.WriteLine("Choose Character");
                j = 0;

                foreach (Tactic tactic in characterTactics)
                {
                    Console.WriteLine("{0}. {1}", j, tactic.GetCharacter().GetName());
                    i = 0;
                    j++;
                    foreach (IAction action in tactic.GetActions())
                    {
                        Console.WriteLine(" {0} {1}", i, action.GetName());
                        i++;
                    }
                }
                Console.WriteLine("{0}. Start Fight", j);
                Console.Write("\n");

                cho = PlayerInput.Input(0, hike.GetAllCharacters().Count() + 1);
                if (cho >= 0 && cho < hike.GetAllCharacters().Count())
                {
                    j = 0;

                    Console.WriteLine("Choose actions");
                    foreach (List<IAction> actionList in listOfActionLists)
                    {
                        Console.Write("{0}: ", j);
                        j++;
                        i = 0;
                        foreach (IAction action in actionList)
                        {
                            Console.WriteLine(" {0} {1}", i, action.GetName());
                            i++;
                        }
                    }
                    w = PlayerInput.Input(0, listOfActionLists.Count());
                    characterTactics.RemoveAt(cho);
                    characterTactics.Insert(cho, new Tactic(listOfActionLists[w], hike.GetAllCharacters()[cho]));
                }
                else
                {
                    if (cho == hike.GetAllCharacters().Count())
                    {
                        continuue = false;
                    }
                }
            }
            Fight fight = new Fight(characterTactics, enemyCharactersTactics);
            Console.WriteLine(fight.Ffight());
        }
    }

}