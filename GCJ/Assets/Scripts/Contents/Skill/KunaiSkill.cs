using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiSkill : Skill
{
    public KunaiSkill() : base(1, 2.0f, Define.SkillType.Kunai)
    {
    }

    public override void Update(Monster target = null)
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        Projectile proj = Managers.Resource.Instantiate("Projectile/Projectile").GetOrAddComponent<Projectile>();
        Vector2 direction = Vector2.zero;
        Transform player = Managers.Game.Player.transform;

        if (target == null)
        {
            float randomAngle = Random.Range(0f, 360f);
            float radianAngle = Mathf.Deg2Rad * randomAngle;
            direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
        }
        else
        {
            direction = target.transform.position - player.position;
        }

        proj.SetInfo(Attack, player.position, direction.normalized);
    }
}