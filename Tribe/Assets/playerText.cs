﻿using UnityEngine;
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
        if (outputString == "")
        {
            outputString = transform.Find("/Multiplayer").GetComponent<multiplayerScript>().getPlayerName()
                + " Tiro dado = " + transform.Find("/Multiplayer").GetComponent<multiplayerScript>().returnDiceResult(); 
        }
    }
}