using UnityEngine;
using System.Collections;

public class UpdateManaScriptP4 : MonoBehaviour {

    public static bool update = false;
    private static string value = "";
    public void Update()
    {
        if (update)
        {
            if (transform.FindChild("ManaText").GetComponent<TextMesh>().text != value)
            {
                if (!this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.on")) //Se non sta' andando
                {
                    this.GetComponent<Animator>().Play("on");
                    transform.FindChild("ManaText").GetComponent<TextMesh>().text = value;
                }
                else
                {
                    Debug.Log("Sta' girando");
                }
            }
        }
    }

    public static void UpdateStatus(string mana)
    {
        value = mana;
        update = true;
    }
}
