using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class gameGUI : MonoBehaviour
{
    public bool gameOver;
    public bool gameStarted;
    public float score;
    public float bestScore;
    GUIStyle style = new GUIStyle();
    public UnityEngine.Rendering.PostProcessing.PostProcessVolume volume;
    void Start() {
        bestScore = bestScoreScript.Score;
        style.fontSize = 30;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.UpperCenter;

        gameOver = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().gameOver;
        gameStarted = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().gameStarted;
        score = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().score;
    }
    private void Update() {
        gameOver = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().gameOver;
        gameStarted = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().gameStarted;
        score = GameObject.Find("_GroundGenerator").GetComponent<SC_GroundGenerator>().score;
    }
    void OnGUI()
    {
        if (gameOver)
        {
            if (score > bestScore)
            {
                bestScore = score;
                bestScoreScript.Score = score;
            }
            volume.weight = 1f;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 200), "Game Over\nYour score is: " + ((int)score) + "\nYour best score is: " + ((int)bestScore) + "\nPress 'Space' to restart", style);
        }
        else
        {
            if (!gameStarted)
            {
                volume.weight = 1f;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 200), "Press 'Space' to start", style);
            }
            else
            {
                volume.weight = 0f;
                GUI.color = Color.white;
                GUI.Label(new Rect(5, 5, 200, 25), "Score: " + ((int)score));
            }
        }
    }
}
