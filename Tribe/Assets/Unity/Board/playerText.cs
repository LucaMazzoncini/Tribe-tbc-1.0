using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEventManagement;
using Communication;
using System.IO;
using System.Reflection;
using UnityEngine.UI;

public class playerText : MonoBehaviour {
    private string playerName;
    // Use this for initialization
    public GameObject testoOutput;
    private static string outputString = "";
    private Text textOutput;

    void Start()
    {
        textOutput = testoOutput.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (textOutput != null)
        {
            textOutput.text = outputString;
        }

        
            bool round = transform.Find("/Multiplayer").GetComponent<multiplayerScript>().returnRound();

            outputString = transform.Find("/Multiplayer").GetComponent<multiplayerScript>().getPlayerName()
                + " Tiro dado = " + transform.Find("/Multiplayer").GetComponent<multiplayerScript>().returnDiceResult()
                + " Turno mio = " + round.ToString()
                + " Mana = " + transform.Find("/Multiplayer").GetComponent<multiplayerScript>().GetManaValue();
        
    }

    public static void Print(string param)
    {
        Debug.Log(param);
    }
}
