using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnTrigger : MonoBehaviour
{
    public bool specialTagOnly = false;
    public string tag;
    public List<GameObject> objectList;
    private void Start()
    {
        //Unity Querk: The object needs to be active at start or you can't fint it at runtime anymore
        //To fix this, let all objects be enabled in editor (nicer to edit too) and disable all at start
        foreach (GameObject g in objectList)
        {
            g.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(specialTagOnly)
        {
            if (collision.tag == tag) ActivateObjects();
        } else ActivateObjects();
    }

    void ActivateObjects()
    {
        foreach(GameObject g in objectList)
        {
            g.SetActive(true);
        }
    }
}
