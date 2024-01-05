using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int attack;
    private float speed = 5f;
    private Vector2 direction = Vector2.zero;

    public void SetInfo(int attack, Vector2 position, Vector2 direction)
    {
        this.attack = attack;
        transform.position = position;
        this.direction = direction;
    }

    private float tick = 0f;
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        tick += Time.deltaTime;
        if (tick > 5f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    private bool isCollided = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << (int)Define.Layer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            if (isCollided)
                return;

            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(attack);

            Managers.Resource.Destroy(gameObject);
            isCollided = true;
        }
    }
}
