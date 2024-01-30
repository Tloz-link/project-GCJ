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

    // 3) 경험치바
    private Image _expBar;              // 경험치 바

    // 4) 체력바
    private Image _hpBar;           // 체력 바

    // 5) 보스체력바 <- 보스 생기면

    // 6) 골드 획득량 (수정 가능성 있음 우선순위 제일 뒤)
    private TextMeshProUGUI _goldText;  // 골드 획득량

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));        

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
        if (!Managers.Game.IsGamePaused)
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
        
        UpdateHealthBar(Managers.Game.HP, 100); // 최대값을 100으로 임의설정.
        UpdateExpBar(Managers.Game.Exp, 100);
        UpdateGoldText(Managers.Game.Gold);
        //

    }

    // 게임 진행 일시정지/재개 토글
    private void TogglePause()
    {        
        Managers.Game.IsGamePaused = !Managers.Game.IsGamePaused;
        bool _isGamePaused = Managers.Game.IsGamePaused;

        Time.timeScale = _isGamePaused ? 0 : 1;           // 인게임 정지
        _pauseBtn.enabled = _isGamePaused ? false : true; // 버튼 클릭 토글

        // 조이스틱 UI 활성화/비활성화        
        if (Managers.Game.JoystickUI != null)
        {
            Managers.Game.JoystickUI.SetActive(!_isGamePaused);
        }

        if (_isGamePaused == true)
        {
            ShowPopupUI();
        }
    }

    // 팝업창을 연다.
    public virtual void ShowPopupUI()
    {        
        UI_Pause pausePopup = Managers.UI.ShowPopupUI<UI_Pause>(); // 정지팝업창 출력
        pausePopup.SetOnCloseCallback(OnPopupClosed);
    }

    // 팝업창이 닫혔을 때 동작한다.
    private void OnPopupClosed()
    {        
        _pauseBtn.enabled = true; // 일시정지 버튼 활성화
    }

    // 체력바를 갱신한다.    
    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {        
        _hpBar.fillAmount = currentHealth / maxHealth;
        
    }

    // 경험치바를 갱신한다.
    private void UpdateExpBar(float currentExp, float maxExp)
    {
        _expBar.fillAmount = currentExp / maxExp;
    }

    // 골드획득량을 갱신한다.
    private void UpdateGoldText(int goldAmount)
    {
        _goldText.text = "골드: " + goldAmount.ToString();
    }

}
