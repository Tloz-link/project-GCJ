using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SatelliteSkill : SkillBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void SetInfo(Creature owner, int skillTemplateID)
    {
        base.SetInfo(owner, skillTemplateID);
    }

    private bool isTimeActive = false;
    private float durationTick = 0.0f;
    protected override void Update()
    {
        if (isTimeActive)
        {
            RotateSatellites();

            durationTick += Time.deltaTime;
            if (durationTick >= SkillData.Duration)
            {
                ClearSatellites();
                durationTick = 0.0f;
                isTimeActive = false;
            }
        }

        base.Update();
    }

    private float orbitRadius = 1f;
    private float rotationSpeed = 200f;
    private List<Bird> birds = new List<Bird>();

    public override void DoSkill()
    {
        ClearSatellites();

        for (int i = 0; i < SkillData.NumProjectiles; ++i)
        {
            float angle = i * 360f / SkillData.NumProjectiles;
            Vector2 spawnPosition = GetCirclePosition(angle, orbitRadius);

            Bird bird = Managers.Object.Spawn<Bird>(spawnPosition, SkillData.ProjectileId, transform);
            bird.SetSpawnInfo(Owner, this, Vector2.up);

            birds.Add(bird);
        }

        isTimeActive = true;
    }

    private Vector2 GetCirclePosition(float angle, float radius)
    {
        Transform hero = Managers.Object.Hero.transform;

        float radian = Mathf.Deg2Rad * angle;
        float x = hero.position.x + Mathf.Cos(radian) * radius;
        float y = hero.position.y + Mathf.Sin(radian) * radius;
        return new Vector2(x, y);
    }

    private void RotateSatellites()
    {
        foreach (Bird bird in birds)
        {
            bird.transform.RotateAround(Owner.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void ClearSatellites()
    {
        for (int i = 0; i < birds.Count; ++i)
        {
            Managers.Object.Despawn(birds[i]);
        }
        birds.Clear();
    }
}
