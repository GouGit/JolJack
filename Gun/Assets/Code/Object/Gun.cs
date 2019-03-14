using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum Kinds
    {
        PISTOL,
        RIFLE,
        SHOTGUN,
        SNIPER,
        BOMB
    }
    public Kinds item;

    protected int bulletCount;
    protected int power;
    protected int speed;
    protected float LifeTime, LiveTime;

    public void SetItem(Kinds it)
    {
        item = it;
    }

    protected virtual void Start()
    {
        switch (item)
        {
            case Kinds.PISTOL:
                speed = 13;
                bulletCount = 6;
                power = 5;
                break;
            case Kinds.RIFLE:
                speed = 15;
                bulletCount = 20;
                power = 5;
                break;
            case Kinds.SHOTGUN:
                speed = 12;
                bulletCount = 8;
                power = 8;
                break;
            case Kinds.SNIPER:
                speed = 20;
                bulletCount = 5;
                power = 10;
                break;
            case Kinds.BOMB:
                speed = 10;
                bulletCount = 3;
                power = 15;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.isItem = true;
            player.bulletCount = bulletCount;
            Destroy(gameObject);
        }
    }

}
