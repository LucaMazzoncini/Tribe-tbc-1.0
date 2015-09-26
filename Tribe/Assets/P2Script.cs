using UnityEngine;
using System.Collections;

public class P2Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        this.GetComponent<Animator>().Play("gigi");
    }
}
