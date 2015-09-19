using System;
using System.Collections;
using System.Collections.Generic;

using System.Text;


namespace XmppCommunicator
{
    static class Utils
    {
        public static string getExpectedClosingTag(string openingTag)
        {
            if (openingTag.Contains("<?xml"))
            {
                return "\">";
            }
            return "";
        }

        public static ArrayList FindRooms(string str, string FirstMarker, string LastMarker)
        {
            string[] firstSplitString = str.Split(new String[]{FirstMarker},StringSplitOptions.None);
            ArrayList list = new ArrayList(); ;

            for (int i = 1; i < firstSplitString.Length; i++)
            {
                list.Add(firstSplitString[i].Substring(0, firstSplitString[i].IndexOf('"')));
            }
            return list;
        }

        public static List<string> FindMultipleTagsValue(string str, string FirstMarker, string LastMarker)
        {
            string[] firstSplitString = str.Split(new String[]{FirstMarker},StringSplitOptions.None);
            List<string> list = new List<string>(); ;

            for (int i = 1; i < firstSplitString.Length; i++)
            {
                list.Add(firstSplitString[i].Substring(0, firstSplitString[i].IndexOf('"')));
            }
            return list;
        }

        internal static string FindRoomDescription(string strRecieved)
        {
            string target = "label=\"Description\"><value>";
            int infoIndex = strRecieved.IndexOf(target) + target.Length;
            strRecieved = strRecieved.Substring(infoIndex);
            int endIndex = strRecieved.IndexOf("</value>");
            return strRecieved.Substring(0, endIndex);

        }

        internal static string FindRoomName(string strRecieved)
        {
            string tempRoomName = strRecieved.Split(new String[]{"from=\""},StringSplitOptions.None)[1];
            return tempRoomName.Substring(0, tempRoomName.IndexOf('"')).Split('/')[0];
        }

        internal static string FindTagValue(string strRecieved, string tag)
        {
            string tempRoomName = strRecieved.Split(new String[]{tag},StringSplitOptions.None)[1];
            return tempRoomName.Substring(0, tempRoomName.IndexOf('"'));
        }

        public static string RequestRoomRooster(string roomName)
        {
            string pre = "<iq type='get' id='roomRoosterRequest' to='";
            string post = "'><query xmlns='http://jabber.org/protocol/disco#items'/></iq>";
            return pre + roomName + post;
        }

        public static string RequestRoomInformation(string roomName)
        {
            string name = roomName.Substring(0,roomName.IndexOf("@"));
            string pre = "<iq type='get' id='roomInformation"+name+"' to='";
            string post = "'><query xmlns='http://jabber.org/protocol/disco#info'/></iq>";
            return pre + roomName + post;
        }

        public static string RequestNewAccountRegistration(string nickname)
        {
            string pre = "<iq type='set' id='reg'> <query xmlns='jabber:iq:register'> <username>";
            string post = "</username> <password>asdasdasd</password> <email>bard@shakespeare.lit</email> </query> </iq>";
            return pre + nickname + post;
        }

        internal static string ExtractUserNameFromPresence(string strRecieved)
        {
            string userName;
			userName = Utils.FindMultipleTagsValue(strRecieved, "item jid=\"", "@tribetbc.eu")[0];
            return userName;
        }

        internal static List<string> FindRoomParticipants(string strRecieved)
        {
            return Utils.FindMultipleTagsValue(strRecieved, "jid=\"", "\"");
        }

        public static void Log(string s)
        {
            Console.Out.WriteLine(s);
        }

        public static string createRoomRequest(string nick)
        {
            //return "<presence from='nikolai@tribetbc.eu'to='teaparty@conference.tribetbc.eu/MioNick'/>";
            return "<presence from='"+nick+"@tribetbc.eu'to='"+nick+"s_tribe_room"+"@conference.tribetbc.eu/"+nick+"'/>";
        }

        public static string joinRoomRequest(string nick, string room)
        {
            return "<presence from='"+nick+"@tribetbc.eu'to='"+ room + "/" + nick+"'/>";
        }

        public static string sendGroupMessage(string message, string nick, string room)
        {
            return "<message type='groupchat' id='groupMessage' to='" + room + "' ><body>" + message + "</body></message>";
            //return "<message from ='" + nick + "@tribetbc.eu" + "' id='groupMessage' to='" + room + "' type='groupchat'> <body>" + message + "</body></message>";
        }

        public static string generatePong(string ping, string nick)
        {
            string server = Utils.FindMultipleTagsValue(ping,"from=\"","\"")[0];
            string id = Utils.FindMultipleTagsValue(ping,"id=\"","\"")[0];
            return "<iq from=\"" + nick + "\" to=\"" + server + "\" id=\"" + id +"\" type=\"result\"/>";
        }

        public static List<string> extractXmppMessage(string xmppMessage)
        {
            List<string> list = new List<string>();
            while (xmppMessage.Contains("<body>"))
            {
                int bodyStarts = xmppMessage.IndexOf("<body>") + 6;
                int bodyEnds = xmppMessage.IndexOf("</body>");

                //gestione stream xml parziali dal server
                if(bodyEnds < bodyStarts)
                {
                    xmppMessage = xmppMessage.Substring(bodyEnds + 7);
                    continue;
                }

                string message = xmppMessage.Substring(bodyStarts, bodyEnds - bodyStarts);
                list.Add(message);
                xmppMessage = xmppMessage.Substring(bodyEnds);
            }
            return list;
        }
    }
}
