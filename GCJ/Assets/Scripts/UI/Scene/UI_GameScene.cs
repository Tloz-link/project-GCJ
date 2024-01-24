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

    // 1) 플레이 시간
    // 2) 정지 버튼
    // 3) 경험치바
    // 4) 체력바
    // 5) 보스체력바 <- 보스 생기면
    // 6) 골드 획득량 (수정 가능성 있음 우선순위 제일 뒤)

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetText((int)Texts.TimeText).text = "30";
        // 생성자 역할

        return true;
    }
}
