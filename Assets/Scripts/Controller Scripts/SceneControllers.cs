using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllers : MonoBehaviour
{
    public int scene;
    public void SwitchScene()
    {
        // Resets the current scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); 
        
        // We need to reset these since they are static
        MasterController.blockController = new int[6][]; 
        MasterController.score = 0; 
        MasterController.scoreUpgrade = 0;
        MasterController.nextUpgrade = 0;
        MasterController.gameOver = false;
        MasterController.takeAction = false;
        MasterController.actionSpeed = 120;
        MasterController.currFrame = 1;
        MasterController.paused = false;

        MusicController.pl2 = false;
        MusicController.pl3 = false;

        TurnController.rotating = false; 
        TurnController.controller = new List<int>();

        // Switches to new scene
        SceneManager.LoadScene(scene); 
    }
}
