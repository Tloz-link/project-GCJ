using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSkill : SkillBase
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
        Rocket rocket = Managers.Object.Spawn<Rocket>(Owner.transform.position, SkillData.ProjectileId);
        rocket.SetSpawnInfo(Owner, this, Owner.Direction);
    }
}
