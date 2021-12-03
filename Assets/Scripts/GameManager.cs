using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance = null;

   public bool IsGameplayActive = true;

   public float restartDelay = 1f;
   private GameObject pauseScreen;
   private GameObject gameScreen;
   private GameObject giveTryScreen;
   private GameObject gameOverScreen;
   public bool gameIsPaused = false;
    

   private bool tryHasEnded = false;
   private int amountOfTry = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }    
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    private void Start() {
        SceneGlobals.Setup();
        if (SceneGlobals.FirstRun)
        {
            SceneGlobals.Tries = amountOfTry;
            SceneGlobals.FirstRun = false;
        }
        Debug.Log("on start Start! " + SceneGlobals.Tries);
        StartLevel();
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
    void StartLevel()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        Debug.Log(pauseScreen.ToString());
        gameScreen = GameObject.FindGameObjectWithTag("GameScreen");
        giveTryScreen = GameObject.FindGameObjectWithTag("GiveTryScreen");
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        giveTryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Debug.Log("Start!");
    }

    public void EndTry()
    {
        Debug.Log("amountOfTry " + SceneGlobals.Tries);
        if (tryHasEnded == false)
        {
            tryHasEnded = true;
            
            if (SceneGlobals.Tries == 0)
            {
                GameOver();
            }
            else
            {
                SceneGlobals.Tries -= 1;
                giveTryScreen.SetActive(true);
                Debug.Log("amountOfTry " + SceneGlobals.Tries);
                Invoke("NewTry", restartDelay);
            }
            
        }   
    }

    public void GameOver()
    {
        Debug.Log("game over");
        gameOverScreen.SetActive(true);

    }

    public void RestartLevel()
    {    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneGlobals.Setup(true);
    }

    public void NewTry()
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
