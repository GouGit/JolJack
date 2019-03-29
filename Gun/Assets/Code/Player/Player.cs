using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayObject
{
    private List<GameObject> MyCard = new List<GameObject>();
    private List<GameObject> HandCard = new List<GameObject>();
    private List<GameObject> TrashCard = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        MyCard.Clear();
        HandCard.Clear();
        for(int i=0;i<GameManager.instance.cardManager.AllCards.Count;i++)
        {
            MyCard.Add(GameManager.instance.cardManager.AllCards[i]);
        }
    }

    void Update()
    {
        if(!GameManager.instance.playerTurn)
            return;

        MyTurn();
        Action();
    }

    void Shuffle()
    {
        for(int i=0;i<5;i++)
        {

        }
    }

    void DrawCard()
    {
        
    }

    protected override void Action()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GameManager.instance.playerTurn = false;
            StartCoroutine(EndTurn());
        }
    }

}
