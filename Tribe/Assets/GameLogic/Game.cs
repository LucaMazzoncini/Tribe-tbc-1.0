using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Game
    {
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);

        public event ResultEventHandler sendDiceResult;
        public event GenericEventHandler requestXmlForBibliotheca;

        private Player shaman;
        private Player opponent;
        private int diceResult;
        private bool myRound = false;
        Communication.Communicator comm;

        Bibliotheca bibliotheca;

        public Game(string name)
        {
            shaman = new Player(name);
            comm = Communication.Communicator.getInstance();
        }

        public bool isMyRound()
        {
            return myRound;
        }

        public void SetOpponent(string name)
        {
            opponent = new Player(name);
        }

        public string GetOppenentName()
        {
            return opponent.Name;
        }

        public string GetShamanName()
        {
            return shaman.Name;
        }

        public void SetOpponentName(string name)
        {
            opponent.Name = name;
        }

        public void ThrowDice()
        {
            ThrowDice(Player.ThrowDice(12));
        }

        public void ThrowDice(int diceValue)
        {
            diceResult = diceValue;
            if(sendDiceResult != null)
                sendDiceResult(diceResult);
        }

        public void LoadBibliotheca(LinkedList<string> xmlInvocations)
        {
            bibliotheca = new Bibliotheca(xmlInvocations);
        }

        public void GameStarted()
        {

            //da controllare....
            ThrowDice(); //lancio il dado per vedere chi inizia
            comm.game_diceResult(diceResult);
             // Qui cancella. Io Esco

            //requestXmlForBibliotheca();
        }

        public void OnOpponentDiceResult(int opponentDiceResult) //in questa funzione viene stabilito di chi e' il turno
        {
                myRound = false;
                if (diceResult > opponentDiceResult)
                {
                    myRound = true;
                }
                else
                {
                    ThrowDice();
                }
        }
    }
}
