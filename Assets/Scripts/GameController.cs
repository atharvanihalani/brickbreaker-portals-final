using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController InstanceG;

    int lives = 3;
    int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

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


    public void IncreaseScore()
    {
        this.score += 10;
        string newScoreText = ("Score: " + score);
        this.scoreText.text = newScoreText;
    }


    public void HandleDeath()
    {
        this.lives--;
        UnityEngine.Object.Destroy(this.livesText.transform.GetChild(this.lives).gameObject);
        if (this.lives == 0)
        {
            this.LoseGame();
        }
        else
        {
            this.ReloadObjectPositions();
        }
    }

    void ReloadObjectPositions()
    {
        this.myBall.ResetPosition();
        this.myPaddle.ResetPosition();
    }

    void LoseGame()
    {
        Debug.Log("you lost");
    }
}
