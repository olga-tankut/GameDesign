using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance = null;

   public bool IsGameplayActive = true;

   public float restartDelay = 1f;
   public GameObject pauseScreen;
   public GameObject gameScreen;
   public bool gameIsPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start() {
       pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
       gameScreen = GameObject.FindGameObjectWithTag("GameScreen");
       pauseScreen.SetActive(false);
       gameScreen.SetActive(true);
   }

   private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
       {
            // Pausing
            PauseLevel();
       }
       if(Input.GetKeyDown(KeyCode.R))
       {
           RestartLevel();
       }
   }

    public void EndGame()
    {
        Debug.Log("Game over");
        Invoke("RestartLevel", restartDelay);     
    } 
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void PauseLevel()
    {
        if(!gameIsPaused)
        {
            gameScreen.SetActive(false);
            pauseScreen.SetActive(true);
            gameIsPaused = true;
        }
        else
        {
            gameScreen.SetActive(true);
            pauseScreen.SetActive(false);
            gameIsPaused = false;
        }   
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
