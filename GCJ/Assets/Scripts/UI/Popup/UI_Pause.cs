using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : UI_Popup
{
    enum GameObjects
    {

    }

    // 1) ������(Ÿ��Ʋ) ��ư -> Ÿ��Ʋ ���� �Ǹ�
    // 2) ���ư��� ��ư
    // 3) �Ҹ� on off
    // 4) ȹ���� ��ų ��� <- ��� �������� �ϴ��� �ָ� ��ȹ���� ���������

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // ������ ����

        return true;
    }
}
