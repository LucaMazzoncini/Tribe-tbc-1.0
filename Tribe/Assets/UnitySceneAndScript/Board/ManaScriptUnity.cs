using UnityEngine;
using System.Collections;
using GameEventManagement;
using System.Collections.Generic;

public class ManaClass
{
    public string manaValue;
    public string manaName;
    
    public ManaClass(string mana,string value)
    {
        manaName = mana;
        manaValue = value;
    }
}

public class ManaScriptUnity : MonoBehaviour {
    private static string reason; //questo parametro potevo anche evitare di passarlo l'ho fatto per una visione piu completa anche dal lato della grafica
    private static List<ManaClass> mana = new List<ManaClass>();
    private static ManaClass enablePool = null;
	void Start () {
        GameEventManager.sendMana += GameEventManager_sendMana; //evento che aggiorna il mana
        GameEventManager.choseMana += GameEventManager_choseMana;
        GameEventManager.displayPool += GameEventManager_displayPool;
    }
	
    public static void GameEventManager_choseMana(string param)
    {
        reason = param;
        UpdateManaScriptP1.ChoseStatus(true); //attivo il cambiamento di stato
        UpdateManaScriptP2.ChoseStatus(true); //attivo il cambiamento di stato
        UpdateManaScriptP3.ChoseStatus(true); //attivo il cambiamento di stato
        UpdateManaScriptP4.ChoseStatus(true); //attivo il cambiamento di stato
        UpdateManaScriptP5.ChoseStatus(true); //attivo il cambiamento di stato

    }

    public bool isPlayingAnimation()
    {
        bool playing = false;
        if (transform.FindChild("Mana_P1").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on"))
            playing = true;
        if (transform.FindChild("Mana_P2").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on"))
            playing = true;
        if (transform.FindChild("Mana_P3").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on"))
            playing = true;
        if (transform.FindChild("Mana_P4").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on"))
            playing = true;
        if (transform.FindChild("Mana_P5").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on"))
            playing = true;
        Debug.Log(playing);
        return playing;
    }

    public static void manaChosed(string param)
    {
        GameEventManager.ManaChosen(param, reason);
        UpdateManaScriptP1.ChoseStatus(false); //Disattivo il cambiamento di stato
        UpdateManaScriptP2.ChoseStatus(false); //Disattivo il cambiamento di stato
        UpdateManaScriptP3.ChoseStatus(false); //Disattivo il cambiamento di stato
        UpdateManaScriptP4.ChoseStatus(false); //Disattivo il cambiamento di stato
        UpdateManaScriptP5.ChoseStatus(false); //Disattivo il cambiamento di stato
        //adesso devo disattivare tutti gli stati
    }
    void Update()
    {
        if (mana.Count > 0)
        {
            if (!isPlayingAnimation()) //se non ci sono animazioni attive
            {
                switch (mana[0].manaName)
                {
                    case "W":
                        UpdateManaScriptP1.UpdateStatus(mana[0].manaValue);
                        break;
                    case "E":
                        UpdateManaScriptP2.UpdateStatus(mana[0].manaValue);
                        break;
                    case "F":
                        UpdateManaScriptP3.UpdateStatus(mana[0].manaValue);
                        break;
                    case "L":
                        UpdateManaScriptP4.UpdateStatus(mana[0].manaValue);
                        break;
                    case "D":
                        UpdateManaScriptP5.UpdateStatus(mana[0].manaValue);
                        break;
                    default:
                        break;
                }
                mana.RemoveAt(0);
            }
        }
        if( enablePool != null )
        {
            switch(enablePool.manaName)
            {
                case "Water":
                    if (enablePool.manaValue == "1")
                        transform.FindChild("Mana_P1/Polla_P1").gameObject.SetActive(true);
                    else
                        transform.FindChild("Mana_P1/Polla_P2").gameObject.SetActive(true);
                    break;
                case "Earth":
                    if (enablePool.manaValue == "1")
                        transform.FindChild("Mana_P2/Polla_P3").gameObject.SetActive(true);
                    else
                        transform.FindChild("Mana_P2/Polla_P4").gameObject.SetActive(true);
                    break;
                case "Fire":
                    if (enablePool.manaValue == "1")
                        transform.FindChild("Mana_P3/Polla_P5").gameObject.SetActive(true);
                    else
                        transform.FindChild("Mana_P3/Polla_P6").gameObject.SetActive(true);
                    break;
                case "Life":
                    if (enablePool.manaValue == "1")
                        transform.FindChild("Mana_P4/Polla_P7").gameObject.SetActive(true);
                    else
                        transform.FindChild("Mana_P4/Polla_P8").gameObject.SetActive(true);
                    break;
                case "Death":
                    if (enablePool.manaValue == "1")
                        transform.FindChild("Mana_P5/Polla_P9").gameObject.SetActive(true);
                    else
                        transform.FindChild("Mana_P6/Polla_P10").gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
            enablePool = null;
        }
    }

    public static void GameEventManager_sendMana(string param)
    {
        //il mana arrivera' cosi' " E:1 F:1 W:1 L:1 D:1"
        string[] splitManaArray = param.Split(' ');//prima splitto per spazio poi per i :
        foreach (string stringApp in splitManaArray)
        {
            string[] valueMana = stringApp.Split(':');
            
            if (valueMana[0] != "")
            {
                ManaClass manaTemp = new ManaClass(valueMana[0], valueMana[1]);
                mana.Add(manaTemp);
            }
        }
    }

    public static void GameEventManager_displayPool(string mana, string value)
    {
        enablePool = new ManaClass(mana,value);

    }
}
