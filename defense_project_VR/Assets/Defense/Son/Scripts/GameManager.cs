using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float sec;
    int min;

    public int round;
    public int enemy_Death; // 적의 죽음
    public int enemyCount; // 적의 수
    public int[] round_enemy = new int[] { 0, 1, 1, 1, 1, 1, 7, 8, 9, 10, 11, 12, 13, 14 };
    public int middleBossCount = 1;
    public int finalBossCount = 1;

    public bool nextMap;
    public bool nextRound;
    public bool isPmove;
    public bool gameover;
    //public bool gameClear = false;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject point1;
    public GameObject point2;
    public GameObject flare;
    public GameObject playing;
    public GameObject clear;
    public GameObject esc;

    public float totalTime;
    public int score;
    public int totalscore;
    public Text timetext;
    public Text roundtext;
    public Text scoretext;
    public Text totalText;
    public Text clearText;

    void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        sec = 60;
        min = 0;
        round = 1;
        score = 0;
        nextMap = false;
        spawn1.SetActive(true);
        spawn2.SetActive(false);
        point1.SetActive(true);
        point2.SetActive(false);
        flare.SetActive(false);
        playing.SetActive(true);
        nextRound = false;
        clear.SetActive(false);
        isPmove = true;
        gameover = false;
        //esc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (sec <= 0 || gameover)
        {
            Clear();
        }
        else
        {
            TimeSet(); // 시간
        }

        Score();
        //Esc();
    }

    void TimeSet()
    {
        timetext.text = "남은시간 : " + (int)sec + "초"; // 플레이 시간
  
        sec -= Time.deltaTime;
        totalTime += Time.deltaTime;
        //Debug.Log("[GM]Timeset / totalTime" + totalTime);
        
    }

     void Score()
    {
        //score = (int)(totalTime * 100 + (round * 100));
        scoretext.text = "Score : " + score;

    }

    void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPmove = !isPmove;
        }
    }
  
    public void mainGo()
    {
        SceneManager.LoadScene("Main 1");
    }
    public void restart()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

     public void Clear()
    {
        playing.SetActive(false);
        spawn1.SetActive(false);
        clear.SetActive(true);
        if (gameover)
        {
            clearText.text = "GameOver";
        }
        else
        {
            clearText.text = "Clear";
        }
        isPmove = false;
        totalscore = score;
        totalText.text = "점수\n" + score;
    }
}
