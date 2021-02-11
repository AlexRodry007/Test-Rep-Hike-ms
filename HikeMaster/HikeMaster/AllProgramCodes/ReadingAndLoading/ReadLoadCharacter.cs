using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;


namespace HikeMaster
{
    class ReadLoadCharacter
    {
        const string prefix = @"..\..\..\XMLFiles\Characters\";
        const string namePrefix = "id";
        const string extention = ".xml";
        const string AllCharactersPathsPath = @"..\..\..\XMLFiles\Characters\AllCharactersPaths.xml";
        public static Character ReadCharacterBySideAndId(int side, int id)
        {
            Character character = null;
            string trueId = Convert.ToString(side) + Convert.ToString(id);
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);

            if(Functions.CheckCharacterExistion(trueId) )
            {
                XmlDocument characterDocument = new XmlDocument();
                string characterDocumentPath = prefix + namePrefix + trueId + extention;
                characterDocument.Load(characterDocumentPath);
                XmlNode characterRoot = characterDocument.DocumentElement;
                string name = characterRoot.ChildNodes[0].InnerText;
                int maxHealth = Convert.ToInt32(characterRoot.ChildNodes[3].InnerText);
                int health = Convert.ToInt32(characterRoot.ChildNodes[4].InnerText);
                int maxEnergy = Convert.ToInt32(characterRoot.ChildNodes[5].InnerText);
                int energy = Convert.ToInt32(characterRoot.ChildNodes[6].InnerText);
                int maxSatiety = Convert.ToInt32(characterRoot.ChildNodes[7].InnerText);
                int satiety = Convert.ToInt32(characterRoot.ChildNodes[8].InnerText);
                int maxSanity = Convert.ToInt32(characterRoot.ChildNodes[9].InnerText);
                int sanity = Convert.ToInt32(characterRoot.ChildNodes[10].InnerText);
                int status = Convert.ToInt32(characterRoot.ChildNodes[11].InnerText);
                XmlNode attributes = characterRoot.ChildNodes[12];
                int[] attributeValues = new int[9];
                int[][] talentValues = new int[9][] {new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9] };
                int i = 0;
                int j;
                foreach(XmlNode attribute in attributes)
                {
                    attributeValues[i] = Convert.ToInt32(attribute.ChildNodes[0].InnerText);
                    j = 0;
                    foreach(XmlNode talent in attribute.ChildNodes[1].ChildNodes)
                    {
                        talentValues[i][j] = Convert.ToInt32(talent.InnerText);
                        j++;
                    }
                    i++;
                }
                character = new Character(name, side, id, attributeValues, talentValues, maxHealth, health, maxEnergy, energy, maxSatiety, satiety, maxSanity, sanity, status);

            }
            return character;
        }
        public static Character ReadCharacterBySideAndId(string trueId)
        {
            Character character = null;
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);

            if (Functions.CheckCharacterExistion(trueId))
            {
                XmlDocument characterDocument = new XmlDocument();
                string characterDocumentPath = prefix + namePrefix + trueId + extention;
                characterDocument.Load(characterDocumentPath);
                XmlNode characterRoot = characterDocument.DocumentElement;
                string name = characterRoot.ChildNodes[0].InnerText;
                int side = Convert.ToInt32(characterRoot.ChildNodes[1].InnerText);
                int id = Convert.ToInt32(characterRoot.ChildNodes[2].InnerText);
                int maxHealth = Convert.ToInt32(characterRoot.ChildNodes[3].InnerText);
                int health = Convert.ToInt32(characterRoot.ChildNodes[4].InnerText);
                int maxEnergy = Convert.ToInt32(characterRoot.ChildNodes[5].InnerText);
                int energy = Convert.ToInt32(characterRoot.ChildNodes[6].InnerText);
                int maxSatiety = Convert.ToInt32(characterRoot.ChildNodes[7].InnerText);
                int satiety = Convert.ToInt32(characterRoot.ChildNodes[8].InnerText);
                int maxSanity = Convert.ToInt32(characterRoot.ChildNodes[9].InnerText);
                int sanity = Convert.ToInt32(characterRoot.ChildNodes[10].InnerText);
                int status = Convert.ToInt32(characterRoot.ChildNodes[11].InnerText);
                XmlNode attributes = characterRoot.ChildNodes[12];
                int[] attributeValues = new int[9];
                int[][] talentValues = new int[9][] { new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9], new int[9] };
                int i = 0;
                int j;
                foreach (XmlNode attribute in attributes)
                {
                    attributeValues[i] = Convert.ToInt32(attribute.ChildNodes[0].InnerText);
                    j = 0;
                    foreach (XmlNode talent in attribute.ChildNodes[1].ChildNodes)
                    {
                        talentValues[i][j] = Convert.ToInt32(talent.InnerText);
                        j++;
                    }
                    i++;
                }
                character = new Character(name, side, id, attributeValues, talentValues, maxHealth, health, maxEnergy, energy, maxSatiety, satiety, maxSanity, sanity, status);

            }
            return character;
        }
        public static int AddNewCharacter(Character character)
        {
            string trueId = Convert.ToString(character.GetSide()) + Convert.ToString(character.GetId());
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);
            if (Functions.CheckCharacterExistion(trueId))
                return 1;
            else
            {
                string characterFileName = prefix + namePrefix + trueId + extention;
                XDocument characterFile = new XDocument();
                XElement rootElement = new XElement(namePrefix + trueId);
                rootElement.Add(Functions.CreateElement("name", character.GetName()));
                rootElement.Add(Functions.CreateElement("side", Convert.ToString(character.GetSide())));
                rootElement.Add(Functions.CreateElement("id", Convert.ToString(character.GetId())));
                rootElement.Add(Functions.CreateElement("maxHealth", Convert.ToString(character.GetMaxHealth())));
                rootElement.Add(Functions.CreateElement("health", Convert.ToString(character.GetHealth())));
                rootElement.Add(Functions.CreateElement("maxEnergy", Convert.ToString(character.GetMaxEnergy())));
                rootElement.Add(Functions.CreateElement("energy", Convert.ToString(character.GetEnergy())));
                rootElement.Add(Functions.CreateElement("maxSatiety", Convert.ToString(character.GetMaxSatiety())));
                rootElement.Add(Functions.CreateElement("satiety", Convert.ToString(character.GetSatiety())));
                rootElement.Add(Functions.CreateElement("maxSanity", Convert.ToString(character.GetMaxSanity())));
                rootElement.Add(Functions.CreateElement("sanity", Convert.ToString(character.GetSanity())));
                rootElement.Add(Functions.CreateElement("status", Convert.ToString(character.GetStatus())));
                XElement attributesElement = new XElement("attributes");
                XElement attrElement = new XElement("Error");
                int i = 0;
                int j;
                foreach (Attribute attribute in character.GetAttributes())
                {
                    attrElement.Name = ICharacter.attrnames[i];
                    attrElement.Add(Functions.CreateElement("Value", Convert.ToString(attribute.GetValue())));
                    XElement talentElement = new XElement("Talents");
                    j = 0;
                    foreach (Talent talent in attribute.GetTalents())
                    {
                        talentElement.Add(Functions.CreateElement(ICharacter.xmltalentnames[i][j], Convert.ToString(attribute.GetTalentValue(j))));
                        j++;
                    }
                    i++;
                    attrElement.Add(talentElement);
                    attributesElement.Add(new XElement(attrElement));
                    attrElement.RemoveAll();
                }
                rootElement.Add(attributesElement);
                rootElement.Add(Functions.CreateElement("weapone", Convert.ToString(character.GetWeaponeId())));
                characterFile.Add(rootElement);
                characterFile.Save(characterFileName);
                XDocument xDocument = XDocument.Load(AllCharactersPathsPath);
                xDocument.Root.Add(Functions.CreateElement(namePrefix + trueId, prefix + namePrefix + trueId + extention));
                xDocument.Save(AllCharactersPathsPath);
                return 0;
            }

        }
        public static int SaveCharacter(Character character)
        {
            string trueId = Convert.ToString(character.GetSide()) + Convert.ToString(character.GetId());
            XmlDocument xACPDoc = new XmlDocument();
            xACPDoc.Load(AllCharactersPathsPath);
            if (Functions.CheckCharacterExistion(trueId))
            {
                string characterFileName = prefix + namePrefix + trueId + extention;
                XDocument characterFile = XDocument.Load(characterFileName);
                XElement rootElement = characterFile.Root;
                rootElement.Element("name").Value = character.GetName();
                rootElement.Element("maxHealth").Value = Convert.ToString(character.GetMaxHealth()); 
                rootElement.Element("health").Value = Convert.ToString(character.GetHealth());
                rootElement.Element("maxEnergy").Value = Convert.ToString(character.GetMaxEnergy());
                rootElement.Element("energy").Value = Convert.ToString(character.GetEnergy());
                rootElement.Element("maxSatiety").Value = Convert.ToString(character.GetMaxSatiety());
                rootElement.Element("satiety").Value = Convert.ToString(character.GetSatiety());
                rootElement.Element("maxSanity").Value = Convert.ToString(character.GetMaxSanity());
                rootElement.Element("sanity").Value = Convert.ToString(character.GetSanity());
                rootElement.Element("status").Value = Convert.ToString(character.GetStatus());
                XElement allAttrsElement = rootElement.Element("attributes");
                XElement attrElement;
                for(int i = 0;i<9;i++)
                {
                    attrElement = allAttrsElement.Element(ICharacter.attrnames[i]);
                    attrElement.Element("Value").Value = Convert.ToString(character.GetAttributeValue(i));
                    for(int j = 0; j < 5;j++)
                    {
                        attrElement.Element("Talents").Element(ICharacter.xmltalentnames[i][j]).Value = Convert.ToString(character.GetTalentValue(i, j));
                    }
                }
                rootElement.Element("weapone").Value = Convert.ToString(character.GetWeaponeId());
                characterFile.Save(characterFileName);
                return 0;

            }
            else
            {
                AddNewCharacter(character);
                return 1;
            }
            }
    }
}