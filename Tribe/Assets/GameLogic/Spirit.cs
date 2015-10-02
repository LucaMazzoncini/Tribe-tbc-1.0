using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    class Spirit : Card
    {
        public Spirit(string name) : base(name) { }
        public int essence;
        public List<string> onAppear;
        public List<string> onDeath;

        public Spirit initFromInv(Invocation invTemp)
        {
            Card cardTemp = base.initFromInvocation(invTemp);
            return (Spirit)cardTemp;
        }
    }
}
