using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// �ΰ��� ȭ���� UI�� ����ϴ� Ŭ����. @ȫ����
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

    // 1) �÷��� �ð�
    private TextMeshProUGUI _timerText; // �ΰ��� �÷��� �ð� Ÿ�̸�
    private float _timeElapsed;         // Ÿ�̸Ӹ� ���� ����

    // 2) ���� ��ư 
    private Button _pauseBtn;           // �Ͻ����� ��ư
    private bool _isGamePaused = false; // ������ �Ͻ����� ���¸� �����ϴ� ����
   
    // TODO: �����˾�â ����
    // TODO: �����˾�â:
    // ������, ���ư���, �Ҹ����� ��ư

    // 3) ����ġ��
    private Image _expBar;              // ����ġ ��

    // 4) ü�¹�
    private Image _hpBar;           // ü�� ��

    // 5) ����ü�¹� <- ���� �����

    // 6) ��� ȹ�淮 (���� ���ɼ� ���� �켱���� ���� ��)
    private TextMeshProUGUI _goldText;  // ��� ȹ�淮

    // 7) ���� óġ�� (�����˾� �ȿ�)

    private GameObject _joystickUI; // ���̽�ƽ UI�� GameObject ����
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
        // Ÿ�̸ӿ� ���� ���� ������Ʈ
        if (!_isGamePaused)
        {
            UpdateTimer();
        }
    }

    // �̺�Ʈ ���ε� �ʱ�ȭ�Ѵ�.
    private void InitEvents()
    {        
        _pauseBtn.onClick.AddListener(TogglePause);
    }

    // 1�ʸ��� Ÿ�̸Ӹ� �����Ѵ�.
    private void UpdateTimer()
    {
        _timeElapsed += Time.deltaTime;
        int minutes = (int)(_timeElapsed / 60);
        int seconds = (int)(_timeElapsed % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        Managers.Game.CurrentTime = _timeElapsed; // GameManager�� ����ð��� �����Ѵ�.

        // ����׿�.
        // TODO: �ܺο��� ���� ���� �ҷ��;� �Ѵ�.
        testFloatVal += 0.05f;
        testIntVal += 1;
        UpdateHealthBar(testFloatVal, 100);
        UpdateExpBar(testFloatVal, 100);
        UpdateGoldText(testIntVal);
        //

    }

    // �Ͻ����� ��ư Ŭ�� �� �����Ѵ�.
    private void TogglePause()
    {
        // Debug.Log("TogglePause()");
        _isGamePaused = !_isGamePaused;
        
        Time.timeScale = _isGamePaused ? 0 : 1;

        // ���̽�ƽ UI Ȱ��ȭ/��Ȱ��ȭ
        if (_joystickUI != null)
        {
            _joystickUI.SetActive(!_isGamePaused);
        }

        // TODO: �����˾�â ���
    }

    // ü�¹ٸ� �����Ѵ�.    
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // TODO: �ܺο��� ���� ü�°��� �ҷ��;� �Ѵ�.
        _hpBar.fillAmount = currentHealth / maxHealth;
        
    }

    // ����ġ�ٸ� �����Ѵ�.
    public void UpdateExpBar(float currentExp, float maxExp)
    {
        // TODO: �ܺο��� ���� ����ġ���� �ҷ��;� �Ѵ�.
        _expBar.fillAmount = currentExp / maxExp;
    }
    
    // ���ȹ�淮�� �����Ѵ�.
    public void UpdateGoldText(int goldAmount)
    {
        // TODO: �ܺο��� ���� ��尪�� �ҷ��;� �Ѵ�.
        _goldText.text = "���: " + goldAmount.ToString();
    }

}
