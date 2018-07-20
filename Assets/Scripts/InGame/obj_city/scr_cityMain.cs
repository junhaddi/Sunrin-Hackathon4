using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cityMain : MonoBehaviour {

    public int cityNum = 0;
    public bool isTarget = false;

    private void Update()
    {
        if (isTarget)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        else
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }

}
