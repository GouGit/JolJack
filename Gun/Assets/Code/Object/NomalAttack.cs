using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttack : CardManager
{

    void Start()
    {
        origin = transform.localScale;
    }

    void OnMouseDown()
    {
        SelectCard();
    }

    void OnMouseUp()
    {
        DropCrad();
    }

}
