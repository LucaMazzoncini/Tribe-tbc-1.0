using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace XmppCommunicator
{
    class XmppMessageManager
    {
        TcpConnector tcpConnector;
        private LinkedList<KeyValuePair<string, string>> messagesQueue;
        private Rooms rooms;
        private string joinedRoomName;
        private string nickname;
        private string password;
        private string partner;
        private bool joiningRoom;
        private bool creatingRoom;
        private bool waitingForOpponent;
        private string roomPartner;
        private string loginSasl;
        private string lastMessageRecievedFromServer;
        private string lastMessageTryingToSend;
        private int uniqueMessageId;
        private Timer messagesTimer;

        public XmppMessageManager(TcpConnector tc, string nickname, string password, string partner)
        {
            this.nickname = nickname;
            this.password = password;
            this.partner = partner;
            this.tcpConnector = tc;
            this.uniqueMessageId = 0;
            messagesQueue = new LinkedList<KeyValuePair<string,string>>();
            loginSasl = XmlGenericMessages.Base64Encode("\0"+nickname+"\0"+password);
            messagesQueue.AddLast(new KeyValuePair<string, string>(XmlGenericMessages.OpenStream, "features>"));
            messagesQueue.AddLast(new KeyValuePair<string, string>(loginSasl, "\"/>"));
            messagesQueue.AddLast(new KeyValuePair<string, string>(XmlGenericMessages.ResourceSetup,"</iq>"));
            messagesQueue.AddLast(new KeyValuePair<string, string>(XmlGenericMessages.OpenSessionRequest,">"));
            messagesQueue.AddLast(new KeyValuePair<string, string>(XmlGenericMessages.PresenceNotify,"/presence>"));
            messagesQueue.AddLast(new KeyValuePair<string, string>(XmlGenericMessages.FindRooms,"/iq>"));
            tcpConnector.sendServerMessage(messagesQueue.First.Value);
            messagesQueue.RemoveFirst();
        }

        private void initTimer()
        {
            if (messagesTimer == null || !messagesTimer.Enabled)
            {
                messagesTimer = new Timer();
                messagesTimer.Elapsed += messagesTimer_Elapsed;
                messagesTimer.Interval = 100;
                messagesTimer.AutoReset = true;
                messagesTimer.Start();
            }
        }

        void messagesTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            messagesTimer.Interval = 3000;
            lastMessageTryingToSend = messagesQueue.First.Value.Key;
            string message = messagesQueue.First.Value.Key;
            tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.sendGroupMessage(message,nickname,joinedRoomName), "/message>"));
        }

        internal void ReadMessage(string strRecieved)
        {
            #region initial loading sequence
            try
            {
                if (messagesQueue.Count > 0)
                {
                    tcpConnector.sendServerMessage(messagesQueue.First.Value);
                    //la sequenza di login attualmente la fa senza lo spam del timer, ma alla vecchia maniera, un messaggio alla volta. Per la chat invece spamma
                    if (ServerMessagesInterpreter.Interpret(strRecieved) != "chat")
                    {
                        messagesQueue.RemoveFirst();
                    }
                }
            }
            catch (Exception e)
            {

            }
            #endregion
            #region response switch
            switch (ServerMessagesInterpreter.Interpret(strRecieved))
            {
                case "foundRooms":
                    rooms = new Rooms(strRecieved);
                    requestRoomsInformation();
                    break;
                case "roomInformation":
                    rooms.LoadRoomInformation(strRecieved);
                    requestRoomsInformation();
                    break;
                case "roomRoosterRequest":
                    rooms.LoadParticipants(strRecieved);
                    requestRoomsInformation();
                    break;
                case "presence":
                    if (joinedRoomName == null && joiningRoom)
                    {
                        joinedRoomName = Utils.FindRoomName(strRecieved);
                        joiningRoom = false;
                        tcpConnector.startGame(joinedRoomName, roomPartner);
                        break;
                    }
                    if (joinedRoomName == null && creatingRoom)
                    {
                        joinedRoomName = Utils.FindRoomName(strRecieved);
                        creatingRoom = false;
                        waitingForOpponent = true;
                        tcpConnector.waitForOpponent(joinedRoomName);
                        break;
                    }
                    if (waitingForOpponent)
                    {
                        waitingForOpponent = false;
                        roomPartner = Utils.ExtractUserNameFromPresence(strRecieved);
                        roomPartner = roomPartner.Split('@')[0];
                        tcpConnector.startGame(joinedRoomName, roomPartner);
                    }
                    break;
                case "chat":
                    //fix temporaneo per evitare il doppio messaggio
                    if (strRecieved.Contains("conference.tribetbc.eu/" + nickname))
                    {
                        if (checkIfServerRecievedTheMessage(strRecieved))
                        {
                            //stop the timer and proceed to send the next message;
                            stopTheTimerAndSendNextMsg();
                        }
                        break;
                    }
                    //attenzione: attualmente la libreria scarta due messaggi consecutivi identici
                    if (Utils.extractXmppMessage(strRecieved)[0] != lastMessageRecievedFromServer || lastMessageRecievedFromServer == null)
                    {
                        lastMessageRecievedFromServer = Utils.extractXmppMessage(strRecieved)[0];
                        tcpConnector.chatRecieved(Utils.extractXmppMessage(strRecieved));
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        private void stopTheTimerAndSendNextMsg()
        {
            if (messagesTimer != null && messagesTimer.Enabled)
            {
                messagesTimer.Stop();
                messagesTimer.Enabled = false;
                messagesTimer.Dispose();
                if (messagesQueue.Count > 0)
                {
                    messagesQueue.RemoveFirst();
                }
                if (messagesQueue.Count > 0)
                {
                    initTimer();
                }
            }
        }

        private bool checkIfServerRecievedTheMessage(string strRecieved)
        {
            if (lastMessageTryingToSend == Utils.extractXmppMessage(strRecieved)[0]) return true;
            return false;
        }

        internal void registerNewAccount(string nickname)
        {
            messagesQueue.AddFirst(new KeyValuePair<string, string>(loginSasl, "\"/>"));
            tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.RequestNewAccountRegistration(nickname), "\"/>"));
        }

        internal void SendMessage(string message)
        {
            message = message + "idsprtr" + uniqueMessageId++;
            messagesQueue.AddLast(new KeyValuePair<string, string>(message, "</message>"));
            initTimer();
            //qui si mandava il messaggio diretto senza provare la feature di spammare col timer per paura di perdere i pacchetti con la connessione scarsa
            //tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.sendGroupMessage(message,nickname,joinedRoomName), "/message>"));
        }

        private void requestRoomsInformation()
        {
            foreach(Room room in rooms.RoomsDictionary.Values)
            {
                if (room.description == null)
                {
                    tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.RequestRoomInformation(room.name), "/iq>"));
                    return;
                }
            }
            requestRoomsRooster();
        }

        private void requestRoomsRooster()
        {
            foreach(Room room in rooms.RoomsDictionary.Values)
            {
                if (room.participants == null)
                {
                    tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.RequestRoomRooster(room.name), "/iq>"));
                    return;
                }
            }
            JoinOrCreate();
        }

        private void JoinOrCreate()
        {
            foreach (Room room in rooms.RoomsDictionary.Values)
            {
                //join
                if(partner != null)
                {
                    if (room.participants.Contains(partner) && room.participants.Count == 1 && room.name.Contains("tribe_room"))
                    {
                        tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.joinRoomRequest(nickname, room.name), ">"));
                        joiningRoom = true;
                        roomPartner = partner;
                        return;
                    }
                }else 
                if(room.participants.Count == 1 && room.name.Contains("tribe_room"))
                {
                    tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.joinRoomRequest(nickname, room.name), ">"));
                    joiningRoom = true;
                    roomPartner = (string)room.participants[0];
                    return;
                }
            }

            //create
            tcpConnector.sendServerMessage(new KeyValuePair<string, string>(Utils.createRoomRequest(nickname),"presence>"));
            creatingRoom = true;
            return;
        }

        internal void SendPong(string ping)
        {
            tcpConnector.sendPong(Utils.generatePong(ping, nickname));
        }
    }
}
