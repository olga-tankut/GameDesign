using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   bool gameHasEnded = false;
   public float restartDelay = 1f;
   private GameObject pauseScreen;
   private GameObject gameScreen;
   public bool gameIsPaused = false;

   private void Start() {
       pauseScreen = transform.Find("PauseScreen").gameObject;
       gameScreen = transform.Find("GameScreen").gameObject;
       pauseScreen.SetActive(false);
       gameScreen.SetActive(true);
   }

   private void Update() {
       if(Input.GetKeyDown(KeyCode.Escape))
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
        if(gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game over");
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
}
