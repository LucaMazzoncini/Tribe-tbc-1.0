//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.Configuration;

//namespace XmppCommunicator
//{
//    public delegate void GameStartEventHandler();
//    public delegate void OpponentWaiting();
//    public delegate void MessageEventHandler(MessageEventArgs messageArg);

//    public class _oldTcpConnector
//    {
//        /// <summary>
//        /// Da questo momento parte la partita e puoi iniziare ad inviare messaggi di gioco
//        /// </summary>
//        public event GameStartEventHandler gameStarted;
//        public event OpponentWaiting waitingForOpponent;
//        public event MessageEventHandler messageRecieved;
//        private TcpClient tcpClient;
//        private string gameRoomName;
//        private string expectedClosingTag = "";
//        private string completeMessage = "";
//        private string nickname;
//        private string password;
//        private string partner;
//        private bool waitingForChat;
//        private bool ignoreNextMessage;
//        private bool _gameStarted;
//        private byte[] mResponse;
//        private Queue<KeyValuePair<string,string>> messagesQueue;
//        private ServerMessagesInterpreter serverMessageInterpreter = new ServerMessagesInterpreter();
//        private Rooms rooms;

//        /// <summary>
//        /// Il costruttore vuole il nome utente, password ed il nome del partner con cui giocare(opzionale)
//        /// </summary>
//        /// <param name="nickname"></param>
//        /// <param name="password"></param>
//        /// <param name="partner"></param>
//        public _oldTcpConnector(string nickname, string password, string partner)
//        {
//            this.nickname = nickname;
//            this.password = password;
//            this.partner = partner;
//        }

//        public _oldTcpConnector(string nickname, string password) : this(nickname, password, null)
//        {
//        }

//        public void Connect()
//        {
//            string ip = ConfigurationManager.AppSettings["ip"];
//            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
//            tcpClient = new TcpClient();
//            tcpClient.BeginConnect(ip, port, onCompleteConnect, tcpClient);
//        }

//        private void onCompleteConnect(IAsyncResult ar)
//        {
//            Utils.Log("connesso");
//            tcpClient = (TcpClient)ar.AsyncState;
//            tcpClient.EndConnect(ar);
//            messagesQueue = new Queue<KeyValuePair<string,string>>();
//            string loginSasl = XmlGenericMessages.Base64Encode("\0"+nickname+"\0"+password);
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(XmlGenericMessages.OpenStream, "features>"));
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(loginSasl, "\"/>"));
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(XmlGenericMessages.ResourceSetup,"</iq>"));
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(XmlGenericMessages.OpenSessionRequest,">"));
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(XmlGenericMessages.PresenceNotify,"/presence>"));
//            messagesQueue.Enqueue(new KeyValuePair<string, string>(XmlGenericMessages.FindRooms,">"));
//            sendServerMessage(messagesQueue.Dequeue());
//        }

//        #region sending messages to server
//        private void sendServerMessage(KeyValuePair<string, string> keyValuePair)
//        {
//            expectedClosingTag = keyValuePair.Value;
//            byte[] tx;
//            tx = Encoding.ASCII.GetBytes(keyValuePair.Key);
//            tcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteOnServer, tcpClient);
//            readFromServer();
//        }
//        private void onCompleteWriteOnServer(IAsyncResult ar)
//        {
//            tcpClient = (TcpClient)ar.AsyncState;
//            tcpClient.GetStream().EndWrite(ar);
//        }
//        #endregion

//        #region reading messages from server
//        private void readFromServer()
//        {
//            mResponse = new byte[1024];
//            tcpClient.GetStream().BeginRead(mResponse, 0, mResponse.Length, onCompleteReadFromServer, tcpClient);
//        }

//        private void onCompleteReadFromServer(IAsyncResult ar)
//        {
//            string strRecieved;
//            int nCountResponseBytes;
//            tcpClient = (TcpClient)ar.AsyncState;
//            nCountResponseBytes = tcpClient.GetStream().EndRead(ar);

//            if (nCountResponseBytes == 0)
//            {
//                //readFromServer();
//                return;
//            }

//            strRecieved = Encoding.ASCII.GetString(mResponse, 0, nCountResponseBytes);

//            //debug per vedere i messaggi grezzi in arrivo
//            //Utils.Log((strRecieved));

//            completeMessage += strRecieved;
//            if (strRecieved.EndsWith(expectedClosingTag))
//            {
//                strRecieved = completeMessage;
//                completeMessage = "";
//            }
//            else
//            {
//                readFromServer();
//                return;
//            }

