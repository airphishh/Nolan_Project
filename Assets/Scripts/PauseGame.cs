using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject quitButton;
    public GameObject resumeButton;

    public Button pauseButton;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if (MasterController.gameOver == true || MasterController.paused == true)
        {
            pauseButton.interactable = false;
        }
    }
    
    // Pauses the game
    public void Pause()
    {
        MasterController.paused = true;

        menu.SetActive(true);
        quitButton.SetActive(true);
        resumeButton.SetActive(true);

        pauseButton.interactable = false;
    }
}
