using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseObject
{
    public Creature Owner { get; private set; }
    public SkillBase Skill { get; private set; }
    public Data.ProjectileData ProjectileData { get; private set; }

    private SpriteRenderer _spriteRenderer;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ObjectType = Define.EObjectType.Projectile;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = SortingLayers.PROJECTILE;

        return true;
    }

    public void SetInfo(int dataTemplateID)
    {
        ProjectileData = Managers.Data.ProjectileDic[dataTemplateID];
    }

    public void SetSpawnInfo(Creature owner, SkillBase skill, Vector2 direction)
    {
        Owner = owner;
        Skill = skill;

        float angle = Util.VectorToAngle(direction);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private float tick = 0f;
    void Update()
    {
        transform.Translate(Vector2.up * ProjectileData.ProjSpeed * Time.deltaTime);

        tick += Time.deltaTime;
        if (tick > 5f)
        {
            Managers.Object.Despawn(this);
        }
    }
}
