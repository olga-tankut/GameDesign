using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds a temporary slowdown on the player
public class PlayerSlowDown : MonoBehaviour
{
    public float reduceGroundspeedBy = 3f; //How much acceleration speed should be removed for a while?
    public float reductionTime = 2f; //How long should the reduction happen?
    public bool destroyParent = true; //Destroy parent object if hit
    public bool destroyThis = false; //Destroy own object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && collision.gameObject.name == "Scripts")
        {
            Debug.Log("Lamp hit Player");
            //PLAY SOUNDEFFEKT
            SlowDownPlayer();
            DestroyCheck();
        }
        if (collision.gameObject.tag == "LVL")
        {
            //PLAY /OTHER/ SOUNDEFFEKT
            DestroyCheck();
        }
    }

    void SlowDownPlayer()
    {
        PlayerMovement.instance.ReduceGroundSpeed(reduceGroundspeedBy, reductionTime);
    }

    void DestroyCheck() //Check what needs to be destoryed
    {
        if (destroyParent)
        {
            Destroy(this.transform.parent.gameObject); //Destroy parent object
        }
        else if (destroyThis)
        {
            Destroy(this.gameObject);
        }
    }
}
