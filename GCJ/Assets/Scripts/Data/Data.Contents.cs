﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region CreatureData
    [Serializable]
    public class CreatureData
    {
        public int DataId;
        public string DescriptionTextID;
        public int MaxHp;
        public int Atk;
        public float MoveSpeed;
        public float ResistDisorder;
        public string AnimatorDataID;
    }
    #endregion

    #region MonsterData
    [Serializable]
    public class MonsterData : CreatureData
    {
        public int DropItemID;
        public int DropPersent;
        public float AtkRange;
    }

    [Serializable]
    public class MonsterDataLoader : ILoader<int, MonsterData>
    {
        public List<MonsterData> monsters = new List<MonsterData>();
        public Dictionary<int, MonsterData> MakeDict()
        {
            Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();
            foreach (MonsterData monster in monsters)
                dict.Add(monster.DataId, monster);
            return dict;
        }
    }
    #endregion

    #region HeroData
    [Serializable]
    public class HeroData : CreatureData
    {
        public int Level;
        public int MaxExp;
        public float ItemAcquireRange;
        public List<int> SkillIdList = new List<int>();
    }

    [Serializable]
    public class HeroDataLoader : ILoader<int, HeroData>
    {
        public List<HeroData> heroes = new List<HeroData>();
        public Dictionary<int, HeroData> MakeDict()
        {
            Dictionary<int, HeroData> dict = new Dictionary<int, HeroData>();
            foreach (HeroData hero in heroes)
                dict.Add(hero.DataId, hero);
            return dict;
        }
    }
    #endregion

    #region HeroLevelData
    [Serializable]
    public class HeroLevelData
    {
        public int DataId;
        public int Level;
        public int Exp;
        public float MoveSpeed;
        public int MaxHp;
        public float ItemAcquireRange;
        public int ResistDisorder;
    }

    [Serializable]
    public class HeroLevelDataLoader : ILoader<int, HeroLevelData>
    {
        public List<HeroLevelData> levels = new List<HeroLevelData>();

        public Dictionary<int, HeroLevelData> MakeDict()
        {
            Dictionary<int, HeroLevelData> dict = new Dictionary<int, HeroLevelData>();
            foreach (HeroLevelData level in levels)
                dict.Add(level.DataId, level);
            return dict;
        }
    }
    #endregion

    #region SkillData
    [Serializable]
    public class SkillData
    {
        public int DataId;
        public string Name;
        public string ClassName;
        public int Level;
        public int ProjectileId;
        public float CoolTime;
        public int Atk;
        public int AtkCount;
        public float AtkAngle;
        public float Duration;
    }

    [Serializable]
    public class SkillDataLoader : ILoader<int, SkillData>
    {
        public List<SkillData> skills = new List<SkillData>();

        public Dictionary<int, SkillData> MakeDict()
        {
            Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
            foreach (SkillData skill in skills)
                dict.Add(skill.DataId, skill);
            return dict;
        }
    }
    #endregion

    #region ProjectileData
    [Serializable]
    public class ProjectileData
    {
        public int DataId;
        public string Name;
        public string ClassName;
        public float ProjSpeed;
    }

    [Serializable]
    public class ProjectileDataLoader : ILoader<int, ProjectileData>
    {
        public List<ProjectileData> projectiles = new List<ProjectileData>();

        public Dictionary<int, ProjectileData> MakeDict()
        {
            Dictionary<int, ProjectileData> dict = new Dictionary<int, ProjectileData>();
            foreach (ProjectileData projectile in projectiles)
                dict.Add(projectile.DataId, projectile);
            return dict;
        }
    }
    #endregion

    #region ItemData
    [Serializable]
    public class ItemData
    {
        public int DataId;
        public string Name;
        public int Value;
        public string IconPath;
    }

    [Serializable]
    public class ItemDataLoader : ILoader<int, ItemData>
    {
        public List<ItemData> items = new List<ItemData>();

        public Dictionary<int, ItemData> MakeDict()
        {
            Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();
            foreach (ItemData item in items)
                dict.Add(item.DataId, item);
            return dict;
        }
    }
    #endregion
}