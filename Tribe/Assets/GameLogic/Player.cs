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
        public Target target;
        public Card[] cars = new Card[4];  //nella 4 carta ci sta' lo spirito
        
        #endregion
        #region utils methods
        public static int ThrowDice(int numberOfFaces)
        {
            return new Random().Next(1,numberOfFaces+1);
        }
        #endregion

        public Player(string name)
        {
            this.Name = name;
            this.hp = MAXHP;
            mana = new Mana(); //alloca e genera i mana random 
            target = null;
            //metti tutta la lista cards a null
        }

        public bool PlayCard(string nameCard)
        {
            if(CanPlayCard(nameCard))
            {
                // Get powers
                // Get params from card -> params
                // get (if necesary) target
                // TargetUpdate() -> card.play(param)
                // else
                // card.play()   
                return true;
            }
            return false;
        }

        public bool CanPlayCard(string nameCard)
        {

            return true;
        }

        public void TargetUpdated()
        {
            //qui si deve sbloccare la microazione
            // target -> param 
            // Card.power -> indice per locare la function nelle microactions
            // Microactions.table["power"](target come  param)     
        }


    }
}
