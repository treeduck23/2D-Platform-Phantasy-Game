using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 size = new Vector2(1, 1);
        float angle = 0;
        Vector2 direction = Vector2.right;
        float distance = 1f;

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            Debug.Log("Hit distance: " + hit.distance);
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * distance, Color.green);
            Debug.Log("No hit");
        }
    }
}
