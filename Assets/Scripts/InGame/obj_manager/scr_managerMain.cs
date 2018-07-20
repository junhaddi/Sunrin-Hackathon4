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

    public int score = 0;
    public int cityMany;

    //  0.0 ~ 1.0
    public float happyPoint = 1f;
    public GameObject thisCity;

    public void SetHappyPoint()
    {
        float _happyPoint = 0;
        for (int i = 0; i < cityMany; i++)
        {
            float _city_people = (GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().peoples);
            _happyPoint += (1 - _city_people / 100) / cityMany;
        }

        happyPoint = _happyPoint;
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
    }
}
