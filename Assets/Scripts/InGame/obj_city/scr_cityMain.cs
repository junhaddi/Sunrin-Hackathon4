using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cityMain : MonoBehaviour {
    enum MATTER
    {
        ROAD, HOUSE, NATURE, CRIME, HIGHWAY
    };
    public GameObject other_city;
    public GameObject Line;
    Transform mask;

    public int cityNum = 0;
    public bool isTarget = false;
    public float peoples = 50;
    public int matter = 0;

    public bool pro_crash = false;
    public bool pro_house = false;
    public bool pro_road = false;
    public bool pro_env = false;

    scr_managerMain managerMain;
    scr_managerCity managerCity;
    scr_managerCity _citiAmount;
    scr_managerMain GetScore;

    public float moveRate;
    public float nextMove = 0;

    private void OnMouseUp()
    {
        if (managerMain.thisCity && Time.time > nextMove)
        {
            nextMove = Time.time + moveRate;
            other_city = managerMain.thisCity;

            if (other_city.GetComponent<scr_cityMain>().peoples > peoples)
            {
                MovePeople(45 / (managerCity.cityAmount + 1));

                GetScore.score += 45 / (managerCity.cityAmount + 1);
            }
            else
            {
                float bestPeoples = 100/(managerCity.cityAmount + 1);
                float people1 = Mathf.Abs(other_city.GetComponent<scr_cityMain>().peoples - bestPeoples);
                float people2 = Mathf.Abs(peoples - bestPeoples);
                MovePeople((int)(people1 + people2 / 2 * 0.8f));

                GetScore.score += (int)(people1 + people2 / 2 * 0.8f);
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
        mask = transform.Find("Mask");
        managerMain = GameObject.Find("MainManager").GetComponent<scr_managerMain>();
        managerCity = GameObject.Find("CityManager").GetComponent<scr_managerCity>();
        _citiAmount = GameObject.Find("CityManager").GetComponent<scr_managerCity>();
        GetScore = GameObject.Find("MainManager").GetComponent<scr_managerMain>();
    }

    private void Update()
    {
        if (managerMain.happyPoint > 0.01f)
        {
            //  Set Move DelayLate
            if (matter != (int)MATTER.HIGHWAY)
            {
                moveRate = 3f / _citiAmount.cityAmount;
            }
            else
            {
                //  Item Active
                moveRate = 1.5f / _citiAmount.cityAmount;
            }

            //  Change Target Color

            if (peoples * 6 / (165 / _citiAmount.cityAmount) > 4)
            {
                mask.localScale = new Vector2(4, 4);
            }
            else
            {
                mask.localScale = new Vector2(4, peoples * 6 / (165 / _citiAmount.cityAmount));
            }
            mask.localPosition = new Vector2(0, (mask.localScale.y / 16f) - 0.3f);
        }


    }

    public void MovePeople(int scale)
    {
        float x = other_city.transform.position.x - transform.position.x;
        float y = other_city.transform.position.y - transform.position.y;

        GameObject line = Instantiate(Line, new Vector3((other_city.transform.position.x + transform.position.x) / 2, (other_city.transform.position.y + transform.position.y) / 2, 2), Quaternion.Euler(0, 0, Mathf.Atan2(y, x) * Mathf.Rad2Deg));
        line.transform.localScale = new Vector2(Vector3.Distance(other_city.transform.position, transform.position) / 4f * 10f, scale / 20f);

        switch(cityNum % 7)
        {
            case 0:
                line.GetComponent<SpriteRenderer>().color = new Color(18 / 255f, 18 / 255f, 141 / 255f);
                break;
            case 1:
                line.GetComponent<SpriteRenderer>().color = new Color(131 / 255f, 84 / 255f, 155 / 255f);
                break;
            case 2:
                line.GetComponent<SpriteRenderer>().color = new Color(191 / 255f, 97 / 255f, 146 / 255f);
                break;
            case 3:
                line.GetComponent<SpriteRenderer>().color = new Color(184 / 255f, 191 / 255f, 10 / 255f);
                break;
            case 4:
                line.GetComponent<SpriteRenderer>().color = new Color(24 / 255f, 24 / 255f, 24 / 255f);
                break;
            case 5:
                line.GetComponent<SpriteRenderer>().color = new Color(32 / 255f, 32 / 255f, 106 / 255f);
                break;
            case 6:
                line.GetComponent<SpriteRenderer>().color = new Color(130 / 255f, 94 / 255f, 59 / 255f);
                break;
        }

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
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
