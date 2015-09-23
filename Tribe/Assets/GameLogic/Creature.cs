using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{/*
    class Creature
    {
        #region variables
        public string name { get; set; }
        public int Hp { get; set; }
        public int maxHp { get; set; }
        public int strength { get; set; }
        public List<Enums.Type> type { get; set; }
        public List<Enums.SubType> subType { get; set; }
        public List<Enums.Role> role { get; set; }
        public List<Enums.Properties> properties { get; set; }
        public List<Enums.Mana> cost { get; set; }
        public List<string> buff { get; set; }   //qui ci sarà la lista dei buff che andranno parsati
        public List<string> debuff { get; set; } //qui ci sarà la lista dei debuff che andranno parsati
        public List<Power> power { get; set; } //lista degli eventuali poteri della creatura
        public List<string> creatureStatus { get; set; } //In questa lista sono contenuti gli stati della cratura,
        //ho pensato a questa soluzione per evitare che esplodesse il numero delle variabili ovviamente poi ogni stato andrà parsato
        //pero' facilità l'inserimento o la modifica di proprietà abilità o addirittura meccaniche future
        //Ad esempio se definiamo ATTACHED vuol dire che la creatura ha già attaccato, baserà utilizzare creatureStatus.Contains("ATTACHED"); per sapere
        //se la creatura ha già attaccato

        #endregion
        #region method
        public Creature(string paramName,
                        int paramHp,
                        int paramStrenght,
                        List<Enums.Type> paramType,
                        List<Enums.SubType> paramSubtype,
                        List<Enums.Role> paramRole,
                        List<Enums.Properties> paramProperties,
                        List<Enums.Mana> paramCost,
                        List<string> paramBuff,
                        List<string> paramDebuff,
                        List<Power> paramPower
                        )
        { }
        public Creature attackCreature(Creature param) //attacca la creatura passata ne modifica lo stato e la ritorna modificata.
        {
            return param;
        }

        public bool canAttackCreature(Creature param)
        {
            return true;
        }

        public Player attackPlayer(Player param) //Attacco l'opponent e ritorno le eventuali modifiche.
        {
            return param;
        }

        public bool canAttackOpponent(Player param) //controllo se posso attaccare l'opponent oppure possiede una creatura con protezione ecc ecc...
        {
            return true;
        }

        public bool powerIsInCd(int n) //controlla se il potere in posizione n della lista powersè in cd oppure no. Per sapere se è in cd si controlla in creatureStatus
        {
            return true;
        }
        public bool canUsePower() //da ricontrollare...
        {
            return true;
        }
        public bool canUsePower(Creature param)
        {
            return true;
        }
        public bool canUsePower(Player param)
        {
            return true;
        }
        #endregion
    }
    */
}
