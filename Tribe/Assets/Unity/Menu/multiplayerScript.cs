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

    private static Communicator communicator;
    private static LinkedList<string> xmlList;
    private static string outputString = "";
    private static string inputString = "";
    public GameObject testoOutput;
    public GameObject testoInput;
    private Text textOutput;
    private Text textInput;
    private static bool gameStart = false;

    // Use this for initialization
    void Start () {
        GameEventManager.waitingForOpponent += gameEventManager_waitingForOpponent;
        GameEventManager.gameStarted += gameEventManager_gameStarted;
        GameEventManager.diceResult += gameEventManager_diceResult;
        GameEventManager.opponentsDiceResult += GameEventManager_opponentsDiceResult;
        GameEventManager.requestXmlForBibliotheca += GameEventManager_requestXmlForBibliotheca;
        textOutput = testoOutput.GetComponent<Text>();
        textInput = testoInput.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
            if(textOutput != null)
                textOutput.text = outputString;
            inputString = textInput.text;

            if(gameStart)
            {
                Application.LoadLevel("Board");
                gameStart = false;
            }   
    }

    public void Login()
    {
        if (inputString != "")
        {
            GameEventManager.waitingForOpponent += gameEventManager_waitingForOpponent;
            GameEventManager.gameStarted += gameEventManager_gameStarted;
            GameEventManager.diceResult += gameEventManager_diceResult;
            GameEventManager.opponentsDiceResult += GameEventManager_opponentsDiceResult;
            GameEventManager.requestXmlForBibliotheca += GameEventManager_requestXmlForBibliotheca;
            communicator = new Communicator(inputString, "46.101.155.56", 5222);
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
        outputString = "Opponent's dice result is " + result;
    }

    static void GameEventManager_requestXmlForBibliotheca()
    {
        xmlList = new LinkedList<string>();
      //  string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
     //   foreach (string file in Directory.GetFiles(currentPath + "/cards_xml/", "*.xml"))
     foreach (string file in Directory.GetFiles("H:/Tribe 1.0/Tribe/Assets/Cards", "*.xml"))
        {
            xmlList.AddLast(File.ReadAllText(file));
        }
        GameEventManager.LoadXmlForBibliotheca(xmlList);
    }

    static void gameEventManager_diceResult(int result)
    {
        outputString = "Your dice result is " + result;

    }

    private static void gameEventManager_gameStarted()
    {
        outputString = "GameStarted!";

        //gameStart = true;

        /*  #region commandSwitch
          switch (Console.ReadLine())
          {
              case "throw":
                  GameEventManager.ThrowDice();
                  break;
              default:
                  break;
          }
          #endregion
          */
    }

    static void gameEventManager_waitingForOpponent()
    {
        outputString = "Waiting for opponent..";
    }
}
