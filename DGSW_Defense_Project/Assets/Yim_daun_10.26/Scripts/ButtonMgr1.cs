﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMgr1: MonoBehaviour
{
    public string SceneToLoad;

    public void LoadGame()
    {
        LodingSceneController1.LoadScene(SceneToLoad);
    }
}