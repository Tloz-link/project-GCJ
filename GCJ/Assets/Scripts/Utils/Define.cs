﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene,
    }

    public enum EUIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum EJoystickState
    {
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum ESound
    {
        Bgm,
        Effect,
        Max,
    }

    public enum EObjectType
    {
        None,
        Creature,
        Projectile,
        Env,
    }

    public enum ECreatureType
    {
        None,
        Hero,
        Monster,
        Npc,
    }

    public enum ECreatureState
    {
        None,
        Idle = 1,
        Move = 2,
        Attack = 3,
        Hit = 4,
        Dead = 5,
    }

    public enum ESkillType
    {
        Kunai,
        Satellite,
        Katana,
        Rocket
    }

    public enum ELayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Dummy1 = 3,
        Water = 4,
        UI = 5,
        Hero = 6,
        Monster = 7,
        Env = 8,
        Obstacle = 9,
        Projectile = 10,
    }

    public const int HERO_ZOOKEEPER_ID = 201000;

    public const int MONSTER_SECURITY1_ID = 1101;
    public const int MONSTER_SECURITY2_ID = 1102;
    public const int MONSTER_SECURITY3_ID = 1103;
}

public static class SortingLayers
{
    public const int ENV = 100;
    public const int MONSTER = 100;
    public const int HERO = 100;
    public const int PROJECTILE = 200;
}