using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GroundGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public SC_PlatformTile tilePrefab;
    public SC_PlatformTile backgroundPrefab;
    public float movingSpeed = 12;
    public int tilesToPreSpawn = 7; //How many tiles should be pre-spawned
    public int tilesWithoutObstacles = 1; //How many tiles at the beginning should not have obstacles, good for warm-up

    List<SC_PlatformTile> spawnedTiles = new List<SC_PlatformTile>();
    public int backgroundsToPreSpawn = 3;
    List<SC_PlatformTile> spawnedBackgrounds = new List<SC_PlatformTile>();
    public bool gameOver = false;
    public bool gameStarted = false;
    public float score = 0;

    public static SC_GroundGenerator instance;

    void Start()
    {
        instance = this;

        Vector3 spawnPosition = startPoint.position;
        int tilesWithNoObstaclesTmp = tilesWithoutObstacles;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            SC_PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as SC_PlatformTile;
            if(tilesWithNoObstaclesTmp > 0)
            {
                spawnedTile.DeactivateAllObstacles();
                tilesWithNoObstaclesTmp--;
            }
            else
            {
                spawnedTile.ActivateRandomObstacle();
            }
            
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }

        spawnPosition = startPoint.position;
        for (int i = 0; i < backgroundsToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            SC_PlatformTile spawnedBackground = Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity) as SC_PlatformTile;
            
            spawnPosition = spawnedBackground.endPoint.position;
            spawnedBackground.transform.SetParent(transform);
            spawnedBackgrounds.Add(spawnedBackground);
        }
    }

    void Update()
    { 
        if (!gameOver && gameStarted)
        {
            transform.Translate(-spawnedTiles[0].transform.right * Time.deltaTime * (movingSpeed + (score/500)), Space.World);
            score += Time.deltaTime * movingSpeed;
        }

        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            SC_PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
        }

        if (mainCamera.WorldToViewportPoint(spawnedBackgrounds[0].endPoint.position).z < 0)
        {
            SC_PlatformTile backgroundTmp = spawnedBackgrounds[0];
            spawnedBackgrounds.RemoveAt(0);
            backgroundTmp.transform.position = spawnedBackgrounds[spawnedBackgrounds.Count - 1].endPoint.position - backgroundTmp.startPoint.localPosition;
            backgroundTmp.ActivateRandomObstacle();
            spawnedBackgrounds.Add(backgroundTmp);
        }

        if (gameOver || !gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameOver)
                {
                    //Restart current scene
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                else
                {
                    //Start the game
                    gameStarted = true;
                }
            }
        }
    }
}