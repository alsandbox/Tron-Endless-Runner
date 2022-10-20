using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private GameObject[] gemsPrefabs;

    [SerializeField] private GameObject specialGemPrefab;

    [HideInInspector] public Vector3 pointToSpawnObstacle;
    private Vector3 pointToSpawnGems;

    private int randomIndexObstacles;
    private int randomIndexGems;
    private int randomRotationObstacle;

    private float yAxisSpawnGem = 1f;
    private float xRotation = -90;
    private Quaternion rotationGem;
    public Quaternion rotationObstacle;

    UnityEvent getRandomPos = new UnityEvent();

    private float startDelay = 0.2f;
    private float startDelaySpecialGem = 0.5f;
    private float repeatRateObstacles = 1.4f;
    private float repeatRateGems = 0.4f;
    private float repeatRateSpecialGem = 20f;

    private Transform spawnParent;

    private void Awake()
    {
        spawnParent = this.transform;
    }

    private void Start()
    {
        getRandomPos.AddListener(GameManager.Instance.RandomSpawnPos);
        rotationGem = Quaternion.Euler(xRotation, 0f, 0f);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRateObstacles);
        InvokeRepeating("SpawnCollectables", startDelay, repeatRateGems);
        InvokeRepeating("SpawnSpecialGem", startDelaySpecialGem, repeatRateSpecialGem);
    }

    public void SpawnObstacle()
    {
        randomIndexObstacles = Random.Range(0, obstaclesPrefabs.Length);
        randomRotationObstacle = Random.Range(0, 180);
        float xAxisSpawnObstacle = -30f;
        float yAxisSpawnObstacle = 2.94f;
        getRandomPos.Invoke();
        
        rotationObstacle = Quaternion.Euler(xRotation, 0, randomRotationObstacle);
        pointToSpawnObstacle = new Vector3(xAxisSpawnObstacle, yAxisSpawnObstacle, GameManager.Instance.randomSpawnObjPos);
        Instantiate(obstaclesPrefabs[randomIndexObstacles], pointToSpawnObstacle, rotationObstacle, spawnParent);
    }

    private void SpawnCollectables()
    {
        randomIndexGems = Random.Range(0, gemsPrefabs.Length);
        float xAxisSpawnGem = -23;
        getRandomPos.Invoke();
       
        pointToSpawnGems = new Vector3(xAxisSpawnGem, yAxisSpawnGem, GameManager.Instance.randomSpawnGemsPos);
        Instantiate(gemsPrefabs[randomIndexGems], pointToSpawnGems, rotationGem, spawnParent);
    }

    private void SpawnSpecialGem()
    {
        float xAxisSpawnSpecialGem = -26;
        getRandomPos.Invoke();
        
        pointToSpawnGems = new Vector3(xAxisSpawnSpecialGem, yAxisSpawnGem, GameManager.Instance.randomSpawnGemsPos);
        Instantiate(specialGemPrefab, pointToSpawnGems, rotationGem, spawnParent);
    }

    public void StopSpawnObstacles()
    {
        CancelInvoke("SpawnObstacle");
    }

    //event is invoked from MainUIHandler, GameOver method
    public void DeleteListenerGameOver()
    {
        getRandomPos.RemoveListener(GameManager.Instance.RandomSpawnPos);
    }
}