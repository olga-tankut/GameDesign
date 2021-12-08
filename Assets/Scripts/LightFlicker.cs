using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//Turns the light off for a short while and on again for a random length
public class LightFlicker : MonoBehaviour
{
    public Light2D light; //place your light here
    public float minTime; //minimum On time before flick
    public float maxTime; //maximum On time before flick
    float time = 0; //time when the lamp is on
    public float offTime; //the time how long the lamp will be out
    float _offtime; //temp copy of offtime


    // Start is called before the first frame update
    void Start()
    {
        time = GetRandomTime(); //First random roll
        _offtime = offTime; //Set temp value once, just in case
        if(light == null) //Try to find the light in children when not set
        {
            try { light = this.gameObject.GetComponentInChildren<Light2D>(); }
            catch { 
                Debug.Log("ERROR: Can't find Lamp!");
                this.enabled = false; //Deactivate this component to prevent errors
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0) time -= Time.deltaTime; //Count down timer
        else //if timer is down, get a new time and turn of lamp
        {
            time = GetRandomTime();
            Flick();
        }
        if (_offtime > 0) _offtime -= Time.deltaTime;
        if (_offtime <= 0) light.enabled = true;
    }

    float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    void Flick() //Flicks the lamp off and on again
    {
        _offtime = offTime; //Set temp value up
        light.enabled = false; //Turn light off
    }
}
