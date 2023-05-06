using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static Scene InstanceS;
    bool isInAlt = false;
    int numBricks;
    GameController myController;
    BricksMap myBricksMap;
    AltBricksMap myAltBricksMap;
    Ball myBall;
    Paddle myPaddle;
    bool justTeleported = false;

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

        // if (this.myBall != null && this.myPaddle != null && this.myBricksMap != null && this.myController != null)
        // {
        //     Debug.Log("all good in da hood");
        // }
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
            this.myController.IncreaseScore();
            CheckLevelComplete();
        }
    }

    void CheckLevelComplete()
    {
        if (numBricks == 0)
        {
            Debug.Log("load next level");
        }
    }

    public IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // if (currentSceneIndex == 8 / /)
        yield return new WaitForSecondsRealtime(2);
    }

    public void HandleDeath() 
    {
        this.myController.HandleDeath();
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
