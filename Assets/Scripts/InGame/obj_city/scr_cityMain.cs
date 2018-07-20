﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cityMain : MonoBehaviour {

    TextMesh text;

    public GameObject other_city;

    public int cityNum = 0;
    public bool isTarget = false;
    public float peoples = 50;

    public bool pro_crash = false;
    public bool pro_house = false;
    public bool pro_road = false;
    public bool pro_env = false;

    scr_managerMain managerMain;
    scr_managerCity managerCity;

    public float moveRate;
    public float nextMove = 0;

    private void OnMouseUp()
    {
        if (managerMain.thisCity && Time.time > nextMove)
        {
            nextMove += Time.time + moveRate;
            other_city = managerMain.thisCity;

            if (other_city.GetComponent<scr_cityMain>().peoples > peoples)
                MovePeople(45/(managerCity.cityAmount + 1));
            else
            {
                float bestPeoples = 100/(managerCity.cityAmount + 1);
                float people1 = Mathf.Abs(other_city.GetComponent<scr_cityMain>().peoples - bestPeoples);
                float people2 = Mathf.Abs(peoples - bestPeoples);
                MovePeople((int)(people1 + people2 / 2 * 0.8f));
            }
            managerMain.thisCity = null;
        }
    }

    private void OnMouseEnter()
    {
        managerMain.thisCity = gameObject;
    }

    private void OnMouseExit()
    {
        managerMain.thisCity = null;
    }

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMesh>();
        managerMain = GameObject.Find("MainManager").GetComponent<scr_managerMain>();
        managerCity = GameObject.Find("CityManager").GetComponent<scr_managerCity>();
    }

    private void Update()
    {
        //  Set Move DelayLate
        moveRate = 8f / GameObject.Find("CityManager").GetComponent<scr_managerCity>().cityAmount;

        if (isTarget)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        else
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        text.text = peoples + "%";
    }

    public void MovePeople(int scale)
    {
        StartCoroutine("MovePeopleC", scale);
    }

    IEnumerator MovePeopleC(int scale)
    {
        if (other_city != null)
        {
            scr_cityMain other_cityMain = other_city.GetComponent<scr_cityMain>();
            other_city = null;

            for (int i = 0; i < scale; i++)
            {
                if (peoples >= 1)
                {
                    peoples--;
                    other_cityMain.peoples++;
                }
                yield return new WaitForSeconds(0.06f);
            }
        }
    }
}
