using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    bool[] activeHomeScenes;

    void Start() 
    {
        // this.activeHomeScenes = GameController.GetActiveScenes();
        // Debug.Log(this.activeHomeScenes);
        // GameObject canvas = GameObject.FindWithTag("Canvas");
        // Button[] buttons = canvas.GetComponents<Button>();
        // GameObject[] buttons = Object.FindObjectsByType(Button, FindObjectsInactive.Include, 
        // FindObjectsSortMode.InstanceID);
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     Button currentButton = buttons[i].GetComponent<Button>();
        //     if (this.activeHomeScenes[i])
        //     {
        //         buttons[i].interactable = true;
        //     }
        //     else 
        //     {
        //         buttons[i].interactable = false;
        //     }
        // }
    }

    public void LevelSelect(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
