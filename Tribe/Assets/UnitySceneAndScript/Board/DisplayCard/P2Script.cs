using UnityEngine;
using System.Collections;
using DisplayCard;

public class P2Script : MonoBehaviour {

    public bool isPlayinPlayCard = false;
    private CardUnity card = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
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
        if (this.card != null)
            GetComponent<Renderer>().material.mainTexture = this.card.text;
        this.card = card;
        transform.FindChild("P2_back").GetComponent<Renderer>().material.mainTexture = card.text;
        GetComponent<Animator>().Play("Enter_card");
    }


    public void Slide_sx()
    {
        GetComponent<Animator>().Play("Slide_sx");
    }
    public void Slide_ctr()
    {
        GetComponent<Animator>().Play("Slide_ctr");
    }

}
