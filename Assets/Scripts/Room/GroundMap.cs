using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMap : MonoBehaviour
{
    GameObject map;
    GameObject map_1;
    private void OnEnable()
    {
        map = transform.parent.gameObject;

        map.SetActive(false);
        //map_1.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            map.SetActive(true);
        }
    }

    public void UpdateState()
    {
        map.SetActive(true);
    }
}
