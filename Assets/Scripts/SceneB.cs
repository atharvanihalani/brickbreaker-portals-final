using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneB : MonoBehaviour
{
    public static SceneB InstanceB;
    Brick myBrick;
    int numBricks;

    void Awake()
    {
        if (InstanceB != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceB = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubtractBrick()
    {
        // add score in game controller
    }

    public void EndScene()
    {
        Destroy(gameObject);
    }

}
