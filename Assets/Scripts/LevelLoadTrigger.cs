using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{

    public string nextScene = null; // name of the nextScene loaded
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if no alternative Scene is typed in load standard next scene
        if(string.IsNullOrEmpty(nextScene))
        {
            if(other.gameObject.transform.parent.tag == "Player")
            {
                GameManager.Instance.LoadNextLevel();
            }
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("you have overriden the next scene in the Build Load order with a custom one");
        SceneManager.LoadScene(nextScene);
    }
}
