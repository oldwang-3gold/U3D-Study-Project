using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject brokenEffect;
    public Sprite sp;
	// Use this for initialization
	void Start ()
	{
	    sr = GetComponent<SpriteRenderer>();
	}

    void Die()
    {
        sr.sprite = sp;
        Instantiate(brokenEffect, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
    }
}
