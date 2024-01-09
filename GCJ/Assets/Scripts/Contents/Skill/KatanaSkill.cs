using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카타나 스킬 구현 클래스. @홍지형
public class KatanaSkill : Skill
{    
    private Transform parent;
    private GameObject katana;    
    private float swingAngle = 45f; // 휘두를 각도
    private float swingSpeed = 5f; // 휘두르는 속도    
    private bool isSwinging = false; // 휘두르고 있는지 여부

    // 2초에 한번 씩 사용되며 한 번 사용시 1초간 플레이어가 바라보는 방향에 부채꼴 범위 데미지 판정 
    public KatanaSkill(Transform parent) : base(1, 2.0f, Define.SkillType.Katana)
    {
        this.parent = parent;
    }
    public override void Update()
    {      
        if (!isSwinging)
        {
            CreateKatana();
        }

        // 스윙 중 업데이트
        if (isSwinging)
        {            
            RotateKatana(); // 플레이어가 바라보는 방향으로 카타나를 회전시킨다.

            SwingKatana(); // 카타나를 휘두른다.
        }

        // TODO: 쿨다운 현재 미구현.
        Debug.Log("cooldownTick:" + cooldownTick);
        Debug.Log("Cooldown:" + Cooldown);
        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;
        // 

        // 쿨타임 지나고 스킬 사용 시작을 알리는 영역
        isSwinging = false;
    }

    // 카타나를 생성한다.
    private void CreateKatana()
    {
        ClearKatana(katana);
        isSwinging = true;

        // 카타나를 생성한다.
        Transform player = Managers.Game.Player.transform;
        float offset = 0f; // player의 중심으로부터 얼마나 멀리 떨어질 지 결정한다.
        Vector2 spawnPosition = new Vector2(player.position.x + offset, player.position.y /*+ offset*/);
        katana = Managers.Resource.Instantiate("Katana/Katana", parent);
        katana.transform.position = spawnPosition;
        katana.GetOrAddComponent<Katana>();
    }

    // 카타나를 제거한다.
    private void ClearKatana(GameObject katana)
    {
        if (katana != null)
        {
            Managers.Resource.Destroy(katana);
        }        
    }
    
    // 플레이어가 바라보는 방향으로 카타나의 방향을 움직인다.
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
        // 카타나의 피벗 포인트를 플레이어 위치로 설정        
        katana.transform.position = player.transform.position;
        // 플레이어의 방향에 맞추어 카타나 회전
        katana.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // 카타나를 휘두른다.
    private void SwingKatana()
    {
        // 휘두르는 애니메이션을 위한 왕복 회전        
        float swingFactor = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        katana.transform.Rotate(Vector3.forward, swingFactor); // 참고) 카타나 sprite의 pivot: 왼쪽 끝으로 수정되어 있음.
    }

}
