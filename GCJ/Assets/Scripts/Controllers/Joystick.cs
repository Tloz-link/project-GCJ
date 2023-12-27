using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��ġ�Ͽ� �����ϴ� ���� ���̽�ƽ Ŭ���� @ȫ���� 23.12.16
public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform guideCircle;         // ����) ���̽�ƽ ��輱
    public RectTransform circle;              // ����) ���̽�ƽ ������
    public PlayerController playerController; // ����) �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    private Vector2 inputDirection;           // ���̽�ƽ �Է� ����

    private void Start()
    {
        HideJoystick();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {        
        Vector2 direction = eventData.position;
        // Debug.Log("direction" + direction);

        float radius = guideCircle.sizeDelta.x / 2f; // ���̽�ƽ ��輱�� ������
        Vector2 offset = direction - (Vector2)guideCircle.position;

        // ��ġ ��ġ�� ���̽�ƽ ��輱�� �ݰ� ���� �ִ��� Ȯ���Ѵ�.
        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius; // ���̽�ƽ �������� �߽��� ���̽�ƽ �������� �̵��ϵ��� �����Ѵ�.
        }
        circle.position = (Vector2)guideCircle.position + offset; // ��ġ ��ġ�� ���̽�ƽ �����̸� �̵���Ų��.

        // �÷��̾� ��Ʈ�ѷ��� ������ �����Ѵ�.
        inputDirection = offset / radius;   // ����ȭ�� �Է� ������ ����Ѵ�.
        if (playerController != null)
        {
            playerController.SetInputDirection(inputDirection);
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        guideCircle.position = eventData.position;
        circle.position = eventData.position;
        OnDrag(eventData);
        ShowJoystick();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        HideJoystick();
        if (playerController != null)
        {
            playerController.SetInputDirection(Vector2.zero); // �÷��̾� �̵��� �����.
        }
    }

    private void HideJoystick()
    {
        guideCircle.gameObject.SetActive(false);
        circle.gameObject.SetActive(false);
        circle.position = Vector2.zero;
        guideCircle.position = Vector2.zero;
    }

    private void ShowJoystick()
    {
        guideCircle.gameObject.SetActive(true);
        circle.gameObject.SetActive(true);
    }
}