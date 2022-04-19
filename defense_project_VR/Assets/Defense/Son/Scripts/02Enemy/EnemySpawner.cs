using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //PlayTime pT;

    public GameObject defalt_EnemyPrefabs; // 생성할 원본 
    public GameObject aerial_EnemyPrefabs; // 생성할 원본2
    public GameObject physical_EnemyPrefabs; // 생성할 원본3
    public GameObject speed_EnemyPrefabs; // 생성할 원본4
    public GameObject explosion_EnemyPrefabs; // 생성할 원본5
    public GameObject reinforced_EnemyPrefabs; // 생성할 원본6
    public GameObject middle_EnemyPrefabs; // 생성할 원본7
    public GameObject final_EnemyPrefabs; // 생성할 원본8
    public float spawnRateMin = 0.5f; // 최소 생성 주기
    public float spawnRateMax = 1.5f; //최대 생성 주기


    public Transform[] spawnPoints;
    public GameObject[] round5;
    public GameObject[] round7;
    public GameObject[] round10;
    public GameObject[] round13;
    public GameObject[] round16;



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

        if (timeAfterSpawn >= spanwRate && GameManager.instance.sec > 0) // 누적된 시간이 생성주기와 같거나 크다면
        {
            int x = Random.Range(0, spawnPoints.Length);
            int y = Random.Range(0, 9);
            //Debug.Log("[ES]Update / round_enemy : " + GameManager.instance.round_enemy[0]);
            Spawn(x, y);
        }
        timeAfterSpawn += Time.deltaTime;// 갱신




    }
    void Spawn(int ranNumx, int ranNumy)
    {
        timeAfterSpawn = 0f; //리셋  
        GameObject speed = Instantiate(round16[ranNumy], spawnPoints[ranNumx]);
        spanwRate = Random.Range(spawnRateMin, spawnRateMax);

        if (GameManager.instance.sec <= 0)
        {
            Destroy(speed);
        }


    }


}
