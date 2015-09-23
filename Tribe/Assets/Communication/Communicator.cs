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
            #endregion

            #region GameEventManager events subscription
            GameEventManager.throwDice += GameEventManager_throwDice;
            GameEventManager.loadXmlForBibliotheca += GameEventManager_loadXmlForBibliotheca;
            GameEventManager.sendOpponentName += GameEventManager_SendOpponentName;
            GameEventManager.nameReceived += GameEventManager_nameReceived;
            GameEventManager.menuFiltered += GameEventManager_menuFiltered; //questa funzione viene chiamata dall'interfaccia per filtrare il menu
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
        void GameEventManager_menuFiltered(List<string> param)
        {
            List<Enums.Filter> Filter = new List<Enums.Filter>();
            foreach (string a in param)
            {
                Filter.Add(parseFilter(a));
            }
            game.MenuFiltered(Filter);
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

        void GameEventManager_nameReceived()
        {
            sendMessage(generateMessage(MessagesEnums.Message.nameReceived, ""));
        }

        private void TcpConnector_messageRecieved(MessageEventArgs messageArg)
        {
            new MessageDeseralizerAndParser().Read(this, messageArg.Message);
        }

        void game_requestXmlForBibliotheca()
        {
            GameEventManager.RequestXmlForBibliotheca();
        }

        void tcpConnector_gameStarted(GameStartedEventArgs gameStartedEventArgs)
        {
            GameEventManager.GameStarted();
            game.GameStarted(); //la sposto di qui, perche' il momento in cui sono sicuro di essere sincronizzato e' quando ricevo l'evento opponentReceveMyName
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
        public void getManaAtStart()
        {
            GameEventManager.GetManaAtStart();
        }
        public void sendMana(Mana mana)
        {
            string manaString = "E:"+mana.valueList[Enums.Mana.Earth]+
                                "F:" + mana.valueList[Enums.Mana.Fire] +
                                "W:" + mana.valueList[Enums.Mana.Water] +
                                "L:" + mana.valueList[Enums.Mana.Life] +
                                "D:" + mana.valueList[Enums.Mana.Death];
            GameEventManager.SendMana(manaString); //invio il mana del player alla gui

        }

        public void setRound(bool param)
        {
            GameEventManager.SetRound(param);
        }
        #endregion

        #region methods triggered by opponent's messages

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

        public void opponentReceveMyName()
        {
            GameEventManager.OpponentReceiveMyName();
        }

        public void SetOpponentInfo(Object data)
        {
            game.SetOpponent((Player)data);
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



    }
}
