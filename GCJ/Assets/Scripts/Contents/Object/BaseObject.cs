using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseObject : InitBase
{
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    public CircleCollider2D Collider { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }
    public SpriteRenderer Renderer { get; protected set; }

    public float ColliderRadius { get { return Collider != null ? Collider.radius : 0.0f; } }
    public Vector3 CenterPosition { get { return transform.position + Vector3.up * ColliderRadius; } }

    public int DataTemplateID { get; set; }

    bool _lookLeft = true;
    public bool LookLeft
    {
        get { return _lookLeft; }
        set
        {
            _lookLeft = value;
            Flip(!value);
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = gameObject.GetOrAddComponent<CircleCollider2D>();
        Animator = gameObject.GetComponentInChildren<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();

        return true;
    }

    public void TranslateEx(Vector3 dir)
    {
        transform.Translate(dir);

        if (dir.x < 0)
            LookLeft = true;
        else if (dir.x > 0)
            LookLeft = false;
    }

    #region Battle
    public virtual void OnDamaged(BaseObject attacker)
    {

    }

    public virtual void OnDead(BaseObject attacker)
    {

    }
    #endregion

    #region Animation
    protected virtual void PlayAnimation(Define.ECreatureState state)
    {

    }

    protected virtual void Flip(bool flag)
    {
        Vector3 scale = transform.localScale;
        scale.x = flag ? -1 : 1;
        transform.localScale = scale;
    }
    #endregion
}
