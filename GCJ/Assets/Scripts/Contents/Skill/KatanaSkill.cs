using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : Skill
{
    // 구현이 끝나고 Player 클래스의 Init 함수에서 KatanaSkill을 생성해서 리스트에 넣고 실행하면 테스트 가능

    private Vector2 direction; // 플레이어가 바라보는 방향
    // 추가
    private Transform parent;
    private GameObject katana;    
    public float swingDuration = 1f; // 휘두르는 지속 시간
    private float swingTimer = 0f; // 휘두르는 타이머
    private bool isSwinging = false; // 휘두르고 있는지 여부
    private float swingDirection = 1f; // 휘두르는 방향    

    // 2초에 한번 씩 사용되며 한 번 사용시 1초간 플레이어가 바라보는 방향에 부채꼴 범위 데미지 판정 
    public KatanaSkill(Transform parent, Vector2 direction) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
        this.direction = direction;
    }

    public override void Update()
    {

        // 스킬 연출 관련 Update처리는 여기서
        if (!isSwinging)
        {
            StartSwing();
        }

        // 스윙 중 업데이트
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
    
    private Vector2 GetCirclePosition(float angle, float radius) // SatelliteSkill의 GetCirclePosition과 동일
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

        // 부채꼴 스윙 계산
        float swingProgress = swingTimer / swingDuration;
        float angle = Mathf.Lerp(0, 45 * swingDirection, swingProgress);
        parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 스윙 종료 조건
        if (swingTimer >= swingDuration)
        {
            isSwinging = false;
            swingDirection *= -1; // 방향 전환
        }
    }


}
