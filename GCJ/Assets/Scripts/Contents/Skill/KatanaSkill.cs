using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : Skill
{    
    private Transform parent;
    private float angleRange = 90;

    public KatanaSkill(Transform parent) : base(1, 2.0f, Define.ESkillType.Katana)
    {
        this.parent = parent;
    }

    public override void Update()
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        PlaySkill();
    }

    private void PlaySkill()
    {
        Hero hero = Managers.Object.Hero;
        Area katana = Managers.Resource.Instantiate("Area/Quarter", parent).GetOrAddComponent<Area>();
        katana.SetInfo(hero.transform.position, 0.2f);

        float angle = Mathf.Atan2(hero.Direction.y, hero.Direction.x) * Mathf.Rad2Deg;
        katana.transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);

        CircleCollider2D circleCollider = katana.GetComponent<CircleCollider2D>();
        float detectionRadius = circleCollider.radius * katana.transform.localScale.x;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(katana.transform.position, detectionRadius);

        foreach (var hitCollider in hitColliders)
        {
            Vector2 directionToTarget = (hitCollider.transform.position - katana.transform.position).normalized;
            float angleToTarget = Vector2.Angle(hero.Direction, directionToTarget);

            if (angleToTarget < angleRange / 2)
            {
                OnTriggerEnter(hitCollider);
            }
        }
    }

    private void OnTriggerEnter(Collider2D other)
    {
        if (((1 << (int)Define.ELayer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(Attack);
        }
    }
}
