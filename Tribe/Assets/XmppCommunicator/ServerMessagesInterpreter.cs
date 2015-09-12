using System;
using System.Collections.Generic;

using System.Text;


namespace XmppCommunicator
{
    class ServerMessagesInterpreter
    {
        public static string Interpret(string message)
        {
            if (message.Contains("message type=\"groupchat\"")) return "chat";

            if (message.Contains("id=\"roomRequest\""))
            {
                return "roomRequest";
            }
            if (message.Contains("id=\"foundRooms\""))
            {
                return "foundRooms";
            }
            if (message.Contains("id=\"roomInformation"))
            {
                return "roomInformation";
            }
            if (message.Contains("id=\"roomRoosterRequest"))
            {
                return "roomRoosterRequest";
            }
            if (message.Contains("/presence>"))
            {
                return "presence";
            }
            if(message.Contains(":ping'/>"))
            {
                return "ping";
            }


            return message;
        }
    }
}
