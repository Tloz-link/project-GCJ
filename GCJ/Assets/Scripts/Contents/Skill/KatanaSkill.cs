using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// īŸ�� ��ų ���� Ŭ����. @ȫ����
public class KatanaSkill : Skill
{    
    private Transform parent;
    private GameObject katana;

    private bool isSwinging = false; // �ֵθ��� �ִ��� ����
    private float swingDuration = 0.5f; // �ֵθ��� ���� �ð�
    private float swingTimer = 0f; // �ֵθ��� Ÿ�̸�
    public float detectionRadius = 0f; // ���� �ݰ�
    public float angle = 90f; // ��ä���� ����
    Vector2 swingDirection = Vector2.zero; // īŸ���� �ֵθ��� �����ϴ� ������ player�� �ٶ󺸴� ����

    // 2�ʿ� �ѹ� �� ���Ǹ� �� �� ���� 0.5�ʰ� �÷��̾ �ٶ󺸴� ���⿡ ��ä�� ���� ������ ���� 
    public KatanaSkill(Transform parent) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
    }
    public override void Update()
    {
        swingTimer += Time.deltaTime;
        // ���ӽð� ���ȸ� īŸ���� �ֵθ���.
        if (swingTimer < swingDuration)
        {
            if (!isSwinging)
            {
                CreateKatana();
            }
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
        
        Player player = Managers.Game.Player;

        // player�� �߽����κ��� �󸶳� �ָ� ������ �� �����Ѵ�.
        // float offset = 0f; 
        // Vector2 spawnPosition = new Vector2(player.transform.position.x + offset, player.transform.position.y /*+ offset*/);

        Vector2 spawnPosition = new Vector2(player.transform.position.x, player.transform.position.y);        
        katana = Managers.Resource.Instantiate("Katana/Katana", parent);
        katana.transform.position = spawnPosition;
        katana.GetOrAddComponent<Katana>();

        // ������ ������ ���Ѵ�.
        float angle = Mathf.Atan2(player.Direction.y, player.Direction.x) * Mathf.Rad2Deg; 
        angle -= 45f; // īŸ���� player�� �ٶ󺸴� ������� ȸ����Ų��.
        // �÷��̾��� ���⿡ ���߾� īŸ�� ȸ��
        katana.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // SwingKatana() ���� ������ ������ ����Ѵ�.
        swingDirection = Managers.Game.Player.Direction; 
    }

    // īŸ���� �ֵθ���. (== ���� �ǰ��� �����Ѵ�)
    private void SwingKatana()
    {
        // īŸ���� �������� ���Ѵ�.
        CircleCollider2D circleCollider = katana.GetComponent<CircleCollider2D>();
        detectionRadius = circleCollider.radius * katana.transform.localScale.x; // x���� ������ �������� ����
        
        // �ݶ��̴����� īŸ���� ���� �ȿ��� ã�´�.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(katana.transform.position, detectionRadius);

        DrawKatanaRange(); // ����׿�. ��ä�� ��輱 �ð�ȭ

        foreach (var hitCollider in hitColliders)
        {
            Vector2 directionToTarget = (hitCollider.transform.position - katana.transform.position).normalized;
            float angleToTarget = Vector2.Angle(swingDirection, directionToTarget);

            DrawKatanaTarget(angleToTarget, directionToTarget); // ����׿�. Ÿ�� �ð�ȭ

            if (angleToTarget < angle / 2) // ��ä�� ���� ���� �ִ��� Ȯ��
            {
                OnTriggerEnter(hitCollider);
            }
        }        
    }

    // �ǰ� ���� �� ����
    private void OnTriggerEnter(Collider2D other)
    {
        int attack = 5; // TODO: ��ġ�� ���� ����Ǿ� ����. 
        if (((1 << (int)Define.Layer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            // Debug.Log("Ÿ�� ����: " + other.gameObject.name);
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(attack);
        }
    }

    // īŸ���� �����Ѵ�.
    private void ClearKatana(GameObject katana)
    {
        if (katana != null)
        {
            Managers.Resource.Destroy(katana);
        }        
    }

    // ����׿�. ��ä�� ��輱 �ð�ȭ
    private void DrawKatanaRange()
    {
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -angle / 2) * swingDirection;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, angle / 2) * swingDirection;
        Debug.DrawRay(katana.transform.position, leftBoundary * detectionRadius, Color.blue);
        Debug.DrawRay(katana.transform.position, rightBoundary * detectionRadius, Color.blue);
    }

    // ����׿�. Ÿ�� �ð�ȭ
    private void DrawKatanaTarget(float angleToTarget, Vector2 directionToTarget)
    {        
        Color lineColor = angleToTarget < angle / 2 ? Color.red : Color.gray;
        Debug.DrawRay(katana.transform.position, directionToTarget * detectionRadius, lineColor);
    }

    // �÷��̾��� �ٶ󺸴� ������ ���Ѵ�. �����丵 �� ����Ұ�. �����ص���
    //private Vector2 GetPlayerDirection()
    //{
    //    Vector2 playerDirection = Vector2.zero;

    //    if (Managers.Game.Player.Direction != null)
    //    {
    //        playerDirection = Managers.Game.Player.Direction;
    //    }

    //    return playerDirection;
    //}

}
