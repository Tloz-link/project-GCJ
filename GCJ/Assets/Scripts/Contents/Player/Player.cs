using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UI_Base
{
    public float speed = 1f;         // 플레이어의 이동 속도
    private Vector2 inputDirection;  // 현재 입력 방향

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

    // 입력 방향을 설정한다.
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
