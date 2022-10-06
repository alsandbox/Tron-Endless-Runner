using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private GameObject[] gemsPrefabs;

    [SerializeField] private GameObject specialGemPrefab;

    [HideInInspector] public Vector3 pointToSpawnObstacle;
    private Vector3 pointToSpawnGems;

    private int randomIndexObstacles;
    private int randomIndexGems;
    [HideInInspector] public int randomRotationObstacle;

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
        InvokeRepeating("SpawnObstacle", startDelay, repeatRateObstacles);
        InvokeRepeating("SpawnCollectables", startDelay, repeatRateGems);
        InvokeRepeating("SpawnSpecialGem", startDelaySpecialGem, repeatRateSpecialGem);
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            RandomIndex();
            RandomObstacleRotation();
        }
        else
        {
            CancelInvoke();
        }
    }

    private void RandomIndex()
    {
        randomIndexObstacles = Random.Range(0, obstaclesPrefabs.Length);
        randomIndexGems = Random.Range(0, gemsPrefabs.Length);
    }

    private void RandomObstacleRotation()
    {
        randomRotationObstacle = Random.Range(0, 180);
    }

    private void SpawnObstacle()
    {
        Quaternion rotationObstacle = Quaternion.Euler(-90f, 0, randomRotationObstacle);
        pointToSpawnObstacle = new Vector3(-30, 2.94f, GameManager.Instance.randomSpawnObjPos);
        Instantiate(obstaclesPrefabs[randomIndexObstacles], pointToSpawnObstacle, rotationObstacle, spawnParent);
    }

    private void SpawnCollectables()
    {
        Quaternion rotationGem = Quaternion.Euler(-90f, 0f, 0f);
        pointToSpawnGems = new Vector3(-23f, 1f, GameManager.Instance.randomSpawnGemsPos);
        Instantiate(gemsPrefabs[randomIndexGems], pointToSpawnGems, rotationGem, spawnParent);
    }

    private void SpawnSpecialGem()
    {
        Quaternion rotationGem = Quaternion.Euler(-90f, 0f, 0f);
        pointToSpawnGems = new Vector3(-26f, 1f, GameManager.Instance.randomSpawnGemsPos);
        Instantiate(specialGemPrefab, pointToSpawnGems, rotationGem, spawnParent);
    }

    public void StopSpawnObstacles()
    {
        CancelInvoke("SpawnObstacle");
    }
}