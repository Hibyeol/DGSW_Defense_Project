using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    public GameObject Clear;
    public GameObject Return;
    public Text totalText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TotalScore();
    }
    void TotalScore()
    {
        totalText.text = "점수" + GameManager.instance.score;

    }
}
