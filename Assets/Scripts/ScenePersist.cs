using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static int numPersists = 0;

    Brick myBrick;
    int numBricks;

    void Awake()
    {
        if (numPersists >= 2)
        {
            Destroy(gameObject);
            return;
        }

        numPersists++;
        DontDestroyOnLoad(gameObject);
    }

    public void AddBrick()
    {
        this.numBricks++;
    }

    public void SubtractBrick()
    {
        this.numBricks--;
    }

    public void EndScene()
    {
        Destroy(gameObject);
    }
}
