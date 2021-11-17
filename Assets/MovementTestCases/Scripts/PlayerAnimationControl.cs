using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI() {
        GetKeyPress(Event.current);
    }

    // implement the animation trigger change here
    private void GetKeyPress(Event e)
    {
        if(e.isKey)
        {
            // return e.keyCode;
        }
        else
        {
            // return null;
        }
    }
}
