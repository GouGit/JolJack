using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : PlayObject
{

    protected override void Start()
    {
        base.Start();
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
      if(Input.GetMouseButtonDown(1))
        {
            GameManager.instance.playerTurn = true;
            StartCoroutine(EndTurn());
        }
    }

    protected override void MyTurn()
    {
        base.MyTurn();
    }

    protected override IEnumerator EndTurn()
    {
        Player.DrawCard();
        base.EndTurn();
    }
}
