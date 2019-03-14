using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime*5);
        transform.position += Vector3.back;
    }
}
