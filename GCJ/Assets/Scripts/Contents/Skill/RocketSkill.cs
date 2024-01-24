using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSkill : Skill
{
    public RocketSkill() : base(3, 4.0f, Define.ESkillType.Rocket)
    {
    }

    public override void Update()
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        Rocket rocket = Managers.Resource.Instantiate("Projectile/Rocket").GetOrAddComponent<Rocket>();
        Hero hero = Managers.Object.Hero;
        rocket.SetInfo(Attack, hero.transform.position, hero.Direction);
    }
}
