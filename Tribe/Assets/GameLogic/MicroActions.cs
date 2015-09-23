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

        //il richiamo a questa funzione sara' MicroAction.table["nomefunzione"](param);  Dove nomefunzione e' ad esempio Armor e invece param un dizionario stringa stringa
        //che in caso deve contenere anche il target

        static MicroActions() 
        {
            MicroActions.table = new Dictionary<string, Action<Params>>();
            MicroActions.table.Add("Armor", armor);
            MicroActions.table.Add("Kill", kill);
        }

        private static void armor(Params param)
        {
            //qui dentro vanno parsati i parametri

            //il target gia' si deve avere prima di chiamare questa
            // target = param["target"]

           /* int idcreature = param["idTarget"];
            Creature a = Game.getCreature(idcreature);
            a.hp -= param[armor];*/

            //eseguito l'effetto della f

            var armorValue = Int32.Parse(param["value"]);
            armorValue += 1;
            // Send to communicator
     }

        private static void kill(Params param)
        {
            return;
        }

        private static void Damage(Params param)
        {
            return;
        }

    }   

}
