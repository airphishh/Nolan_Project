using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResumeGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject quitButton;
    public GameObject resumeButton;

    public Button pauseButton;
    
    // Resumes the game
    public void Resume()
    {
        menu.SetActive(false);
        quitButton.SetActive(false);
        resumeButton.SetActive(false);

        pauseButton.interactable = true;

        MasterController.paused = false;
    }
}
