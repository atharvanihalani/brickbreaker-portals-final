using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneA : MonoBehaviour, IScene
{
    public static SceneA InstanceA;
    BricksMap myBricksMap;
    int numBricks;
    GameController myController;

    void Awake()
    {
        if (InstanceA != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceA = this;
        DontDestroyOnLoad(gameObject);

        this.myBricksMap = GetComponentInChildren<BricksMap>();
        this.myController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        this.numBricks = this.myBricksMap.GetBrickCount();
    }

    public void SubtractBrick()
    {
        this.numBricks--;
        this.myController.IncreaseScore();
        CheckLevelComplete();
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
}
