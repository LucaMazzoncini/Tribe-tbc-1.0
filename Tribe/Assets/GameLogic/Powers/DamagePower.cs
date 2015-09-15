using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class DamagePower : ArmorAction
    {
        private int cooldown = 0; //il cooldown del potere del danno
        public DamagePower(int n,int cd) : base(n)
        {
            cooldown = cd;
            value = n;
        }
        public override void Execute(string target)
        {
            throw new NotImplementedException();
        }

    }
}
