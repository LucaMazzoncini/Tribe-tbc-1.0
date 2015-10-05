using UnityEngine;
using System.Collections;

public class EnviromentScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void PlayAnimationGears()
    {
        transform.FindChild("Ingr1").GetComponent<Animator>().Play("Rotaz");
        transform.FindChild("Ingr2").GetComponent<Animator>().Play("Rotaz");
        transform.FindChild("Ingr3").GetComponent<Animator>().Play("Rotaz");
        transform.FindChild("Ingr4").GetComponent<Animator>().Play("Rotaz");
    }
    // Update is called once per frame
    void Update () {
	
	}
}
