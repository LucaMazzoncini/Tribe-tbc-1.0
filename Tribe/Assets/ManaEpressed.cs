using UnityEngine;
using System.Collections;
using GameEventManagement;

public class ManaEpressed : MonoBehaviour {

    static bool choseYourMana = false;
    static string reasonForChoseMana = "";
	// Use this for initialization
	void Start () {
        GameEventManager.choseMana += gameEventManager_chooseMana;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void gameEventManager_chooseMana(string param)
    {
        choseYourMana = true;
        reasonForChoseMana = param;
    }

    public void OnMouseDown()
    {

        if(choseYourMana)
        {

            GameEventManager.ManaChosen("Earth", reasonForChoseMana);

        }
    }
}
