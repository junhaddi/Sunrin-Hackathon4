using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_MainMenu : MonoBehaviour {

	public void gameStart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void Help()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
