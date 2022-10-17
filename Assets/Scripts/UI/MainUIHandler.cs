using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainUIHandler : BaseUIHandler
{
    [SerializeField] private GameObject hintsUI;
    [SerializeField] private GameObject scorePanelUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject resumeButton;

    [SerializeField] private GameObject backgroundSound;

    private AudioSource playedMainSound;
    private AudioLowPassFilter pauseSound;

    private bool gameIsPaused;

    protected override void Awake()
    {
        base.Awake();
        Hints();
        playedMainSound = backgroundSound.GetComponent<AudioSource>();
        pauseSound = backgroundSound.GetComponent<AudioLowPassFilter>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !GameManager.Instance.isGameOver)
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                GameManager.Instance.PlaySound(clickAudio);
                PauseGame();
            }
        }
        else if(gameIsPaused)
        {
            SetSelectedButton(resumeButton);
        }
        
        if (GameManager.Instance.isGameOver)
        {
            SetSelectedButton(restartButton);
        }
    }

    private void Hints()
    {
        if (!PlayerPrefs.HasKey("FirstTimeOpening"))
        {
            PlayerPrefs.SetInt("FirstTimeOpening", 0);
            StartCoroutine(HintsForTime());
        }
    }

    IEnumerator HintsForTime()
    {
        hintsUI.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        hintsUI.SetActive(false);
    }

    private void PauseGame()
    {
        gameIsPaused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        pauseSound.cutoffFrequency = 800f;
    }

    private void ResumeGame()
    {
        GameManager.Instance.PlaySound(clickAudio);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseSound.cutoffFrequency = 5000f;
    }

    //called from PlayerController, UnityEvent
    private void GameOver()
    {
        playedMainSound.Stop();
        gameOverUI.SetActive(true);
        scorePanelUI.SetActive(false);
        currentScoreText.text = $"Score: {GameManager.Instance.score}";
        bestScoreText.text = $"Best score: {GameManager.Instance.bestScore}";
        firstSelectedNone = true;
    }

    private void RestartGame()
    {
        if (Time.timeScale == 0f) 
        { 
            Time.timeScale = 1f; 
        }
        else if (GameManager.Instance.isGameOver)
        {
            GameManager.Instance.isGameOver = false;
        }
        GameManager.Instance.speed = 25f;
        GameManager.Instance.PlaySound(clickAudio);
        SceneManager.LoadScene(1);
    }
}
