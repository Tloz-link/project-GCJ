using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int attack;
    private float speed = 5f;

    public void SetInfo(int attack, Vector2 position, Vector2 direction)
    {
        this.attack = attack;
        transform.position = position;

        float angle = Util.VectorToAngle(direction);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private float tick = 0f;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        tick += Time.deltaTime;
        if (tick > 5f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    private bool isCollided = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        BaseObject target = other.GetComponent<BaseObject>();
        if (target.IsValid() == false)
            return;

        Creature creature = target as Creature;
        if (creature.CreatureType != Define.ECreatureType.Monster)
            return;

        if (isCollided)
            return;

        creature.OnDamaged(Managers.Object.Hero);

        Managers.Resource.Destroy(gameObject);
        isCollided = true;
    }
}
