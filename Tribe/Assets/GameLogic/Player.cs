using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Target
    {
        public Enums.Target target;
        public int id = 0;
        public string name = "";
    }
    public class Player
    {
        #region variables
        public const int MAXHP = 6;
        public string Name { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public Mana mana { get; set; }
        private int id;
        public Target target;
        public List<Card> cardsOnBoard = new List<Card>();  //nella 4 carta ci sta' lo spirito
        public Dictionary<string,int> castCounter = new Dictionary<string, int>(); //lista che serve per verificare il castLimit
        
        #endregion
        #region utils methods
        public static int ThrowDice(int numberOfFaces)
        {
            return new Random().Next(1,numberOfFaces+1);
        }
        #endregion
       
        
        public Player(string name, int idTemp)
        {
            this.Name = name;
            this.hp = MAXHP;
            this.id = idTemp;
            mana = new Mana(); //alloca e genera i mana random 
            target = null;              
            //metti tutta la lista cards a null
        }
        public Player() { }

        public bool PlayCard(Card cardTemp)
        {
            if(CanPlayCard(cardTemp))
            {
                // Get powers
                // Get params from card -> params
                // get (if necesary) target
                // TargetUpdate() -> card.play(param)
                // else
                // card.play()  
                castCounter[cardTemp.name] -= 1; // scala dal CastCounter;
                return true;
            }
            return false;
        }

        public bool CanPlayCard(Card cardTemp)
        {
            bool canPlay = true;
            if (this.mana.CanPay(cardTemp.manaCost) && cardsOnBoard != null) //controlla che si possa pagare
                if (cardTemp.castLimit > 0) // check sul CastCounter. Se hai già raggiunto il castLimit, ritorna false.
                {
                    foreach (KeyValuePair<string, int> ktemp in castCounter)
                        if (ktemp.Key == cardTemp.name)
                            if (ktemp.Value == 0)  // ogni volta che si gioca una carta, si decrementa questo valore. Se è a 0 è raggiunto il castlimit.
                                canPlay = false;
                }
                switch (cardTemp.type)
                {
                    case Enums.Type.Elemental:
                        if (cardsOnBoard.Count < 4) // controlla che il board non sia pieno
                        {
                            Elemental elemCard = (Elemental)cardTemp;
                            foreach (Elemental elemTemp in cardsOnBoard)
                                if (elemCard.from != "" && elemCard.from == elemTemp.name)
                                    canPlay = false;
                        }
                        break;
                    case Enums.Type.Spirit:
                        if (cardsOnBoard.Count < 4) // controlla che il board non sia pieno
                            foreach (Card Ctemp in cardsOnBoard)
                                if (Ctemp.type == Enums.Type.Spirit)
                                    canPlay = false;
                        break;
                    case Enums.Type.Ritual:
                        break;
                }
            return canPlay;  
            // mancano i check sui Target Validi per i Rituals. Da aggiungere dopo aver fatto la parte delle Microactions.              
        }
                           
        public void TargetUpdated()
        {
            //qui si deve sbloccare la microazione
            // target -> param 
            // Card.power -> indice per locare la function nelle microactions
            // Microactions.table["power"](target come  param)     
        }

        public void InitCastCounter(Bibliotheca invList) //inizializza il castCounter
        {
            foreach (Invocation invTemp in invList.Invocations)
                if (invTemp.castLimit > 0)
                    castCounter.Add(invTemp.name, invTemp.castLimit);
        }
    }

        
}

