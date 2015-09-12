using System;
using System.Collections.Generic;

using System.Text;


namespace XmppCommunicator
{
    public class GameStartedEventArgs : EventArgs
    {
        public string Opponent { get; set; }
    }
}
