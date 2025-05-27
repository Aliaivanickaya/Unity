using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverText;
    public TMP_Text timeText;
    public TMP_Text recordText;

    private float timer = 0f;
    private bool isGameOver = false;
    private bool isGameStarted = false; 

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        float record = PlayerPrefs.GetFloat("record", 0f);
        recordText.text = "Best: " + record.ToString("F2");
        timeText.text = "Time: 0.00"; 
    }

    void Update()
    {
        if (!isGameOver && isGameStarted)  
        {
            timer += Time.deltaTime;
            timeText.text = "Time: " + timer.ToString("F2");
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);

        float record = PlayerPrefs.GetFloat("record", 0f);

        if (timer > record)
        {
            PlayerPrefs.SetFloat("record", timer);
            record = timer;
        }

        timeText.text = "Time: " + timer.ToString("F2");
        recordText.text = "Best: " + record.ToString("F2");

        UIManager ui = FindObjectOfType<UIManager>();
        ui.ShowGameOverUI();
    }

    public void StartTimer() 
    {
        isGameStarted = true;
    }
}
