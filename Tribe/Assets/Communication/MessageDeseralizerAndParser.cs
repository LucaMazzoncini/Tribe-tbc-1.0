using System;
using System.Collections.Generic;
using System.Text;

namespace Communication
{
    class MessageDeseralizerAndParser
    {
        public void Read(Communicator caller, string message)
        {
            string[] splittedData = message.Split(new string[] { ":separator:" }, StringSplitOptions.None);
            MessagesEnums.Message name = (MessagesEnums.Message)Enum.Parse(typeof(MessagesEnums.Message), splittedData[0]);
            Object data = splittedData[1];
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
                default:
                    break;
            }
        }
    }
}
