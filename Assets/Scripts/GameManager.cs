using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    
   public bool IsGameplayActive = true;

   public float restartDelay = 1f;
   public GameObject pauseScreen;
   public GameObject gameScreen;
   public GameObject winScreen;
   public bool gameIsPaused = false;

    private void Awake()
    {
        if (GameManager.Instance != null) Destroy(this);
        else { Instance = this; }
    }

    private void Start() {
       //pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
       //gameScreen = GameObject.FindGameObjectWithTag("GameScreen");
       pauseScreen.SetActive(false);
       winScreen.SetActive(false);
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

    public void LoadNextLevel() //Load next lvl without a timer, used for buttons
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextLevel(float delay, int index) //Load next lvl with a delay + index
    {
        winScreen.SetActive(true); //Activate winScreen
        StartCoroutine(LoadDelay(delay, index)); //Start delay
    }

    public void LoadNextLevel(float delay) //Load next lvl with a delay + NEXT index
    {
        winScreen.SetActive(true); //Activate winScreen
        StartCoroutine(LoadDelay(delay, SceneManager.GetActiveScene().buildIndex + 1)); //Start delay
    }

    private IEnumerator LoadDelay(float time, int index) //Scene loader that loads next lvl after delay
    {
       yield return new WaitForSeconds(time);
       SceneManager.LoadScene(index);
    }
}
