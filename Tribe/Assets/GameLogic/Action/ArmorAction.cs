using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class ArmorAction : Action
    {
        public int value; //quanta armatura viene data al target della spell
        public ArmorAction(int n)
        {
            value = n;
        }
        public override void Execute(string target)
        {

        }
    }
}
