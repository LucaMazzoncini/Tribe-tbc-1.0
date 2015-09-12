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

        TcpConnector tcpConnector;
        Game game;

        #region constructor and event subscriptions
        public Communicator(string name, string serverIp, int port)
        {
            game = new Game(name);
            tcpConnector = new TcpConnector(name, "asdasdasd", serverIp, port);
            
            #region TcpConnector events subscription
            tcpConnector.waitingForOpponent += tcpConnector_waitingForOpponent;
            tcpConnector.gameStarted += tcpConnector_gameStarted;
            tcpConnector.messageRecieved += TcpConnector_messageRecieved;
            #endregion

            #region Game events subscription
            game.diceResult += game_diceResult;
            game.requestXmlForBibliotheca += game_requestXmlForBibliotheca;
            #endregion

            #region GameEventManager events subscription
            GameEventManager.throwDice += GameEventManager_throwDice;
            GameEventManager.loadXmlForBibliotheca += GameEventManager_loadXmlForBibliotheca;
            #endregion

            tcpConnector.Connect();
        }


        void GameEventManager_loadXmlForBibliotheca(LinkedList<string> xmlList)
        {
            game.LoadBibliotheca(xmlList);
        }
        #endregion

        #region methods triggered by events
        void GameEventManager_throwDice()
        {
            game.ThrowDice();
        }

        void game_diceResult(int result)
        {
            sendMessage(generateMessage(MessagesEnums.Message.DiceResult, result));
            GameEventManager.DiceResult(result);
        }

        private void TcpConnector_messageRecieved(MessageEventArgs messageArg)
        {
            new MessageDeseralizerAndParser().Read(this,messageArg.Message);
        }

        void game_requestXmlForBibliotheca()
        {
            GameEventManager.RequestXmlForBibliotheca();
        }

        void tcpConnector_gameStarted(GameStartedEventArgs gameStartedEventArgs)
        {
            GameEventManager.GameStarted();
            game.GameStarted();
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
