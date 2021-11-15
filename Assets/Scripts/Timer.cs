using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;
    [SerializeField] private float timeLeft = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft > 0)
        {
            decimal x = (decimal)timeLeft;
            timer.text = "" + (Math.Round(x, 2));
        }else{
            timer.text = " 0.00";
            // gameOver screen?
        }


    }
}
