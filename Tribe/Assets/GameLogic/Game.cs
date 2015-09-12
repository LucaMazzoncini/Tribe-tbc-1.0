using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Game
    {
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);

        public event ResultEventHandler diceResult;
        public event GenericEventHandler requestXmlForBibliotheca;

        Player shaman;
        Player opponent;
        Bibliotheca bibliotheca;

        public Game(string name)
        {
            shaman = new Player(name);
        }

        public void ThrowDice()
        {
            diceResult(shaman.ThrowDice(12));
        }

        public void LoadBibliotheca(LinkedList<string> xmlInvocations)
        {
            bibliotheca = new Bibliotheca(xmlInvocations);
        }

        public void GameStarted()
        {
            //requestXmlForBibliotheca();
        }
    }
}
