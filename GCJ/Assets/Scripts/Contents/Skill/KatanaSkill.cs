using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : Skill
{
    // ������ ������ Player Ŭ������ Init �Լ����� KatanaSkill�� �����ؼ� ����Ʈ�� �ְ� �����ϸ� �׽�Ʈ ����

    private Vector2 direction; // �÷��̾ �ٶ󺸴� ����

    // 2�ʿ� �ѹ� �� ���Ǹ� �� �� ���� 1�ʰ� �÷��̾ �ٶ󺸴� ���⿡ ��ä�� ���� ������ ���� 
    public KatanaSkill(Vector2 direction) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.direction = direction;
    }

    public override void Update(Monster target = null)
    {
        // ��ų ���� ���� Updateó���� ���⼭

        //////////////////////////////
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;
        //////////////////////////////

        // ��Ÿ�� ������ ��ų ��� ������ �˸��� ����
    }
}
