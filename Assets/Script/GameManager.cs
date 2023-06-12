using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string startSceneName;


    public void StartGame()
    {
        SceneManager.LoadScene(startSceneName);
    }

    public void QuitGame()
    {
        // Menutup aplikasi saat bermain di standalone
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        // Menutup permainan saat bermain di editor Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
