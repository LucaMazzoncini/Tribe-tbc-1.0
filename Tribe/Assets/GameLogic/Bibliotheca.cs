using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace GameLogic
{
    class Bibliotheca
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

        public LinkedList<Invocation> getCards(List<Enums.Filter> filterParam,Mana manaParam) //questa funzione filtra le carte e ritorna una lista
        {
            return Invocations;
        }

        public Card getCardByName(string name)
        {
            // Per esempio se è una creatura
            // inizializza card = new Creatura(...parametri...)
            // Dovrebbe settare anche le microactions (le stringhe e basta)
            // return card;

            Card card = new Card(" ");
            //card.powers = ["Armor"];
            return card;
         }
    }
}
