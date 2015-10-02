using UnityEngine;
using System.Collections;
using GameEventManagement;

public class UpdateManaOpponent : MonoBehaviour {

    private static string manaPool = "";
    private static int valuePool = 0;
    private static string playAnimation = "";
    // Use this for initialization
    void Start () {
        GameEventManager.opponentPoolUpdate += GameEventManager_opponentPoolUpdate;
        GameEventManager.opponentChoseMana += GameEventManager_opponentChoseMana;
    }


    public static void GameEventManager_opponentChoseMana(string mana)
    {
            playAnimation = mana;
    }

    public static void GameEventManager_opponentPoolUpdate(string mana, int value)
    {
        manaPool = mana;
        valuePool = value;
    }

    // Update is called once per frame
    void Update () {
        if (manaPool != "")
        {
            string gameObj = manaPool + "/Polla_" + valuePool.ToString();
            transform.FindChild(gameObj).gameObject.SetActive(true);
            transform.FindChild(manaPool).GetComponent<Animator>().Play("on");
            manaPool = "";
            valuePool = 0;
        }
        if (playAnimation != "")
        {
            transform.FindChild(playAnimation).GetComponent<Animator>().Play("on");
            playAnimation = "";
        }
    }
}
