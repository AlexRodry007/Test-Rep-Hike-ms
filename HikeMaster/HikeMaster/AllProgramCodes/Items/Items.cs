using System;
using System.Collections.Generic;
using System.IO;


namespace HikeMaster
{
    public class Storage
    {
        List<Item> items;
        int maxVolume;
        int maxMass;
        int nowVolume = 0;
        int nowMass = 0;
        public Storage(List<Item> itms, int maxv, int maxm)
        {
            items = itms;
            foreach(Item item in itms)
            {
                nowMass += item.GetMass();
                nowVolume += item.GetVolume();
            }
            maxVolume = maxv;
            maxMass = maxm;
        }
        public bool AddItem(Item itm)
        {
            if (nowVolume + itm.GetVolume() <= maxVolume)
            {
                if (nowMass + itm.GetMass() <= maxMass)
                {
                    items.Add(itm);
                    return true;
                }
            }
            return false;
        }
        public bool RemoveItem(Item itm)
        {
            if (items.Contains(itm))
            {
                nowVolume -= itm.GetVolume();
                nowMass -= itm.GetMass();
                return true;
            }
            else
                return false;
        }
        public Item GetItem(int i)
        {
            return items[i];
        }
        public List<Item> GetAllItems()
        {
            return items;
        }
        public List<Item> FindPassiveUses(string searchedName)
        {
            List<Item> foundItems = new List<Item> { };
            foreach (Item tryableItem in items)
            {
                if (tryableItem.FindPassiveUses(searchedName) != 0)
                    foundItems.Add(tryableItem);
            }
            return foundItems;
        }
        public int GetMass()
        {
            return nowMass;
        }
        public int GetMaxMass()
        {
            return maxMass;
        }
        public int GetMaxVolume()
        {
            return maxVolume;
        }
    }
    public struct PassiveUse
    {
        string useName;
        int level;
        public PassiveUse(string name, int lvl)
        {
            useName = name;
            level = lvl;
        }
        public int CompareAndReturn(string comparableName)
        {
            if (comparableName == useName)
                return level;
            else
                return 0;
        }
    }
    public class UseItem
    {
        string description;
        bool consume;
        IConsequence consequence;
        public UseItem(string desc, bool consum, IConsequence cons)
        {
            description = desc;
            consume = consum;
            consequence = cons;
        }
        public void ShowDescription()
        {
            Console.WriteLine(description);
        }
        public string GetDescription()
        {
            return description;
        }
        public bool Use(Hike nowHike, Character subject)
        {
            consequence.Play(nowHike, subject);
            return consume;
        }
    }
    public class Item
    {
        const string weaponeTag = "Weapone";
        const string weaponeDamageTag = "WeaponeDamage";
        string name;
        string description;
        int id;
        int mass;
        int volume;
        List<PassiveUse> passiveUses = new List<PassiveUse> { };
        List<UseItem> uses;
        public Item(int numb)
        {
            Item buffer = ReadLoadItem.ReadItemById(numb);
            name = buffer.GetName();
            description = buffer.GetDescription();
            id = numb;
            mass = buffer.GetMass();
            volume = buffer.GetVolume();
            passiveUses = buffer.passiveUses;
            uses = buffer.GetAllUseItems();
        }
        public Item(string nam, string descr, int d, int m, int v, List<PassiveUse> pUses, List<UseItem> useItems)
        {
            name = nam;
            description = descr;
            id = d;
            mass = m;
            volume = v;
            passiveUses = pUses;
            uses = useItems;
        }
        
        public int FindPassiveUses(string comparableUse)
        {
            int ret = 0;
            foreach (PassiveUse pUse in passiveUses)
            {
                ret = pUse.CompareAndReturn(comparableUse);
                if (ret != 0)
                    break;
            }
            return ret;
        }

        public string GetName()
        {
            return name;
        }
        public int GetId()
        {
            return id;
        }
        public string GetDescription()
        {
            return description;
        }
        public int GetMass()
        {
            return mass;
        }
        public int GetVolume()
        {
            return volume;
        }
        public List<UseItem> GetAllUseItems()
        {
            return uses;
        }
        public List<PassiveUse> GetAllPassiveUses()
        {
            return passiveUses;
        }
        public bool UseItem(int index, Hike nowHike, Character subject)
        {
            return uses[index].Use(nowHike, subject);
        }
        public bool IsWeapone()
        {
            if (FindPassiveUses(weaponeTag) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /*
         1 - Hand to Hand   (0/3)
         2 - Light weapone  (1/1)
         3 - Heavy weapone  (0/0)
         4 - Ranged weapone (1/0)
        */
        public int GetWeaponeAttribute()
        {
            switch (FindPassiveUses(weaponeTag))
            {
                case (1):
                    {
                        return 0;
                    }
                case (2):
                    {
                        return 1;
                    }
                case (3):
                    {
                        return 0;
                    }
                case (4):
                    {
                        return 1;
                    }
                default:
                    {
                        return -1;
                    }
            }

        }
        public int GetWeaponeTalent()
        {
            switch (FindPassiveUses(weaponeTag))
            {
                case (1):
                    {
                        return 3;
                    }
                case (2):
                    {
                        return 1;
                    }
                case (3):
                    {
                        return 0;
                    }
                case (4):
                    {
                        return 1;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }
        public int GetWeaponeDamage()
        {
            return FindPassiveUses(weaponeDamageTag);
        }
    }
}