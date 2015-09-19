using System;
using System.Collections.Generic;
using System.Collections;

using System.Text;


namespace XmppCommunicator
{
    class Rooms
    {
        public Dictionary<string,Room> RoomsDictionary;
        public bool infoRequested = false;

        public Rooms(string serverRoomsListing)
        {
            ArrayList roomNames = Utils.FindRooms(serverRoomsListing, "<item jid=\"", "\"");
            RoomsDictionary = new Dictionary<string,Room>();
            foreach (string roomName in roomNames)
            {
                RoomsDictionary.Add(roomName, new Room(roomName));
            }
        }

        internal void LoadRoomInformation(string strRecieved)
        {
            string roomName = Utils.FindRoomName(strRecieved);
            string roomDescription = Utils.FindRoomDescription(strRecieved);
            RoomsDictionary[roomName].description = roomDescription;
        }

        internal void LoadParticipants(string strRecieved)
        {
            string roomName = Utils.FindRoomName(strRecieved);
            List<string> roomParticipants = Utils.FindRoomParticipants(strRecieved);
            if (roomParticipants.Count == 0)
            {
                RoomsDictionary[roomName].AddParticipant(null);
            }
            //Utils.Log("Nella stanza " + roomName + " ci sono: ");
            foreach(string participant in roomParticipants)
            {
                RoomsDictionary[roomName].AddParticipant(participant);
            }
                RoomsDictionary[roomName].participantsLoaded = true;
        }
    }
}
