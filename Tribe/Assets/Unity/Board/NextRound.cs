using UnityEngine;
using System.Collections;
using GameEventManagement;
public class NextRound : MonoBehaviour {

    private bool premibile = true;
    Renderer rend;
    // Use this for initialization
    void Start () {

    }
	
    

	// Update is called once per frame
	void Update () {
          if (multiplayerScript.round)
          {
            this.GetComponent<Renderer>().material.mainTexture = Resources.Load("FBX/Materials/End_turn_tapped") as Texture;
            premibile = true;
          }
          else
          {

            this.GetComponent<Renderer>().material.mainTexture = Resources.Load("FBX/Materials/End_turn") as Texture;
            premibile = false;
          }

    }

    void OnMouseDown()
    {
        if (premibile)
        {
            GetComponent<AudioSource>().Play();
            GameEventManager.EndRound();
            this.GetComponent<Animator>().Play("anim");
        }
    }

}
