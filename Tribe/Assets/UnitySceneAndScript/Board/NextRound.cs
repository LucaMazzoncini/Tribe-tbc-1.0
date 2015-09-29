using UnityEngine;
using System.Collections;
using GameEventManagement;
public class NextRound : MonoBehaviour {

    private bool premibile = true;
	// Setto la texture giusta ed attivo e disattivo il tasto
	void Update ()
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
    }
    void OnMouseDown()
    {
        if (premibile)
        {
            GetComponent<AudioSource>().Play();
            GameEventManager.EndRound();
            this.GetComponent<Animator>().Play("Press");
        }
    }
}
