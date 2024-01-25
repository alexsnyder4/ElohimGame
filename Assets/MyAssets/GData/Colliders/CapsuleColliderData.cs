using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleColliderData
{
    public CapsuleCollider Collider { get; private set; }

    public Vector3 ColliderCenterInLocalSpace { get; private set; }

    public Vector3 ColiderVerticalExtents{ get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if (Collider != null)
        {
            return;
        }

        Collider = gameObject.GetComponent<CapsuleCollider>();
        UpdateColliderData();
    }

    public void UpdateColliderData()
    {
        ColliderCenterInLocalSpace = Collider.center;

        ColiderVerticalExtents = new Vector3(0f, Collider.bounds.extents.y, 0f);
    }
}
