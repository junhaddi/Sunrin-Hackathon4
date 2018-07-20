using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_MainMenu : MonoBehaviour {

    GameObject blackpanel;
    Image blackpanel_image;

    private void Awake()
    {
        blackpanel = GameObject.Find("Blackpanel");
        blackpanel_image = blackpanel.GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine("first");
    }

    public void gameStart()
    {
        StartCoroutine("End");
    }

    IEnumerator first()
    {
        for (int i = 100; i >= 0; i -= 3)
        {
            blackpanel_image.color = new Color(0.09f, 0, 0.09f, i / 100f);
            yield return new WaitForSeconds(0.005f);
        }
        blackpanel_image.color = new Color(0.09f, 0, 0.09f, 0);
        blackpanel.SetActive(false);
    }

    IEnumerator End()
    {
        blackpanel.SetActive(true);
        for (int i = 0; i <= 100; i += 3)
        {
            blackpanel_image.color = new Color(0.09f, 0, 0.09f, i / 100f);
            yield return new WaitForSeconds(0.005f);
        }
        SceneManager.LoadScene("InGame");
    }
}
