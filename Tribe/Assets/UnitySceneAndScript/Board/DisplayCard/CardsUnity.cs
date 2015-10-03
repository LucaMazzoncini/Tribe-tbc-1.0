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
            Invoke("carica", 2);
            Invoke("carica1", 4);
            Invoke("slide", 6);
            Invoke("slide", 8);
            Invoke("slide", 10);
            Invoke("slide", 12);
        }


        public void carica()
        {
            CardUnity cardTmp = new CardUnity("Stonebear", "0", "3");
            transform.FindChild("P1").GetComponent<P1Script>().PlayCard(cardTmp);
            transform.FindChild("P3").GetComponent<P3Script>().PlayCard(cardTmp);
        }

        public void carica1()
        {
            CardUnity cardTmp = new CardUnity("BasaltSpike", "0", "3");
            transform.FindChild("P1").GetComponent<P1Script>().PlayCard(cardTmp);
            transform.FindChild("P2").GetComponent<P2Script>().PlayCard(cardTmp);
        }

        private int slidare = 0;
        public void slide()
        {
            switch (slidare)
            {
                case 0:
                    transform.FindChild("P1").GetComponent<P1Script>().Slide_dx();
                    break;
                case 1:
                    transform.FindChild("P1").GetComponent<P1Script>().Slide_dx_ctr();
                    break;
                case 2:
                    transform.FindChild("P1").GetComponent<P1Script>().Slide_sx();
                    break;
                case 3:
                    transform.FindChild("P1").GetComponent<P1Script>().Slide_sx_ctr();
                    break;
                case 4:
                    transform.FindChild("P2").GetComponent<P2Script>().Slide_sx();
                    break;
                case 5:
                    transform.FindChild("P2").GetComponent<P2Script>().Slide_ctr();
                    break;
                case 6:
                    transform.FindChild("P3").GetComponent<P3Script>().Slide_dx();
                    break;
                case 7:
                    transform.FindChild("P3").GetComponent<P3Script>().Slide_ctr();
                    break;
                default:
                    break;
            }
            slidare++;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}