//            if (ignoreNextMessage == true)
//            {
//                ignoreNextMessage = false;
//                return;
//            }
//            #region message switcher
//            switch (serverMessageInterpreter.Interpret(strRecieved))
//            {
//                case "findRooms":
//                    if (rooms == null)
//                    {
//                        rooms = new Rooms(strRecieved);
//                    }
//                    break;
//                case "roomInformation":
//                    {
//                        rooms.LoadRoomInformation(strRecieved);
//                        //aspetta che tutte le descrizioni delle stanze siano caricate
//                        foreach (KeyValuePair<string, Room> kpair in rooms.RoomsDictionary)
//                        {
//                            if (kpair.Value.description == null)
//                            {
//                                sendServerMessage(new KeyValuePair<string, string>(Utils.RequestRoomInformation(kpair.Value.name), ">"));
//                                return;
//                            }
//                        }

//                        List<string> roomCollection = rooms.RoomsDictionary.Keys.ToList();
//                        sendServerMessage(new KeyValuePair<string, string>(Utils.RequestRoomRooster(roomCollection.First()), "</iq>"));
//                    }
//                    break;
//                case "roomRoosterRequest":
//                    {
//                        rooms.LoadParticipants(strRecieved);
//                        foreach (KeyValuePair<string, Room> kpair in rooms.RoomsDictionary)
//                        {
//                            if (!kpair.Value.participantsLoaded)
//                            {
//                                sendServerMessage(new KeyValuePair<string,string>(Utils.RequestRoomRooster(kpair.Value.name),">"));
//                                return;
//                            }
//                        }
//                        lookForYourPartner();
//                    }
//                    break;
//                case "Chat":
//                    List<string> messageList = Utils.extractXmppMessage(strRecieved);
//                    foreach(string message in messageList)
//                    {
//                        if (message.Contains("*startNewGame^_^*"))
//                        {
//                            startGame(gameRoomName);
//                            return;
//                        }

//                        messageRecieved(new MessageEventArgs() { Message = message });
//                    }
                    
//                    readFromServer();
//                    break;
//                case "presence":
//                    if (waitingForChat)
//                    {
//                        readFromServer();
//                        expectedClosingTag = "message>";
//                    }
//                    break;
//                default:
//                    break;
//            }
//            #endregion

//            try
//            {
//                if (messagesQueue.Count > 0)
//                {
//                    sendServerMessage(messagesQueue.Dequeue());
//                }
//                else
//                {
//                    if (rooms != null && !rooms.infoRequested)
//                    {
//                        foreach (KeyValuePair<string,Room> room in rooms.RoomsDictionary)
//                        {
//                            rooms.infoRequested = true;
//                            sendServerMessage(new KeyValuePair<string, string>(Utils.RequestRoomInformation(room.Value.name), ">"));
//                            break;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Utils.Log(ex.Message);
//            }
//        }
//        #endregion

//        private void lookForYourPartner()
//        {
//            foreach (KeyValuePair<string, Room> kpair in rooms.RoomsDictionary)
//            {
//                if (partner == null && kpair.Value.participants.Count == 2 )
//                {
//                    sendServerMessage(new KeyValuePair<string, string>(Utils.joinRoomRequest(nickname, kpair.Key),">"));
//                    startGame(kpair.Key);
//                    return;
//                }

//                if (partner != null)
//                {
//                    if(kpair.Value.participants.Contains(partner))
//                    {
//                        sendServerMessage(new KeyValuePair<string, string>(Utils.joinRoomRequest(nickname, kpair.Key),">"));
//                        startGame(kpair.Key);
//                        return;
//                    }
//                }
//            }
//            sendServerMessage(new KeyValuePair<string, string>(Utils.createRoomRequest(nickname),"presence>"));
//            gameRoomName = nickname + "s_room" + "@conference.tribetbc.eu";
//            waitingForOpponent();
//            waitingForChat = true;
//            ignoreNextMessage = true;
//        }

//        private void startGame(string p)
//        {
//            gameRoomName = p;
//            readFromServer();
//            _gameStarted = true;
//            gameStarted();
//        }

//        public void SendMessage(string p)
//        {
//            if (_gameStarted && gameRoomName != null)
//            {
//                sendServerMessage(new KeyValuePair<string, string>(Utils.sendGroupMessage(p,nickname,gameRoomName),"message>"));
//            }
//        }
//    }
//}
