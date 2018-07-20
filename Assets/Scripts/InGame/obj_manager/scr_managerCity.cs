using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_managerCity : MonoBehaviour {

    public int cityAmount;
    public float newTargetDelay = 0;
    public float peopleMoveDelay = 0;
    GameObject cities;

    public GameObject pre_city;

    public void Awake()
    {
        cities = GameObject.Find("Cities");
    }

    public void Update()
    {
        //---------------------------------------------------------------[[ 집중 타겟을 변경 ]]
        newTargetDelay += Time.deltaTime * 5;
        if (newTargetDelay >= 15f)
        {
            newTargetDelay -= 15;
            newTarget();
        }

        //---------------------------------------------------------------[[ 인구가 이동 ]]
        peopleMoveDelay += Time.deltaTime;
        if (peopleMoveDelay >= 1f)
        {
            peopleMoveDelay -= 1;
            for (int i = 0; i < cityAmount; i++)
            {
                //StartCoroutine("moveCity", GameObject.Find("obj_city" + i));
            }
        }
    }

    void newTarget()
    {
        //---------------------------------------------------------------[[ 집중 타겟을 변경하는 함수 ]]
        for (int i = 0; i < cityAmount; i++)
        {
            GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().isTarget = false;
        }

        int random = Random.Range(0, cityAmount);
        GameObject.Find("obj_city" + random).GetComponent<scr_cityMain>().isTarget = true;
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
