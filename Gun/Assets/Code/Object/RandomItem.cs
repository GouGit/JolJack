using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    private GameObject itemBox;
    public GameObject[] box;

    public Transform createPos1, createPos2;

    // Start is called before the first frame update
    void Start()
    {
        int number =  Random.Range(0, box.Length);
        itemBox = Instantiate(box[number], 
            new Vector3(Random.Range(createPos1.position.x,createPos2.position.x), 
            Random.Range(createPos1.position.y, createPos2.position.y), 0), Quaternion.identity);
    }
}