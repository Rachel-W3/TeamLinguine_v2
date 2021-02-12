using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 121;
    public TextMeshProUGUI timerText;
    private float minutes;
    private float seconds;
    public RawImage redBar;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            minutes = Mathf.FloorToInt(timeRemaining / 60);
            seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            redBar.rectTransform.sizeDelta = new Vector2((100f - ((timeRemaining % 120f) * .83f)) * 1.75f, 100f);
            //redBar.rectTransform.position = new Vector3((x + (120 -(timeRemaining % 120f) * .83f)), redBar.rectTransform.position.y, redBar.rectTransform.position.z);
// new Rect(redBar.uvRect.x, redBar.uvRect.y, , redBar.uvRect.height);

        }
        else
        {
            gameOver = true;

        }
    }
}
