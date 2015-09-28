using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace GameLogic
{
    public class Bibliotheca
    {
        public LinkedList<Invocation> Invocations { get; set; } //lista di tutte le carte

        public Bibliotheca(LinkedList<string> xmlInvocations)
        {
            Invocations = new LinkedList<Invocation>();
            foreach (string xmlInvocation in xmlInvocations)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlInvocation);
                Invocation invocation = new Invocation(xmlDoc);
                Invocations.AddLast(invocation);
            }
        }

        public LinkedList<Invocation> getCards(List<Enums.Filter> filterList,Mana manaParam) //questa funzione filtra le carte e ritorna una lista 
        {
            if (filterList.Count == 0) // Se non ci sono filtri, ritorna la lista di tutte le carte.
                return Invocations;

            LinkedList<Invocation> InvTempList = new LinkedList<Invocation>();//lista temporanea su cui caricare le carte filtrate.
            

            foreach (Invocation InvTemp in Invocations)
            {
                int filterCount = 0; // contatore dei controlli sui filtri.

                if (filterList.Contains(Enums.Filter.Playable)) //Se tra i filtri c'è Playable, verifica che la carta possa essere pagata.
                {
                    if (manaParam.CanPay(InvTemp.manaCost))
                        filterCount++;
                    else
                        continue; // se la carta non è Playble, il foreach passa direttamente all'iterazione successiva senza controllare gli altri filtri.
                }
                foreach (Enums.Filter filtro in filterList)// filtra le carte in base alla lista di filtri
                {                                                  
                        if (InvTemp.type.Equals(filtro) ||
                        InvTemp.role.Equals(filtro) ||
                        InvTemp.subType.Equals(filtro) ||
                        InvTemp.RANK.Equals(filtro.ToString()))
                        filterCount++;             
                }

                if (filterCount == filterList.Count)//se tutti i controlli vengono passati, aggiunge la carta.
                    InvTempList.AddLast(InvTemp);
            }
            return InvTempList;
        }

        public Card getCardByName(string name)
        {
            // Per esempio se è una creatura
            // inizializza card = new Creatura(...parametri...)
            // Dovrebbe settare anche le microactions (le stringhe e basta)
            // return card;

            //Card card = new Elemental(" ");
            //card.powers = ["Armor"];

            
            return null;
         }
    }
}
