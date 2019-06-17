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
    protected int defensPower; //방어력 (몬스터 패턴, 카드효과로 수치를 올림 - 일시적)
    public int hp; //체력

    protected virtual void Action() //행동패턴 정의
    {

    }

    protected virtual void MyTurn() //자신의 차례
    {
        defensPower = 0;
        Vector3 scale = transform.localScale;
        scale.x = 1.0f;
        scale.y = 1.0f;
        transform.localScale = scale;
    }

    public virtual void EndTurn()  //턴 종료
    {

    }

    public virtual void LoseHp(int damage) //피격
    {
        int shild;
        shild = defensPower - damage;
        if(shild<0) //방어도보다 강한 공격
        {
            defensPower = 0;
            hp += shild;
        }
        else
        {
            defensPower = shild;    
        }

        if(hp<=0) //죽음
        {
            Destroy(gameObject);
        }
    }
}
