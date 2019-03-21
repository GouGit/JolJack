﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayObject
{

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(!GameManager.instance.playerTurn)
            return;

        MyTurn();
        Action();
    }

    protected override void Action()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GameManager.instance.playerTurn = false;
        }
        StartCoroutine(EndTurn());
    }

}
