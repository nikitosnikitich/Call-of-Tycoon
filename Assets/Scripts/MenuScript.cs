using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitPress()
    {
        Application.Quit();
    }
    public void EscapeButtonInGame(int index)
    {
        SceneManager.LoadScene(index);
    }
}
