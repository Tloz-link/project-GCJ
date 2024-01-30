using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.EScene.GameScene;

        Managers.Object.Spawn<Hero>(new Vector3(0f, -4f, 0f), Define.HERO_ZOOKEEPER_ID);
        Camera.main.GetOrAddComponent<FollowCamera>();

        for (int i = 0; i < 5; ++i)
            Managers.Object.Spawn<Monster>(new Vector3(-3f, 0f, 0f), Define.MONSTER_SECURITY1_ID);

        for (int i = 0; i < 5; ++i)
            Managers.Object.Spawn<Monster>(new Vector3(0f, 1f, 0f), Define.MONSTER_SECURITY2_ID);

        for (int i = 0; i < 5; ++i)
            Managers.Object.Spawn<Monster>(new Vector3(3f, 0f, 0f), Define.MONSTER_SECURITY3_ID);

        Managers.UI.ShowBaseUI<UI_Joystick>();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        return true;
    }

    public override void Clear()
    {
        
    }
}
