using UnityEngine;

public class SC_PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject[] obstacles;

    public void ActivateRandomObstacle()
    {
        DeactivateAllObstacles();

        System.Random random = new System.Random();
        int i = random.Next(obstacles.Length);
        obstacles[i].SetActive(true);
    }

    public void DeactivateAllObstacles()
    {
        foreach (GameObject gameObject in obstacles)
        {
            gameObject.SetActive(false);
        }
    }
}