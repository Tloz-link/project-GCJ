using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene
{
    enum GameObjects
    {

    }

    enum Texts
    {
        TimeText
    }

    // 1) �÷��� �ð�
    // 2) ���� ��ư
    // 3) ����ġ��
    // 4) ü�¹�
    // 5) ����ü�¹� <- ���� �����
    // 6) ��� ȹ�淮 (���� ���ɼ� ���� �켱���� ���� ��)

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetText((int)Texts.TimeText).text = "30";
        // ������ ����

        return true;
    }
}
