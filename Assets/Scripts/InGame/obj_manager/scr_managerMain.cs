using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_managerMain : MonoBehaviour {

    GameObject managers;
    GameObject blackpanel;
    Image blackpanel_image;

    public int score;
    public float happyPoint;

    private void Awake()
    {
        managers = GameObject.Find("Managers");
        blackpanel = GameObject.Find("Blackpanel");
        blackpanel_image = blackpanel.GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine("mainCoroutine");
    }

    IEnumerator mainCoroutine()
    {
        //---------------------------------------------------------------[[ 검은 화면이 사라지는 부분 ]]
        for (int i = 100; i >= 0; i-=2)
        {
            blackpanel_image.color = new Color(0, 0, 0, i / 100f);
            yield return new WaitForSeconds(0.005f);
        }
        blackpanel_image.color = new Color(0, 0, 0, 0);

        //---------------------------------------------------------------[[ 스타트 매니저를 활성화 ]]
        managers.transform.Find("StartManager").gameObject.SetActive(true);
    }
}
