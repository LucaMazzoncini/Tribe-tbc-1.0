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

    }
}
