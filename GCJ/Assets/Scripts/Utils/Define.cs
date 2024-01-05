using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Pressed,
        PressedLong,
        PointerDown,
        PointerUp,
        PointerExit
    }

    public enum Layer
    {
        Monster = 6
    }

    public enum SkillType
    {
        Kunai,
        Satellite,
        Katana
    }
}
