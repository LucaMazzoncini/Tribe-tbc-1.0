using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class HealYouAndAllAlliesAction : Action
    {
        int value; //valore che indica quanto cura questa azione
        public HealYouAndAllAlliesAction(int n)
        {
            value = n;
        }
        public override void Execute(string target)
        {

        }
    }
}
