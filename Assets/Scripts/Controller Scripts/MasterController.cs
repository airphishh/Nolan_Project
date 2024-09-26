using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasterController : MonoBehaviour
{

    // 2D ARRAY FOR CONTROLLING BLOCKS. A '0' means block is not present, a '1' 
    // means a block is present and connected to the central hexagon, and a '2'
    // means a block is present but is currently falling. Row zero in our array 
    // represents blocks outside the border. After a certain amount of frames 
    // have passed (denoted by actionSpeed), our blocks will fall.
    public static int[][] blockController = new int[6][];
    
    // Score variables. Every increase in score by 50 + (scoreUpgrade * 10)
    // will increase scoreUpgrade by 1 and increase game speed
    [SerializeField] TextMeshProUGUI scoreText;
    public static int score = 0;
    public static int scoreUpgrade = 0;
    public static int nextUpgrade = 50;

    // Checks if the game is over
    public static bool gameOver = false;

    // Variable for determining if an action can be taken by the AI
    public static bool takeAction = false;

    // Variables for determining how many frames it takes for the AI to make a move
    public static int actionSpeed = 120;
    public static int currFrame = 1;

    // Variable for if game is paused
    public static bool paused = false;

    // GameObject References for when game ends
    public GameObject lose, quitButton, playAgainButton;

    // Start is called before the first frame update
    void Start()
    {
        // Intializes the blocks in the block controller
        for (int i = 0; i < 6; i++)
        {
            blockController[i] = new int[] {0, 0, 0, 0, 0, 0};
        }
        // We put an intial spawn block here
        blockController[0] = new int[] {0, 2, 0, 0, 0, 0};

        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Action updater
        // If 'actionSpeed' amount of frames have passed, our scripts can take
        // actions, like spawning in blocks or moving them down
        if (currFrame == actionSpeed)
        {
            takeAction = true;
            currFrame = 1;
        }
        else
        {
            takeAction = false;
            currFrame += 1;
        }

        // Check if the game is over (we don't need to make this action dependent
        // since our blocks moving down are already action dependent)
        gameOver = GameOverChecker();

        // These functions run every frame
        if (!gameOver && !paused)
        {
            ClearChecker();
        }
        else if (!paused)
        {
            GameOver();
        }
    }

    // Function for checking if we've gone beyond the border
    private bool GameOverChecker() {

        for (int j = 0; j < 6; j++)
            if (blockController[0][j] == 1 && blockController[1][j] == 1)
                return true;

        return false;
    }

    // Function for when the game ends (NEED TO DO)
    private void GameOver()
    {
        lose.SetActive(true);
        quitButton.SetActive(true);
        playAgainButton.SetActive(true);
    }

    // Function for checking if there's a clear (if there's a row with all 1's)
    private void ClearChecker()
    {
        for (int i = 1; i < 6; i++) 
        {
            bool allOne = true;

            // Checking if there's a clear at the current row
            for (int j = 0; j < 6; j++)
                if (blockController[i][j] != 1)
                    allOne = false;


            // If there is a clear, update the block controller array and 
            // increase the score
            if (allOne)
            {
                Clear(i);
                ScoreIncrease();
                break;
            }
        }
    }

    // Function for clearing blocks
    private void Clear(int end)
    {
        // Move every row above clear down one row
        for (int i = end; i > 1; i--) 
        {
            for (int j = 0; j < 6; j++)
            {
                blockController[i][j] = blockController[i - 1][j];
            }
        }

        // Set top row to all 0's
        for (int j = 0; j < 6; j++)
        {
            blockController[1][j] = 0;
        }

        // Clear Sound
        MusicController.pl3 = true;
    }

    // Function for increasing score
    private void ScoreIncrease()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();

        // Basically think of it like a level up
        if (score == nextUpgrade)
        {
            scoreUpgrade += 1;
            nextUpgrade = score + 50 + (scoreUpgrade * 10);

            // We start out by reducing actionSpeed by a constant, but as there's
            // less frames for actionSpeed to iterate through as the score
            // increases, the game speeds up exponentially, and as such, we reduce
            // that constant amount to not make the game go super fast. We also
            // don't want our actionSpeed to be faster than our rotation frames,
            // so that's a thing
            if (actionSpeed > TurnController.rotateTimes)
            {
                if (actionSpeed > 70)
                {
                    actionSpeed -= 10;
                }
                else if (actionSpeed > 40)
                {
                    actionSpeed -= 5;
                }
                else
                {
                    actionSpeed -= 2;
                }

            }
        }

    }
}
