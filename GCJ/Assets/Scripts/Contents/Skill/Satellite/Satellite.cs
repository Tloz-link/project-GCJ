using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    private int attack = 2;

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << (int)Define.ELayer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(attack);
        }
    }
}
