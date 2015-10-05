using UnityEngine;
using System.Collections;
using DisplayCard;

public class P2Script : MonoBehaviour {

    public bool isPlaying; //questo flag serve per non accumulare le animazioni
    public bool isPlayinPlayCard = false;
    private CardUnity card = null;

    public void Start()
    {
        card = new CardUnity("P", "", "");
    }
    public CardUnity GetCardDisplayerd()
    {
        return card;
    }

    void Update()
    {
        if (isPlayinPlayCard)
        {
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Enter_card"))
            {
                GetComponent<Renderer>().material.mainTexture = card.text;
                isPlayinPlayCard = false;
            }
        }
    }

    public void PlayCard(CardUnity card)
    {
        this.card = card;
        transform.FindChild("P2_back").GetComponent<Renderer>().material.mainTexture = card.text;
        GetComponent<Animator>().Play("Enter_card");
        isPlayinPlayCard = true;
        transform.Find("/UI/Enviroment").GetComponent<EnviromentScript>().PlayAnimationGears();
    }


    public void Slide_sx()
    {
        GetComponent<Animator>().Play("Slide_sx");
    }
    public void Slide_ctr()
    {
        GetComponent<Animator>().Play("Slide_ctr");
    }

    public void OnMouseDown()
    {
        if( card != null ) //se c'e' una carta.
            if(card.name != "P") //se e' stata tolta
                transform.parent.GetComponent<CardsUnity>().SwitchCard(2);
    }

    public void RemoveCard()
    {
        card = null;
        CardUnity emptyCard = new CardUnity("P", "", "");
        PlayCard(emptyCard);
    }
}
