using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnContact : MonoBehaviour
{
    public string soundName = "BoxContact";
    public float soundOffTime = 1f; //The downtime off the soundeffekt after playing
    bool enabled = false; //at the beginnen, let falling parts fall in place and NOT make a sound

    private void Start()
    {
        Invoke("EnableDelay", 0.5f); //enable after a short time
    }

    void EnableDelay()
    {
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Play();   
    }

    void Play()
    {
        if (enabled)
        {
            enabled = false; //disable here for a short while to eliminate incoming spam
            AudioManager.instance.Play(soundName);
            Invoke("EnableDelay", soundOffTime); //delay b4 playing the sound again, prevent horrible spam
        }
    }
}
