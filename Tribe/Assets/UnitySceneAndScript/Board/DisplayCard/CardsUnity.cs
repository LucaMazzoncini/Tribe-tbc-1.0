using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


namespace DisplayCard
{

    public class CardUnity
    {
        public string strength = "";
        public string hp = "";
        public int id = -1;
        public string name = "";
        public Texture text = null;

        public CardUnity(string name, string strength, string hp)
        {
            this.name = Regex.Replace(name, @"\s+", "");
            this.strength = strength;
            this.hp = hp;
            text = Resources.Load("ImageCard/" + name) as Texture;
        }
    }

    public class CardsUnity : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Invoke("carica", 3);
             Invoke("carica1", 6);
             Invoke("carica2", 9);
             Invoke("Rimuovi2", 12);
             Invoke("carica1", 15);

        }

        public void PlayCard(CardUnity param)
        {
            if (transform.FindChild("P1").GetComponent<P1Script>().GetCardDisplayerd().name == "P") //se non c'e' niente nello spicchio principale la inserisco li
            {
                transform.FindChild("P1").GetComponent<P1Script>().PlayCard(param);

            }else
            {
                if (transform.FindChild("P2").GetComponent<P2Script>().GetCardDisplayerd().name == "P")
                {
                    transform.FindChild("P2").GetComponent<P2Script>().PlayCard(transform.FindChild("P1").GetComponent<P1Script>().GetCardDisplayerd());
                    transform.FindChild("P1").GetComponent<P1Script>().PlayCard(param);

                }
                else
                {
                    if (transform.FindChild("P3").GetComponent<P3Script>().GetCardDisplayerd().name == "P")
                    {
                        transform.FindChild("P3").GetComponent<P3Script>().PlayCard(transform.FindChild("P2").GetComponent<P2Script>().GetCardDisplayerd());
                        transform.FindChild("P2").GetComponent<P2Script>().PlayCard(transform.FindChild("P1").GetComponent<P1Script>().GetCardDisplayerd());
                        transform.FindChild("P1").GetComponent<P1Script>().PlayCard(param);

                    }
                }

            }


        }

      public void carica()
        {
            CardUnity cardTmp = new CardUnity("Stonebear", "0", "3");
            PlayCard(cardTmp);
        }

        public void carica1()
        {
            CardUnity cardTmp = new CardUnity("BasaltSpike", "0", "3");
            PlayCard(cardTmp);

        }
        public void carica2()
        {
            CardUnity cardTmp = new CardUnity("BackToAsh", "0", "3");
            PlayCard(cardTmp);
        }
        
        public void Rimuovi1()
        {
            transform.FindChild("P1").GetComponent<P1Script>().RemoveCard();
        }
        public void Rimuovi2()
        {
            transform.FindChild("P2").GetComponent<P2Script>().RemoveCard();
        }
        public void Rimuovi3()
        {
            transform.FindChild("P3").GetComponent<P3Script>().RemoveCard();
        }
        
        public void SwitchCard(int param)
        {
            if(param == 3)
            {
                CardUnity cardP1 = transform.FindChild("P1").GetComponent<P1Script>().GetCardDisplayerd();
                CardUnity cardP3 = transform.FindChild("P3").GetComponent<P3Script>().GetCardDisplayerd();
                transform.FindChild("P1").GetComponent<P1Script>().PlayCard(cardP3);
                transform.FindChild("P3").GetComponent<P3Script>().PlayCard(cardP1);
            }
            if (param == 2)
            {
                CardUnity cardP1 = transform.FindChild("P1").GetComponent<P1Script>().GetCardDisplayerd();
                CardUnity cardP2 = transform.FindChild("P2").GetComponent<P2Script>().GetCardDisplayerd();
                transform.FindChild("P1").GetComponent<P1Script>().PlayCard(cardP2);
                transform.FindChild("P2").GetComponent<P2Script>().PlayCard(cardP1);
            }


        }

    }

}