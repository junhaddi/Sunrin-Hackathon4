using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_managerStart : MonoBehaviour {

    scr_managerCity cityManager;

    private void Awake()
    {
        cityManager = GameObject.Find("CityManager").GetComponent<scr_managerCity>();
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            cityManager.pushCity();
        else if (Input.GetMouseButtonDown(2))
        {
            for (int i = 0; i < cityManager.cityAmount; i++)
            {
                GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().other_city = GameObject.Find("obj_city1");
                GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().MovePeople(10);
            }
        }
    }
}
