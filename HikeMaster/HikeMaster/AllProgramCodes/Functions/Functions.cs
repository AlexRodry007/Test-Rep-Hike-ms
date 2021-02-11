using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;


namespace HikeMaster
{
    class Functions
    {
        public const double PI = 3.1415926535897931;
        public static bool TestCharacter(int attr, int talent, int diff, Character character)
        {
            talent = character.GetTalentValue(attr, talent);
            attr = character.GetAttributeValue(attr);
            return Test(attr, talent, diff);
        }
        public static bool Test(int attr, int skill, int diff)
        {
            Random rnd = new Random();
            int probability = Convert.ToInt32(Math.Atan(Convert.ToDouble((attr + 2 * skill - 2 * diff) / (5 + diff))) * (2 / PI))*50 + 50;
            if (rnd.Next(0, 50) + rnd.Next(0, 50) <= probability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static XElement CreateElement(string elementName, string elementValue)
        {
            XElement newElement = new XElement(elementName);
            newElement.Value = elementValue;
            return newElement;
        }
        public static bool CheckCharacterExistion(string trueId)
        {
            const string AllCharactersPathsPath = @"..\..\..\XMLFiles\Characters\AllCharactersPaths.xml";
            const string namePrefix = "id";

            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);
            foreach (XmlNode characterId in xACPDoc.DocumentElement)
            {
                if (characterId.Name == namePrefix + trueId)
                    return true;
            }
            return false;
        }
        public static bool CheckHikeExistion(string id)
        {
            const string AllHikesPathsPath = @"..\..\..\XMLFiles\Hikes\AllHikesPaths.xml";
            const string namePrefix = "id";
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllHikesPathsPath);
            foreach(XmlNode hikeId in xACPDoc.DocumentElement)
            {
                if (hikeId.Name == namePrefix + id)
                    return true;
            }
            return false;
        }
        public static void SetIds()
        {
            int maxId = 0;
            while (CheckHikeExistion(Convert.ToString(maxId)))
            {
                maxId++;
            }
            Hike.SetMaxId(maxId);
            Character.SetMaxId(ReadCharactersMaxId());
        }
        public static int ReadCharactersMaxId()
        {
            const string AllCharactersPathsPath = @"..\..\..\XMLFiles\Characters\AllCharactersPaths.xml";
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);
            XmlNode root = xACPDoc.DocumentElement;
            string id = root.ChildNodes[root.ChildNodes.Count-1].Name.Remove(0, 3);
            return Convert.ToInt32(id)+1;
        }

    }

}