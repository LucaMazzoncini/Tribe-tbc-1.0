using System;
using System.Collections.Generic;
using System.Collections;

using System.Text;


namespace XmppCommunicator
{
    class Room
    {
        public string name;
        public string description;
        public bool participantsLoaded = false;
        public ArrayList participants;

        public Room(string name)
        {
            this.name = name;
        }

        public void AddParticipant(string _participant)
        {
            if(participants == null) participants = new ArrayList();
            if (_participant == null) return;
            string participant = _participant.Split(new String[]{"/"},StringSplitOptions.None)[1];
            participants.Add(participant);
            //Utils.Log(participant + " ");
        }

    }
}
