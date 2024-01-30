using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 일시정지 팝업 UI를 담당하는 클래스. @홍지형
public class UI_Pause : UI_Popup
{
    enum Texts
    {
        MonsterKillCount,
    }
    enum Images
    {

    }
    enum Buttons
    {
        ReturnBtn, // 이전창으로 돌아가기 (돌아가기)
        ExitBtn,   // 팝업창 전부 닫기 (나가기)
        AddBtn,    // 팝업창 추가 버튼 
        SoundBtn,    // 소리끄기/켜기
    }

    private Button _returnBtn; // 이전창으로 돌아가기
    private Button _exitBtn;   // 팝업창 전부 닫기
    private Button _addBtn;    // 팝업창 추가 버튼
    private Button _soundBtn;  // 소리끄기/켜기
    private TextMeshProUGUI _monsterKillCount;  // 몬스터 처치수  

    private Action onCloseCallback; // 이벤트: (일시정지 버튼 활성화)

    // 1) 나가기(타이틀) 버튼 -> 타이틀 구현 되면
    // 2) 돌아가기 버튼
    // 3) 소리 on off
    // 4) 획득한 스킬 목록 <- 어떻게 보여져야 하는지 애매 기획한테 물어봐야함

    public override bool Init()
    {
        if (base.Init() == false)
            return false;       

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        _returnBtn = GetButton((int)Buttons.ReturnBtn);
        _exitBtn = GetButton((int)Buttons.ExitBtn);
        _addBtn = GetButton((int)Buttons.AddBtn);
        _soundBtn = GetButton((int)Buttons.SoundBtn);
        _monsterKillCount = GetText((int)Texts.MonsterKillCount);

        InitEvents();

        UpdateMonsterKillCount(Managers.Game.MonsterKillCount);

        return true;
    }

    // 이벤트 바인딩 초기화한다.
    private void InitEvents()
    {
        _returnBtn.onClick.AddListener(ClosePopupUI);
        _exitBtn.onClick.AddListener(CloseAllPopupUI);
        _addBtn.onClick.AddListener(ShowPopupUI);
        _soundBtn.onClick.AddListener(ToggleSound);
    }

    // 팝업창을 모두 닫는다.
    public virtual void CloseAllPopupUI()
    {
        Managers.UI.CloseAllPopupUI();
        StopPause();
        onCloseCallback?.Invoke(); // 이벤트: (일시정지 버튼 활성화)
    }

    // 이전 팝업창만 닫는다.
    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
        
        if (Managers.UI.GetPopupStackCount() == 0) // 모든 팝업창이 닫혔다면
        {
            StopPause();
            onCloseCallback?.Invoke(); // 이벤트: (일시정지 버튼 활성화)
        }   
    }

    // 팝업창을 연다.
    public virtual void ShowPopupUI() 
    {        
        // Managers.UI.ShowPopupUI<UI_Pause>();  // 임의의 팝업창. 필요 시 추후 추가 @홍지형        
        // 현재 일시정지 버튼 이벤트함수를 가져오지 못해서 주석처리함.
    }

    // 게임 진행 재개
    private void StopPause()
    {
        Managers.Game.IsGamePaused = false;
        bool _isGamePaused = Managers.Game.IsGamePaused;

        Time.timeScale = 1;       // 인게임 시작

        // 조이스틱 UI 활성화/비활성화        
        if (Managers.Game.JoystickUI != null)
        {
            Managers.Game.JoystickUI.SetActive(!_isGamePaused);
        }
    }

    // 소리 끄기/켜기 토글
    private void ToggleSound()
    {
        // TODO: 소리 토글
    }

    // 몬스터 처치수를 갱신한다.
    private void UpdateMonsterKillCount(int killCount)
    {
        _monsterKillCount.text = "처치 수: " + killCount.ToString();
    }

    // 팝업창이 닫힐 때 콜백이벤트를 등록한다.
    public void SetOnCloseCallback(Action callback)
    {
        onCloseCallback = callback;
    }

}
