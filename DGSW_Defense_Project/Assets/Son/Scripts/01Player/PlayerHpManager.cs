using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpManager: MonoBehaviour
{
    public static PlayerHpManager instance;

    public float player1_cur_Hp = 100;
    public float player2_cur_Hp = 100;
    public float player3_cur_Hp = 100;
    public float player4_cur_Hp = 100;

    public float player1_max_Hp = 100;
    public float player2_max_Hp = 100;
    public float player3_max_Hp = 100;
    public float player4_max_Hp = 100;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1();
    }

    public void player1()
    {

    }
}
