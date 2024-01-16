using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카타나 스킬 구현 클래스. @홍지형
public class KatanaSkill : Skill
{    
    private Transform parent;
    private GameObject katana;

    private bool isSwinging = false; // 휘두르고 있는지 여부
    private float swingDuration = 0.5f; // 휘두르는 지속 시간
    private float swingTimer = 0f; // 휘두르는 타이머
    public float detectionRadius = 0f; // 감지 반경
    public float angle = 90f; // 부채꼴의 각도
    Vector2 swingDirection = Vector2.zero; // 카타나를 휘두르기 시작하는 시점의 player가 바라보는 방향

    // 2초에 한번 씩 사용되며 한 번 사용시 0.5초간 플레이어가 바라보는 방향에 부채꼴 범위 데미지 판정 
    public KatanaSkill(Transform parent) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
    }
    public override void Update()
    {
        swingTimer += Time.deltaTime;
        // 지속시간 동안만 카타나를 휘두른다.
        if (swingTimer < swingDuration)
        {
            if (!isSwinging)
            {
                CreateKatana();
            }
                SwingKatana();
        }
        // 카타나 휘두르기를 멈춘다.
        else
        {
            isSwinging = false;
            ClearKatana(katana);
        }

        // 카타나 휘두르기를 멈춘 이후
        if (swingTimer >= swingDuration)
        {
            // 쿨타임 대기
            cooldownTick += Time.deltaTime;
            if (cooldownTick <= Cooldown)
                return;
            cooldownTick = 0.0f;
            //             
            swingTimer = 0.0f;
        }
    }

    // 카타나를 생성한다.
    private void CreateKatana()
    {
        ClearKatana(katana);
        isSwinging = true;
        
        Player player = Managers.Game.Player;

        // player의 중심으로부터 얼마나 멀리 떨어질 지 결정한다.
        // float offset = 0f; 
        // Vector2 spawnPosition = new Vector2(player.transform.position.x + offset, player.transform.position.y /*+ offset*/);

        Vector2 spawnPosition = new Vector2(player.transform.position.x, player.transform.position.y);        
        katana = Managers.Resource.Instantiate("Katana/Katana", parent);
        katana.transform.position = spawnPosition;
        katana.GetOrAddComponent<Katana>();

        // 생성할 방향을 정한다.
        float angle = Mathf.Atan2(player.Direction.y, player.Direction.x) * Mathf.Rad2Deg; 
        angle -= 45f; // 카타나를 player가 바라보는 정가운데로 회전시킨다.
        // 플레이어의 방향에 맞추어 카타나 회전
        katana.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // SwingKatana() 시작 시점의 방향을 기억한다.
        swingDirection = Managers.Game.Player.Direction; 
    }

    // 카타나를 휘두른다. (== 적의 피격을 감지한다)
    private void SwingKatana()
    {
        // 카타나의 반지름을 구한다.
        CircleCollider2D circleCollider = katana.GetComponent<CircleCollider2D>();
        detectionRadius = circleCollider.radius * katana.transform.localScale.x; // x기준 스케일 보정값을 곱함
        
        // 콜라이더들을 카타나의 범위 안에서 찾는다.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(katana.transform.position, detectionRadius);

        DrawKatanaRange(); // 디버그용. 부채꼴 경계선 시각화

        foreach (var hitCollider in hitColliders)
        {
            Vector2 directionToTarget = (hitCollider.transform.position - katana.transform.position).normalized;
            float angleToTarget = Vector2.Angle(swingDirection, directionToTarget);

            DrawKatanaTarget(angleToTarget, directionToTarget); // 디버그용. 타겟 시각화

            if (angleToTarget < angle / 2) // 부채꼴 범위 내에 있는지 확인
            {
                OnTriggerEnter(hitCollider);
            }
        }        
    }

    // 피격 감지 시 동작
    private void OnTriggerEnter(Collider2D other)
    {
        int attack = 5; // TODO: 수치가 임의 적용되어 있음. 
        if (((1 << (int)Define.Layer.Monster) & (1 << other.gameObject.layer)) != 0)
        {
            // Debug.Log("타겟 감지: " + other.gameObject.name);
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.Damaged(attack);
        }
    }

    // 카타나를 제거한다.
    private void ClearKatana(GameObject katana)
    {
        if (katana != null)
        {
            Managers.Resource.Destroy(katana);
        }        
    }

    // 디버그용. 부채꼴 경계선 시각화
    private void DrawKatanaRange()
    {
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -angle / 2) * swingDirection;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, angle / 2) * swingDirection;
        Debug.DrawRay(katana.transform.position, leftBoundary * detectionRadius, Color.blue);
        Debug.DrawRay(katana.transform.position, rightBoundary * detectionRadius, Color.blue);
    }

    // 디버그용. 타겟 시각화
    private void DrawKatanaTarget(float angleToTarget, Vector2 directionToTarget)
    {        
        Color lineColor = angleToTarget < angle / 2 ? Color.red : Color.gray;
        Debug.DrawRay(katana.transform.position, directionToTarget * detectionRadius, lineColor);
    }

    // 플레이어의 바라보는 방향을 구한다. 리팩토링 시 사용할것. 삭제해도됨
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
