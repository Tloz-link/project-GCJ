using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// īŸ�� ��ü. Satellite�� ���� Contents������ ���ο�Katana ������ ����� �������. @ȫ����
public class Katana : MonoBehaviour
{
    private int attack = 1; // TODO: ��ġ�� ���� ����Ǿ� ����. 
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) // OnTriggerEnter2D�� �ٸ���?
    {
        Debug.Log("in");
        if (((1 << (int)Define.Layer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(attack);
        }
    }
}
