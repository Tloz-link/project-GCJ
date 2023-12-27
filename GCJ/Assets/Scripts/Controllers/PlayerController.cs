using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;         // 플레이어의 이동 속도
    private Vector2 inputDirection;  // 현재 입력 방향

    // 입력 방향을 설정한다.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void Update()
    {
        // 플레이어를 이동시킨다.
        transform.Translate(inputDirection * speed * Time.deltaTime);
    }
}
