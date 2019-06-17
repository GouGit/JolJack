using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardManager : MonoBehaviour
{
    
    protected enum TPYE
    {
        TOPAZ,
        RUBY,
        SAPPHIRE,
        DIAMOND
    }
    protected TPYE tpye; //속성정의 (토파즈 < 루비 < 사파이어 < 토파즈), 다이아 서로 2배
    protected int attackPower; //데미지
    protected int defensPower; //방어 상승치
    protected Vector3 origin; //이미지 원래크기
    protected BoxCollider2D myBox;
    protected int useCost;

    protected virtual void SelectCard() //선택한 카드
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.3f;
        scale.y = 1.3f;
        transform.localScale = scale;
    }

    protected virtual void DropCrad() //선택종료
    {
        transform.localScale = origin;
        if(GameManager.instance.cost >= useCost)
        {
            UseCard();
        }
    }

    protected virtual void UseCard() //카드사용트리거
    {
        myBox.enabled = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 0.0f);
        if(hit.collider != null)
        {
            GameManager.instance.cost -= useCost;
            CardAction(hit.collider.gameObject);
            gameObject.SetActive(false);
        }
        myBox.enabled = true;
    }

    protected virtual void CardAction(GameObject monster) //카드효과
    {

    }
    
}
