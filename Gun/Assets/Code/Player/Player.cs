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

    void Show()
    {
        int i = -2;
        for(var node = HandCard.First; node != null; node = node.Next)
        {
            GameObject temp;
            temp = Instantiate(node.Value,new Vector3(i,-0.5f,0),Quaternion.identity);
            i += 1;
        }
    }

    void Shuffle()
    {
        List<GameObject> result = new List<GameObject>();
        
        for(var node = MyCard.First; node != null; node = node.Next)
        {
            result.Add(node.Value);
        }

        for(int i= 0; i<result.Count; i++)
        {
            GameObject temp = result[i];
            int index = Random.Range(0,MyCard.Count);
            result[i] = result[index];
            result[index] = temp;
        }

        MyCard.Clear();
        for(int i=0;i<result.Count;i++)
        {
            MyCard.AddLast(result[i]);
        }
    }

    public void DrawCard()
    {
        HandCard.Clear();
        for(int i=0; i<5; i++)
        {
            HandCard.AddLast(MyCard.Last.Value);
            MyCard.RemoveLast();

            if(MyCard.Count == 0)
                ReBulid();
        }
        Show();
    }

    void DropCard()
    {
        for(int i=0;i<5;i++)
        {
           TrashCard.AddFirst(HandCard.First.Value);
           HandCard.RemoveFirst();
        }
    }

    void ReBulid()
    {
        for(int i=0;i<TrashCard.Count;i++)
        {
            MyCard.AddFirst(TrashCard.First.Value);
            TrashCard.RemoveFirst();
        }
        Shuffle();   
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
