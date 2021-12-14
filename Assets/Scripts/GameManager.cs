using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    
   public bool IsGameplayActive = true;

   public float restartDelay = 1f;
   private GameObject pauseScreen;
   private GameObject gameScreen;
   private GameObject winScreen;
   private GameObject giveTryScreen;
   
   public bool gameIsPaused = false;

    private void Awake()
    {
        if (GameManager.Instance != null) Destroy(this);
        else { Instance = this; }
    }

    private void Start() {
        /*pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        gameScreen = GameObject.FindGameObjectWithTag("GameScreen");
        giveTryScreen = GameObject.FindGameObjectWithTag("GiveTryScreen");
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");*/

        pauseScreen = transform.parent.Find("PauseScreen").gameObject;
        gameScreen = transform.parent.Find("GameScreen").gameObject;
        giveTryScreen = transform.parent.Find("GiveTryScreen").gameObject;
        winScreen = transform.parent.Find("WinScreen").gameObject;

        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        giveTryScreen.SetActive(false);
        winScreen.SetActive(false);
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
        Debug.Log("Game over / Restart Level?");
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
