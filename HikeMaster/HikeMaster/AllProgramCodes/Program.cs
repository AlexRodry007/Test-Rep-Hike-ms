using System;
using System.Collections.Generic;
using System.IO;

namespace HikeMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] att = new int[9] { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[][] tal = new int[9][]
            {
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10}
            };
            //Coords start = new Coords(2, 2);
            //Coords finish = new Coords(0, 0);
            //Character Alex = new Character("Alex", 1, att, tal, 100, 100, 100, 100);
           //Character Berta = new Character("Berta", 1, att, tal, 100, 100, 100, 100);

            //ReadLoadCharacter.AddNewCharacter(Alex);
            //ReadLoadCharacter.AddNewCharacter(Berta);

            //Console.WriteLine("({0}/{1}) {2}", Alex.GetWeaponeAttribute(), Alex.GetWeaponeTalent(), Alex.GetWeaponeDamage());
            //Console.ReadLine();
            Functions.SetIds();
            Map.GetMap();
            Character Alex = ReadLoadCharacter.ReadCharacterBySideAndId(1, 0);
            Character Berta = ReadLoadCharacter.ReadCharacterBySideAndId(1, 1);
            Alex.ShowAll();
            Berta.ShowAll();
            /*Character Clark = new Character("Clark", 1, att, tal, 100, 100, 100, 100);
            ReadLoadCharacter.AddNewCharacter(Clark);*/
            Character Dummy = ReadLoadCharacter.ReadCharacterBySideAndId(2, 2);
            Town.AddFreeCharacter(Alex);
            Town.AddFreeCharacter(Berta);


            Hike hike;
            do
            {
                hike = PlayerInteractions.StartHike();
                hike.Go();
                Console.WriteLine("Press 9 to end the program");
            }
            while (Console.ReadLine() != "9");
           //Hike hike = ReadLoadHike.LoadHike(0);
            //hike.Go();
            ReadLoadCharacter.SaveCharacter(Alex);
        }
    }
}
