using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{

    public void LevelSelect(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);        
    }

}
