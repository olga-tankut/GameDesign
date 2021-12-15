using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // the parameter is the name of the Level to load
    public void LoadScene(string levelToLoad)
    {
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        SceneManager.LoadScene(levelToLoad);
        Debug.Log("Make sure all new Scenes/Levels follow the naming convention");
        Debug.Log("Currently loaded scene: " + levelToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}