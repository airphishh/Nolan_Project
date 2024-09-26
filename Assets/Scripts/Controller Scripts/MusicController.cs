using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource src1, src2, src3;

    public AudioClip music, bop, success;

    public static bool pl2 = false;
    public static bool pl3 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        src1.clip = music;
        src2.clip = bop;
        src3.clip = success;
        
        // Our background music
        src1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // When the game ends, we stop playin our background music
        if (MasterController.gameOver)
        {
            src1.Stop();
        }
        
        PlayerSound();
        ClearSound();
    }

    // Sound for turning the central hexagon
    private void PlayerSound()
    {
        if (pl2)
        {
            src2.Play();
            pl2 = false;
        }
    }

    // Sound for when there's a clear
    private void ClearSound()
    {
        if (pl3)
        {
            src3.Play();
            pl3 = false;
        }
    }
}
