using UnityEngine;
using System.Collections;
using DisplayCard;

public class P1Script : MonoBehaviour {

    public bool isPlaying = false; //questo flag serve per non accumulare le animazioni
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
        GetComponent<Animator>().Play("Enter_card");
        transform.Find("/UI/Enviroment").GetComponent<EnviromentScript>().PlayAnimationGears();
        transform.FindChild("P1_back").GetComponent<Renderer>().material.mainTexture = card.text;
    }

    

    public void RemoveCard()
    {
        CardUnity emptyCard = new CardUnity("P", "", "");
        PlayCard(emptyCard);
    }

}
