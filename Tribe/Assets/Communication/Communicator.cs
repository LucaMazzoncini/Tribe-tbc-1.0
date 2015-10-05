using System;
using System.Collections.Generic;
using XmppCommunicator;
using GameLogic;
using GameEventManagement;
using Newtonsoft.Json;


namespace Communication
{
    public class Communicator
    {
        #region delegates
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);
        #endregion

        #region events
        public event GenericEventHandler waitingForOpponent;
        public event GenericEventHandler gameStarted;
        public event ResultEventHandler diceResult;
        #endregion
        
        private static Communicator _instance = null;

        public static void init(string name, string serverIp, int port)
        {
            _instance = new Communicator(name, serverIp, port);
        }

        public static Communicator getInstance()
        {
            return _instance; // This may return null if not initialized
        }

        TcpConnector tcpConnector;
        Game game;

        #region constructor and event subscriptions
        private Communicator(string name, string serverIp, int port)
        {
            tcpConnector = new TcpConnector(name, "asdasdasd", serverIp, port);

            #region TcpConnector events subscription
            tcpConnector.waitingForOpponent += tcpConnector_waitingForOpponent;
            tcpConnector.gameStarted += tcpConnector_gameStarted;
            tcpConnector.messageRecieved += TcpConnector_messageRecieved;
            tcpConnector.opponentDisconnected += TcpConnector_opponentDisconnected;
            #endregion

            #region GameEventManager events subscription
            GameEventManager.throwDice += GameEventManager_throwDice;
            GameEventManager.loadXmlForBibliotheca += GameEventManager_loadXmlForBibliotheca;
            GameEventManager.sendOpponentName += GameEventManager_SendOpponentName;
            GameEventManager.endRoud += GameEventManager_endRound;
            GameEventManager.playCard += GameEventManager_playCard;
            GameEventManager.canPlayCard += GameEventManager_CanPlayCard;
            GameEventManager.idTarget += GameEventManager_idTarget;
            GameEventManager.unityReady += GameEventManager_unityReady;
            GameEventManager.manaChosen += GameEventManager_manaChosen;
            GameEventManager.createPool += GameEventManager_createPool;
            GameEventManager.canCreateManaPool += GameEventManager_canCreateManaPool;
            GameEventManager.menuRequest += GameEventManager_menuRequest;
            #endregion

            tcpConnector.Connect();
        }

        public void SetGame(Game game)
        {
            this.game = game;

            #region Game events subscription
            game.sendDiceResult += game_diceResult;
            game.requestXmlForBibliotheca += game_requestXmlForBibliotheca;
            #endregion

        }

        void GameEventManager_SendOpponentName(string param)
        {
            sendMessage(generateMessage(MessagesEnums.Message.OpponentName, param));
        }
        void GameEventManager_loadXmlForBibliotheca(LinkedList<string> xmlList)
        {
            game.LoadBibliotheca(xmlList);
        }


        #endregion

        #region methods triggered by events or called by Game

        public void GameEventManager_menuRequest(List<string> filter,string mana)  //il mana deve essere in questo formato ":Fire 2:Earth 3" ecc ecc
        {
            List<Enums.Filter> tempFilter = new List<Enums.Filter>();
            Mana tempMana = new Mana();
            foreach (string temp in filter)
                tempFilter.Add((Enums.Filter)Enum.Parse(typeof(Enums.Filter), temp)); //aggiungo i tipi di filtri da string a Enums.filter

            string[] parseMana = mana.Split(':');

            
            foreach(string stringTemp in parseMana)
            {
                if(stringTemp != "") //la prima stringa sara' vuota
                {
                    string[] subDivisioString = stringTemp.Split(' ');
                    tempMana.valueList.Add((Enums.Mana)Enum.Parse(typeof(Enums.Mana), subDivisioString[0]), Int32.Parse(subDivisioString[1])); //carico i valori del mana direttamente nella lista
                }
            }
            game.MenuRequest(tempFilter,tempMana);

        }

        public void MenuFiltered(LinkedList<Invocation> List)
        {
            List<string> cards = new List<string>();
            foreach(Invocation card in List)
            {
                string temp = "";
                temp += ":Name "      + card.name;
                temp += ":Hp "        + card.constitution;
                temp += ":Pw "        + card.powers;
                temp += ":Cost "      + ManaCostToString(card.manaCost);
                temp += ":Flavor "    + card.flavour;
                temp += ":Propreties " + PropertiesToString(card.properties);

                cards.Add(temp);
            }
            GameEventManager.MenuFiltered(cards);
        }

