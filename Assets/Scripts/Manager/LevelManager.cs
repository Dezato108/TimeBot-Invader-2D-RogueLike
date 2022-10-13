using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    [SerializeField] float timeToLoad = 2f;

    private bool gameIsPaused;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseResumeGame();
        }
    }

    public IEnumerator LoadingNextLevel(string nextLevel)
    {
        Time.timeScale = 0.5f;

        UIManager.instance.FadeImage();

        yield return new WaitForSecondsRealtime(timeToLoad);

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextLevel);
    }

    public void RestarLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);        
    }
    public bool IsGamePaused() { return gameIsPaused; }

    public void PauseResumeGame()
    {
        if (!gameIsPaused)
        {
            UIManager.instance.TurnPauseMenuOnOff(true);
            gameIsPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIManager.instance.TurnPauseMenuOnOff(false);
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
    }

    

}
