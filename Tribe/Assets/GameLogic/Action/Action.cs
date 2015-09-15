using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    abstract class Action
    {
        public virtual void Execute(string target) { }
    }
}
