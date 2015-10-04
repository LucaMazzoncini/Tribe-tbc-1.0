using UnityEngine;
using System.Collections;
using DisplayCard;

public class P1Script : MonoBehaviour {

    public bool isPlaying; //questo flag serve per non accumulare le animazioni
    private CardUnity card = null;

    public void Start()
    {
        card = new CardUnity("P", "","");
    }

    public CardUnity GetCardDisplayerd()
    {
        return card;
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
        Texture text = new Texture();
        text = transform.FindChild("P1_back").GetComponent<Renderer>().material.mainTexture;
        transform.FindChild("P1").GetComponent<Renderer>().material.mainTexture = text;
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
    public void RemoveCard()
    {
        CardUnity emptyCard = new CardUnity("P", "", "");
        PlayCard(emptyCard);
    }

}
