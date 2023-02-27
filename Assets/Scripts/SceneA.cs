using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneA : MonoBehaviour
{
    public static SceneA InstanceA;
    Brick myBrick;
    int numBricks;

    void Awake()
    {
        if (InstanceA != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceA = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddBrick()
    {
        this.numBricks++;
    }

    public void SubtractBrick()
    {
        this.numBricks--;
        CheckLevelComplete();
    }

    public void EndScene()
    {
        Destroy(gameObject);
    }

    void CheckLevelComplete()
    {
        if (numBricks == 0)
        {
            
        }
    }
}
