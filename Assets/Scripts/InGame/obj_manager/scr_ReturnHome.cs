using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_ReturnHome : MonoBehaviour {

	public void goto_home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
