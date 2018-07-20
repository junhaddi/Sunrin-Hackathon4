using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_managerCity : MonoBehaviour {

    public int cityAmount = 2;
    public float newTargetDelay = 0;
    public float peopleMoveDelay = 0;
    public Sprite[] sprites = new Sprite[7];

    scr_managerMain ManagerMain;
    GameObject cities;
    GameObject city2;

    public GameObject pre_city;

    private void Start()
    {
        StartCoroutine(PushCity());
    }

    public void Awake()
    {
        cityAmount = 2;
        cities = GameObject.Find("Cities");
        ManagerMain = GameObject.Find("MainManager").GetComponent<scr_managerMain>();
    }

    public void Update()
    {
        if (ManagerMain.happyPoint > 0.01f)
        {
            //---------------------------------------------------------------[[ 집중 타겟을 변경 ]]
            newTargetDelay += Time.deltaTime;
            if (newTargetDelay >= 6f)
            {
                newTargetDelay -= 6f;
                newTarget();
            }

            //---------------------------------------------------------------[[ 인구가 이동 ]]
            peopleMoveDelay += Time.deltaTime;
            if (peopleMoveDelay >= 1.2f)
            {
                peopleMoveDelay -= 1.2f;
                for (int i = 0; i < cityAmount; i++)
                {
                    if (GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().isTarget)
                        city2 = GameObject.Find("obj_city" + i);
                }

                for (int i = 0; i < cityAmount; i++)
                {
                    if (Random.Range(0, 6) != 1 && cityAmount > 1)
                    {
                        GameObject city1 = GameObject.Find("obj_city" + i);

                        if (Random.Range(0, 10) <= 2)
                        {
                            city1.GetComponent<scr_cityMain>().other_city = city2;
                            city1.GetComponent<scr_cityMain>().MovePeople(35 / (cityAmount + 1));
                        }
                        else
                        {
                            int rand;
                            do
                            {
                                rand = Random.Range(0, cityAmount);
                                if (rand != i)
                                {
                                    city1.GetComponent<scr_cityMain>().other_city = GameObject.Find("obj_city" + rand);
                                    city1.GetComponent<scr_cityMain>().MovePeople(35 / (cityAmount + 1));
                                }
                            } while (rand == i);
                        }
                    }
                }
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

        float angle_before = Mathf.Atan2(-city.transform.position.y, city.transform.position.x) * Mathf.Rad2Deg + 90;
        float angle_after = 360f / (cityAmount + 1) * (cityNum - cityAmount);

        while (angle_before < 0)
            angle_before += 360;

        while (angle_before > 360)
            angle_before -= 360;

        while (angle_after > 360)
            angle_after -= 360;

        while (angle_after < 0)
            angle_after += 360;

        if (angle_before < angle_after)
        {
            for (float i = angle_before; i <= angle_after; i += (angle_after - angle_before) / 40f)
            {
                city.transform.position = new Vector2(Mathf.Sin(i * Mathf.PI / 180) * 2.1f, Mathf.Cos(i * Mathf.PI / 180) * 2.1f);

                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            for (float i = angle_before; i >= angle_after; i += (angle_after - angle_before) / 40f)
            {
                city.transform.position = new Vector2(Mathf.Sin(i * Mathf.PI / 180) * 2.1f, Mathf.Cos(i * Mathf.PI / 180) * 2.1f);

                yield return new WaitForSeconds(0.005f);
            }
        }
    }

    //---------------------------------------------------------------[[ 도시 생성 코루틴 ]]
    IEnumerator newCity()
    {
        yield return new WaitForSeconds(0.75f);

        GameObject NewObj = Instantiate(pre_city);
        NewObj.name = "obj_city" + cityAmount;
        NewObj.GetComponent<scr_cityMain>().cityNum = cityAmount;
        NewObj.GetComponent<scr_cityMain>().peoples = 0;
        NewObj.transform.SetParent(cities.transform);
        NewObj.transform.position = new Vector2(0, 2.1f);
        NewObj.GetComponent<SpriteRenderer>().sprite = sprites[cityAmount % 7];

        for (int i = 0; i < cityAmount; i++)
        {
            GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().other_city = NewObj;
            GameObject.Find("obj_city" + i).GetComponent<scr_cityMain>().MovePeople((100 / (cityAmount + 1)) / cityAmount);
        }
    }

    // 일정 시간마다 도시 추가
    IEnumerator PushCity()
    {
        yield return new WaitForSeconds(10f);
        pushCity();
        yield return new WaitForSeconds(20f);
        pushCity();
        yield return new WaitForSeconds(20f);
        pushCity();
        yield return new WaitForSeconds(20f);
        pushCity();
    }

}
