using UnityEngine;
using System.Collections;
using GameEventManagement;
public class NextRound : MonoBehaviour
{

    private static bool premibile = true;
    private static bool pushedManaSelect = true;
    private static bool playAnimationUp = false;
    // Setto la texture giusta ed attivo e disattivo il tasto
    void Update()
    {
        if (multiplayerScript.round)
        {
            this.GetComponent<Renderer>().material.mainTexture = Resources.Load("Materials/End_turn_tapped") as Texture;
            premibile = true;
        }
        else
        {

            this.GetComponent<Renderer>().material.mainTexture = Resources.Load("Materials/End_turn") as Texture;
            premibile = false;
        }

        if (playAnimationUp)
        {
            this.GetComponent<Animator>().Play("Su");
            playAnimationUp = false;
        }
    }
    public static void EnableDisableChangeRound(bool enabled)
    {
        pushedManaSelect = enabled;
        //if (enabled)
            playAnimationUp = true;
    }

    void OnMouseDown()
    {
        if (premibile && pushedManaSelect)
        {
            GetComponent<AudioSource>().Play();
            GameEventManager.EndRound();
            this.GetComponent<Animator>().Play("Giu");
        }
    }

}