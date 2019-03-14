using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    int speed, jump;
    int JumpCount = 0;
    bool isJump = false;
    bool isHit = false;
    int power;

    public Vector3 knockback;
    public bool isItem = false;
    public int bulletCount;
    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 10;
        jump = 20;
    }

    private Vector3 VectorRotation(float _angle)
    {
        //각도에따른 벡터값
        _angle -= 90;
        _angle *= -1;
        if (_angle < 0f)
            _angle += 360f;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), Mathf.Cos(_angle * Mathf.Deg2Rad), 0);
    }

    void Knockback()
    {
        if(isHit)
        {
            transform.position -= knockback;
            knockback -= knockback * power * 10 * Time.deltaTime;
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;

            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;

            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 1)
        {
            if(isJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0.0f);
                rb2d.AddForce(new Vector2(0f, jump - 5), ForceMode2D.Impulse);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0.0f);
                rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            }
            isJump = true;
            JumpCount++;
        }
    }

    float GetAngle(Vector2 vec1, Vector2 vec2)
    {
        Vector2 pos = vec2 - vec1;
        float angle = (float)Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }

    void Shot()
    {
        if(isItem)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, transform.position + (Vector3.right * transform.localScale.x), Quaternion.Euler(0,0,GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))));
                bulletCount--;
                if (bulletCount <= 0)
                    isItem = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            power = bullet.ReturnPower();
            knockback = VectorRotation(GetAngle(transform.position, other.gameObject.transform.position));
            isHit = true;
            if(other.gameObject.transform.position.y > transform.position.y)
            {
                knockback.y *= -1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            if(other.gameObject.transform.position.y < transform.position.y)
            {
                isJump = false;
                JumpCount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Knockback();
        Move();
        Shot();
    }
}
