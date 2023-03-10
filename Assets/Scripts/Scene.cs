using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public static Scene InstanceS;
    bool isInAlt = false;
    int numBricks;
    GameController myController;
    BricksMap myBricksMap;
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
        this.myBall = GetComponentInChildren<Ball>();
        this.myPaddle = GetComponentInChildren<Paddle>();

        if (this.myBall != null && this.myPaddle != null && this.myBricksMap != null && this.myController != null)
        {
            Debug.Log("all good in da hood");
        }
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
        } else
        {
            // 
            this.myController.IncreaseScore();
        }
    }

    public void DestroyScene()
    {
        Destroy(gameObject);
    }

    void CheckLevelComplete()
    {
        if (numBricks == 0)
        {
            Debug.Log("load next level");
        }
    }

    public void HandleDeath() 
    {
        this.myController.HandleDeath();
    }

    public void ReloadObjectPositions()
    {
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
        StartCoroutine(this.ResetJustTeleported());

        this.myBall.Teleport(ballNewPos);
        
        // change tile map
        // change background etc
        // move ball
    }

    public IEnumerator ResetJustTeleported()
    {
        yield return new WaitForSeconds(1);
        this.justTeleported = false;
    }
}
