using UnityEngine;
using System.Collections;

public class UpdateManaScriptP5 : MonoBehaviour {

    public static bool update = false;
    private static string value = "";
    public void Update()
    {
        if (update)
        {
            if (transform.FindChild("ManaText").GetComponent<TextMesh>().text != value)
            {
                this.GetComponent<Animator>().Play("on");
                transform.FindChild("ManaText").GetComponent<TextMesh>().text = value;
            }
            update = false;
        }
    }

    public static void UpdateStatus(string mana)
    {
        value = mana;
        update = true;
    }
}
