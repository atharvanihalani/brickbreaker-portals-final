using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController InstanceG;

    int score = 0;
    int lives = 3;

    Ball myBall;
    Paddle myPaddle;

    void Awake() 
    {
        if (InstanceG != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceG = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateReferences()
    {
        this.myBall = FindObjectOfType<Ball>();
        this.myPaddle = FindObjectOfType<Paddle>();
    }

    public void HandleDeath()
    {
        this.lives--;
        // UnityEngine.Object.Destroy(livesText.transform.GetChild(numPlayerLives).gameObject);
        if (this.lives == 0)
        {
            this.LoseGame();
        }
        else
        {
            this.Reload();
        }
    }

    void Reload()
    {
        this.myBall.ResetPosition();
        this.myPaddle.ResetPosition();
    }

    void LoseGame()
    {
        Debug.Log("you lost");
    }
}
