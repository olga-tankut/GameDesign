using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DisableShadowOnTrigger : MonoBehaviour
{
    public bool specialTagOnly = false;
    public string tag;
    public ShadowCaster2D shadow;

    // Start is called before the first frame update
    void Start()
    {
        if(shadow == null)
        {
            Debug.Log("ERROR CAN'T FIND COMPONENT!");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(specialTagOnly)
        {
            if (collision.tag == tag) DisableComponent();
        } else DisableComponent();
    }

    void DisableComponent()
    {
        shadow.enabled = false;
    }
}
