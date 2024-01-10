using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// īŸ�� ��ų ���� Ŭ����. @ȫ����
public class KatanaSkill : Skill
{    
    private Transform parent;
    private GameObject katana;
    private float swingAngle = 90f; // �ֵθ� ����
    private float swingSpeed = 10f; // �ֵθ��� �ӵ�
    private float swingDuration = 1.0f; // �ֵθ��� ���� �ð�
    private float swingTimer = 0.0f; // �ֵθ��� Ÿ�̸�
    private bool isSwinging = false; // �ֵθ��� �ִ��� ����


    // 2�ʿ� �ѹ� �� ���Ǹ� �� �� ���� 1�ʰ� �÷��̾ �ٶ󺸴� ���⿡ ��ä�� ���� ������ ���� 
    public KatanaSkill(Transform parent) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
    }
    public override void Update()
    {
        swingTimer += Time.deltaTime;

        // īŸ���� �ֵθ���.
        if (swingTimer < swingDuration)
        {
            if (!isSwinging)
            {
                CreateKatana();
            }
            RotateKatana();
            SwingKatana(); 
        }
        // īŸ�� �ֵθ��⸦ �����.
        else
        {
            isSwinging = false;
            ClearKatana(katana);
        }

        // īŸ�� �ֵθ��⸦ ���� ����
        if (swingTimer >= swingDuration)
        {
            // ��Ÿ�� ���
            cooldownTick += Time.deltaTime;
            if (cooldownTick <= Cooldown)
                return;
            cooldownTick = 0.0f;
            //             
            swingTimer = 0.0f;
        }
    }

    // īŸ���� �����Ѵ�.
    private void CreateKatana()
    {
        ClearKatana(katana);
        isSwinging = true;

        // īŸ���� �����Ѵ�.
        Transform player = Managers.Game.Player.transform;
        float offset = 0f; // player�� �߽����κ��� �󸶳� �ָ� ������ �� �����Ѵ�.
        Vector2 spawnPosition = new Vector2(player.position.x + offset, player.position.y /*+ offset*/);
        katana = Managers.Resource.Instantiate("Katana/Katana", parent);
        katana.transform.position = spawnPosition;
        katana.GetOrAddComponent<Katana>();
    }

    // īŸ���� �����Ѵ�.
    private void ClearKatana(GameObject katana)
    {
        if (katana != null)
        {
            Managers.Resource.Destroy(katana);
        }        
    }
    
    // �÷��̾ �ٶ󺸴� �������� īŸ���� ������ ȸ����Ų��.
    private void RotateKatana()
    {
        if (Managers.Game.Player == null)
        {
            Debug.Log("RotateKatana():: Error! Player class is null.");
            isSwinging = false;
            return;
        }

        Player player = Managers.Game.Player;        
        float angle = Mathf.Atan2(player.Direction.y, player.Direction.x) * Mathf.Rad2Deg;

        // �÷��̾��� ���⿡ ���߾� īŸ�� ȸ��
        katana.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // īŸ���� �ֵθ���. TODO: ���� �����۰��� ������� ����.
    private void SwingKatana()
    {
        // �ֵθ��� ������ ���� �պ� ȸ��
        float swingFactor = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        katana.transform.Rotate(Vector3.forward, swingFactor); // ����) īŸ�� sprite�� pivot: ���� ������ ���� / box collider x��: ������ +0.32 �߰�      
    }

}
