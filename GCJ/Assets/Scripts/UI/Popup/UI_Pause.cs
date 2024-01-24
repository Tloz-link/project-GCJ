using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : UI_Popup
{
    enum GameObjects
    {

    }

    // 1) 나가기(타이틀) 버튼 -> 타이틀 구현 되면
    // 2) 돌아가기 버튼
    // 3) 소리 on off
    // 4) 획득한 스킬 목록 <- 어떻게 보여져야 하는지 애매 기획한테 물어봐야함

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // 생성자 역할

        return true;
    }
}
