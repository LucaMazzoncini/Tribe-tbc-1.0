using System;
using System.Collections.Generic;
using System.Text;

namespace GameEventManagement
{
    public static class GameEventManager
    {
        #region handlers
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);
        public delegate void GetOpponentNameEventHandler(string name);
        public delegate void SendOpponentNameEventHandler(string name);
        public delegate void LoadXmlForBibliothecaEventHandler(LinkedList<string> xmlList);
        #endregion

        #region events for interface
        public static event GenericEventHandler waitingForOpponent;
        public static event GenericEventHandler gameStarted;
        public static event GenericEventHandler nameReceived;
        public static event ResultEventHandler diceResult;
        public static event ResultEventHandler opponentsDiceResult;
        public static event GenericEventHandler requestXmlForBibliotheca;
        public static event LoadXmlForBibliothecaEventHandler loadXmlForBibliotheca;
        public static event GetOpponentNameEventHandler getOpponentName;
        public static event SendOpponentNameEventHandler sendOpponentName;
        #endregion

        #region events for communicator
        public static event GenericEventHandler throwDice;
        public static event GenericEventHandler opponentReceveMyName;
        #endregion

        #region methods called from communicator
        public static void DiceResult(int result)
        {
            diceResult(result);
        }

        public static void OpponentsDiceResult(int result)
        {
            opponentsDiceResult(result);
        }

        public static void GameStarted()
        {
            gameStarted();
        }

        public static void WaitingForOpponent()
        {
            waitingForOpponent();
        }

        public static void RequestXmlForBibliotheca()
        {
            requestXmlForBibliotheca();
        }

        public static void OpponentReceiveMyName()
        {
            opponentReceveMyName();
        }

        #endregion

        #region methods called from interface
        public static void ThrowDice()
        {
            throwDice();
        }
        public static void LoadXmlForBibliotheca(LinkedList<string> xmlList)
        {
            loadXmlForBibliotheca(xmlList);
        }
        public static void GetOpponentName(string name)
        {
            getOpponentName(name);
        }
        public static void SendOpponentName(string name)
        {
            sendOpponentName(name);
        }

        public static void NameReceived()
        {
            nameReceived();
        }
        #endregion
    }
}
