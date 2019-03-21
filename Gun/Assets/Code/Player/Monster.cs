using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : PlayObject
{

    protected override void Start()
    {
        base.Start();
        StartCoroutine(EndTurn());
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
       // GameManager.instance.playerTurn = true;
        //StartCoroutine(EndTurn());
    }
}
