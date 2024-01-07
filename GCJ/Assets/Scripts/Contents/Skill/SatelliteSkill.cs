using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSkill : Skill
{
    private Transform parent;
    private int count;
    private float durationTime;

    public SatelliteSkill(Transform parent, int count, float durationTime) : base(1, 7.0f, Define.SkillType.Satellite)
    {
        this.parent = parent;
        this.count = count;
        this.durationTime = durationTime;
    }

    private bool isTimeActive = false;
    private float durationTick = 0.0f;
    public override void Update()
    {
        if (isTimeActive)
        {
            RotateSatellites();

            durationTick += Time.deltaTime;
            if (durationTick >= durationTime)
            {
                ClearSatellites();
                durationTick = 0.0f;
                isTimeActive = false;
            }
        }

        cooldownTick += Time.deltaTime;
        if (cooldownTick <= Cooldown)
            return;
        cooldownTick = 0.0f;

        CreateSatellites();
        isTimeActive = true;
    }

    private float orbitRadius = 1f;
    private float rotationSpeed = 200f;
    private List<GameObject> satellites = new List<GameObject>();

    private void CreateSatellites()
    {
        ClearSatellites();

        for (int i = 0; i < count; ++i)
        {
            float angle = i * 360f / count;
            Vector2 spawnPosition = GetCirclePosition(angle, orbitRadius);

            GameObject satellite = Managers.Resource.Instantiate("Satellite/Satellite", parent);
            satellite.transform.position = spawnPosition;
            satellite.GetOrAddComponent<Satellite>();

            satellites.Add(satellite);
        }
    }

    private Vector2 GetCirclePosition(float angle, float radius)
    {
        Transform player = Managers.Game.Player.transform;

        float radian = Mathf.Deg2Rad * angle;
        float x = player.position.x + Mathf.Cos(radian) * radius;
        float y = player.position.y + Mathf.Sin(radian) * radius;
        return new Vector2(x, y);
    }

    private void RotateSatellites()
    {
        Transform player = Managers.Game.Player.transform;

        foreach (GameObject satellite in satellites)
        {
            satellite.transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void ClearSatellites()
    {
        for (int i = 0; i < satellites.Count; ++i)
        {
            Managers.Resource.Destroy(satellites[i]);
        }
        satellites.Clear();
    }
}
