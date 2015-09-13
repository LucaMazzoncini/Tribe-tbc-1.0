using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Configuration;

namespace XmppCommunicator
{
    public delegate void GameStartEventHandler(GameStartedEventArgs gameStartedEventArgs);
    public delegate void WaitingForOpponentEventHandler();
    public delegate void MessageEventHandler(MessageEventArgs messageArg);
    public delegate void ErrorEventHandler();
    public delegate void OpponentDisconnectedEventHandler();

    public class TcpConnector
    {
        public event GameStartEventHandler gameStarted;
        public event WaitingForOpponentEventHandler waitingForOpponent;
        public event MessageEventHandler messageRecieved;
        public event ErrorEventHandler errorRecieved;
        public event OpponentDisconnectedEventHandler opponentDisconnected;

        private string nickname;
        private string password;
        private string partner;
        private string serverIp;
        private int port;
        private string expectedClosingTag;
        private string completeMessage;
        private TcpClient tcpClient;
        private bool listening;
        private XmppMessageManager xmppMessageManager;
        private byte[] mResponse;
        private bool gameStartedStatus;

        //constructors
        public TcpConnector(string nickname, string password, string partner, string serverIp, int port)
        {
            this.serverIp = serverIp;
            this.port = port;
            this.nickname = nickname.Trim().ToLower();
            this.password = password;
            if (partner != null) partner = partner.Trim().ToLower();
            this.partner = partner;
        }
        public TcpConnector(string nickname, string password, string serverIp, int port) : this(nickname, password, null, serverIp, port) { }

        public void Connect()
        {
			//string ip = "46.101.155.56";
			string ip = serverIp;
			int port = this.port;
            tcpClient = new TcpClient();
            tcpClient.BeginConnect(ip, port, onCompleteConnect, tcpClient);
        }

		public void Disconnect()
		{
			tcpClient.Close();	
		}

        private void onCompleteConnect(IAsyncResult ar)
        {
            Utils.Log("Connessione stabilita.");
            tcpClient.EndConnect(ar);
            xmppMessageManager = new XmppMessageManager(this, nickname, password, partner);
        }

        #region sending messages to server
        internal void sendServerMessage(KeyValuePair<string, string> keyValuePair)
        {
            expectedClosingTag = keyValuePair.Value;
            byte[] tx;
            tx = Encoding.ASCII.GetBytes(keyValuePair.Key);
            tcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteOnServer, tcpClient);
            readFromServer();
        }
        private void onCompleteWriteOnServer(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            tcpClient.GetStream().EndWrite(ar);
        }
        public void SendMessage(string p)
        {
            xmppMessageManager.SendMessage(p);
        }
        #endregion

        #region reading messages from server
        private void readFromServer()
        {
            if (!listening)
            {
                listening = true;
                mResponse = new byte[1024];
                tcpClient.GetStream().BeginRead(mResponse, 0, mResponse.Length, onCompleteReadFromServer, tcpClient);
            }
        }

        private void readAgain()
        {
            mResponse = new byte[1024];
            tcpClient.GetStream().BeginRead(mResponse, 0, mResponse.Length, onCompleteReadFromServer, tcpClient);
        }

        private void onCompleteReadFromServer(IAsyncResult ar)
        {
            #region stream reading
            string strRecieved;
            int nCountResponseBytes;
            tcpClient = (TcpClient)ar.AsyncState;
            nCountResponseBytes = tcpClient.GetStream().EndRead(ar);

            if (nCountResponseBytes == 0)
            {
                return;
            }

            strRecieved = Encoding.ASCII.GetString(mResponse, 0, nCountResponseBytes);

            //debug per vedere i messaggi grezzi in arrivo
            //Utils.Log((strRecieved));

            completeMessage += strRecieved;

            //ping pong
            if(completeMessage.Contains(":ping\"/>"))xmppMessageManager.SendPong(completeMessage);
            //not authorized
            if (completeMessage.Contains("<not-authorized/></failure>"))
            {
                completeMessage = "";
                xmppMessageManager.registerNewAccount(nickname);
            }
            //login error
			if (completeMessage.Contains("<error code=\"409\""))
            {
                completeMessage = "";
                errorRecieved();
            }
            //presence after game start management
            if(completeMessage.Contains("status code=\"100\"") && gameStartedStatus)
            {
                gameStartedStatus = false;
                completeMessage = "";
            }
            //opponent disconnected
            if (completeMessage.Contains("type=\"unavailable\">"))
            {
                completeMessage = "";
                opponentDisconnected();
            }

            if (completeMessage.EndsWith(expectedClosingTag))
            {
                listening = false;
                strRecieved = completeMessage;
                completeMessage = "";
            }
            else
            {
                readAgain();
                return;
            }
            //Utils.Log((strRecieved));
            #endregion

            xmppMessageManager.ReadMessage(strRecieved);
            readFromServer();

        }
        #endregion

        internal void startGame(string p, string partner)
        {
            gameStartedStatus = true;
            expectedClosingTag = "/message>";
            readFromServer();
            gameStarted(new GameStartedEventArgs() { Opponent = partner });
        }

        internal void waitForOpponent(string p)
        {
            expectedClosingTag = "/presence>";
            readFromServer();
            if (waitingForOpponent != null)
            {
                waitingForOpponent();
            }
        }

        internal void chatRecieved(List<string> list)
        {
            foreach (string message in list)
            {
                string splitMessage = message.Split(new[]{"idsprtr"},StringSplitOptions.None)[0];
                messageRecieved(new MessageEventArgs() { Message = splitMessage });
            }
            readFromServer();
        }

        internal void sendPong(string p)
        {
            byte[] tx;
            tx = Encoding.ASCII.GetBytes(p);
            tcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteOnServer, tcpClient);
            readFromServer();
        }
    }
}
