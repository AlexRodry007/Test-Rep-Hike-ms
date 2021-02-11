using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;


namespace HikeMaster
{
    public class Map
    {
        static Coords townCoords = new Coords(2, 2);
        static List<List<int>> mapp;
        public static Coords GetTownCoords()
        {
            return townCoords;
        }
        public static void GetMap()
        {
            const string mapPath = @"..\..\..\XMLFiles\Tiles\Map.xml";
            List<List<int>> map = new List<List<int>> { };
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(mapPath);
            XmlElement root = xDoc.DocumentElement;
            int i = 0;
            foreach(XmlElement rowElement in root.ChildNodes)
            {
                map.Add(new List<int> { });
                foreach(XmlElement columnElement in rowElement.ChildNodes)
                {
                    map[i].Add(Convert.ToInt32(columnElement.InnerText));
                }
                i++;
            }
            mapp = map;
        }
        private static void ShowPlacementMap(Coords nowPlacement)
        {
            int x = 0;
            int y = 0;
            foreach (List<int> a in mapp)
            {
                foreach (int b in a)
                {
                    if (nowPlacement.Get()[0] == x && nowPlacement.Get()[1] == y)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                    Console.Write(b);
                    x++;
                }
                x = 0;
                y++;
                Console.WriteLine();

            }
        }
        private static void ShowPlacementMap(Coords nowPlacement, Coords finishCoords)
        {
            int x = 0;
            int y = 0;
            foreach (List<int> a in mapp)
            {
                foreach (int b in a)
                {
                    if (nowPlacement.Get()[0] == x && nowPlacement.Get()[1] == y)
                        Console.Write("#");
                    else
                        if (finishCoords.Get()[0] == x && finishCoords.Get()[1] == y)
                        Console.Write("T");
                    else
                        Console.Write(" ");
                    Console.Write(b);
                    x++;
                }
                x = 0;
                y++;
                Console.WriteLine();

            }
        }
        public static int GetTileByCoords(Coords tileCoords)
        {
            int id;
            try
            {
                id = mapp[tileCoords.Get()[1]][tileCoords.Get()[0]];
            }
            catch (Exception)
            {
                id = -1;
            }
            return id;
        }
        public static TileWithCoords GetTileWIthCOordsByCoords(Coords tileCoords)
        {
            int id;
            try
            {
                id = mapp[tileCoords.Get()[0]][tileCoords.Get()[1]];
            }
            catch (Exception)
            {
                id = -1;
            }
            return new TileWithCoords(id, tileCoords);
        }
        public static List<Coords> BuildPath(Coords nowPlacement, Coords destination)
        {
            List<Coords> path = new List<Coords> { };
            List<Coords> movement = new List<Coords> { };
            char playerInput;
            do
            {
                ShowPlacementMap(nowPlacement, destination);
                playerInput = Console.ReadKey().KeyChar;
                Console.Write('\n');
                switch (playerInput)
                {
                    case '1':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, 1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(-1, 1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '2':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, 1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(0, 1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '3':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, 1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(1, -1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '6':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, 0);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(1, 0));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '9':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, -1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(1, -1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '8':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, -1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(0, -1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '7':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, -1);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(-1, -1));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '4':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, 0);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(-1, 0));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '5':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, 0);
                                path.Add(nowPlacement);
                                movement.Add(new Coords(0, 0));
                                Console.WriteLine("Tile {0} on {1}, {2} added to the path", path[path.Count() - 1], nowPlacement.Get()[0], nowPlacement.Get()[1]);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '-':
                        {
                            if (path.Count() != 0)
                            {
                                path.RemoveAt(path.Count - 1);
                                nowPlacement = nowPlacement - movement[movement.Count() - 1];
                                movement.RemoveAt(movement.Count() - 1);
                                Console.WriteLine("Removed last step");
                            }
                            else
                            {
                                Console.WriteLine("Nothing to remove");
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            } while (!(destination == nowPlacement && playerInput == '5'));
            path.RemoveAt(path.Count() - 1);
            movement.RemoveAt(movement.Count() - 1);
            return path;
        }
        public static TileWithCoords ChooseTileOnMap(Coords nowPlacement)
        {
            int choosenTile = -1;
            char playerInput;
            do
            {
                ShowPlacementMap(nowPlacement);
                playerInput = Console.ReadKey().KeyChar;
                Console.Write('\n');
                switch (playerInput)
                {
                    case '1':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, 1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '2':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, 1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '3':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, 1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, 1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '6':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, 0);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '9':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(1, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(1, -1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '8':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, -1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '7':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, -1)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, -1);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '4':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(-1, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(-1, 0);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    case '5':
                        {
                            if (GetTileByCoords(nowPlacement + new Coords(0, 0)) != -1)
                            {
                                nowPlacement = nowPlacement + new Coords(0, 0);
                                choosenTile = GetTileByCoords(nowPlacement);
                            }
                            else
                            {
                                Console.WriteLine("Out of bounds");
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            } while (!(playerInput == '5'));
            return new TileWithCoords(choosenTile, nowPlacement);
        }
    }

    public struct TileWithCoords
    {
        int tile;
        Coords coords;
        public TileWithCoords(int t, Coords c)
        {
            tile = t;
            coords = c;
        }
        public int GetTile()
        {
            return tile;
        }
        public Coords GetCoords()
        {
            return coords;
        }
    }
    public struct Coords
    {
        int x;
        int y;
        public static bool operator ==(Coords c1, Coords c2)
        {
            if (c1.Get()[0] == c2.Get()[0] && c1.Get()[1] == c2.Get()[1])
                return true;
            else
                return false;
        }
        public static bool operator !=(Coords c1, Coords c2)
        {
            if (c1.Get()[0] == c2.Get()[0] && c1.Get()[1] == c2.Get()[1])
                return false;
            else
                return true;
        }
        public static Coords operator +(Coords c1, Coords c2)
        {
            return new Coords(c1.Get()[0] + c2.Get()[0], c1.Get()[1] + c2.Get()[1]);
        }
        public static Coords operator -(Coords c1, Coords c2)
        {
            return new Coords(c1.Get()[0] - c2.Get()[0], c1.Get()[1] - c2.Get()[1]);
        }
        public Coords(int x1, int y1)
        {
            x = x1;
            y = y1;
        }
        public int[] Get()
        {
            return new int[] { x, y };
        }
    }

}