using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController InstanceG;

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

    public void LoseGame()
    {
        Debug.Log("you lost");
    }
}
