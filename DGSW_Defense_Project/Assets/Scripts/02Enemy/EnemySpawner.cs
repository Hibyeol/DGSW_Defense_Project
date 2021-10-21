using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //PlayTime pT;

    public GameObject defalt_EnemyPrefabs; // 생성할 원본 
    public GameObject aerial_EnemyPrefabs; // 생성할 원본2
    public GameObject physical_EnemyPrefabs; // 생성할 원본3
    public float spawnRateMin = 0.5f; // 최소 생성 주기
    public float spawnRateMax = 3f; //최대 생성 주기

    public Transform[] spawnPoints;

    //public int round;
    //private Transform target; // 추적당할 대상 
    private float spanwRate; //생성주기
    private float timeAfterSpawn; //최근 생성 시점에서 지난 시간

    void Start()
    {
        timeAfterSpawn = 0f; // 누적 시간 초기화
        //pT = FindObjectOfType<PlayTime>();
        spanwRate = Random.Range(spawnRateMin, spawnRateMax);
        //round = 1;

    }

    void Update()
    {

        if (timeAfterSpawn >= spanwRate && GameManager.instance.enemyCount < GameManager.instance.round_enemy[GameManager.instance.round]) // 누적된 시간이 생성주기와 같거나 크다면
        {
            int x = Random.Range(0, spawnPoints.Length);
            Debug.Log("[ES]Update / round_enemy : " + GameManager.instance.round_enemy[0]);
            Spawn(x);
        }
        timeAfterSpawn += Time.deltaTime;// 갱신

    }
    void Spawn(int ranNum)
    {
        Debug.Log("[ES]Spawn / test");

        timeAfterSpawn = 0f; //리셋  
        if (GameManager.instance.round > 0)
        {
            GameObject defalt = Instantiate(defalt_EnemyPrefabs, spawnPoints[ranNum]);//
            GameManager.instance.enemyCount++;
        }
        if (GameManager.instance.round > 5)
        {
            GameObject aerial = Instantiate(aerial_EnemyPrefabs, spawnPoints[ranNum]);
            GameManager.instance.enemyCount++;
        }
        if (GameManager.instance.round > 6)
        {
            GameObject aerial = Instantiate(physical_EnemyPrefabs, spawnPoints[ranNum]);
            GameManager.instance.enemyCount++;
        }


        //GameObject physical = Instantiate(physical_EnemyPrefabs, transform.position, transform.rotation);//

        spanwRate = Random.Range(spawnRateMin, spawnRateMax);


    }
}
