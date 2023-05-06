using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    int nextIndex = 0;
    GameController myController;

    void Awake()
    {
        GameObject game = GameObject.FindWithTag("GameController");
        this.myController = game.GetComponent<GameController>();
        this.myController.SetIndexPlus(this);
    }

    public void SetNextIndex(int index)
    {
        this.nextIndex = index;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(this.nextIndex);
    }
}
