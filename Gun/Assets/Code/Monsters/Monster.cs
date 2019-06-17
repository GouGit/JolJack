using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : PlayObject
{

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.instance.playerTurn)
            return;

        MyTurn();
        Action();
    }

    protected override void Action()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameManager.instance.playerTurn = true;
            EndTurn();
        }
    }

    protected override void MyTurn()
    {
        base.MyTurn();
    }

    protected override void EndTurn()
    {
        Player.inst.DrawCard();
        Vector3 scale = transform.localScale;
        scale.x = 0.8f;
        scale.y = 0.8f;
        transform.localScale = scale;
    }
}
