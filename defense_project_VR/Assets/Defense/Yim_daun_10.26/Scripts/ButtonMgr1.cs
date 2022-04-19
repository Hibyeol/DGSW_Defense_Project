using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMgr1: MonoBehaviour
{
    public string SceneToLoad;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) > 0)
        {
            SceneManager.LoadScene("SampleScene 1");
        }
    }
    public void LoadGame()
    {
        LodingSceneController1.LoadScene(SceneToLoad);
    }
}