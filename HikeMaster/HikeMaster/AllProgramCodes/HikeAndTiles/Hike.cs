using System;
using System.Collections.Generic;

namespace HikeMaster
{
    public class Eventt
    {
        string name;
        List<IChoice> choices;
        bool continuee = true;
        public Eventt(string n, List<IChoice> cho)
        {
            name = n;
            choices = cho;
        }
        public int Play(Hike nowHike)
        {
            Console.WriteLine(name);
            int i = 0;
            foreach (IChoice choice in choices)
            {
                choice.writeDescription(i);
                i++;
            }
            while (continuee)
            {
                int cho = PlayerInput.Input(0, choices.Count);
                Console.Write("\n");
                choices[cho].play(nowHike);
                continuee = false;
            }
            continuee = true;
            int progress = 10;
            progress += nowHike.GetProgressChanger();
            return progress;
        }
        public string GetName()
        {
            return name;
        }
        public List<IChoice> GetListOFIChoices()
        {
            return choices;
        }
    }
    public class Tile
    {
        List<Eventt> quests;
        int progress = 0;
        int tileLength = 0;
        Random rnd = new Random();
        string name;
        int id;
        string description;
        int safeness;
        List<Eventt> safeEvents;
        List<Eventt> dangerousEvents;
        public Tile(List<Eventt> seve, List<Eventt> deve, string n, string desc, int safe, int i, int l, List<Eventt> qsts)
        {
            safeEvents = seve;
            dangerousEvents = deve;
            name = n;
            description = desc;
            safeness = safe;
            id = i;
            tileLength = l;
            quests = qsts;
        }
        public int Walk(Hike nowHike)
        {
            Eventt nowEvent;
            Console.WriteLine("Walked into {0}", name);
            while (progress <= tileLength)
            {
                if (!AskToContinue())
                    return progress;
                int sRating;
                sRating = rnd.Next(0, 100);
                if (sRating < safeness)
                {
                    int sChance = safeEvents.Count;
                    int sEvent = rnd.Next(0, sChance);
                    nowEvent = safeEvents[sEvent];
                }
                else
                {
                    int dChance = dangerousEvents.Count;
                    int dEvent = rnd.Next(0, dChance);
                    nowEvent = dangerousEvents[dEvent];
                }
                nowEvent.Play(nowHike);
                progress += 10;
            }
            progress = 0;
            return -1;
        }
        public int Walk(Hike nowHike, int prg)
        {
            Eventt nowEvent;
            progress = prg;
            Console.WriteLine("Walked into {0}", name);
            while (progress <= tileLength)
            {
                if (!AskToContinue())
                    return progress;
                int sRating;
                sRating = rnd.Next(0, 100);
                if (sRating < safeness)
                {
                    int sChance = safeEvents.Count;
                    int sEvent = rnd.Next(0, sChance);
                    nowEvent = safeEvents[sEvent];
                }
                else
                {
                    int dChance = dangerousEvents.Count;
                    int dEvent = rnd.Next(0, dChance);
                    nowEvent = dangerousEvents[dEvent];
                }
                progress += nowEvent.Play(nowHike);
                nowHike.SetProgressChanger(0);

            }
            progress = 0;
            return -1;
        }
        private bool AskToContinue()
        {
            Console.WriteLine("Start the Event?");
            Console.WriteLine("0. Yes");
            Console.WriteLine("1. No");
            if (PlayerInput.Input(0, 2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Eventt> GetQuests()
        {
            return quests;
        }
        public string GetName()
        {
            return name;
        }
        public int GetProgress()
        {
            return progress;
        }
        public void SetProgress(int prg)
        {
            progress = prg;
        }
    }
    public class Hike
    {
        string name;
        Storage hikeStorage;
        List<Character> characters;
        List<Coords> tilesCoords;
        Eventt quest;
        Coords hikePlacement;
        bool endOfTheHike = false;
        int money;
        int progress = 0;
        static int maxId = 0;
        int id;
        int[] strongAttr = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[][] strongTals = new int[9][]
            {
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0}
            };
        public Hike(string nam, List<Character> chars, List<Coords> way, Eventt questt, List<Item> itemm, int v, int m)
        {
            name = nam;
            hikeStorage = new Storage(itemm, v, m);
            characters = chars;
            tilesCoords = way;
            quest = questt;
            id = maxId;
            maxId++;
            hikePlacement = way[0];
            foreach (Character character in chars)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (strongAttr[i] < character.GetAttributeValue(i))
                        strongAttr[i] = character.GetAttributeValue(i);
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (strongTals[i][j] < character.GetTalentValue(i, j))
                            strongTals[i][j] = character.GetTalentValue(i, j);
                    }
                }
            }
        }
        public Hike(string nam, List<Character> chars, List<Coords> way, Eventt questt, Storage storage)
        {
            name = nam;
            hikeStorage = storage;
            characters = chars;
            tilesCoords = way;
            quest = questt;
            id = maxId;
            maxId++;
            foreach (Character character in chars)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (strongAttr[i] < character.GetAttributeValue(i))
                        strongAttr[i] = character.GetAttributeValue(i);
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (strongTals[i][j] < character.GetTalentValue(i, j))
                            strongTals[i][j] = character.GetTalentValue(i, j);
                    }
                }
            }
        }
        public Hike(string nam, int progres, List<Character> chars, List<Coords> way, Eventt questt, Storage storage)
        {
            progress = progres;
            name = nam;
            hikeStorage = storage;
            characters = chars;
            tilesCoords = way;
            quest = questt;
            id = maxId;
            maxId++;
            foreach (Character character in chars)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (strongAttr[i] < character.GetAttributeValue(i))
                        strongAttr[i] = character.GetAttributeValue(i);
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (strongTals[i][j] < character.GetTalentValue(i, j))
                            strongTals[i][j] = character.GetTalentValue(i, j);
                    }
                }
            }
        }
        public void Downtime()
        {
            int cho;
            do
            {
                Console.WriteLine("Choose your downtime action:");
                Console.WriteLine("0. Use an item");
                Console.WriteLine("9. Go on");
                cho = PlayerInput.Input(0, 10);
                switch (cho)
                {
                    case 0:
                        {
                            PlayerInteractions.ChooseItem(this, PlayerInteractions.ChooseCharacter(this));
                            break;
                        }
                }
            } while (cho != 9);

        }
        public void AddItem(Item itm)
        {
            if (!hikeStorage.AddItem(itm))
                Console.WriteLine("Not enough Space");
        }
        public int Go()
        {
            int unrealProgres;
            while (tilesCoords.Count != 0)
            {
                hikePlacement = tilesCoords[0];
                unrealProgres = ReadLoadTile.ReadTileById(Map.GetTileByCoords(hikePlacement)).Walk(this, progress);
                if (unrealProgres != -1)
                {
                    progress = unrealProgres;
                    ReadLoadHike.SaveHike(this);
                    return 0;
                }
                progress = 0;
                tilesCoords.RemoveAt(0);
                Downtime();
            }
            quest.Play(this);
            if (!endOfTheHike)
                PlayerInteractions.ContinueHike(this).Go();
            return 1;
        }
        public void Continue()
        {
            for (int nowTile = 0; nowTile < tilesCoords.Count; nowTile++)
            {
                ReadLoadTile.ReadTileById(Map.GetTileByCoords(tilesCoords[nowTile])).Walk(this, progress);
                hikePlacement = tilesCoords[nowTile];
                Downtime();
            }
            quest.Play(this);
            if (!endOfTheHike)
                PlayerInteractions.ContinueHike(this).Go();
        }
        public int GetStrongAttr(int a)
        {
            return strongAttr[a];
        }
        public int GetStrongTalent(int a, int t)
        {
            return strongTals[a][t];
        }
        public Storage GetStorage()
        {
            return hikeStorage;
        }
        public List<Character> GetAllCharacters()
        {
            return characters;
        }
        public void EndOfTheHike()
        {
            endOfTheHike = true;
        }
        public Coords GetHikePlacement()
        {
            return hikePlacement;
        }
        public void SetProgress(int prg)
        {
            progress = prg;
        }
        public int GetId()
        {
            return id;
        }
        public List<Coords> GetCoords()
        {
            return tilesCoords;
        }
        public string GetName()
        {
            return name;
        }
        public Eventt GetQuest()
        {
            return quest;
        }
        public int GetProgress()
        {
            return progress;
        }
        int progressChanger = 0;
        public void SetProgressChanger(int pc)
        {
            progressChanger = pc;
        }
        public int GetProgressChanger()
        {
            return progressChanger;
        }
        public static void SetMaxId(int hikeMaxId)
        {
            maxId = hikeMaxId;
        }
    }
}