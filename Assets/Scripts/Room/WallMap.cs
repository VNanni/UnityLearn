using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject map_0;
    private void OnEnable()
    {
        map_0 = transform.parent.GetChild(0).gameObject;

        map_0.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            map_0.SetActive(true);
        }
    }
}
