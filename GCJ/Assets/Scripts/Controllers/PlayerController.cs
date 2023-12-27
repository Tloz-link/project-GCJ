using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;         // �÷��̾��� �̵� �ӵ�
    private Vector2 inputDirection;  // ���� �Է� ����

    // �Է� ������ �����Ѵ�.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void Update()
    {
        // �÷��̾ �̵���Ų��.
        transform.Translate(inputDirection * speed * Time.deltaTime);
    }
}
