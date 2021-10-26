using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    float sec;
    int min;
    public float totalTime;
    public Text timetext;

    // Start is called before the first frame update
    void Start()
    {
        sec = 0;
        min = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timetext.text = "Time : " + min +"분"+ (int)sec + "초";
        //Debug.Log("[PT]Update / 시간" + min + sec);
        sec += Time.deltaTime;
        totalTime += Time.deltaTime;
        //Debug.Log("[PT]Update / totalTime" + totalTime);
        if (sec >= 60)
        {
            min += 1;
            sec = 0;
        }
    }
}
