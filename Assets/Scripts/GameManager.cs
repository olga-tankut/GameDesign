using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public float restartDelay = 1f;
   private GameObject pauseScreen;
   private GameObject gameScreen;
   private GameObject gameOverScreen;
   private GameObject gameWinScreen;


   public bool gameIsPaused = false;
   private bool gameHasEnded = false;


    private void Awake()
    {
        if (GameManager.Instance != null) Destroy(this);
        else { Instance = this; }
    }

    private void Start() {
        pauseScreen = transform.parent.transform.Find("PauseScreen").gameObject;
        gameScreen = transform.parent.transform.Find("GameScreen").gameObject;
        gameOverScreen = transform.parent.transform.Find("GameOverScreen").gameObject;
        gameWinScreen = transform.parent.transform.Find("GameWinScreen").gameObject;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        gameWinScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Game");
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
            gameOverScreen.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Lose");
            Invoke("RestartLevel", restartDelay);
        }   
    }

    public void RestartLevel()
    {
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void PauseLevel()
    {
        FindObjectOfType<AudioManager>().Stop("Game");
        FindObjectOfType<AudioManager>().Play("MainMenu");
        if (!gameIsPaused)
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
