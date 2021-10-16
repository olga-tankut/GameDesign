using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float timeLeft = 20.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft > 0)
        {
            timer.text = "" + timeLeft;
        }else{
            timer.text = " 0.00";
            // gameOver screen?
        }


    }
}
