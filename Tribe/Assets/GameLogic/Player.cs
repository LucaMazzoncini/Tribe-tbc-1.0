using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class Player
    {
        public string Name { get; set; }
        public int hp { get; set; }
        public int MaxHp { get; set; }
        #region utils methods
        public int ThrowDice(int numberOfFaces)
        {
            return new Random().Next(1,numberOfFaces+1);
        }
        #endregion

        public Player(string name)
        {
            this.Name = name;
        }
    }
}
