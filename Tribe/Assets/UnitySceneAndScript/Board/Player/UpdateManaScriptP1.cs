using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameEventManagement;
public class UpdateManaScriptP1 : MonoBehaviour {

    private const string MANA = "Water";
    private static string value;
    private Texture on = new Texture();
    private Texture off = new Texture();
    public static bool textureOn = false;
    public static bool canCreateManaPool = false;

    void Start()
    {
        GameEventManager.yesYouCanCreateManaPool += YesYouCanCreateManaPool;
    }

    public static void UpdateStatus(string mana)
    {
        value = mana;
    }

    public void Update()
    {
        if(value!=null)
            if (transform.FindChild("ManaText").GetComponent<TextMesh>().text != value)
            {
                this.GetComponent<Animator>().Play("on");
                transform.FindChild("ManaText").GetComponent<TextMesh>().text = value;  
            }
        updateTexture();
    }

    public static void ChoseStatus(bool param)
    {
        textureOn = param;
        NextRound.EnableDisableChangeRound(!param);
    }

    public void updateTexture()
    {
        if (on == null)
            on = Resources.Load("Materials/Mana_P_blu_tapped") as Texture;
        if (off == null)
            off = Resources.Load("Materials/Mana_P_blu1") as Texture;

        if (textureOn)
            this.GetComponent<Renderer>().material.mainTexture = on;
        else
            this.GetComponent<Renderer>().material.mainTexture = off;
    }

    public void OnMouseDown()
    {
        
        if (textureOn) //controllo se sono in selezione mana
        {
            textureOn = false;
            NextRound.EnableDisableChangeRound(true);
            ManaScriptUnity.manaChosed(MANA);
        }else
        {
            GameEventManager.CanCreateManaPool(MANA);
            if (canCreateManaPool)
            {
                transform.FindChild("GOLoadingPool").GetComponent<EnableLoadingPoolScript>().SetLoadingPool(true, MANA);
                canCreateManaPool = false;
            }
        }
    }

    public static void YesYouCanCreateManaPool(string mana)
    {
        if (MANA == mana)
            canCreateManaPool = true;
    }
    public void OnMouseUp()
    {
        transform.FindChild("GOLoadingPool").GetComponent<EnableLoadingPoolScript>().SetLoadingPool(false,MANA);
    }
}
