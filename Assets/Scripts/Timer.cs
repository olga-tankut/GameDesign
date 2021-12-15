using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;
    [SerializeField] private float timeLeft = 20.0f;
    public static bool timerIsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning) // is timer stopped?
        {
            if (timeLeft >= 0) // is timer over?
            {
                timeLeft -= Time.deltaTime; // count down
                DisplayTime(timeLeft); // update Timer UI
            }
            else
            {
                timeLeft = 0; // if timer below 0 set timer to 0
                timerIsRunning = false; // stop timer
                timer.text = "0";
                GameManager.Instance.EndGame(); // restart Level
            }
        }
    }
    // Updates UI timer
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // get seconds
        timer.text = string.Format("{0:00}", seconds); // display the new value
    }
}
