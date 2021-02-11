using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HikeMaster
{
    public class Town
    {
        static int townStorageVolume = 10000;
        static int townStorageMass = 10000;
        static List<Character> freeCharacters = new List<Character> { };
        static Storage townStorage = new Storage(new List<Item> { }, townStorageVolume, townStorageMass);
        public static List<Character> GetFreeCharacters()
        {
            return freeCharacters;
        }
        public static void AddFreeCharacter(Character character)
        {
            freeCharacters.Add(character);
        }
        public static void RemoveFreeCharacter(Character character)
        {
            freeCharacters.Remove(character);
        }
        public static void AddStorageItem(Item item)
        {
            townStorage.AddItem(item);
        }
        public static Item TakeStorageItem(Item item)
        {
            townStorage.RemoveItem(item);
            return item;
        }

    }
}