using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool isPlayerBullet;
    
	// Use this for initialization
    void Awake()
    {
        
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)//敌人的子弹射向玩家
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)//玩家的子弹射向敌人
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
