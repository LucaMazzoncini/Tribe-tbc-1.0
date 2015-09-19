using System;
using System.Collections.Generic;

using System.Text;


namespace XmppCommunicator
{
    class XmlGenericMessages
    {
        public static string OpenStream = "<stream:stream to='tribetbc.eu' xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams' version='1.0'>";
        public static string Starttls_1 =	"<starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls'/>";
        public static string AuthFieldRequest = "<iq type='get' to='tribetbc.eu' id='auth1'> <query xmlns='jabber:iq:auth'/> </iq>";
        public static string NewSessionRequest_2 = "<stream:stream    xmlns='jabber:client'    xmlns:stream='http://etherx.jabber.org/streams'    to='tribetbc.eu'    version='1.0'>";
        public static string NewSessionRequest_3 = "<iq to='tribetbc.eu' type='set' id='sess_1'> <session xmlns='urn:ietf:params:xml:ns:xmpp-session'/> </iq>";
        public static string AuthInfo = "<iq type='set' id='auth2'> <query xmlns='jabber:iq:auth'> <username>nikolai</username> <password>asdasdasd</password> <digest></digest> <resource>pc</resource> </query> </iq>";
        public static string OpenSessionRequest = "<iq type='set' id='session_1'><session xmlns='urn:ietf:params:xml:ns:xmpp-session'/></iq>"; 
        public static string ResourceSetup = "<iq type='set' id='bind_1'><bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'><resource>Pc-client</resource></bind></iq>"; 
        public static string PresenceNotify = "<presence xml:lang='en'><show>chat</show><priority>1</priority></presence>"; 
        public static string MessageTry = "<message to='pidgin@tribetbc.eu' from='nikolsi@tribetbc.eu'><body>Ecco il primo messaggio</body></message>";
        public static string JoinOrCreateRoom = "<presence from='nikolai@tribetbc.eu'to='teaparty@conference.tribetbc.eu/MioNick'/>";
        public static string FindRooms = "<iq type='get' id='foundRooms' to='conference.tribetbc.eu'><query xmlns='http://jabber.org/protocol/disco#items'/></iq>";
        public static string FindRoomParticipants = "<iq type='get' id='FindRoomParticipants' to='one@conference.tribetbc.eu'><query xmlns='http://jabber.org/protocol/disco#items'/></iq>";
        
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            string nameEncoded = System.Convert.ToBase64String(plainTextBytes);
            return "<auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl' mechanism='PLAIN'>" + nameEncoded + "</auth>";
        }
    }
}
