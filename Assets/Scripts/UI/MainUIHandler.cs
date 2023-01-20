using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Rendering;

public class MainUIHandler : BaseUIHandler
{
    [SerializeField] private GameObject hintsUI;
    [SerializeField] private GameObject scorePanelUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject resumeButton;

    [SerializeField] private GameObject backgroundSound;
    public UnityEvent stopSpawn;

    private AudioSource playedMainSound;
    private AudioLowPassFilter pauseSound;

    [SerializeField] protected Animator transitionAnimator;
    private readonly float delayTime = 1f;

    private bool gameIsPaused;

    private void Awake()
    {
        Hints();
        playedMainSound = backgroundSound.GetComponent<AudioSource>();
        pauseSound = backgroundSound.GetComponent<AudioLowPassFilter>();
    }

    private void Start()
    {
        TransitionUI();

        if (Cursor.visible & Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void TransitionUI()
    {
        transitionAnimator.SetTrigger("End");
        StartCoroutine(TransitionDelay());
    }

    IEnumerator TransitionDelay()
    {
        yield return new WaitForSeconds(delayTime);
        transitionAnimator.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
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

    //event is invoked from PlayerController, UpdateScore method
    public void DisplayCurrentScore()
    {
        scoreText.text = $"Score: {GameManager.Instance.score}";
    }

    //event is invoked from PlayerController, UpdateLife method
    public void DisplayCurrentLife()
    {
        lifeText.text = $"Life: {GameManager.Instance.life}";
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

    //event is invoked from PlayerController, GameOver method
    private void GameOver()
    {
        stopSpawn.Invoke();
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
