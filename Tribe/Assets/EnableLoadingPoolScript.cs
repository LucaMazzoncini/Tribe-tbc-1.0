using UnityEngine;
using System.Collections;

public class EnableLoadingPoolScript : MonoBehaviour {


    public void SetLoadingPool(bool param)
    {
        transform.FindChild("LoadingPool").gameObject.SetActive(param);
        if (transform.FindChild("LoadingPool").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base.Idle"))
        {
            transform.FindChild("LoadingPool").gameObject.SetActive(false);
        }

        
    }

}
