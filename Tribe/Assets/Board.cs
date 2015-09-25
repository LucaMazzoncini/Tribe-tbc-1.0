using UnityEngine;
using System.Collections;
using GameEventManagement;

public class Board : MonoBehaviour {

    //tramite questa funzione sono sicuro che Unity carichi tutta la scena
    void OnLevelWasLoaded(int level)
    {
        if( level == 1 )
        {
            GameEventManager.UnityReady();
            Debug.Log("Caricata");
        }
    }
}
