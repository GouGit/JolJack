using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayObject : MonoBehaviour
{
    
    protected enum TPYE
    {
        TOPAZ,
        RUBY,
        SAPPHIRE,
        DIAMOND
    }
    protected TPYE tpye; //속성정의 (토파즈 < 루비 < 사파이어 < 토파즈), 다이아 서로 2배
    protected int attackPower; //공격력 (플레이어는 정의하지 않음)
    protected int hp; //체력

    protected virtual void Action() //행동패턴 정의
    {

    }

    protected virtual void MyTurn() //자신의 차례
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.0f;
        scale.y = 1.0f;
        transform.localScale = scale;
    }

    protected virtual void EndTurn()  //턴 종료
    {

    }
}
