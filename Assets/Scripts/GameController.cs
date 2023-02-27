using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    int score = 0;
    int lives = 3;

    Ball myBall;
    Paddle myPaddle;

    void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // this.myBall = FindObjectByType<Ball>();
        // this.myPaddle = FindObjectByType<Paddle>();
    }

    void Update()
    {
        
    }

    public void decreaseLife()
    {
        this.lives--;
        if (this.lives == 0) 
        {
            Debug.Log("game over");
        }
    }
}
