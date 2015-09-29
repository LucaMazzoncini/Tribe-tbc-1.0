using UnityEngine;
using System.Collections;
using GameEventManagement;

public class ManaScriptUnity : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEventManager.sendMana += gameEventManager_sendMana;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void gameEventManager_sendMana(string param)
    {
        //il mana arrivera' cosi' " E:1 F:1 W:1 L:1 D:1"
        string[] splitManaArray = param.Split(' ');//prima splitto per spazio poi per i :
        foreach(string stringApp in splitManaArray)
        {
            string[] valueMana = stringApp.Split(':');
            switch(valueMana[0])
            {
                case "W":
                    UpdateManaScriptP1.UpdateStatus(valueMana[1]);
                    break;
                case "E":
                    UpdateManaScriptP2.UpdateStatus(valueMana[1]);
                    break;
                case "F":
                    UpdateManaScriptP3.UpdateStatus(valueMana[1]);
                    break;
                case "L":
                    UpdateManaScriptP4.UpdateStatus(valueMana[1]);
                    break;
                case "D":
                    UpdateManaScriptP5.UpdateStatus(valueMana[1]);
                    break;
                default:
                    break;
            }

        }

    

    }
}