        public void SendOpponentManaChosen(Enums.Mana param)
        {
            sendMessage(generateMessage(MessagesEnums.Message.OpponentManaChosen, param.ToString()));
        }

        public void SendOpponentName(string name)
        {
            GameEventManager.SendOpponentName(name);

        }

        public void UnityOpponentIsReady()
        { 
            sendMessage(generateMessage(MessagesEnums.Message.UnityOpponentIsReady, null));
        }

        public void Loaded() //questo evento notifica che sia io che l'opponent abbiamo caricato tutto
        {
            GameEventManager.Loaded();
        }

        void GameEventManager_throwDice()
        {
            game.ThrowDice();
        }

        public void game_diceResult(int result)
        {
            sendMessage(generateMessage(MessagesEnums.Message.DiceResult, result));
            GameEventManager.DiceResult(result);
        }

       private void TcpConnector_messageRecieved(MessageEventArgs messageArg)
        {
            MessageDeseralizerAndParser.Read(this, messageArg.Message);
        }

        private void TcpConnector_opponentDisconnected()
        {
            GameEventManager.OpponentDisconnected();
        }

        void game_requestXmlForBibliotheca()
        {
            GameEventManager.RequestXmlForBibliotheca();
        }

        void tcpConnector_gameStarted(GameStartedEventArgs gameStartedEventArgs)
        {
            GameEventManager.GameStarted();
            //game.GameStarted(); //la sposto di qui, perche' il momento in cui sono sicuro di essere sincronizzato e' quando ricevo l'evento opponentReceveMyName
            game.SetOpponentName(gameStartedEventArgs.Opponent);
            
        }

        void tcpConnector_waitingForOpponent()
        {
            GameEventManager.WaitingForOpponent();
        }

        public void sendPlayerInfo(Player param)
        {
            //lo invio solo a xmpp
            sendMessage(generateMessage(MessagesEnums.Message.OpponentInfo, param));
        }
        public void ChoseMana(Enums.ManaEvent param)
        {
            string enumString = param.ToString();
            GameEventManager.ChoseMana(enumString);
        }
        public void sendMana(Mana mana)
        {
            //il mana verra' inviato cosi' " E:1 F:1 W:1 L:1 D:1"
            string manaString = " E:"+ mana.valueList[Enums.Mana.Earth]+
                                " F:" + mana.valueList[Enums.Mana.Fire] +
                                " W:" + mana.valueList[Enums.Mana.Water] +
                                " L:" + mana.valueList[Enums.Mana.Life] +
                                " D:" + mana.valueList[Enums.Mana.Death];
            GameEventManager.SendMana(manaString); //invio il mana del player alla gui

        }

        public void setRound(bool param)
        {
            GameEventManager.SetRound(param);
        }

        public void EndRound()
        {
            sendMessage(generateMessage(MessagesEnums.Message.ChangeRound, null)); //invio param negato perche' e' il turno dell'avversario
        }

        public void GetAnyTarget()
        {
            GameEventManager.GetAnyTarget();
        }
        public void GetPlayersTarget()
        {
            GameEventManager.GetPlayersTarget();
        }
        public void GetSpiritTarget()
        {
            GameEventManager.GetSpiritsTarget();
        }
        public void GetElementalTarget()
        {
            GameEventManager.GetElementalTarget();
        }
        public void GetAllyElementalTarget()
        {
            GameEventManager.GetAllyElementalTarget();
        }
        public void GetEnemyElementalTarget()
        {
            GameEventManager.GetEnemyElementalTarget();
        }
        public void GameEventManager_createPool(string mana)
        {
            Enums.Mana manaEnum = (Enums.Mana)Enum.Parse(typeof(Enums.Mana), mana);
            game.CreateShamanPool(manaEnum);
        }

        public void GameEventManager_canCreateManaPool(string mana)
        {
            Enums.Mana manaEnum = (Enums.Mana)Enum.Parse(typeof(Enums.Mana), mana);
            game.CanCreateManaPool(manaEnum);
        }
        public void DisplayPool(Enums.Mana mana,int value) //questa funzione prende in ingresso il tipo di polla da visualizzare e quante ne sono state fatte
        {
            string enumString = mana.ToString();
            GameEventManager.DisplayPool(enumString, value.ToString());
        }

        public void sendOpponentPool(Enums.Mana mana, int value)
        {
            string param = mana.ToString() + " " + value.ToString();
            sendMessage(generateMessage(MessagesEnums.Message.OpponentPool, param));
            
        }
        public void YesYouCanCreateManaPool(Enums.Mana mana)
        {
            string enumString = mana.ToString();
            GameEventManager.YesYouCanCreateManaPool(enumString);
        }

