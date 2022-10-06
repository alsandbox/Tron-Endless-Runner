using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuUIHandler : BaseUIHandler
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private GameObject startButton;

    private void Start()
    {
        SetBestScore();
    }

    private void Update()
    {
        SetSelectedButton(startButton);
    }

    private void SetBestScore()
    {
        bestScoreText.text = $"Best score: {GameManager.Instance.bestScore}";
    }

    private void StartGame()
    {
        GameManager.Instance.PlaySound(clickAudio);
        SceneManager.LoadScene(1);
    }
}
