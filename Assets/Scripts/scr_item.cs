
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_item : MonoBehaviour {

    /*  GameSystem
        0 : None
        1 : City + Road
        2 : House + Crime
        3 : All
        4 : Fast Drag   */
    public int item = 0;
    GameObject touch_city;

    //  Find GameSystem
    GameObject inGameSys;

    //  Skill Cool Time
    public float skillRate = 20f;
    float nextSkill = 0;

    private void Awake()
    {
        inGameSys = GameObject.Find("드래그 쿨타임 가지고있는 오브젝트");    
    }

    private void Update()
    {
        //  Active Skill
        if (Input.GetMouseButtonDown(0) && Time.time > nextSkill)
        {
            nextSkill = Time.time + skillRate;

            item = Random.Range(0, 5);
            switch (item)
            {
                case 0:
                    //  Reset
                    //inGameSys.드래그쿨타임 = 원래기본값;
                    break;
                case 1:
                    //touch_city.교통_주거문제_제거함수();
                    break;
                case 2:
                    //touch_city.범죄_주거문제_제거함수();
                    break;
                case 3:
                    //touch_city.모든문제_제거함수();
                    break;
                case 4:
                    //inGameSys.드래그쿨타임 = 줄어든값;
                    break;
            }
        }
    }
}
