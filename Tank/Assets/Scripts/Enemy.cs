using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float moveSpeed = 3;
    private float timeVal = 0;//攻击时间计时器
    private float timeRotateVal = 0;//敌人移动反应计时器
//  private float timerSpeed = 2;
    private Vector3 bulletEulerAngles;

    public GameObject explosionPrefab;   
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bullet;

    private float h;
    private float v=-1;

    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        //攻击CD
        if (timeVal >= 2f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    private void Attack()
    {    
         //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
         Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
         timeVal = 0;
    }
    //坦克的移动方法
    private void Move()
    {
        if (timeRotateVal > 4f)
        {
            int num = Random.Range(0, 8);
            if (num >= 5)//朝下走
            {
                h = 0;
                v = -1;
            }
            else if(num==0)
            {
                h = 0;
                v = 1;
            }
            else if (num > 0 && num <= 2)
            {
                h = 1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = -1;
                v = 0;
            }
            timeRotateVal = 0;
        }
        else
        {
            timeRotateVal += Time.fixedDeltaTime;
        }
        


        transform.Translate(transform.up * v * moveSpeed * Time.fixedDeltaTime,Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }

        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }

        if (v != 0) return;

        transform.Translate(transform.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }

        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
    }

    //坦克的死亡方法
    public void Die()
    {
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(this.gameObject);
        PlayerManager.Instance.score++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //直接转向
            timeRotateVal = 4;
        }
    }
}
