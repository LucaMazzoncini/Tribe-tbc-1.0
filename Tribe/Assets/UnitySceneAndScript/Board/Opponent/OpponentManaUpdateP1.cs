using UnityEngine;
using System.Collections;
using GameEventManagement;
public class OpponentManaUpdateP1 : MonoBehaviour {

    public static string MANA = "Water";
    public static bool playAnimation = false;
	// Use this for initialization
	void Start () {
        GameEventManager.opponentChoseMana += GameEventManager_opponentChoseMana;
	}
	

    public static void GameEventManager_opponentChoseMana(string mana)
    {
        if(mana == MANA)
        playAnimation = true;   
    }
    // Update is called once per frame
    void Update () {
        if(playAnimation)
        {
            GetComponent<Animator>().Play("on");
            playAnimation = false;
        }
    }
}
