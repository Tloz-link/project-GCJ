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
        AttackRocket(0);

        for (int i = 2; i <= SkillData.AtkCount; ++i)
        {
            float angle = (i / 2) * SkillData.AtkAngle;
            if (i % 2 == 1)
                angle *= -1;
            AttackRocket(angle);
        }
    }

    private void AttackRocket(float angle)
    {
        Rocket proj = Managers.Object.Spawn<Rocket>(Owner.transform.position, SkillData.ProjectileId);
        proj.SetSpawnInfo(Owner, this, Util.RotateVectorByAngle(Owner.Direction, angle));
    }
}
