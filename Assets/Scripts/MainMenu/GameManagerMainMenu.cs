using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // the parameter is the name of the Level to load
    public void LoadScene(string levelToLoad)
    {
        //AudioManager.instance.Stop("MainMenu");
        StartCoroutine(DelayLoad(levelToLoad));
        //Debug.Log("Make sure all new Scenes/Levels follow the naming convention");
        //Debug.Log("Currently loaded scene: " + levelToLoad);
    }

    IEnumerator DelayLoad(string levelToLoad)
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}