using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : UI_Base
{
    enum GameObjects
    {
        Sprite
    }

    public float speed = 3f;         // �÷��̾��� �̵� �ӵ�
    private Vector2 inputDirection;  // ���� �Է� ����

    private Monster target;

    public override bool Init()
    {
        if (_init)
            return false;

        BindObject(typeof(GameObjects));
        return _init = true;
    }

    private void Update()
    {
        UpdateMove();
        UpdateTarget();
        UpdateAttack();
    }

    // �Է� ������ �����Ѵ�.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void UpdateMove()
    {
        // �̵�
        transform.Translate(inputDirection * speed * Time.deltaTime);

        // ����
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

    private float tick = 0f;
    private void UpdateAttack()
    {
        tick += Time.deltaTime;
        if (tick > 1.0f)
        {
            Projectile proj = Managers.Resource.Instantiate("Projectile/Projectile").GetOrAddComponent<Projectile>();
            Vector2 direction = Vector2.zero;
            if (target == null)
            {
                float randomAngle = Random.Range(0f, 360f);
                float radianAngle = Mathf.Deg2Rad * randomAngle;
                direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
            }
            else
            {
                direction = target.transform.position - transform.position;
            }

            proj.SetInfo(transform.position, direction.normalized);
            tick = 0f;
        }
    }
}
