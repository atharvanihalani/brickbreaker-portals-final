using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadHomeScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
