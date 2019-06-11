using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Traveler traveler;

    public void Change()
    {
        Debug.Log("spot이 변경 되었습니다.");
    }

    public void GoStart()
    {
        Debug.Log("움직이기 시작합니다");
    }

    public void GoEnd()
    {
        Spot nowSpot = traveler.nowSpot;
        if(nowSpot.nextRoutes.Count > 0)
        {
            Debug.Log("움직임이 끝나 다음으로 이동합니다.");
            traveler.ChangeSpot(nowSpot.nextRoutes[0]);
        }
        else
        {
            Debug.Log("더 이상 갈수 없습니다.");
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        traveler.ChangeSpot(traveler.nowSpot.nextRoutes[0]);
    }
}
