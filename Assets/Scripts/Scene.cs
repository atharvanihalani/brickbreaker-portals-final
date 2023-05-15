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

    [SerializeField] GameObject regBackground;
    [SerializeField] GameObject altBackground;
    [SerializeField] Sprite regBall;
    [SerializeField] Sprite altBall;
    [SerializeField] Sprite regPaddle;
    [SerializeField] Sprite altPaddle;


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

    public void Teleport(Vector3 ballNewPos, Portal portal)
    {
        if (this.justTeleported) 
        {
            return;
        }
        portal.PlaySound();
        this.justTeleported = true;
        this.myBall.StopTrail();
        StartCoroutine(this.ResetJustTeleported());

        this.isInAlt = !this.isInAlt;
        this.myBall.Teleport(ballNewPos);

        this.SwitchBrickMaps();
        this.SwitchSkins();
    }

    void SwitchSkins()
    {
        SpriteRenderer ballRenderer = this.myBall.GetComponent<SpriteRenderer>();
        SpriteRenderer paddleRenderer = this.myPaddle.GetComponent<SpriteRenderer>();
        if (this.isInAlt)
        {
            this.regBackground.SetActive(false);
            this.altBackground.SetActive(true);
            ballRenderer.sprite = this.altBall;
            paddleRenderer.sprite = this.altPaddle;
        }
        else
        {
            this.altBackground.SetActive(false);
            this.regBackground.SetActive(true);
            ballRenderer.sprite = this.regBall;
            paddleRenderer.sprite = this.regPaddle;
        }        
    }

    void SwitchBrickMaps()  
    {
        Vector3Int[] brickPos = this.myBricksMap.GetBrickPositions().ToArray();
        Vector3Int[] altBrickPos = this.myAltBricksMap.GetBrickPositions().ToArray();

        this.myAltBricksMap.ClearAll();
        this.myAltBricksMap.AddBricksAt(brickPos);
        this.myBricksMap.ClearAll();
        this.myBricksMap.AddBricksAt(altBrickPos, this.isInAlt);
    }

    public IEnumerator ResetJustTeleported()
    {
        yield return new WaitForSeconds(0.5f);
        this.justTeleported = false;
        this.myBall.ResumeTrail();
    }
}
