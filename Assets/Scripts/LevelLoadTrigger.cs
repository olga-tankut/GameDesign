using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{
    public bool disableSpriteOnCollide = true; //Disable the sprite?
    public bool loadNextIndex = true; //Just loads the next on in the list
    public int sceneIndex; //Dndex number of the next scene
    public float loadDelay; //Delay before loading next lvl
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        if (disableSpriteOnCollide)
        {
            try
            {
                sp = this.gameObject.GetComponent<SpriteRenderer>();
            }
            catch
            {
                Debug.Log("ERROR: CAN'T FIND SPRITE RENDERER");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DisableSprite();
            if (loadNextIndex) GameManager.Instance.LoadNextLevel(loadDelay);
            else GameManager.Instance.LoadNextLevel(loadDelay, sceneIndex);
        }
    }

    void DisableSprite()
    {
        if (disableSpriteOnCollide)
        {
            sp.enabled = false;
        }
    }

}
