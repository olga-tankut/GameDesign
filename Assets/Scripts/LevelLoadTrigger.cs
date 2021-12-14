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

    public string nextScene = null; // name of the nextScene loaded
    // Start is called before the first frame update
    void Start()
    {
        if(disableSpriteOnCollide)
        {
            try
            {
                sp = this.gameObject.GetComponent<SpriteRenderer>();
            } catch
            {
                Debug.Log("ERROR: CAN'T FIND SPRITE RENDERER");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            DisableSprite();
            if(loadNextIndex) GameManager.Instance.LoadNextLevel(loadDelay);
            else GameManager.Instance.LoadNextLevel(loadDelay, sceneIndex);
        }
    }

    void DisableSprite()
    {
        if(disableSpriteOnCollide)
    private void OnTriggerEnter2D(Collider2D other) {
        // if no alternative Scene is typed in load standard next scene
        if(string.IsNullOrEmpty(nextScene))
        {
            sp.enabled = false;
            if(other.gameObject.transform.parent.tag == "Player")
            {
                Timer.timerIsRunning = false;
                GameManager.Instance.LoadNextLevel();
            }
        }
        else
        {
            Timer.timerIsRunning = false;
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("you have overriden the next scene in the Build Load order with a custom one");
        SceneManager.LoadScene(nextScene);
    }
}
