using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEventManagement;
using Communication;
using System.IO;
using System.Reflection;
using UnityEngine.UI;

public class multiplayerScript : MonoBehaviour {

    private GameLogic.Game game;
    private Communicator communicator;
    private static LinkedList<string> xmlList;
    private static string outputString = "";
    private static string inputString = "";
    public GameObject testoOutput;
    public GameObject testoInput;
    private Text textOutput;
    private Text textInput;
    private static bool gameStart = false;
    private static bool connected = false; //quando l'opponent mi restituisce il nome lo considero connesso (carico tutta la scena ma aspetto il nome)
    private static string playerName = "";
    private static string opponentName = "";
    private static int opponentDiceResult = 0;
    private static int diceResult = 0;
    // Use this for initialization
    void Start () {
        GameEventManager.waitingForOpponent += gameEventManager_waitingForOpponent;
        GameEventManager.gameStarted += gameEventManager_gameStarted;
        GameEventManager.diceResult += gameEventManager_diceResult;
        GameEventManager.opponentsDiceResult += GameEventManager_opponentsDiceResult;
        GameEventManager.requestXmlForBibliotheca += GameEventManager_requestXmlForBibliotheca;
        GameEventManager.getOpponentName += gameEventManager_getOpponentName;
        GameEventManager.opponentReceveMyName += gameEventManager_opponentReceveMyName;
        
        textOutput = testoOutput.GetComponent<Text>();
        textInput = testoInput.GetComponent<Text>();
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    public string getPlayerName()
    {
        return playerName;
    }

    public string getOpponentName()
    {
        return opponentName;
    }


    // Update is called once per frame
    void Update () {
            if(textOutput != null)
                textOutput.text = outputString;
            inputString = textInput.text;
            if(gameStart)
            {
                if (connected) //questo flag indica che l'avversario si e' connesso alla partita e mi ha inviato il nome
                {
                    Application.LoadLevel("Board");
                    gameStart = false;
                }
                else
                {
                    GameEventManager.SendOpponentName(playerName); //invio il nome del player fino a che non ricevo il suo
                }
            }   
    }
    public void Login()
    {
        if (inputString != "")
        {
            string playerName = inputString;
            game = new GameLogic.Game(playerName);

            Communicator.init(inputString, "46.101.155.56", 5222);
            communicator = Communicator.getInstance();
            /*
            
            communicator = new Communicator();
            */
            communicator.SetGame(game);
            outputString = "Connesso";
            
            GameEventManager_requestXmlForBibliotheca();
            
        }
        else
        {
            outputString = "Inserisci il nome Utente";
        }
    }
    public void TiraDado()
    {
        GameEventManager.ThrowDice();
    }
    private static void GameEventManager_opponentsDiceResult(int result)
    {
        opponentDiceResult = result;
        outputString = "Opponent's dice result is " + result;
    }

    public int returnOpponentDiceResult()
    { return opponentDiceResult; }

    public int returnDiceResult()
    { return diceResult; }

    static void GameEventManager_requestXmlForBibliotheca()
    {
        xmlList = new LinkedList<string>();
        //  string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //   foreach (string file in Directory.GetFiles(currentPath + "/cards_xml/", "*.xml"))
        string localPath = "Assets/Cards";
        foreach (string file in Directory.GetFiles(localPath, "*.xml"))
        {
            xmlList.AddLast(File.ReadAllText(file));
        }
        GameEventManager.LoadXmlForBibliotheca(xmlList);
    }

    #region Metodi chiamati da GameEvent Manager
    static void gameEventManager_diceResult(int result)
    {
        diceResult = result;
        outputString = "Your dice result is " + result;
    }
    private static void gameEventManager_gameStarted()
    {
        outputString = "GameStarted!";
        playerName = inputString;
        gameStart = true;
    }
    static void gameEventManager_getOpponentName(string param)
    {
        opponentName = param;
        GameEventManager.SendOpponentName(playerName); //invio il nome del player
        GameEventManager.NameReceived(); //notifico all'opponent che ho ricevuto il suo nome e quindi sono connesso
    }

    static void gameEventManager_opponentReceveMyName()
    {
        connected = true;
    }

    static void gameEventManager_waitingForOpponent()
    {
        outputString = "Waiting for opponent..";
    }
    #endregion

}
