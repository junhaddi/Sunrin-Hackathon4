using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_managerCity : MonoBehaviour {

    int cityAmount;
    GameObject cities;

    public GameObject pre_city;

    public void Awake()
    {
        cities = GameObject.Find("Cities");
    }

    public void pushCity()
    {
        //---------------------------------------------------------------[[ 도시의 수를 파악 ]]
        cityAmount = 0;

        Transform[] cities = GameObject.FindObjectsOfType<Transform>();
        foreach (Transform city in cities)
        {
            if (city.tag == "city")
            {
                cityAmount++;
            }
        }

        //---------------------------------------------------------------[[ 도시를 이동, 생성 ]]
        for (int i = 0; i < cityAmount;i++)
        {
            StartCoroutine("moveCity", GameObject.Find("obj_city" + i));
        }
        StartCoroutine("newCity");
    }
    
    //---------------------------------------------------------------[[ 도시 이동 코루틴 ]]
    IEnumerator moveCity(GameObject city)
    {
        int cityNum = city.GetComponent<scr_cityMain>().cityNum;

        float angle_before = 360f / cityAmount * cityNum;
        float angle_after = 360f / (cityAmount + 1) * (cityNum + 1);

        Debug.Log(angle_before + ", " + angle_after);

        for (float i = angle_before; i <= angle_after; i += (angle_after - angle_before) / 40f)
        {
            city.transform.position = new Vector2(Mathf.Sin(i * Mathf.PI / 180) * 2.1f, Mathf.Cos(i * Mathf.PI / 180) * 2.1f);

            yield return new WaitForSeconds(0.005f);
        }
    }

    //---------------------------------------------------------------[[ 도시 생성 코루틴 ]]
    IEnumerator newCity()
    {
        yield return new WaitForSeconds(0.75f);

        GameObject NewObj = Instantiate(pre_city);
        NewObj.name = "obj_city" + cityAmount;
        NewObj.GetComponent<scr_cityMain>().cityNum = cityAmount;
        NewObj.transform.SetParent(cities.transform);
        NewObj.transform.position = new Vector2(0, 2.1f);
    }

}
