using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMgr : MonoBehaviour
{
    public string SceneToLoad;

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene 1");
        //SceneManager.LoadScene(SceneToLoad);
    }
}