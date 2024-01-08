using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : Skill
{
    // ������ ������ Player Ŭ������ Init �Լ����� KatanaSkill�� �����ؼ� ����Ʈ�� �ְ� �����ϸ� �׽�Ʈ ����

    private Vector2 direction; // �÷��̾ �ٶ󺸴� ����
    // �߰�
    private Transform parent;
    private GameObject katana;    
    public float swingDuration = 1f; // �ֵθ��� ���� �ð�
    private float swingTimer = 0f; // �ֵθ��� Ÿ�̸�
    private bool isSwinging = false; // �ֵθ��� �ִ��� ����
    private float swingDirection = 1f; // �ֵθ��� ����    

    // 2�ʿ� �ѹ� �� ���Ǹ� �� �� ���� 1�ʰ� �÷��̾ �ٶ󺸴� ���⿡ ��ä�� ���� ������ ���� 
    public KatanaSkill(Transform parent, Vector2 direction) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
        this.direction = direction;
    }

    public override void Update()
    {

        // ��ų ���� ���� Updateó���� ���⼭
        if (!isSwinging)
        {
            StartSwing();
        }

        // ���� �� ������Ʈ
        if (isSwinging)
        {
            SwingWeapon();
        }

        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

    }

    private void StartSwing()
    {
        ClearKatana(katana);

        isSwinging = true;
        swingTimer = 0f;

        Vector2 spawnPosition = GetCirclePosition(360f, 0f);

        katana = Managers.Resource.Instantiate("Katana/Katana", parent);
        katana.transform.position = spawnPosition;
        katana.GetOrAddComponent<Katana>();
    }

    private void ClearKatana(GameObject katana)
    {
        if(katana != null)
        {
            Managers.Resource.Destroy(katana);
        }
    }
    
    private Vector2 GetCirclePosition(float angle, float radius) // SatelliteSkill�� GetCirclePosition�� ����
    {
        Transform player = Managers.Game.Player.transform;

        float radian = Mathf.Deg2Rad * angle;
        float x = player.position.x + Mathf.Cos(radian) * radius;
        float y = player.position.y + Mathf.Sin(radian) * radius;
        return new Vector2(x, y);
    }

    private void SwingWeapon()
    {
        swingTimer += Time.deltaTime;

        // ��ä�� ���� ���
        float swingProgress = swingTimer / swingDuration;
        float angle = Mathf.Lerp(0, 45 * swingDirection, swingProgress);
        parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // ���� ���� ����
        if (swingTimer >= swingDuration)
        {
            isSwinging = false;
            swingDirection *= -1; // ���� ��ȯ
        }
    }


}
