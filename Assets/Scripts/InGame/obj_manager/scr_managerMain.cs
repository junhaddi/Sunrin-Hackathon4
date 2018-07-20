using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_managerMain : MonoBehaviour {

    GameObject managers;
    GameObject blackpanel;
    Text _Score;
    Image blackpanel_image;
    Image _happy_bar;
    scr_managerCity _cityAmount; 

    public int score = 0;

    //  0.0 ~ 1.0
    public float happyPoint = 1f;
    public GameObject thisCity;

    public void SetHappyPoint()
    {
        for (int i = 0; i < _cityAmount.cityAmount; i++)
        {
            float a = (GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().peoples);
            if (a >= (150 / _cityAmount.cityAmount))
            {
                happyPoint -= 3 / 35f * Time.deltaTime;
                
            }
            else
            {
                happyPoint += 1 / 100f * Time.deltaTime;
                if (happyPoint >= 1)
                    happyPoint = 1;
            }
            //happyPoint -= 1 / 10;
        }
    }

    public void GameOver()
    { 
        //GameOver!!
    }

    private void Awake()
    {
        managers = GameObject.Find("Managers");
        blackpanel = GameObject.Find("Blackpanel");
        blackpanel_image = blackpanel.GetComponent<Image>();

        _Score = GameObject.Find("Score").GetComponent<Text>();
        _happy_bar = GameObject.Find("happy_bar").GetComponent<Image>();
        _cityAmount = GameObject.Find("CityManager").GetComponent<scr_managerCity>();
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

    private void Update()
    {
        //  Set UI
        _Score.text = "Score\n" + score;
        _happy_bar.fillAmount = happyPoint;
        SetHappyPoint();
        Debug.Log(happyPoint);
    }
}
