using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Scene : MonoBehaviour
{
    public static Scene InstanceS;
    bool isInAlt = false;
    int numBricks;
    int lives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    GameController myController;
    BricksMap myBricksMap;
    AltBricksMap myAltBricksMap;
    Ball myBall;
    Paddle myPaddle;
    bool justTeleported = false;
    int currentLevel;

    void Awake()
    {
        if (InstanceS != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceS = this;
        DontDestroyOnLoad(gameObject);

        this.myController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.myBricksMap = GetComponentInChildren<BricksMap>();
        this.myAltBricksMap = GetComponentInChildren<AltBricksMap>();
        this.myBall = GetComponentInChildren<Ball>();
        this.myPaddle = GetComponentInChildren<Paddle>();
        this.currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        this.numBricks = this.myBricksMap.GetBrickCount();
    }

    public void SubtractBrick()
    {
        if (!this.isInAlt)
        {
            this.numBricks--;
            CheckLevelComplete();
        }
    }

    void CheckLevelComplete()
    {
        if (numBricks == 0)
        {
            this.NextLevel();
            Debug.Log("load next level");
        }
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // if (currentSceneIndex == 8 / /)
        // yield return new WaitForSecondsRealtime(2);
    }

    public void HandleDeath() 
    {
        this.lives--;
        UnityEngine.Object.Destroy(this.livesText.transform.GetChild(this.lives).gameObject);

        if (this.lives == 0)
        {
            this.myController.LoseGame();
        }
        else 
        {
            StartCoroutine(this.ReloadObjectPositions());
        }
    }

    public IEnumerator ReloadObjectPositions()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 1;
        this.myBall.ResetPosition();
        this.myPaddle.ResetPosition();
    }

    public void Teleport(Vector3 ballNewPos)
    {
        if (this.justTeleported) 
        {
            return;
        }
        this.justTeleported = true;
        this.myBall.StopTrail();
        StartCoroutine(this.ResetJustTeleported());

        this.myBall.Teleport(ballNewPos);

        Vector3Int[] brickPositions = this.myBricksMap.GetBrickPositions().ToArray();
        Vector3Int[] altBrickPositions = this.myAltBricksMap.GetBrickPositions().ToArray();

        this.SwitchBrickMaps(brickPositions, altBrickPositions);
        this.isInAlt = !this.isInAlt;

        // change background etc
    }

    void SwitchBrickMaps(Vector3Int[] brickPos, Vector3Int[] altBrickPos)  
    {
        this.myAltBricksMap.ClearAll();
        this.myAltBricksMap.AddBricksAt(brickPos);
        this.myBricksMap.ClearAll();
        this.myBricksMap.AddBricksAt(altBrickPos);
    }


    public IEnumerator ResetJustTeleported()
    {
        yield return new WaitForSeconds(0.5f);
        this.justTeleported = false;
        this.myBall.ResumeTrail();
    }
}
