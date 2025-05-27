using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject exitButton;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject restartButton; 

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        resumeButton.SetActive(false);
        restartButton.SetActive(false); 
    }

    public void StartGame()
    {
        Time.timeScale = 1f;  
        startButton.SetActive(false);  
        exitButton.SetActive(false);  
        pauseButton.SetActive(true);   

        GameManager.Instance.StartTimer(); 
    }

    public void ExitGame()
    {
        Application.Quit(); 

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }

    public void PauseGame()
    {
        isPaused = true; 
        Time.timeScale = 0f;  
        resumeButton.SetActive(true);  
        pauseButton.SetActive(false); 
    }

    public void ResumeGame()
    {
        isPaused = false; 
        Time.timeScale = 1f;  
        resumeButton.SetActive(false);  
        pauseButton.SetActive(true);  
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOverUI()
    {
        pauseButton.SetActive(false);      
        resumeButton.SetActive(false);     
        restartButton.SetActive(true);     
        exitButton.SetActive(true);     
    }

}
