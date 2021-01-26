using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_interaction : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemy;

    public bool attack()
    {

        float extraHeight = 0.5f;
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, enemy);
        if (ray.collider != null)
         Debug.Log("askdjla");
        return ray.collider != null;

    }
}
