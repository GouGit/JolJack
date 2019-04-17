using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttack : MonoBehaviour
{

    private Vector3 origin;

    void Start()
    {
        origin = transform.localScale;
    }

    void OnMouseDown()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.3f;
        scale.y = 1.3f;
        transform.localScale = scale;
    }

    void OnMouseUp()
    {
        transform.localScale = origin;
    }

}
