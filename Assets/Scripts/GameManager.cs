using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public int bestScore;

    public bool isGameOver;

    public float speed;
    private float maxSpeed = 45f;
    private float acceleration = .03f;

    public AudioSource soundEffectsSource;

    public float randomSpawnObjPos;
    public float randomSpawnGemsPos;
    public float randomXPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        speed = 25f;
        bestScore = LoadBestScore(bestScore);
    }

    private void Update()
    {
        RandomSpawnPos();
    }
    public void RandomSpawnPos()
    {
        randomSpawnObjPos = Random.Range(-6f, 6f);
        randomSpawnGemsPos = Random.Range(-8f, 8f);
        randomXPos = Random.Range(-35f, -60f);
    }

    public void PlaySound(AudioClip clip)
    {
        soundEffectsSource.clip = clip;
        soundEffectsSource.Play();
    }

    public void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            speed += acceleration;
        }
        else
        {
            speed = maxSpeed;
        }
    }

    public void CheckBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            SaveBestScore(bestScore);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int BestScore;
    }

    private void SaveBestScore(int bestScore) 
    {
        SaveData data = new SaveData();

        data.BestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savebestscore.json", json);
    }

    private int LoadBestScore(int bestScore)
    {
        string path = Application.persistentDataPath + "/savebestscore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.BestScore;
        }
        return bestScore;
    }
}
