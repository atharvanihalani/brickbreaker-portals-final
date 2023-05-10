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
    bool bluePilled = false;

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
        SceneManager.LoadScene(Constants.loseScreenIndex);
    }

    public void OnLevelWin()
    {
        if (this.currentIndex == 9 && !this.bluePilled)
        {
            SceneManager.LoadScene(Constants.sikeIndex);
        }
        else if (this.currentIndex == 9 && this.bluePilled)
        {
            SceneManager.LoadScene(Constants.gameWonFr);
        }
        else if (this.currentIndex == 10)
        {
            this.bluePilled = true;
            SceneManager.LoadScene(Constants.matrixIndex);
        }
        else
        {
            activeScenes[this.currentIndex] = true;
            SceneManager.LoadScene(Constants.winScreenIndex);
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
