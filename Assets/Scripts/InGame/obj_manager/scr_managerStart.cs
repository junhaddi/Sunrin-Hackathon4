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
        if (Input.GetMouseButtonDown(0))
            cityManager.pushCity();
    }
}
