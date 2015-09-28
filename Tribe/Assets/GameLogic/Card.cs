using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Card
    {
        //aggiungere il cast limit
        public string name;
        public int id; //identificatore univoco che viene valorizzato quando entra in gioco
        public List<string> powers;
        public Mana cost;
        public Enums.Type type;
        public Enums.Target target; //se non puo' essere targettata va' messo a null

        public Card(){}

        public Card(string name)
        {
            this.name = name;
        }
        public bool canPlay()
        {
            return true;
        }
        public void play() 
        {
            //paga il costo della carta
        }

        public void processMicroaction(List<string> paramList) //questa funzione processa e prepara le microazioni
        {

            //deve parsare la stringa 
            foreach (string microaction in paramList) //deve ciclare 
            {
                MicroActions.table[microaction](null);
            }
        }
    }
}
