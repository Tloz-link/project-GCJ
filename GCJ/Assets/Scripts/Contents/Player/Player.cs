using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : UI_Base
{
    enum GameObjects
    {
        Sprite,
        Satellite
    }

    public float speed = 3f;         // 플레이어의 이동 속도
    private Vector2 inputDirection;  // 현재 입력 방향

    private Monster target;

    public override bool Init()
    {
        if (_init)
            return false;

        BindObject(typeof(GameObjects));

        CreateSatellites(3);
        return _init = true;
    }

    private void Update()
    {
        UpdateMove();
        UpdateTarget();
        UpdateAttack(); // 임시
        RotateSatellites();
    }

    // 입력 방향을 설정한다.
    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void UpdateMove()
    {
        // 이동
        transform.Translate(inputDirection * speed * Time.deltaTime);

        // 방향
        float scaleX = (inputDirection.x < 0f) ? 1f : -1f;
        GetObject((int)GameObjects.Sprite).transform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    private void UpdateTarget()
    {
        if (target != null && target.IsAlive())
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < Managers.Game.Range)
                return;
        }

        target = Managers.Monster.FindClosestMonster(transform.position);
    }

    // 임시
    private float tick = 0f;
    private void UpdateAttack()
    {
        tick += Time.deltaTime;
        if (tick > 1.0f)
        {
            Projectile proj = Managers.Resource.Instantiate("Projectile/Projectile").GetOrAddComponent<Projectile>();
            Vector2 direction = Vector2.zero;
            if (target == null)
            {
                float randomAngle = Random.Range(0f, 360f);
                float radianAngle = Mathf.Deg2Rad * randomAngle;
                direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
            }
            else
            {
                direction = target.transform.position - transform.position;
            }

            proj.SetInfo(transform.position, direction.normalized);
            tick = 0f;
        }
    }

    #region Satellite
    private float orbitRadius = 1f;
    private float rotationSpeed = 200f;
    private List<GameObject> satellites = new List<GameObject>();

    private void CreateSatellites(int count)
    {
        ClearSatellites();

        for (int i = 0; i < count; ++i)
        {
            float angle = i * 360f / count;
            Vector2 spawnPosition = GetCirclePosition(angle, orbitRadius);

            GameObject satellite = Managers.Resource.Instantiate("Satellite/Satellite", GetObject((int)GameObjects.Satellite).transform);
            satellite.transform.position = spawnPosition;
            satellite.GetOrAddComponent<Satellite>();

            satellites.Add(satellite);
        }
    }

    private Vector2 GetCirclePosition(float angle, float radius)
    {
        float radian = Mathf.Deg2Rad * angle;
        float x = transform.position.x + Mathf.Cos(radian) * radius;
        float y = transform.position.y + Mathf.Sin(radian) * radius;
        return new Vector2(x, y);
    }

    private void RotateSatellites()
    {
        for (int i = 0; i < satellites.Count; ++i)
        {
            satellites[i].transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
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

    #endregion
}
