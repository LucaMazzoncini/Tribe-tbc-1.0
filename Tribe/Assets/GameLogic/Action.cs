using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    abstract class Action
    {
        public abstract void Execute(string target);
    }
}
