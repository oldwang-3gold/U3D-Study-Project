using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 3;
    private float timeVal=0;//攻击时间间隔
    private float timeDefended = 3;//无敌时间
    private float timerSpeed = 2;
    private Vector3 bulletEulerAngles;
    private bool isDefended=true; //玩家是否处于被保护状态
    public GameObject explosionPrefab;
    public GameObject defendedEffectPrefab;
    
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bullet;

    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }
	void Start () {
	    	
	}
	
	
	void Update ()
	{
        //是否处于无敌状态
	    if (isDefended)
	    {
	        defendedEffectPrefab.SetActive(true);
	        timeDefended -= Time.deltaTime ;
	        if (timeDefended <= 0)
	        {
	            isDefended = false;
	            defendedEffectPrefab.SetActive(false);
	        }
	    }


        
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;//停止操作
        }
        Move();
        //攻击CD
        if (timeVal >= 0.4f)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }
    //坦克的移动方法
    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles=new Vector3(0,0,180);
        }

        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }

        if (v != 0) return;



        float h = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
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
        
        if (isDefended)
        {
            return;
        }


        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(this.gameObject);
        PlayerManager.Instance.isDead = true;
        
        
    }
}
