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
    Scene currentScene;

    void Awake() 
    {
        if (InstanceG != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceG = this;
        DontDestroyOnLoad(gameObject);

        this.currentScene = GameObject.FindWithTag("Scene").GetComponent<Scene>();
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
            this.currentScene.ReloadObjectPositions();
        }
    }

    void LoseGame()
    {
        Debug.Log("you lost");
    }
}
