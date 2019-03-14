using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Gun
{

    protected override void Start()
    {
        base.Start();
        LiveTime = 0;
        LifeTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        LiveTime += Time.deltaTime;
        if (LifeTime < LiveTime)
            Destroy(gameObject);
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
