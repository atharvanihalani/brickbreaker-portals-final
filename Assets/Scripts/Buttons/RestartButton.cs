using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    int restartIndex = 0;
    GameController myController;

    void Awake()
    {
        GameObject game = GameObject.FindWithTag("GameController");
        this.myController = game.GetComponent<GameController>();
        this.myController.SetIndex(this);
    }

    public void SetRestartIndex(int index)
    {
        this.restartIndex = index;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(this.restartIndex);
        Time.timeScale = 1f;
    }
}
