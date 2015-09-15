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
        public List<Dictionary<string,Action>> powers { get; set; }  //La lista contiene i poteri composta da piu azioni e cooldown
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
            if(GetDataFromXml("Rank")!="")
                rank = Int32.Parse(GetDataFromXml("Rank"));
            if (GetDataFromXml("Strength") != "")
                strength = Int32.Parse(GetDataFromXml("Strength"));
            if (GetDataFromXml("Constitution") != "")
                constitution = Int32.Parse(GetDataFromXml("Constitution"));
            properties = LoadPropertiesFromString(GetDataFromXml("Properties"));
            powers = LoadPowersFromString(GetDataFromXml("Powers"));
            onAppearActions = LoadActionsFromString(GetDataFromXml("Onappear"));
            effects = LoadEffectFromString(GetDataFromXml("Effect"));
            flavour = GetDataFromXml("Flavour");
        }



        #region methods
        private List<Enums.Role> LoadRoleFromString(string roleString)
        {
            List<Enums.Role> list = new List<Enums.Role>();
            string[] roleArray = roleString.ToUpper().Split(' ');
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
            string[] subTypeArray = subTypeString.ToUpper().Split(' ');
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
            if (actionsString != "")
            {
                string[] actionsArray = actionsString.Split(' ');//Utils.SplitString(actionsString," ");
                string[] nameActionArray = new string[actionsArray.Length];
                string[] valueArray = new string[actionsArray.Length];
                int i = 0;
                foreach (string action in actionsArray)
                {
                    string[] app = new string[2];  //utilizzo una stringa di appoggio per dividere es: armor.1
                    app = action.Split('.');       //divido la stringa per separare nome e valore abilita' 
                    nameActionArray[i] = app[0].ToUpper();      //inserisco il nome dell'abilita' appena parsata
                    valueArray[i] = app[1];        //inserisco il valore dell'abilita'
                    i++;
                }
                i = 0;                             //riazzero il contatore
                foreach (string action in nameActionArray)
                {
                    Action actionApp;           //variabile di appoggio dove alloco la nuova azione
                    switch (action)
                    {
                        case "ARMOR":
                            {
                                actionApp = new ArmorAction(Int32.Parse(valueArray[i]));  //Alloco l'azione passandogli il valore
                                actionsDict.Add("ARMOR", actionApp);                      //aggiungo l'oggetto alla variabile di ritorno
                            }
                            break;
                        case "DAMAGE":
                            {
                                actionApp = new DamageAction(Int32.Parse(valueArray[i]));  //Alloco l'azione passandogli il valore
                                actionsDict.Add("DAMAGE", actionApp);                      //aggiungo l'oggetto alla variabile di ritorno
                            }
                            break;
                        case "HEALYOUANDALLALLIES":
                            {
                                actionApp = new HealYouAndAllAlliesAction(Int32.Parse(valueArray[i]));  //Alloco l'azione passandogli il valore
                                actionsDict.Add("HEALYOUANDALLALLIES", actionApp);       //aggiungo l'oggetto alla variabile di ritorno
                            }
                            break;
                        default:
                            break;
                    }
                    i++;
                }
            }
            return actionsDict;
        }


        private Dictionary<string, Action> LoadEffectFromString(string actionsString)
        {
            Dictionary<string, Action> actionsDict = new Dictionary<string, Action>();
            string[] actionsArray = Utils.SplitString(actionsString);
            foreach (string action in actionsArray)
            {
                switch (action)
                {
                    case "ARMOR":
                        break;
                    default:
                        break;
                }
            }
            return actionsDict;
        }
        private List<Dictionary<string, Action>> LoadPowersFromString(string actionsString)
        {
            List<Dictionary<string, Action>> actionsDict = new List<Dictionary<string, Action>>(); 
            string[] actionsArray = actionsString.Split(' ');//Utils.SplitString(actionsString," ");
            
            
            int i = 0;

            Dictionary<string, Action> appEffect = new Dictionary<string, Action>(); //alloco un oggetto temporaneo dove tenere il potere
            int cd = 0; //creo una variabile di appoggio per mantenere il cooldown
            string[] appSplit; //creo una variabile di appoggio per splittare
            foreach (string action in actionsArray)
            {
                if( action.Contains("cooldown." ) )//controllo in action c'e' un nuovo potere
                {
                    if (appEffect.Count > 0)
                    {
                        actionsDict.Add(appEffect); //se dentro appEffect c'e' qualcosa e mi trovo dentro il tag cooldow vuol dire che ci sono piu poteri
                        actionsDict.Clear();        //devo rimuovere la lista altrimenti al secondo giro ho anche i poteri del primo
                    }
                        appSplit = action.Split('.');
                        cd = Int32.Parse(appSplit[1]);
                }else
                {
                    if( action.Contains(".") ) //nel primo caso ho escluso che la stringa sia del cooldown, adesso tratto tutti i poteri parametrici
                    {
                        appSplit = action.Split('.'); //in appSplit[0] ci andra' il nome dell'abilita' e in appSplit[1] il numero dell'effetto (es: 3 danni)
                        switch (appSplit[0])
                        {
                            case "Armor":
                                appEffect.Add("Armorx", new ArmorPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere (aggiungo la x in fondo perche' sono parametrici)
                                break;
                            case "Damage":
                                    appEffect.Add("Damagex", new DamagePower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "Heal":
                                    appEffect.Add("Healx", new HealPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "DecStr":
                                    appEffect.Add("DecStrx", new DecStrPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "SelfDamage":
                                    appEffect.Add("SelfDamagex", new SelfDamagePower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "LostRandomElement":
                                    appEffect.Add("LostRandomElementx", new LostRandomElementPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "Poison":
                                    appEffect.Add("Poisonx", new PoisonPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "AddCos":
                                    appEffect.Add("AddCosx", new AddCosPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "HealAllAllies":
                                    appEffect.Add("HealAllAlliesx", new HealAllAlliesPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;
                            case "HealYouAndAllAllies":
                                    appEffect.Add("HealYouAndAllAlliesx", new HealYouAndAllAlliesPower(Int32.Parse(appSplit[1]), cd)); //qui alloco e compongo il potere
                                break;

                            default:
                                { }
                                break;
                        }
                    }else
                    {
                        switch (action) //questo e' il caso nel cui ci siano poteri non parametrici
                        {
                            case "Incurable":
                                appEffect.Add("Armor", new ArmorPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "Immortal":
                                appEffect.Add("Immortal", new ImmortalPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "Kill":
                                appEffect.Add("Kill", new KillPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "Dispel":
                                appEffect.Add("Dispel", new DispelPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "Asleep":
                                appEffect.Add("Asleep", new AsleepPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "AddMana":
                                appEffect.Add("AddMana", new AddManaPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "Shield":
                                appEffect.Add("Shield", new ShieldPower(0, cd)); //qui alloco e compongo il potere
                                break;
                            case "GuardianBuff":
                                appEffect.Add("GuardianBuff", new GuardianBuffPower(0, cd)); //qui alloco e compongo il potere
                                break;


                            default:
                                { }
                                break;
                        }

                    }
                }
                
                i++;
            }

            actionsDict.Add(appEffect); //devo ricordarmi sempre di aggiungere l'ultimo
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
            string[] propertiesArray = properties.ToUpper().Split(' ');
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
                    case "PENETRATE":
                        list.Add(Enums.Properties.Penetrate);
                        break;
                    case "RAGE":
                        list.Add(Enums.Properties.Rage);
                        break;
                    case "THORNS.1":
                        list.Add(Enums.Properties.Thorns1);  //ATTENTO qui ci sara' un problema se si utilizza Thorns.x ma per motivi di tempo per adesso e' cosi
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
            string[] typeArray = typeString.ToUpper().Split(' ');
            foreach (string tString in typeArray)
            {
                switch (tString)
                {
                    case "RITUAL":
                        list.Add(Enums.Type.Ritual);
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
