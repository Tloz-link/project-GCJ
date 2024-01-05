using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSkill : Skill
{
    // 구현이 끝나고 Player 클래스의 Init 함수에서 KatanaSkill을 생성해서 리스트에 넣고 실행하면 테스트 가능

    private Vector2 direction; // 플레이어가 바라보는 방향

    // 2초에 한번 씩 사용되며 한 번 사용시 1초간 플레이어가 바라보는 방향에 부채꼴 범위 데미지 판정 
    public KatanaSkill(Vector2 direction) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.direction = direction;
    }

    public override void Update(Monster target = null)
    {
        // 스킬 연출 관련 Update처리는 여기서

        //////////////////////////////
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;
        //////////////////////////////

        // 쿨타임 지나고 스킬 사용 시작을 알리는 영역
    }
}
