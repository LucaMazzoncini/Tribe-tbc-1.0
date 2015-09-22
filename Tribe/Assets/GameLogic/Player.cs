using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Player
    {
        public string Name { get; set; }
        public int hp { get; set; }
        public int MaxHp { get; set; }
        private Mana mana { get; set; }
        
        #region utils methods
        public static int ThrowDice(int numberOfFaces)
        {
            return new Random().Next(1,numberOfFaces+1);
        }
        #endregion

        public Player(string name)
        {
            this.Name = name;
            mana = new Mana(); //alloca e genera i mana random
            
        }
    }
}
