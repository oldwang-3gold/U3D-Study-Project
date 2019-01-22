using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public int lifeValue = 3;
    public int score = 0;
    public bool isDefeat;
    public bool isDead;
    public GameObject Born;
    public Text playerScore;
    public Text playerLife;
    public GameObject UIGameOver;
    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (isDead)
        {
            Recover();
        }
        playerScore.text = score.ToString();
        playerLife.text = lifeValue.ToString();
        if (isDefeat)
        {
            UIGameOver.SetActive(true);
        }
    }
    void Recover()
    {
        
        lifeValue--;
        if (lifeValue == 0)
        {            
            isDefeat = true;
            return;
            //弹出菜单
        }
        else
        {
            GameObject go = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
        }
        isDead = false;
    }
}
