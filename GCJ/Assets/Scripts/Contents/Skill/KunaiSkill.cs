using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiSkill : Skill
{
    public KunaiSkill() : base(1, 2.0f, Define.SkillType.Kunai)
    {
    }

    public override void Update()
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        Projectile proj = Managers.Resource.Instantiate("Projectile/Projectile").GetOrAddComponent<Projectile>();
        Vector2 direction = Vector2.zero;
        Player player = Managers.Game.Player;

        if (player.Target == null)
        {
            float randomAngle = Random.Range(0f, 360f);
            float radianAngle = Mathf.Deg2Rad * randomAngle;
            direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
        }
        else
        {
            direction = player.Target.transform.position - player.transform.position;
        }

        proj.SetInfo(Attack, player.transform.position, direction.normalized);
    }
}