using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent gameOver;
    public UnityEvent scoreToUI;
    public UnityEvent lifeToUI;

    private int score;
    private int life;
    private int maxBestScore = 999999;

    [SerializeField] private AudioClip collectGems;
    [SerializeField] private AudioClip collisionWithObstacle;
    [SerializeField] private AudioClip gameOverSound;

    [SerializeField] private ParticleSystem destroyObstacleParticle;
    private ParticleSystem pillarObstacleParticle;

    [SerializeField] UnityEvent checkBestScore = new UnityEvent();

    private void Start()
    {
        score = 0;
        UpdateScore(0);
        checkBestScore.AddListener(GameManager.Instance.CheckBestScore);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem") & score < maxBestScore)
        {
            CollectGem(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SpecialGem"))
        {
            GameManager.Instance.PlaySound(collectGems);
            UpdateLife(1);
            Destroy(other.gameObject);
        }
        else if (!other.gameObject.CompareTag("Obstacle") & score == maxBestScore)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle") & life > 0)
        {
            DestroyObstacle(other.gameObject);
        }
        else
        {
            GameManager.Instance.PlaySound(gameOverSound);
            GameOver();
        }
    }

    private void CollectGem(GameObject targetGem)
    {
        GameManager.Instance.PlaySound(collectGems);
        UpdateScore(1);
        
        checkBestScore.Invoke();

        Destroy(targetGem);

        if (score % 50 == 0)
        {
            GameManager.Instance.speed++;
        }
        if (score % 100 == 0)
        {
            UpdateLife(1);
        }
    }

    private void DestroyObstacle(GameObject targetObstacle)
    {
        pillarObstacleParticle = targetObstacle.GetComponentInChildren<ParticleSystem>();
        pillarObstacleParticle.Play();

        destroyObstacleParticle.Play();
        destroyObstacleParticle.Stop();

        pillarObstacleParticle.Stop();

        GameManager.Instance.PlaySound(collisionWithObstacle);
        targetObstacle.GetComponent<Renderer>().enabled = false;

        UpdateLife(-1);
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        GameManager.Instance.score = score;
        scoreToUI.Invoke();
    }

    private void UpdateLife(int lifeToAdd)
    {
        life += lifeToAdd;
        GameManager.Instance.life = life;
        lifeToUI.Invoke();
    }

    private void GameOver()
    {
        GameManager.Instance.isGameOver = true;
        gameOver.Invoke();
        checkBestScore.RemoveListener(GameManager.Instance.CheckBestScore);
    }
}
