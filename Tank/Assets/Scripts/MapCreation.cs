using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

    //初始化地图需要的物体
    //0.基地 1.墙  2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] item;
    private float enemyCreateVal = 0;

    //已经包含的地图坐标
    private List<Vector3> itemPosition = new List<Vector3>();

    //地图长度 -10~10 宽度-8~-8
    private void Awake()
    {
        //创建基地
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //创建基地周围的墙
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //创建空气墙
        for(int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for(int j = -8; j < 9; j++)
        {
            CreateItem(item[6], new Vector3(-11, j, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(11, j, 0), Quaternion.identity);
        }

        //初始化玩家
        
        GameObject go= Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //产生敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4f, 10f);

        //实例化地图
        for (int i = 0; i < 40; i++)
        {
            CreateItem(item[1],createRandomPoition(),Quaternion.identity);       
        }
        for(int i = 0; i < 10; i++)
        {
            CreateItem(item[2], createRandomPoition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], createRandomPoition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], createRandomPoition(), Quaternion.identity);
        }

        



    }
    //private void FixedUpdate()
    //{
    //    CreateEnemy();
    //}
    //创建物体
    void CreateItem(GameObject go,Vector3 v3,Quaternion qu)
    {
        GameObject item = Instantiate(go, v3, qu);
        item.transform.SetParent(gameObject.transform);
        itemPosition.Add(v3);
    }
    //创建不重复的坐标 其中嵌套判断
    private Vector3 createRandomPoition()
    {
        //规定长度-10,10那两列空着 宽度-8，8那两行空着
        while (true)
        {
            //产生的随机坐标
            Vector3 createPositon = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPositon))
            {
                return createPositon;
            }
        }
    }
    //用来判断当前位置是否在列表中
    private bool HasThePosition(Vector3 v3)
    {
        for(int i = 0; i < itemPosition.Count; i++)
        {
            if (v3 == itemPosition[i])
            {
                return true;
            }
        }
        return false;
    }

    //间隔时间随机产生敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3[] enemyPos = { new Vector3(-10, 8, 0), new Vector3(0, 8, 0), new Vector3(10, 8, 0) };
        CreateItem(item[3], enemyPos[num], Quaternion.identity);
        //将下方代码也可以写在FixedUpdate间隔调用 
        //if (enemyCreateVal >= 10f)
        //{
        //    CreateItem(item[3], enemyPos[num], Quaternion.identity);
        //    enemyCreateVal = 0;
        //}
        //else
        //{
        //    enemyCreateVal += Time.fixedDeltaTime;
        //}
    }
    
}
