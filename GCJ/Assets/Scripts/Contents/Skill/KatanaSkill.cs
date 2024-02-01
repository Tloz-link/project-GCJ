using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : SkillBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void SetInfo(Creature owner, int skillTemplateID)
    {
        base.SetInfo(owner, skillTemplateID);
    }

    public override void DoSkill()
    {
        Hero hero = Managers.Object.Hero;
        Area katana = Managers.Resource.Instantiate("Area/Quarter", transform).GetOrAddComponent<Area>();
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

            if (angleToTarget < SkillData.AngleRange / 2)
            {
                CheckEnemyInRange(hitCollider);
            }
        }
    }

    private void CheckEnemyInRange(Collider2D other)
    {
        if (((1 << (int)Define.ELayer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.OnDamaged(Owner, this);
        }
    }
}
