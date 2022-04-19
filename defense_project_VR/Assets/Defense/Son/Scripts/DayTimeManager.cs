using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeManager: MonoBehaviour
{

    public static DayTimeManager instance;

    public GameObject[] directional;
    public GameObject[] sky;
    public GameObject[] flashlight;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        directional[0].SetActive(true);
        directional[1].SetActive(false);
        sky[0].SetActive(true);
        sky[1].SetActive(false);
        //flashlight[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
