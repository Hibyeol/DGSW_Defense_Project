using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float sec;
    int min;

    public int round;
    public int enemy_Death; // 적의 죽음
    public int enemyCount; // 적의 수
    public int[] round_enemy = new int[] { 0, 1, 1, 1, 1, 1, 7, 8, 9, 10, 11, 12, 13, 14 };

    public float totalTime;
    public Text timetext;
    public Text roundtext;
    public Text scoretext;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sec = 0;
        min = 0;
        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSet(); // 시간
        RoundSet();
        Score();
    }

    void TimeSet()
    {
        timetext.text = "Time : " + min + "분" + (int)sec + "초"; // 플레이 시간
        //Debug.Log("[GM]Timeset / 시간" + min + sec);
        sec += Time.deltaTime;
        totalTime += Time.deltaTime;
        //Debug.Log("[GM]Timeset / totalTime" + totalTime);
        if (sec >= 60)
        {
            min += 1;
            sec = 0;
        }
    }
    void RoundSet()
    {
        roundtext.text = "Round " + round ;
        if (round_enemy[round] == enemy_Death)
        {
            round++;
            enemy_Death = 0;
            enemyCount = 0;
        }
    
    }

    void Score()
    {
        scoretext.text = "Score : " + (int)(totalTime * 100 + (round*100));
        
    }
}
