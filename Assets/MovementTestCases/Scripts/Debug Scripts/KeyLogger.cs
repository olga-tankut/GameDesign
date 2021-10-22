using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class KeyLogger : MonoBehaviour
{
    private KeyCode lastKeyPressed;
    private float timeKeyOnHold = 0f; 
    private bool keyOnHold = false;

    [SerializeField]private bool keyLogsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        KeyLoging(Event.current);
    }

    // frame timings?
    private void KeyLoging(Event e)
    {
        if(keyLogsOn)
        {
            if(e.isKey)
            {
                // Key onhold
                if(lastKeyPressed == e.keyCode)
                {
                    timeKeyOnHold += Time.deltaTime;
                    keyOnHold = true;
                }
                else
                {
                    if(keyOnHold)
                    {
                        keyOnHold = false;
                        Debug.Log("" + lastKeyPressed + " on hold " + timeKeyOnHold + "seconds");
                    }
                    // prints Keyboard KeyPresses
                    Debug.Log("Detected key Code: " + e.keyCode);
                    lastKeyPressed = e.keyCode;
                }
            }
        }
    }
}
