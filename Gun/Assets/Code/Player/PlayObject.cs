using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayObject : MonoBehaviour
{
    
    protected virtual void Start()
    {

    }

    protected virtual void Action()
    {

    }

    protected virtual void MyTurn()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.0f;
        scale.y = 1.0f;
        transform.localScale = scale;
    }

    protected virtual IEnumerator EndTurn()
    {
        Vector3 scale = transform.localScale;
        scale.x = 0.8f;
        scale.y = 0.8f;
        transform.localScale = scale;
        yield return null;
    }
}
