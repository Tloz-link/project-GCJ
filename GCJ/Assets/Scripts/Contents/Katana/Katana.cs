using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카타나 객체. Satellite와 같이 Contents폴더에 새로운Katana 폴더를 만들어 만들었음. @홍지형
public class Katana : MonoBehaviour
{
    private int attack = 5; // TODO: 수치가 임의 적용되어 있음. 
    void Update()
    {
        
    }

    //void OnTriggerEnter2D(Collider2D other) 
    //{
    //    if (((1 << (int)Define.Layer.Monster) & (1 << other.gameObject.layer)) != 0)
    //    {
    //        Monster monster = other.gameObject.GetComponent<Monster>();
    //        monster.Damaged(attack);
    //    }
    //}
}
