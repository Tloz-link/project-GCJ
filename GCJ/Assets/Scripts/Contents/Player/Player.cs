using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UI_Base
{
    public float speed = 1f;         // �÷��̾��� �̵� �ӵ�
    private Vector2 inputDirection;  // ���� �Է� ����

    public override bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    private void Update()
    {
        MoveUpdate();
    }

    // �Է� ������ �����Ѵ�.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void MoveUpdate()
    {
        transform.Translate(inputDirection * speed * Time.deltaTime);
        float scaleX = (inputDirection.x < 0f) ? 1f : -1f;
        transform.localScale = new Vector3(scaleX, 1f, 1f);
    }
}
