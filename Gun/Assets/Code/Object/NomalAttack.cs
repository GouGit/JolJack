using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttack : CardManager
{

    void Start()
    {
        myBox = GetComponent<BoxCollider2D>();
        attackPower = 5;
        origin = transform.localScale;
        useCost = 1;
    }

    protected override void CardAction(GameObject monster)
    {
        PlayObject mon = monster.GetComponent<PlayObject>();
        mon.LoseHp(attackPower);
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
