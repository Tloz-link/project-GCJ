using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiSkill : Skill
{
    public KunaiSkill() : base(1, 2.0f, Define.ESkillType.Kunai)
    {
    }

    public override void Update()
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        Vector2 direction = Vector2.zero;
        Hero hero = Managers.Object.Hero;
        Monster target = Managers.Object.FindClosestMonster(hero.transform.position);

        if (target == null)
        {
            float randomAngle = Random.Range(0f, 360f);
            float radianAngle = Mathf.Deg2Rad * randomAngle;
            direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
        }
        else
        {
            direction = target.transform.position - hero.transform.position;
        }

        Projectile proj = Managers.Resource.Instantiate("Projectile/Projectile").GetOrAddComponent<Projectile>();
        proj.SetInfo(Attack, hero.transform.position, direction.normalized);
    }
}