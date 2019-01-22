using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{

    public GameObject player;
    public GameObject[] enemy;
    public bool createPlayer;
	void Start ()
	{
	    Invoke("BornTank",1f);
	}

    void BornTank()
    {
        if (createPlayer)
        {
            Instantiate(player, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            int ran = Random.Range(0, 2);
            Instantiate(enemy[ran], transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
