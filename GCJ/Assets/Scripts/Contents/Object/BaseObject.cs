using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseObject : InitBase
{
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    public CircleCollider2D Collider { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }

    //public float ColliderRadius { get { return Collider != null ? Collider.radius : 0.0f; } }
    public float ColliderRadius { get { return Collider?.radius ?? 0.0f; } }
    public Vector3 CenterPosition { get { return transform.position + Vector3.up * ColliderRadius; } }

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

    #region Animation
    protected virtual void UpdateAnimation()
    {
    }

    public void PlayAnimation(string AnimName, bool loop)
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
