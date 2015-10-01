using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateManaScriptP5 : MonoBehaviour {

    private const string MANA = "Death";
    private static string value;
    private Texture on = new Texture();
    private Texture off = new Texture();
    public static bool textureOn = false;



    public static void UpdateStatus(string mana)
    {
        value = mana;
    }

    public void Update()
    {
        if (value != null)
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
            on = Resources.Load("Materials/Mana_P_tapped") as Texture;
        if (off == null)
            off = Resources.Load("Materials/Mana_P") as Texture;

        if (textureOn)
            this.GetComponent<Renderer>().material.mainTexture = on;
        else
            this.GetComponent<Renderer>().material.mainTexture = off;
    }

    public void OnMouseDown()
    {
        if (textureOn)
        {
            textureOn = false;
            NextRound.EnableDisableChangeRound(true);
            ManaScriptUnity.manaChosed(MANA);
        }
    }

}
