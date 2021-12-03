using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance = null;

   public float restartDelay = 1f;
   private GameObject pauseScreen;
   private GameObject gameScreen;
   
   public bool gameIsPaused = false;
   private bool gameHasEnded = false;
   

    private void Awake()
    {
        Instance = this;
    }

    private void Start() {
        pauseScreen = transform.parent.transform.Find("PauseScreen").gameObject;
        gameScreen = transform.parent.transform.Find("GameScreen").gameObject;
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
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("RestartLevel", restartDelay);
        }   
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
