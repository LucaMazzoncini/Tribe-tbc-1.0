using UnityEngine;
using System.Collections;
using GameEventManagement;

public class EnableLoadingPoolScript : MonoBehaviour {

    public string manaParam = "";
    public void SetLoadingPool(bool param,string mana)
    {
        manaParam = mana;
        transform.FindChild("LoadingPool").gameObject.SetActive(param);

        if (transform.FindChild("LoadingPool").gameObject.activeSelf)
        if (transform.FindChild("LoadingPool").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.Particelle_loading"))
        {
            //transform.FindChild("LoadingPool").gameObject.SetActive(false);
            Debug.Log("E' in Particelle_loading");
        }



    }

    public void Update()
    {
        if(transform.FindChild("LoadingPool").gameObject.activeSelf)
        if (transform.FindChild("LoadingPool").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.Idle"))
        {
            GameEventManager.CreatePool(manaParam);
            transform.FindChild("LoadingPool").gameObject.SetActive(false);
            
        }
    }

}
