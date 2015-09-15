using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class DamageAction : Action
    {
        int value; //quanti danni vengono fatti al target da questa spell
        public DamageAction(int n)
        {
            value = n;
        }
        public override void Execute(string target)
        {

        }
    }
}
