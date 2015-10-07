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

        //Vanno aggiunti tutti i case delle microazioni
        public static List<Enums.Target> getTargetsByMicroaction(string microaction)
        {
            List<Enums.Target> targetsList = new List<Enums.Target>();

            switch(microaction)
              {
                case "DAMAGE":
                    targetsList.Add(Enums.Target.Player);
                    targetsList.Add(Enums.Target.Elemental);
                break;
                case "DAMAGEELEMENTAL":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "DAMAGEPLAYER":
                    targetsList.Add(Enums.Target.Player);
                    break;
                case "DISPEL":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "ADDCOS":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "KILLALLY":
                    targetsList.Add(Enums.Target.Ally);
                    break;
                case "SELFDAMAGE":
                    targetsList.Add(Enums.Target.Shaman);
                    break;
                case "DECSTR":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "INCURABLE":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "SHIELD":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "POISON":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "ARMOR":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "HEAL":
                    targetsList.Add(Enums.Target.Player);
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "HEALELEMENTAL":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                case "HEALYOUANDALLALLIES":
                    targetsList.Add(Enums.Target.Shaman);
                    targetsList.Add(Enums.Target.Ally);
                    break;
                case "ADDMANA":
                    targetsList.Add(Enums.Target.Mana);
                    break;
                case "LOSTRANDOMELEMENT":
                    targetsList.Add(Enums.Target.Mana);
                    break;
                case "ASLEEP":
                    targetsList.Add(Enums.Target.Elemental);
                    break;
                default:
                    break;
              }                      
            return targetsList;

        }

        public static List<Enums.Target> getTargets(string actions)
        {
            List<Enums.Target> targetsList = new List<Enums.Target>();

            //creo un vettore microActions con tutte le microazioni
            string[] microactions = actions.ToUpper().Split(' ');

            //filtro le microazioni togliendogli i parametri dopo il punto
            for( int i = 0; i < microactions.Length;i++)
            {
                string[] stringTemp = microactions[i].Split('.');
                microactions[i] = stringTemp[0];
                targetsList.AddRange(getTargetsByMicroaction(microactions[i]));
            }

            //tolgo le occorrenze inutili dalla lista
            targetsList = targetsList.Distinct<Enums.Target>().ToList<Enums.Target>();

            return targetsList;
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
