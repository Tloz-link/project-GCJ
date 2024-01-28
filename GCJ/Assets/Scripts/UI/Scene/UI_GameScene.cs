using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 인게임 화면의 UI를 담당하는 클래스. @홍지형
public class UI_GameScene : UI_Scene
{
    enum GameObjects
    {
        
    }
    enum Texts
    {
        Timer,
        Gold,
    }
    enum Images
    {
        HPBar,
        ExpBar,
    }
    enum Buttons
    {
        PauseBtn,
    }

    // 1) 플레이 시간
    private TextMeshProUGUI _timerText; // 인게임 플레이 시간 타이머
    private float _timeElapsed;         // 타이머를 위한 변수

    // 2) 정지 버튼 
    private Button _pauseBtn;           // 일시정지 버튼
    private bool _isGamePaused = false; // 게임의 일시정지 상태를 추적하는 변수
   
    // TODO: 정지팝업창 구현
    // TODO: 정지팝업창:
    // 나가기, 돌아가기, 소리제거 버튼

    // 3) 경험치바
    private Image _expBar;              // 경험치 바

    // 4) 체력바
    private Image _hpBar;           // 체력 바

    // 5) 보스체력바 <- 보스 생기면

    // 6) 골드 획득량 (수정 가능성 있음 우선순위 제일 뒤)
    private TextMeshProUGUI _goldText;  // 골드 획득량

    // 7) 몬스터 처치수 (정지팝업 안에)

    private GameObject _joystickUI; // 조이스틱 UI의 GameObject 참조
    private float testFloatVal = 0;
    private int testIntVal = 0;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        _joystickUI = GameObject.Find("UI_Joystick");
        _timerText = GetText((int)Texts.Timer);
        _goldText = GetText((int)Texts.Gold);
        _pauseBtn = GetButton((int)Buttons.PauseBtn);
        _hpBar = GetImage((int)Images.HPBar);
        _expBar = GetImage((int)Images.ExpBar);

        InitEvents();

        return true;
    }

    void Update()
    {
        // 타이머와 게임 상태 업데이트
        if (!_isGamePaused)
        {
            UpdateTimer();
        }
    }

    // 이벤트 바인딩 초기화한다.
    private void InitEvents()
    {        
        _pauseBtn.onClick.AddListener(TogglePause);
    }

    // 1초마다 타이머를 갱신한다.
    private void UpdateTimer()
    {
        _timeElapsed += Time.deltaTime;
        int minutes = (int)(_timeElapsed / 60);
        int seconds = (int)(_timeElapsed % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        Managers.Game.CurrentTime = _timeElapsed; // GameManager에 현재시간을 저장한다.

        // 디버그용.
        // TODO: 외부에서 계산된 값을 불러와야 한다.
        testFloatVal += 0.05f;
        testIntVal += 1;
        UpdateHealthBar(testFloatVal, 100);
        UpdateExpBar(testFloatVal, 100);
        UpdateGoldText(testIntVal);
        //

    }

    // 일시정지 버튼 클릭 시 동작한다.
    private void TogglePause()
    {
        // Debug.Log("TogglePause()");
        _isGamePaused = !_isGamePaused;
        
        Time.timeScale = _isGamePaused ? 0 : 1;

        // 조이스틱 UI 활성화/비활성화
        if (_joystickUI != null)
        {
            _joystickUI.SetActive(!_isGamePaused);
        }

        // TODO: 정지팝업창 출력
    }

    // 체력바를 갱신한다.    
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // TODO: 외부에서 계산된 체력값을 불러와야 한다.
        _hpBar.fillAmount = currentHealth / maxHealth;
        
    }

    // 경험치바를 갱신한다.
    public void UpdateExpBar(float currentExp, float maxExp)
    {
        // TODO: 외부에서 계산된 경험치값을 불러와야 한다.
        _expBar.fillAmount = currentExp / maxExp;
    }
    
    // 골드획득량을 갱신한다.
    public void UpdateGoldText(int goldAmount)
    {
        // TODO: 외부에서 계산된 골드값을 불러와야 한다.
        _goldText.text = "골드: " + goldAmount.ToString();
    }

}
