﻿using System;
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
        public bool hasAttackedThunderborn = false; // flag solo per Thunderborn.
        public bool hasWeakness = true; // debolezza da evocazione.

        
        

        public Elemental initFromInv(Invocation invTemp)
        {
            Card cardTemp = base.initFromInvocation(invTemp);
            return (Elemental)cardTemp;
        }
        
        public Elemental attackElemental(Elemental targetElem) //attacca la creatura passata modifica lo stato dell'attaccante e del target e ritorn il target modificato.
        {
           
            if(!this.buff.Contains(Enums.Buff.Shield)) // controllo che non abbia shield, se no mi limito a rimuovere shield.
            {
                for (int dmg = 0; dmg < targetElem.strength; dmg++) // controlla se attaccante e target hanno armor. Se ce l'hanno scalano prima quella poi gli hp. 
                    if (this.properties.Contains(Enums.Properties.Armor) && !targetElem.properties.Contains(Enums.Properties.Penetrate))
                        this.properties.Remove(Enums.Properties.Armor);
                    else
                        this.hp -= 1;
            }   
            else
            {
                this.buff.Remove(Enums.Buff.Shield);
            }
            if (!targetElem.buff.Contains(Enums.Buff.Shield)) // controllo che non abbia shield, altrimenti tolgo shield e ritorno il target.
            {
                for (int dmg = 0; dmg < this.strength; dmg++)
                {
                    if (targetElem.properties.Contains(Enums.Properties.Armor) && !this.properties.Contains(Enums.Properties.Penetrate))
                        targetElem.properties.Remove(Enums.Properties.Armor);
                    else
                        targetElem.hp -= 1;
                }
                if (this.properties.Contains(Enums.Properties.Thunderborn) && this.hasAttackedThunderborn == false)//controlla se l'attaccante ha Thunderborn e ne cambia il flag;
                    this.hasAttackedThunderborn = true;
                else
                    this.hasAttacked = true;
                return targetElem;
            }
            else
            {
                targetElem.buff.Remove(Enums.Buff.Shield);
                if (this.properties.Contains(Enums.Properties.Thunderborn) && this.hasAttackedThunderborn == false)
                    this.hasAttackedThunderborn = true;
                else
                    this.hasAttacked = true;
                return targetElem;
            }


            //this.hp -= targetElem.strength;
            //targetElem.hp -= this.strength;

            return targetElem;
        }

        public bool canAttackElem(Elemental targetElem, Player controller)
        {
            if (this.hasAttacked == false && !this.debuff.Contains(Enums.Debuff.Asleep)) // check se ha già attaccato o se è addormentato
            {
                if (this.properties.Contains(Enums.Properties.Quickness) || this.hasWeakness == false) //check se ha debolezza da evocazione o Quickness
                {
                    if (!targetElem.properties.Contains(Enums.Properties.Guardian))
                        foreach (Elemental elemTemp in controller.cardsOnBoard)
                        {
                            if (elemTemp.properties.Contains(Enums.Properties.Guardian))
                                return false;
                        }
                    return true;
                }
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
