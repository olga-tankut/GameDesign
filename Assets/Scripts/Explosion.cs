using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject explosionEffect;

    void Start()
    {
        explosionEffect = this.gameObject.transform.GetChild(0).gameObject;
        explosionEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Explode");
        }
    }
    IEnumerator Explode()
    {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
