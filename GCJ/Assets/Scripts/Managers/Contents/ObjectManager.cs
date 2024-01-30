using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ObjectManager
{
    public Hero Hero { get; private set; }
    public HashSet<Monster> Monsters { get; } = new HashSet<Monster>();

    #region Roots
    public Transform GetRootTransform(string name)
    {
        GameObject root = GameObject.Find(name);
        if (root == null)
            root = new GameObject { name = name };

        return root.transform;
    }

    public Transform HeroRoot { get { return GetRootTransform("@Heroes"); } }
    public Transform MonsterRoot { get { return GetRootTransform("@Monsters"); } }
    #endregion

    public T Spawn<T>(Vector3 position, int templateID) where T : BaseObject
    {
        string prefabName = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate(prefabName);
        go.name = prefabName;
        go.transform.position = position;

        BaseObject obj = go.GetComponent<BaseObject>();

        if (obj.ObjectType == EObjectType.Creature)
        {
            Creature creature = go.GetComponent<Creature>();
            switch (creature.CreatureType)
            {
                case ECreatureType.Hero:
                    obj.transform.parent = HeroRoot;
                    Hero hero = creature as Hero;
                    Hero = hero;
                    break;
                case ECreatureType.Monster:
                    obj.transform.parent = MonsterRoot;
                    Monster monster = creature as Monster;
                    Monsters.Add(monster);
                    break;
            }

            creature.SetInfo(templateID);
        }
        else if (obj.ObjectType == EObjectType.Projectile)
        {
            // TODO
        }
        else if (obj.ObjectType == EObjectType.Env)
        {
            // TODO
        }

        return obj as T;
    }

    public void Despawn<T>(T obj) where T : BaseObject
    {
        EObjectType objectType = obj.ObjectType;

        if (obj.ObjectType == EObjectType.Creature)
        {
            Creature creature = obj.GetComponent<Creature>();
            switch (creature.CreatureType)
            {
                case ECreatureType.Hero:
                    Hero hero = creature as Hero;
                    Hero = null;
                    break;
                case ECreatureType.Monster:
                    Monster monster = creature as Monster;
                    Monsters.Remove(monster);
                    break;
            }
        }
        else if (obj.ObjectType == EObjectType.Projectile)
        {
            // TODO
        }
        else if (obj.ObjectType == EObjectType.Env)
        {
            // TODO
        }

        Managers.Resource.Destroy(obj.gameObject);
    }

    public List<Monster> FindMonsterByRange(Vector2 position, float range)
    {
        List<Monster> list = new List<Monster>();

        foreach (Monster monster in Monsters)
        {
            if (monster.Hp <= 0)
            {
                continue;
            }

            float distance = Vector2.Distance(position, monster.transform.position);
            if (distance > range)
            {
                continue;
            }

            list.Add(monster);
        }

        return list;
    }

    public Monster FindClosestMonster(Vector2 position)
    {
        Monster closestMonster = null;
        float closestDistance = Mathf.Infinity;

        foreach (Monster monster in Monsters)
        {
            if (monster.Hp <= 0)
            {
                continue;
            }

            float distance = Vector2.Distance(position, monster.transform.position);
            if (distance > Hero.AtkRange)
            {
                continue;
            }

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;
            }
        }

        return closestMonster;
    }
}
