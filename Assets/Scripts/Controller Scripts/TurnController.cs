using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    // Variable that represents the z-rotation per frame
    public static int angle = 3; // MUST BE LESS THAN AND DIVISIBLE BY 60
    // Variable for how many frames it takes to rotate
    public static int rotateTimes = 60 / angle; // Currently 20 rotations
    // Variable for if we are in the rotation animation
    public static bool rotating = false;
    // Vector for rotating
    Vector3 rotateDirection = new Vector3(0,0,angle);
    
    // Since we press arrow keys to turn faster than our actual animation,
    // we create a list variable that stores all the turn commands and executes
    // one every frame
    public static List<int> controller = new List<int>();

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if (!MasterController.gameOver && !MasterController.paused)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                TurnLeftChecker();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                TurnRightChecker();                    
            } 
            
            if (controller.Count > 0) 
            {
                Turn();
            }
        }
    }

    // Function for animation our turning
    private void Turn()
    {
        int currCommand = controller[0];
        controller.RemoveAt(0);

        // A '1' means we turn left
        if (currCommand == 1) 
            transform.Rotate(rotateDirection);
        // A '0' means we turn right
        else if (currCommand == 0)
            transform.Rotate(-rotateDirection);

        // This is where we rotate the actual arrays
        if (controller[0] == 2)
        {
            controller.RemoveAt(0);
            TurnLeftArray();
        }
        else if (controller[0] == 3)
        {
            controller.RemoveAt(0);
            TurnRightArray();
        }

        // Once our controller list is empty, we are done with our rotation
        // animation
        if (controller.Count == 0)
        {
            rotating = false;
        }
    }

    // Function for checking if we can turn left
    public void TurnLeftChecker()
    {
        // Assume initially we can turn
        bool canTurn = true;
        
        for (int i = 5; i >= 0; i--)
        {
            for (int j = 5; j >= 0; j--)
            {
                // If there's a '1' to the right of a '2', we can't turn
                if (MasterController.blockController[i][j] == 1 &&
                    ((j > 0 && MasterController.blockController[i][j - 1] == 2) || 
                    (j == 0 && MasterController.blockController[i][5] == 2)))
                {
                    canTurn = false;
                    break;        
                }
            }
        }

        // If we can turn, then turn
        if (canTurn)
        {
            TurnLeftVisual();
        }
    }

    // Function for checking if we can turn right
    public void TurnRightChecker()
    {
        // Assume initially we can turn
        bool canTurn = true;
        
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                // If there's a '1' to the left of a '2', we can't turn
                if (MasterController.blockController[i][j] == 1 &&
                    ((j < 5 && MasterController.blockController[i][j + 1] == 2) || 
                    (j == 5 && MasterController.blockController[i][0] == 2)))
                {
                    canTurn = false;
                    break;        
                }
            }
        }

        // If we can turn, then turn
        if (canTurn)
        {
            TurnRightVisual();
        }
    }

    // Function for actually turning left
    private void TurnLeftVisual()
    {
        // Actual Turning mechanism
        for (int i = 0; i < rotateTimes; i++)
            controller.Add(1);

        controller.Add(2);

        rotating = true;

        MusicController.pl2 = true; // SOUND PLAYER
    }

    // Function for actually turning right
    private void TurnRightVisual()
    {   
        // Actual Turning mechanism
        for (int i = 0; i < rotateTimes; i++)
            controller.Add(0);

        controller.Add(3);

        rotating = true;

        MusicController.pl2 = true; // SOUND PLAYER
    }

    private void TurnLeftArray()
    {
        // Changing the blockController array
        for (int i = 0; i < 6; i++)
        {
            int temp = MasterController.blockController[i][0];

            for (int j = 0; j < 5; j++)
            {
                if (MasterController.blockController[i][j] == 2)
                {
                    // If our current block is 2, do nothing
                }
                else if (MasterController.blockController[i][j + 1] == 2)
                {
                    MasterController.blockController[i][j] = 0; // If the block after is 2, set current block to 0
                }
                else
                {
                    MasterController.blockController[i][j] = MasterController.blockController[i][j + 1]; // Otherwise, shift every block one to the right
                }
            }

            if (MasterController.blockController[i][5] == 2)
            {
                // If the last block is 2, do nothing
            }
            else if (temp == 2)
            {
                MasterController.blockController[i][5] = 0; // If the initial first block is 2, set last block to 0
            }
            else
            {
                MasterController.blockController[i][5] = temp; // Otherwise, our last block becomes our initial first block
            }
        }
    }

    private void TurnRightArray()
    {
        // Changing the blockController array
        for (int i = 0; i < 6; i++)
        {
            int temp = MasterController.blockController[i][5];

            for (int j = 5; j > 0; j--)
            {
                if (MasterController.blockController[i][j] == 2)
                {
                    // If our current block is 2, do nothing
                }
                else if (MasterController.blockController[i][j - 1] == 2)
                {
                    MasterController.blockController[i][j] = 0; // If the block before is 2, set current block to 0
                }
                else
                {
                    MasterController.blockController[i][j] = MasterController.blockController[i][j - 1]; // Otherwise, shift every block one to the right
                }
            }

            if (MasterController.blockController[i][0] == 2)
            {
                // If the first block is 2, do nothing
            }
            else if (temp == 2)
            {
                MasterController.blockController[i][0] = 0; // If the initial last block is 2, set first block to 0
            }
            else
            {
                MasterController.blockController[i][0] = temp; // Otherwise, our first block becomes our initial last block
            }
        }
    }
}
