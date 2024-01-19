using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSkill : Skill
{
    public RocketSkill() : base(3, 4.0f, Define.SkillType.Rocket)
    {
    }

    public override void Update()
    {
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        Rocket rocket = Managers.Resource.Instantiate("Projectile/Rocket").GetOrAddComponent<Rocket>();
        Player player = Managers.Game.Player;
        rocket.SetInfo(Attack, player.transform.position, player.Direction);
    }
}
