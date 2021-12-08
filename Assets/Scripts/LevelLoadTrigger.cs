using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{

    public string nextScene; // name of the nextScene loaded
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(PlayerMovement.Instance.ToString());
        
        if (other.gameObject == PlayerMovement.Instance)
        {
           
            FindObjectOfType<AudioManager>().Play("Win");
            GameManager.Instance.LoadNextLevel();
        }
        
    }
}
