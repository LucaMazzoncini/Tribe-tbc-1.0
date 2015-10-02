using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Elemental : Card
    {
        public Elemental(string param) : base(param){ }

        public int strength;
        public int constitution;
        public int hp;
        public int rank;
        public string from;
        public List<Enums.Role> role;
        public List<Enums.Properties> properties;
        public List<string> onAppear;
        public List<string> onDeath;
        public List<Enums.Debuff> debuff { get; set; } //qui ci sarà la lista dei debuff che andranno parsati
        public List<Enums.Buff> buff { get; set; }   //qui ci sarà la lista dei buff che andranno parsati7
        public bool hasAttacked = false;

        

        public Elemental initFromInv(Invocation invTemp)
        {
            Card cardTemp = base.initFromInvocation(invTemp);
            return (Elemental)cardTemp;
        }
        
        public Elemental attackCreature(Elemental targetElem) //attacca la creatura passata ne modifica lo stato e la ritorna modificata.
        {
            return targetElem;
        }

        public bool canAttackElem(Elemental targetElem, Player controller)
        {
            if (this.hasAttacked == false && !this.debuff.Contains(Enums.Debuff.Asleep))
            {
                if (!targetElem.properties.Contains(Enums.Properties.Guardian))
                    foreach (Elemental elemTemp in controller.cardsOnBoard)
                    {
                        if (elemTemp.properties.Contains(Enums.Properties.Guardian))
                            return false;
                    }
                return true;
            }
            
            return false;
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
        public bool canUsePower(Elemental elemTemp)
        {
            return true;
        }
        public bool canUsePower(Player param)
        {
            return true;
        }
        
    }
 
}
