using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Variable for checking if spawn blocks are in block controller
    // Initially set to true because we have a spawn block in block controller
    // (Look at line 49 of MasterController.cs)
    bool spawned = true; 

    public AudioSource musicSrc;

    public AudioClip wompwomp;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSrc.clip = wompwomp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MasterController.gameOver && !MasterController.paused)
        {
            // If the down arrow key is pressed, we speedup the game and all
            // spawn blocks immediately fall and connect to the central
            // hexagon
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Skip();
            }

            // We can only create spawn blocks and move them down if we can
            // take an action
            if (MasterController.takeAction)    
            {
                SpawnChecker();

                // If no spawn blocks are in the block controller, add some in
                if (!spawned)
                {
                    SpawnBlocks();
                }
                // Otherwise, moves all spawn blocks down/connects them to the 
                // block controller
                else
                {
                    MoveBlocks();
                }
            }
        }
    }

    // Function for speeding up the game
    public void Skip(){
        while (spawned)
        {
            MoveBlocks();
            SpawnChecker();
        }
    }

    // Function for checking if any spawn blocks are in the block controller
    private void SpawnChecker()
    {
        bool twoExists = false;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            
                // Once we find a spawn block in our block controller, we don't
                // need to check anymore
                if (MasterController.blockController[i][j] == 2)
                {
                    twoExists = true;
                    break;
                }
            
        }

        if (!twoExists)
        {
            spawned = false;
        }
    }

    // Function that creates spawn blocks inside our block controller
    private void SpawnBlocks()
    {
        int randomSpawn = (int) Random.Range(0, 10);

        // Higher score means more spawn blocks come in at a time (max is 3 spawn blocks)
        if (MasterController.score > 800)
        {
            randomSpawn = (int) Random.Range(7, 10);
        }
        if (MasterController.score > 500)
        {
            randomSpawn = (int) Random.Range(5, 10);
        }
        else if (MasterController.score > 200)
        {
            randomSpawn = (int) Random.Range(3, 10);
        }
        else if (MasterController.score > 10)
        {
            randomSpawn = (int) Random.Range(0, 10);
        }
        // If the user hasn't scored any points yet, then only one spawn block
        // can come in at a time
        else
        {
            randomSpawn = (int) Random.Range(0, 1);
        }

        // Basically the columns for where the spawn blocks can be created
        List<int> spawnersAvailable = new List<int> {0, 1, 2, 3, 4, 5};

        // Creates one spawn block
        if (randomSpawn < 6) // 
        {
            int randomIndex = (int) Random.Range(0, spawnersAvailable.Count);
            int spawner = spawnersAvailable[randomIndex];
            spawnersAvailable.RemoveAt(randomIndex);

            MasterController.blockController[0][spawner] = 2;
        }
        // Creates two spawn blocks
        else if (randomSpawn < 9)
        {
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = (int) Random.Range(0, spawnersAvailable.Count);
                int spawner = spawnersAvailable[randomIndex];
                spawnersAvailable.RemoveAt(randomIndex);

                MasterController.blockController[0][spawner] = 2;
            }
        }
        // Creates three spawn blocks
        else
        {
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = (int) Random.Range(0, spawnersAvailable.Count);
                int spawner = spawnersAvailable[randomIndex];
                spawnersAvailable.RemoveAt(randomIndex);

                MasterController.blockController[0][spawner] = 2;
            }
        }

        // We set spawned to true here because there's spawn blocks in the block
        // controller
        spawned = true;
    }

    // Function that moves spawn blocks down and attaches them to the central
    // hexagon
    private void MoveBlocks()
    {
        // If a spawn block directly touches the central hexagon, it attaches
        // itself to it
        for (int j = 0; j < 6; j++)
        {
            if (MasterController.blockController[5][j] == 2)
            {
                MasterController.blockController[5][j] = 1; 
            }
        }

        for (int i = 4; i >= 0; i--)
        {
            for (int j = 0; j < 6; j++)
            {
                if (MasterController.blockController[i][j] == 2)
                {
                    // If a spawn block touches any blocks parented by the central
                    // hexagon it attaches itself to it
                    if (MasterController.blockController[i + 1][j] == 1)
                    {
                        MasterController.blockController[i][j] = 1; 
                    }
                    // Otherwise, the spawn block drops down to the next row
                    else if (MasterController.blockController[i + 1][j] == 0)
                    {
                        MasterController.blockController[i][j] = 0; 
                        MasterController.blockController[i + 1][j] = 2;
                    }
                }
                
                // Basically another game over checker where if an attached
                // central block exists outside our border, we play our
                // game over sound and the game ends
                if (MasterController.blockController[i][j] == 1 && i == 0)
                {
                    EndSound();
                    MasterController.gameOver = true;
                    break;
                }
            }

            if (MasterController.gameOver)
            {
                break;
            }
        }
    }

    // Function for playing our game over sound
    private void EndSound()
    {
        musicSrc.Play();
    }
}
