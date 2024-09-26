using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour
{

    // Both visualControllerSpawn and visualControllerConnect are
    public GameObject[][] visualControllerSpawn = new GameObject[6][];
    public GameObject[] spawnSub1 = new GameObject[6];
    public GameObject[] spawnSub2 = new GameObject[6];
    public GameObject[] spawnSub3 = new GameObject[6];
    public GameObject[] spawnSub4 = new GameObject[6];
    public GameObject[] spawnSub5 = new GameObject[6];
    public GameObject[] spawnSub6 = new GameObject[6];

    public GameObject[][] visualControllerConnect = new GameObject[6][];
    public GameObject[] connectSub1 = new GameObject[6];
    public GameObject[] connectSub2 = new GameObject[6];
    public GameObject[] connectSub3 = new GameObject[6];
    public GameObject[] connectSub4 = new GameObject[6];
    public GameObject[] connectSub5 = new GameObject[6];
    public GameObject[] connectSub6 = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
            visualControllerSpawn[0] = spawnSub1;
            visualControllerSpawn[1] = spawnSub2;
            visualControllerSpawn[2] = spawnSub3;
            visualControllerSpawn[3] = spawnSub4;
            visualControllerSpawn[4] = spawnSub5;
            visualControllerSpawn[5] = spawnSub6;

            visualControllerConnect[0] = connectSub1;
            visualControllerConnect[1] = connectSub2;
            visualControllerConnect[2] = connectSub3;
            visualControllerConnect[3] = connectSub4;
            visualControllerConnect[4] = connectSub5;
            visualControllerConnect[5] = connectSub6;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawn();

        // This only is called if we are not in the rotation animation
        // (See TurnController.cs)
        if (!TurnController.rotating)
        {
            UpdateConnect();
        }
    }

    // Function for visually updating our spawn blocks
    private void UpdateSpawn()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (MasterController.blockController[i][j] == 2)
                {
                    visualControllerSpawn[i][j].SetActive(true);
                }
                else
                {
                    visualControllerSpawn[i][j].SetActive(false);
                }
            }
        }
    }

    // Function for visually updating our connected blocks
    private void UpdateConnect()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                // Not sure if this is necessary, but it basically acts as
                // another checker to see if we are in the rotation animation
                if (TurnController.controller.Count == 0)
                {
                    if (MasterController.blockController[i][j] == 1)
                    {
                        visualControllerConnect[i][j].SetActive(true);
                    }
                    else
                    {
                        visualControllerConnect[i][j].SetActive(false);
                    }
                }
            }
        }

        // Because of the way this game is animated, once our turn animation
        // is finished, the block visuals get all wonky, so we have to set the
        // rotation back to (0,0,0), which occurs instantenously
        float zRotation = transform.rotation.eulerAngles.z;
        Vector3 rotateDirection = new Vector3(0,0,zRotation);

        if (zRotation > 0 || zRotation < 0)
        {
            transform.Rotate(0,0,-zRotation);
        }
    }
}
