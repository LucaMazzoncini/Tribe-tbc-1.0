using System;
using System.Collections.Generic;
using System.Text;

namespace Communication
{
    class MessageDeseralizerAndParser
    {
        public static void Read(Communicator caller, string message)
        {
            string[] splittedData = message.Split(new string[] { ":separator:" }, StringSplitOptions.None);
            MessagesEnums.Message name = (MessagesEnums.Message)Enum.Parse(typeof(MessagesEnums.Message), splittedData[0]);
            Object data = splittedData[1];
            XmppCommunicator.Utils.Log(message);
            switch (name)
            {
                case MessagesEnums.Message.DiceResult:
                    caller.OpponentsDiceResult(Int32.Parse(data.ToString()));
                    break;
                case MessagesEnums.Message.OpponentName:
                    caller.OpponentName(data.ToString());
                    break;
                case MessagesEnums.Message.OpponentInfo:
                    caller.SetOpponentInfo(data);
                    break;
                case MessagesEnums.Message.ChangeRound:
                    caller.ChangeRound();
                    break;
                case MessagesEnums.Message.UnityOpponentIsReady:
                    caller.OpponentIsReady();
                    break;
                case MessagesEnums.Message.OpponentManaChosen:
                    caller.OpponentManaChosenUpdate(data);
                    break;
                case MessagesEnums.Message.OpponentPool:
                    caller.OpponentPoolUpdate(data.ToString()); //questa stringa e' composta cosi' : "Fire 1"
                    break;
                default:
                    break;
            }
        }
        public static void pisello(string message)
        {
            XmppCommunicator.Utils.Log(message);
        }
    }
}
