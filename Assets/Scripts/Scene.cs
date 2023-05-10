using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class Scene : MonoBehaviour
{
    bool isInAlt = false;
    int numBricks;
    int lives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject pausePanel;
    GameController myController;
    BricksMap myBricksMap;
    AltBricksMap myAltBricksMap;
    Ball myBall;
    Paddle myPaddle;
    bool justTeleported = false;
    bool isPaused = false;
    int currentLevelIndex;


    void Awake()
    {
        Debug.Log(this.isPaused);
        this.myBricksMap = GetComponentInChildren<BricksMap>();
        this.myAltBricksMap = GetComponentInChildren<AltBricksMap>();
        this.myBall = GetComponentInChildren<Ball>();
        this.myPaddle = GetComponentInChildren<Paddle>();
        this.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        this.myController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        this.numBricks = this.myBricksMap.GetBrickCount();
        this.myController.ReloadSceneDeets();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.isPaused)
            {
                Time.timeScale = 1f;
                this.pausePanel.SetActive(false);
                this.isPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                this.pausePanel.SetActive(true);
                this.isPaused = true;
            }
        }

    }

    public int GetIndex()
    {
        return this.currentLevelIndex;
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
            this.myController.OnLevelWin();
            Debug.Log("load next level");
        }
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
    }

    public void HandleDeath() 
    {
        this.lives--;
        UnityEngine.Object.Destroy(this.livesText.transform.GetChild(this.lives).gameObject);

        if (this.lives == 0)
        {
            this.myController.OnLevelLose();
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
