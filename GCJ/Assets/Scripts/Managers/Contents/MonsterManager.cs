using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager
{
    private int id = 0;

    private Dictionary<int, Monster> monsters;
    public Dictionary<int, Monster> MonsterList { get { return monsters; } }

    private Transform root;

    public void Init()
    {
        if (root == null)
        {
            root = new GameObject { name = "@Monster" }.transform;
        }

        id = 0;
        monsters = new Dictionary<int, Monster>();

        // 테스트 용 코드
        for (int i = 0; i < 10; ++i)
        {
            Monster monster = CreateMonster("Monster/Monster");
            monster.transform.position = new Vector2(-2, 3);
        }
        //////////////////////
    }

    public Monster CreateMonster(string path)
    {
        Monster monster = Managers.Resource.Instantiate(path, root).GetOrAddComponent<Monster>();
        monsters.Add(++id, monster);
        monster.SetInfo(id, Managers.Game.Player.transform);

        return monster;
    }

    public void DestoryMonster(int id)
    {
        if (monsters.TryGetValue(id, out Monster monster))
        {
            Managers.Resource.Destroy(monster.gameObject);
            monsters.Remove(id);
        }
    }

    public Monster FindClosestMonster(Vector2 position)
    {
        Monster closestMonster = null;
        float closestDistance = Mathf.Infinity;

        foreach (Monster monster in monsters.Values)
        {
            if (!monster.IsAlive())
            {
                continue;
            }

            float distance = Vector2.Distance(position, monster.transform.position);
            if (distance > Managers.Game.Range)
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

    public void Clear()
    {
        foreach (Monster monster in monsters.Values)
        {
            Managers.Resource.Destroy(monster.gameObject);
        }
        monsters.Clear();
    }
}
