using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;


namespace HikeMaster
{
    class ReadLoadHike
    {
        const string prefix = @"..\..\..\XMLFiles\Hikes\";
        const string namePrefix = "id";
        const string extention = ".xml";
        const string AllHikesPathsPath = @"..\..\..\XMLFiles\Hikes\AllHikesPaths.xml";

       public static void SaveHike(Hike hike)
        {
            string id = Convert.ToString(hike.GetId());
            string name = hike.GetName();
            string characterFileName = prefix + namePrefix + id + extention;
            XDocument characterFile = new XDocument();
            XElement rootElement = new XElement(namePrefix + id);
            XElement hikeName = Functions.CreateElement("name", name);
            rootElement.Add(hikeName);
            rootElement.Add(Functions.CreateElement("progress", Convert.ToString(hike.GetProgress())));
            XElement characterElement = new XElement("characters");
            foreach(Character character in hike.GetAllCharacters())
            {
                characterElement.Add(Functions.CreateElement("characterTrueId", Convert.ToString(character.GetSide()) + Convert.ToString(character.GetId())));
            }
            rootElement.Add(characterElement);
            XElement pathElement = new XElement("path");
            XElement coordElement;
            foreach(Coords coords in hike.GetCoords())
            {
                coordElement = new XElement("coords");
                coordElement.Add(Functions.CreateElement("x", Convert.ToString(coords.Get()[1])));
                coordElement.Add(Functions.CreateElement("y", Convert.ToString(coords.Get()[0])));
                pathElement.Add(coordElement);
            }
            rootElement.Add(pathElement);
            XElement questElement;
            string questName = hike.GetQuest().GetName();
            Coords questTileCoords = hike.GetCoords()[hike.GetCoords().Count - 1];
            Tile questTile = ReadLoadTile.ReadTileById(Map.GetTileByCoords(questTileCoords));
            int i = 0;
            foreach(Eventt tileQuest in questTile.GetQuests())
            {
                if(tileQuest.GetName() == questName)
                {
                    break;
                }
                i++;
            }
            XmlDocument tileDocument = ReadLoadTile.GetTileDocumentById(Map.GetTileByCoords(questTileCoords));
            XmlNode root = tileDocument.DocumentElement;
            XmlNode allQuestsElement = root.ChildNodes[7];
            questElement = XElement.Load(allQuestsElement.ChildNodes[i].CreateNavigator().ReadSubtree());
            rootElement.Add(questElement);
            XElement storageElement = new XElement("storage");
            storageElement.Add(Functions.CreateElement("maxVolume", Convert.ToString(hike.GetStorage().GetMaxVolume())));
            storageElement.Add(Functions.CreateElement("maxMass", Convert.ToString(hike.GetStorage().GetMaxMass())));
            XElement itemsElement = new XElement("Items");
            foreach(Item item in hike.GetStorage().GetAllItems())
            {
                itemsElement.Add(Functions.CreateElement("item", Convert.ToString(item.GetId())));
            }
            storageElement.Add(itemsElement);
            rootElement.Add(storageElement);
            characterFile.Add(rootElement);
            characterFile.Save(characterFileName);
            if(!Functions.CheckHikeExistion(Convert.ToString(id)))
            {
                XDocument xDocument = XDocument.Load(AllHikesPathsPath);
                xDocument.Root.Add(Functions.CreateElement(namePrefix + id, prefix + namePrefix + id + extention));
                xDocument.Save(AllHikesPathsPath);
            }
     
            

        }
        public static Hike LoadHike(int id)
        {
            Hike hike = null;
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllHikesPathsPath);
            if(Functions.CheckHikeExistion(Convert.ToString(id)))
            {
                XmlDocument hikeDocument = new XmlDocument();
                string characterDocumentPath = prefix + namePrefix + id + extention;
                hikeDocument.Load(characterDocumentPath);
                XmlNode hikeRoot = hikeDocument.DocumentElement;
                string name = hikeRoot.ChildNodes[0].InnerText;
                int progress =Convert.ToInt32(hikeRoot.ChildNodes[1].InnerText);
                List<Character> characters = new List<Character> { };
                foreach(XmlNode characterNode in hikeRoot.ChildNodes[2])
                {
                    characters.Add(ReadLoadCharacter.ReadCharacterBySideAndId(characterNode.InnerText));
                }
                List<Coords> coords = new List<Coords> { };
                Coords coord;
                foreach(XmlNode coordNode in hikeRoot.ChildNodes[3])
                {
                    coord = new Coords(Convert.ToInt32(coordNode.ChildNodes[0].InnerText), Convert.ToInt32(coordNode.ChildNodes[1].InnerText));
                    coords.Add(coord);
                }
                Eventt quest = ReadLoadTile.GetEventt(hikeRoot.ChildNodes[4]);//!!!!!!!!!
                ReadLoadTile.tCheck();
                XmlNode StorageNode = hikeRoot.ChildNodes[5];
                int maxVolume = Convert.ToInt32(StorageNode.ChildNodes[0].InnerText);
                int maxMass = Convert.ToInt32(StorageNode.ChildNodes[1].InnerText);

                List<Item> items = new List<Item> { };
                foreach(XmlNode itemNode in StorageNode.ChildNodes[2])
                {
                    items.Add(ReadLoadItem.ReadItemById(Convert.ToInt32(itemNode.InnerText)));
                }
                Storage storage = new Storage(items, maxVolume, maxMass);
                hike = new Hike(name, progress, characters, coords, quest, storage);
            }
            return hike;
        }
    }
}