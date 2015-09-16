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
    }
}
