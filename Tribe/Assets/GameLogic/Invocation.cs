using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GameLogic
{
    class Invocation
    {
        #region variables
        public string name { get; set; }
        public string from { get; set; }
        public int rank { get; set; }
        public int strength { get; set; }
        public int constitution { get; set; }
        public Dictionary<Enums.Mana,int> manaCost{ get; set; }
        public List<Enums.Type> type{ get; set; }
        public List<Enums.SubType> subType{ get; set; }
        public List<Enums.Role> role{ get; set; }
        public List<Enums.Properties> properties { get; set; }
        public Dictionary<string,Action> powers { get; set; }
        public Dictionary<string,Action> onAppearActions { get; set; }
        public Dictionary<string,Action> effects { get; set; }
        public string flavour { get; set; }
        private System.Xml.XmlDocument xmlDoc;
        #endregion

        public Invocation(System.Xml.XmlDocument xmlDoc)
        {
            this.xmlDoc = xmlDoc;
            name = GetDataFromXml("Name");
            from = GetDataFromXml("From");
            manaCost = LoadCostFromString(GetDataFromXml("Cost"));
            type = LoadTypeFromString(GetDataFromXml("Type"));
            subType = LoadSubTypeFromString(GetDataFromXml("SubType"));
            role = LoadRoleFromString(GetDataFromXml("Role"));
            rank = Int32.Parse(GetDataFromXml("Rank"));
            strength = Int32.Parse(GetDataFromXml("Strength"));
            constitution = Int32.Parse(GetDataFromXml("Constitution"));
            properties = LoadPropertiesFromString(GetDataFromXml("Properties"));
            powers = LoadActionsFromString(GetDataFromXml("Powers"));
            onAppearActions = LoadActionsFromString(GetDataFromXml("OnAppearAbilities"));
            flavour = GetDataFromXml("Flavour");
        }



        #region methods
        private List<Enums.Role> LoadRoleFromString(string roleString)
        {
            List<Enums.Role> list = new List<Enums.Role>();
            string[] roleArray = Utils.SplitString(roleString);
            foreach (string rString in roleArray)
            {
                switch (rString)
                {
                    case "TANK":
                        list.Add(Enums.Role.Tank);
                        break;
                    case "HEALER":
                        list.Add(Enums.Role.Healer);
                        break;
                    case "DPS":
                        list.Add(Enums.Role.Dps);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private List<Enums.SubType> LoadSubTypeFromString(string subTypeString)
        {
            List<Enums.SubType> list = new List<Enums.SubType>();
            string[] subTypeArray = Utils.SplitString(subTypeString);
            foreach (string sString in subTypeArray)
            {
                switch (sString)
                {
                    case"FLORA":
                        list.Add(Enums.SubType.Flora);
                        break;
                    case"MAGMA":
                        list.Add(Enums.SubType.Magma);
                        break;
                    case"VAPOR":
                        list.Add(Enums.SubType.Vapor);
                        break;
                    case"EARTH":
                        list.Add(Enums.SubType.Earth);
                        break;
                    case"WATER":
                        list.Add(Enums.SubType.Water);
                        break;
                    case"FIRE":
                        list.Add(Enums.SubType.Fire);
                        break;
                    case"ANCESTRAL":
                        list.Add(Enums.SubType.Ancestral);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private Dictionary<string,Action> LoadActionsFromString(string actionsString)
        {
            Dictionary<string,Action> actionsDict = new Dictionary<string,Action>();
            string[] actionsArray = Utils.SplitString(actionsString);
            foreach (string action in actionsArray)
            {
                switch (action)
                {
                    case"ARMOR":
                        break;
                    default:
                        break;
                }
            }
            return actionsDict;
        }
        /// <summary>
        /// If no node is found, the method returns ""
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private string GetDataFromXml(string nodeName)
        {
            try
            {
                return xmlDoc.GetElementsByTagName(nodeName).Item(0).InnerText.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private List<Enums.Properties> LoadPropertiesFromString(string properties)
        {
            List<Enums.Properties> list = new List<Enums.Properties>();
            string[] propertiesArray = Utils.SplitString(properties);
            foreach (string prop in propertiesArray)
            {
                switch (prop)
                {
                    case"QUICKNESS":
                        list.Add(Enums.Properties.Quickness);
                        break;
                    case"GUARDIAN":
                        list.Add(Enums.Properties.Guardian);
                        break;
                    default:
                        list.Add(Enums.Properties.None);
                        break;
                }
            }
            return list;
        }

        private List<Enums.Type> LoadTypeFromString(string typeString)
        {
            List<Enums.Type> list = new List<Enums.Type>();
            string[] typeArray = Utils.SplitString(typeString);
            foreach (string tString in typeArray)
            {
                switch (tString)
                {
                    case "SPELL":
                        list.Add(Enums.Type.Spell);
                        break;
                    case "ELEMENTAL":
                        list.Add(Enums.Type.Elemental);
                        break;
                    case "SPIRIT":
                        list.Add(Enums.Type.Spirit);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private Dictionary<Enums.Mana, int> LoadCostFromString(string manaString)
        {
            Dictionary<Enums.Mana, int> costDic = new Dictionary<Enums.Mana, int>();
            char[] manaCharArray = manaString.ToUpper().ToCharArray();
            foreach (char manaChar in manaCharArray)
            {
                switch (manaChar)
                {
                    case'E':
                        AddManaCost(costDic, Enums.Mana.Earth);
                        break;
                    case'W':
                        AddManaCost(costDic, Enums.Mana.Water);
                        break;
                    case'F':
                        AddManaCost(costDic, Enums.Mana.Fire);
                        break;
                    case'L':
                        AddManaCost(costDic, Enums.Mana.Life);
                        break;
                    case'D':
                        AddManaCost(costDic, Enums.Mana.Death);
                        break;
                    default:
                        break;
                }
            }
            return costDic;
        }

        private void AddManaCost(Dictionary<Enums.Mana, int> costDic, Enums.Mana type)
        {
            if (costDic.ContainsKey(type))
            {
                costDic[type] += 1;
            }
            else
            {
                costDic.Add(type, 1);
            }
        }
        #endregion
    }
}
