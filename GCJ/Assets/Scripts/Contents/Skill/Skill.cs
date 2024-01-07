using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public int Attack { get; private set; }
    public float Cooldown { get; private set; }
    public Define.SkillType SkillType { get; private set; }

    protected float cooldownTick;
    public Skill(int attack, float cooldown, Define.SkillType skillType)
    {
        Attack = attack;
        Cooldown = cooldown;
        SkillType = skillType;

        cooldownTick = cooldown; // ó������ �ٷ� ���
    }
    
    public abstract void Update();
}
