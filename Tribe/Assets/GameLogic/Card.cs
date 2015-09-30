using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class Card
    {
        //aggiungere il cast limit
        public string name;
        public int id; //identificatore univoco che viene valorizzato quando entra in gioco
        public List<Power> powers;
        public Dictionary<Enums.Mana, int> manaCost;
        public Enums.Type type;
        public Enums.SubType subtype;
        public Enums.Target target; //se non puo' essere targettata va' messo a null

        public Card(){}

        public Card(string name)
        {
            this.name = name;
        }
        public bool canPlay()
        {
            return true;
        }
        public void play() 
        {
            //paga il costo della carta
        }
        public Card InitFromInvocation(Invocation InvTemp)
        {
            this.name = InvTemp.name;
            this.manaCost = InvTemp.manaCost;
            this.type = InvTemp.type[0];
            this.subtype = InvTemp.subType[0];
            this.powers = InvTemp.powers;
            if (this.type == Enums.Type.Elemental)
            {
                Elemental ElemTemp = new Elemental(this.name);
                ElemTemp = (Elemental)this;
                ElemTemp.strength = InvTemp.strength;
                ElemTemp.constitution = InvTemp.constitution;
                ElemTemp.hp = ElemTemp.constitution;
                ElemTemp.rank = InvTemp.rank;
                if (ElemTemp.rank > 0)
                    ElemTemp.from = InvTemp.from;
                ElemTemp.role = InvTemp.role;
                ElemTemp.properties = InvTemp.properties;
                ElemTemp.onAppear = InvTemp.onAppear;
                ElemTemp.onDeath = InvTemp.onDeath;
                return ElemTemp;
            }
            if (this.type == Enums.Type.Spirit)
            {
                Spirit SpiritTemp = new Spirit(this.name);
                SpiritTemp = (Spirit)this;
                SpiritTemp.essence = InvTemp.essence;
                SpiritTemp.onAppear = InvTemp.onAppear;
                SpiritTemp.onDeath = InvTemp.onDeath;
                return SpiritTemp;
            }
            return this;
        }

        }

        public void processMicroaction(List<string> paramList) //questa funzione processa e prepara le microazioni
        {

            //deve parsare la stringa 
            foreach (string microaction in paramList) //deve ciclare 
            {
                MicroActions.table[microaction](null);
            }
        }
    }
}
