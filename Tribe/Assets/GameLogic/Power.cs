using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Power
        {
            public int cooldown;
            public List<string> microActions;
            public Power()
            {
                cooldown = 0;
                microActions = new List<string>();
            }
        }
}
