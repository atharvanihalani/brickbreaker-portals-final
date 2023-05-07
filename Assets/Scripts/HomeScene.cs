using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    bool[] activeHomeScenes;
    [SerializeField] Button[] buttons;

    void Start() 
    {
        this.activeHomeScenes = GameController.GetActiveScenes();
        Debug.Log(this.activeHomeScenes);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (this.activeHomeScenes[i])
            {
                buttons[i].interactable = true;
            }
            else 
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void LevelSelect(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
