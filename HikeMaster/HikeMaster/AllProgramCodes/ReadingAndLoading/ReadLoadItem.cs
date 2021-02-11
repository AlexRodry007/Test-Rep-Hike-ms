using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace HikeMaster
{
    class ReadLoadItem 
    {
        public static Item ReadItemById(int id)
        {
            const string AllItemPathsPath = @"..\..\..\XMLFiles\Items\AllItemPaths.xml";

            string name;
            string description;
            int mass;
            int volume;
            List<UseItem> uses = new List<UseItem> { };
            List<PassiveUse> passiveUses = new List<PassiveUse> { };

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(AllItemPathsPath);
            XmlNode root = xDoc.DocumentElement;
            string path = root.ChildNodes[id].InnerText;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            root = xmlDocument.DocumentElement;
            XmlNodeList rootChilds = root.ChildNodes;
            name = rootChilds[0].InnerText;
            description = rootChilds[1].InnerText;
            id = Convert.ToInt32(rootChilds[2].InnerText);
            mass = Convert.ToInt32(rootChilds[3].InnerText);
            volume = Convert.ToInt32(rootChilds[4].InnerText);
            uses = new List<UseItem> { };
            foreach (XmlNode itemUsesNode in rootChilds[5].ChildNodes)
            {
                uses.Add(new UseItem(itemUsesNode.ChildNodes[0].InnerText, Convert.ToBoolean(Convert.ToInt32(itemUsesNode.ChildNodes[1].InnerText)), ReadLoadTile.GetCharacterConsequence(itemUsesNode.ChildNodes[2])));
            }
            foreach (XmlNode passiveItemUsesNode in rootChilds[6].ChildNodes)
            {
                passiveUses.Add(new PassiveUse(passiveItemUsesNode.ChildNodes[0].InnerText, Convert.ToInt32(passiveItemUsesNode.ChildNodes[1].InnerText)));
            }
            return new Item(name, description, id, mass, volume, passiveUses, uses);
        }
    }
}