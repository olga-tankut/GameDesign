using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public int damage = 20;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Do something, cause collider is with Player");
            PlayerMovement victim = collision.gameObject.GetComponent<PlayerMovement>();
            victim.Damage(damage);
        }
    }
}