        #endregion

        #region methods triggered by opponent's messages

        public void OpponentIsReady()
        {
            game.OpponentIsReady();
        }
        public void OpponentPoolUpdate(string param)
        {
            param = param.Replace("\"", "");
            string[] vector = param.Split(' ');
            GameEventManager.OpponentPoolUpdate(vector[0], Int32.Parse(vector[1]));

        }
        public void OpponentsDiceResult(int result)
        {
            GameEventManager.OpponentsDiceResult(result);
            game.OnOpponentDiceResult(result);
        }

        public void OpponentName(string param)
        {
            param = param.Replace("\"", "");
            GameEventManager.GetOpponentName(param);
            game.SetOpponentName(param);
        }

        public void ChangeRound()
        {
            game.StartTourn();
        }

        public void SetOpponentInfo(Object data)
        {
            game.SetOpponent((Player)data);
        }

        public void OpponentManaChosenUpdate(Object data)
        {
            string param = data.ToString().Replace("\"", "");
            GameEventManager.OpponentChoseMana(param);
        }

        public void GameEventManager_endRound()
        {
            game.EndTourn();
        }
        public void GameEventManager_playCard(string name)
        {
            game.PlayCard(name);
        }
        public void GameEventManager_CanPlayCard(string name)
        {
            GameEventManager.CanPlayCardChecked(name, game.CanPlayCard(name));
        }

        public void GameEventManager_idTarget(int id)
        {
            game.TargetEvent(id);
        }

        public void GameEventManager_unityReady()
        {
            game.UnityReady();
        }
        public void GameEventManager_manaChosen(string mana,string reason)
        {

            Enums.Mana manaEnum = (Enums.Mana)Enum.Parse(typeof(Enums.Mana), mana); 
            Enums.ManaEvent manaEventEnum = (Enums.ManaEvent)Enum.Parse(typeof(Enums.ManaEvent), reason);  
            game.manaChoosen(manaEnum,manaEventEnum);
        }

        #endregion

        private void sendMessage(string message)
        {
            tcpConnector.SendMessage(message);
        }

        private string generateMessage(MessagesEnums.Message name, Object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return name + ":separator:" + json; 
        }

        private Enums.Filter parseFilter(string param)
        {
            // Water, Earth, Fire, Magma, Vapor, Flora, Ancestral, Ritual, Elemental, Spirit, Tank, Healer, Dps, Playable,Rank1,Rank2,Rank3
            Enums.Filter ret = Enums.Filter.None;
            switch (param)
            {
                case "WATER":
                    ret = Enums.Filter.Water;
                    break;
                case "EARTH":
                    ret = Enums.Filter.Earth;
                    break;
                case "FIRE":
                    ret = Enums.Filter.Fire;
                    break;
                case "MAGMA":
                    ret = Enums.Filter.Magma;
                    break;
                case "VAPOR":
                    ret = Enums.Filter.Vapor;
                    break;
                case "FLORA":
                    ret = Enums.Filter.Flora;
                    break;
                case "ANCESTRAL":
                    ret = Enums.Filter.Ancestral;
                    break;
                case "RITUAL":
                    ret = Enums.Filter.Ritual;
                    break;
                case "ELEMENTAL":
                    ret = Enums.Filter.Elemental;
                    break;
                case "SPIRIT":
                    ret = Enums.Filter.Spirit;
                    break;
                case "TANK":
                    ret = Enums.Filter.Tank;
                    break;
                case "HEALER":
                    ret = Enums.Filter.Healer;
                    break;
                case "DPS":
                    ret = Enums.Filter.Dps;
                    break;
                case "PLAYABLE":
                    ret = Enums.Filter.Playable;
                    break;
                case "RANK1":
                    ret = Enums.Filter.Rank1;
                    break;
                case "RANK2":
                    ret = Enums.Filter.Rank2;
                    break;
                case "RANK3":
                    ret = Enums.Filter.Rank3;
                    break;
                default:
                    break;
            }    


            
            return ret;
        }


        //utlis
        private string ManaCostToString(Dictionary<Enums.Mana,int> manaCost)
        {
            string ret = "";
            foreach(KeyValuePair<Enums.Mana,int> iterator in manaCost)
                ret += iterator.Key.ToString() + " " + iterator.Value.ToString() + ":"; 
            return ret;
        }

        private string PropertiesToString(List<Enums.Properties> properties)
        {
            string ret = "";
            foreach (Enums.Properties iterator in properties)
                ret += " " + iterator.ToString();
            return ret;
        }
    }
}
