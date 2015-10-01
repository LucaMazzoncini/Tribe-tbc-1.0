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
public class opponentText : MonoBehaviour {



    public GameObject testoOutput;
    private static string outputString = "";
    private Text textOutput;
    // Use this for initialization
    void Start()
    {
        textOutput = testoOutput.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textOutput != null)
            textOutput.text = outputString;
       /* 
            bool round = !transform.Find("/Multiplayer").GetComponent<multiplayerScript>().returnRound();

        outputString = transform.Find("/Multiplayer").GetComponent<multiplayerScript>().getOpponentName()
           + " Tiro dado = " + transform.Find("/Multiplayer").GetComponent<multiplayerScript>().returnOpponentDiceResult();
       */       
        
    }
}
