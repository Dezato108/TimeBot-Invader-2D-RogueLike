using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    [SerializeField] float timeToLoad = 2f;

    private bool gameIsPaused;

    public int levelToGoTo_1, levelToGoTo_2;
    public LevelExit levelExit_1, levelExit_2;
    

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

    public IEnumerator LoadingNextLevel(int nextLevel)
    {
        Time.timeScale = 0.5f;

        UIManager.instance.FadeImage();

        yield return new WaitForSecondsRealtime(timeToLoad);

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextLevel);
    }

    public void LevelPicker()
    {
        levelToGoTo_1 = SceneManager.GetActiveScene().buildIndex;
        levelToGoTo_2 = SceneManager.GetActiveScene().buildIndex;

        while (levelToGoTo_1 == SceneManager.GetActiveScene().buildIndex)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings-1);
            levelToGoTo_1 = rand;
        }

        levelExit_1.PrintRoomName(levelToGoTo_1);
        
        while (levelToGoTo_2 == SceneManager.GetActiveScene().buildIndex || levelToGoTo_2 == levelToGoTo_1)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings-1);
            levelToGoTo_2 = rand;
        }

        levelExit_2.PrintRoomName(levelToGoTo_2);
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
