using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController InstanceG;
    Scene currentSceneScript;
    int currentIndex;
    int loseScreenIndex = 10;
    int winScreenIndex = 11;

    static bool[] activeScenes = new bool[] {true, false, false, 
                                    false, false, false, 
                                    false, false, false};

    void Awake() 
    {
        if (InstanceG != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceG = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ReloadSceneDeets()
    {
        this.currentSceneScript = GameObject.FindWithTag("Scene").GetComponent<Scene>();
        this.currentIndex = this.currentSceneScript.GetIndex();
    }

    public void OnLevelLose()
    {
        SceneManager.LoadScene(this.loseScreenIndex);
    }

    public void OnLevelWin()
    {
        if (this.currentIndex == 9)
        {
            // 
        }
        else 
        {
            activeScenes[this.currentIndex] = true;
            SceneManager.LoadScene(this.winScreenIndex);
        }
    }

    public void SetIndex(RestartButton restartButton)
    {        
        restartButton.SetRestartIndex(this.currentIndex);
    }

    public void SetIndexPlus(NextButton nextButton)
    {
        nextButton.SetNextIndex(this.currentIndex + 1);
    }

    public static bool[] GetActiveScenes()
    {
        return activeScenes;
    }

}
