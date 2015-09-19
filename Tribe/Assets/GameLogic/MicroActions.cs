using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    using Params = Dictionary<string, string>;

    public static class MicroActions
    {
        public static Dictionary<string, Action<Params>> table;

        /*public MicroActions()
        {
            
        }*/
        static MicroActions() // La puttana dei costruttori static! 
        {
            MicroActions.table = new Dictionary<string, Action<Params>>();
            MicroActions.table.Add("Armor", armor);
            MicroActions.table.Add("Kill", kill);
        }

        private static void armor(Params param)
        {
            //qui dentro vanno parsati i parametri
            //preso l'eventuale target
            //eseguito l'effetto della f

            var armorValue = Int32.Parse(param["value"]);
            armorValue += 1;
            // Send to communicator
     }

        private static void kill(Params param)
        {
            return;
        }
    }   
}
