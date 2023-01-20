using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuUIHandler : BaseUIHandler
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private GameObject startButton;

    private Animator transitionAnimator;
    private readonly float delayTime = 0.5f;

    private void Start()
    {
        SetBestScore();
    }

    private void Update()
    {
        SetSelectedButton(startButton);
        transitionAnimator = GetComponent<Animator>();
    }

    private void SetBestScore()
    {
        bestScoreText.text = $"Best score: {GameManager.Instance.bestScore}";
    }

    private void StartGame()
    {
        GameManager.Instance.PlaySound(clickAudio);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(delayTime);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
