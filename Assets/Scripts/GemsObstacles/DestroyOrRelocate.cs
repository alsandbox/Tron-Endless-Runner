using UnityEngine;
using UnityEngine.Events;

public class DestroyOrRelocate : MonoBehaviour
{
    private SpawnManager spawnManagerScript;
    [SerializeField] private GameObject spawnManager;

    private int countObstacles;
    private int targetNumberOfObstacles = 5;

    GameObject targetObstacle;

    private void Start()
    {
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            targetObstacle = other.gameObject;

            if (GameManager.Instance.speed < GameManager.Instance.maxSpeed)
            {
                countObstacles++;

                if (countObstacles % targetNumberOfObstacles == 0)
                {
                    GameManager.Instance.IncreaseSpeed();
                }
                if (countObstacles == targetNumberOfObstacles)
                {
                    spawnManagerScript.StopSpawnObstacles();
                }
            }
            MoveObject();
        }

        if (other.CompareTag("Gem") || other.CompareTag("SpecialGem"))
        {
            Destroy(other.gameObject);
        }
    }

    private void MoveObject()
    {
        targetObstacle.transform.rotation = spawnManagerScript.rotationObstacle;
        GameManager.Instance.RandomSpawnPos();

        targetObstacle.transform.position = GameManager.Instance.pointToRelocateObstacle;

        Renderer targetObstacleMesh = targetObstacle.GetComponent<Renderer>();

        if (!targetObstacleMesh.enabled)
        {
            targetObstacleMesh.enabled = true;
        }
    }
}
