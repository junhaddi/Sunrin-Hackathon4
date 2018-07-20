using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    private void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector2(Screen.width / 720f, Screen.height / 1280f);
        if (name == "ScoreBack")
            GetComponent<RectTransform>().localPosition = new Vector2(Screen.width / 360f, Screen.height / 1280f * (1123-580));
        if (name == "happy_bar")
            GetComponent<RectTransform>().localPosition = new Vector2(Screen.width / 360f, Screen.height / 1280f * (72-580));
    }
}