using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayObject
{
    public  LinkedList<GameObject> MyCard = new LinkedList<GameObject>();
    private LinkedList<GameObject> HandCard = new LinkedList<GameObject>();
    private LinkedList<GameObject> TrashCard = new LinkedList<GameObject>();

    private static Player instance = null;
    public static Player inst;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            inst = instance;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(inst);
    } 

    protected override void Start()
    {
        base.Start();
        MyCard.Clear();
        HandCard.Clear();
        TrashCard.Clear();
        for(int i=0;i<GameManager.instance.cardManager.AllCards.Count;i++)
        {
            MyCard.AddFirst(GameManager.instance.cardManager.AllCards[i]);
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
            // GameObject temp = MyCard.;
            // int index = Random.Range(0,MyCard.Count);
            // MyCard[i] = MyCard[index];
            // MyCard[index] = temp;
        }
    }

    public void DrawCard()
    {
        HandCard.Clear();
        for(int i=0; i<5; i++)
        {
            // HandCard.Add(MyCard[i]);
            // MyCard.RemoveAt(i);
            ReBulid();
        }
    }

    void DropCard()
    {
        for(int i=0;i<5;i++)
        {
           //TrashCard.Add(HandCard[i]);
        }

        ReBulid();
    }

    void ReBulid()
    {
        if(MyCard.Count == 0)
        {
            for(int i=0;i<TrashCard.Count;i++)
            {
                //MyCard.Add(TrashCard[i]);
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
        Vector3 scale = transform.localScale;
        scale.x = 0.6f;
        scale.y = 0.6f;
        transform.localScale = scale;
        yield return null;
    }

}
