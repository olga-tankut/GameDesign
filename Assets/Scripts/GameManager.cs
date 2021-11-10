using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   bool gameHasEnded = false;
   public float restartDelay = 1f;
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
}
