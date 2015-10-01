using UnityEngine;
using System.Collections;
using GameEventManagement;

public class Board : MonoBehaviour {

    public GameObject loading;
    //tramite questa funzione sono sicuro che Unity carichi tutta la scena
    void OnLevelWasLoaded(int level)
    {
        if( level == 1 )
        {
            Invoke("OkReady", 1.0f);
            
        }
    }

    void Start()
    {
        loading.SetActive(true);
    }

    public void OkReady()
    {
        GameEventManager.UnityReady();
    }
}
