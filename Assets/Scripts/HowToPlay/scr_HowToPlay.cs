using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_HowToPlay : MonoBehaviour {

    public void openHTP()
    {
        GameObject.Find("Canvas").transform.Find("HTP").gameObject.SetActive(true);
    }

    public void closeHTP()
    {
        GameObject.Find("Canvas").transform.Find("HTP").gameObject.SetActive(false);
    }
}
