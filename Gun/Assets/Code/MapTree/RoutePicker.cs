using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RoutePicker : MonoBehaviour
{
    public Image image;
    public Spot spot;
    public List<Sprite> spirteList;

    void Start()
    {
        image.sprite = spirteList[(int)spot.sceneOption.type];
    }
}