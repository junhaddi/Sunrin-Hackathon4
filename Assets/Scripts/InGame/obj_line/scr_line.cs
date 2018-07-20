using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_line : MonoBehaviour {

    SpriteRenderer renderer;

    private void Start()
    {
        StartCoroutine("disappear");
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator disappear()
    {
        for (int i = 100; i >= 0; i -= 4)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, i / 100f);
            yield return new WaitForSeconds(0.005f);
        }
        Destroy(gameObject);
    }
}
