using System;
using System.Collections;
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
        AudioManager.instance.Play("Game");
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
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void PauseLevel()
    {
        if (!gameIsPaused)
        {
            AudioManager.instance.Stop("Game");
            AudioManager.instance.Play("MainMenu");
            gameScreen.SetActive(false);
            pauseScreen.SetActive(true);
            gameIsPaused = true;
        }
        else
        {
            AudioManager.instance.Stop("MainMenu");
            AudioManager.instance.Play("Game");
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
