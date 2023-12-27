using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 터치하여 조작하는 가상 조이스틱 클래스 @홍지형 23.12.16
public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform guideCircle;         // 참조) 조이스틱 경계선
    public RectTransform circle;              // 참조) 조이스틱 손잡이
    public PlayerController playerController; // 참조) 플레이어 컨트롤러 스크립트
    private Vector2 inputDirection;           // 조이스틱 입력 방향

    private void Start()
    {
        HideJoystick();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {        
        Vector2 direction = eventData.position;
        // Debug.Log("direction" + direction);

        float radius = guideCircle.sizeDelta.x / 2f; // 조이스틱 경계선의 반지름
        Vector2 offset = direction - (Vector2)guideCircle.position;

        // 터치 위치가 조이스틱 경계선의 반경 내에 있는지 확인한다.
        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius; // 조이스틱 손잡이의 중심이 조이스틱 경계까지만 이동하도록 제한한다.
        }
        circle.position = (Vector2)guideCircle.position + offset; // 터치 위치로 조이스틱 손잡이를 이동시킨다.

        // 플레이어 컨트롤러에 방향을 전달한다.
        inputDirection = offset / radius;   // 정규화된 입력 방향을 계산한다.
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
            playerController.SetInputDirection(Vector2.zero); // 플레이어 이동을 멈춘다.
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