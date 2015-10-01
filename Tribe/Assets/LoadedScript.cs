using UnityEngine;
using System.Collections;
using GameEventManagement;

public class LoadedScript : MonoBehaviour {

    public GameObject selfObj;
    private static bool destroy = false;
	// Use this for initialization
	void Start () {
        GameEventManager.loaded += GameEventManager_loaded;
        selfObj.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    if(destroy)
            selfObj.SetActive(false);
    }

    public static void GameEventManager_loaded()
    {
        destroy = true;
    }
}
