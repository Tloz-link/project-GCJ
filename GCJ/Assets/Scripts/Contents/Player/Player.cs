using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : UI_Base
{
    enum GameObjects
    {
        Sprite,
        Satellite,
        Katana, 
        Direction 
    }

    private float speed = 2f;

    private Vector2 inputDirection;
    public Vector2 Direction { get; private set; } = Vector2.up;

    private Monster target;
    public Monster Target { get { return target; } }

    private List<Skill> skillList = new List<Skill>();

    public override bool Init()
    {
        if (_init)
            return false;

        BindObject(typeof(GameObjects));

        {
            KunaiSkill kunai = new KunaiSkill();
            skillList.Add(kunai);
        }

        {
            SatelliteSkill satellite = new SatelliteSkill(GetObject((int)GameObjects.Satellite).transform, 3, 3.0f);
            skillList.Add(satellite);
        }
        // 카타나 추가 @홍지형
        {
            KatanaSkill katana = new KatanaSkill(GetObject((int)GameObjects.Katana).transform);
            skillList.Add(katana);
        }

        return _init = true;
    }

    private void Update()
    {
        UpdateMove();
        UpdateTarget();
        UpdateSkill();
    }

    // 입력 방향을 설정한다.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;

        if (direction != Vector2.zero)
        {
            Direction = direction;

            float angle = Util.VectorToAngle(Direction) - 90f;
            GetObject((int)GameObjects.Direction).transform.rotation = Quaternion.Euler(new Vector3(1f, 1f, angle));
        }
    }

    private void UpdateMove()
    {
        // 이동
        transform.Translate(inputDirection * speed * Time.deltaTime);

        // 방향
        float scaleX = (inputDirection.x < 0f) ? 1f : -1f;
        GetObject((int)GameObjects.Sprite).transform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    private void UpdateTarget()
    {
        if (target != null && target.IsAlive())
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < Managers.Game.Range)
                return;
        }

        target = Managers.Monster.FindClosestMonster(transform.position);
    }

    private void UpdateSkill()
    {
        foreach (Skill skill in skillList)
        {
            // TODO : 스킬 봉인 처리
            skill.Update();
        }
    }
}
