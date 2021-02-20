using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    string scoreDisplay;
    TextMeshProUGUI scoreTMP;

    // Start is called before the first frame update
    void Start()
    {
        scoreTMP = GetComponentInChildren<TextMeshProUGUI>();
        scoreDisplay = scoreTMP.text + RatScript.scoreInt;
        scoreTMP.SetText(scoreDisplay);
    }

    // For the finish button...much copy pasta
    public void Quit()
    {
        Application.Quit();
    }
}
