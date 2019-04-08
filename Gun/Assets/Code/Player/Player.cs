using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayObject
{
    public  List<GameObject> MyCard = new List<GameObject>();
    private List<GameObject> HandCard = new List<GameObject>();
    private List<GameObject> TrashCard = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        MyCard.Clear();
        HandCard.Clear();
        TrashCard.Clear();
        for(int i=0;i<GameManager.instance.cardManager.AllCards.Count;i++)
        {
            MyCard.Add(GameManager.instance.cardManager.AllCards[i]);
        }
        Shuffle();
        DrawCard();
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
        for(int i=0;i<MyCard.Count;i++)
        {
            GameObject temp = MyCard[i];
            int index = Random.Range(0,MyCard.Count);
            MyCard[i] = MyCard[index];
            MyCard[index] = temp;
        }
    }

    public static void DrawCard()
    {
        HandCard.Clear();
        for(int i=0;i<5;i++)
        {
            HandCard.Add(MyCard[i]);
            MyCard.RemoveAt(i);

            Dedug.Log(HandCard[i]);
        }
    }

    void DropCard()
    {
        for(int i=0;i<5;i++)
        {
           TrashCard.Add(HandCard[i]);
        }

        if(MyCard.Count == 0)
        {
            for(int i=0;i<TrashCard.Count;i++)
            {
                MyCard.Add(TrashCard[i]);
            }
        }
    }

    protected override void Action()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GameManager.instance.playerTurn = false;
            StartCoroutine(EndTurn());
        }
    }

    protected override void MyTurn()
    {
        base.MyTurn();
    }

    protected override IEnumerator EndTurn()
    {
        DropCard();
        base.EndTurn();
    }

}
