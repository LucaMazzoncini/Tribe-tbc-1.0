using UnityEngine;
using System.Collections;
using DisplayCard;

public class P1Script : MonoBehaviour {

    public bool isPlaying; //questo flag serve per non accumulare le animazioni
    private CardUnity card = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayCard(CardUnity card)
    {
        if(this.card != null)
            GetComponent<Renderer>().material.mainTexture = this.card.text;
        this.card = card;
        transform.FindChild("P1_back").GetComponent<Renderer>().material.mainTexture = card.text;
        GetComponent<Animator>().Play("Enter_card");
    }

    public void Slide_dx()
    {
        GetComponent<Animator>().Play("Slide_dx");
    }
    public void Slide_sx()
    {
        GetComponent<Animator>().Play("Slide_sx");
    }
    public void Slide_dx_ctr()
    {
        GetComponent<Animator>().Play("Slide_dx_ctr");
    }
    public void Slide_sx_ctr()
    {
        GetComponent<Animator>().Play("Slide_sx_ctr");
    }

}
