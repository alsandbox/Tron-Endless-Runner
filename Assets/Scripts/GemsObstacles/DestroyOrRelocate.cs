using UnityEngine;

public class DestroyOrRelocate : MonoBehaviour
{
    private SpawnManager spawnManagerScript;
    [SerializeField] private GameObject spawnManager;

    private int countObstacles;
    private int targetNumberOfObstacles = 5;

    GameObject targetObstacle;

    public TagsHandler tagsHandler;

    private void Start()
    {
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagsHandler.obstacle))
        {
            targetObstacle = other.gameObject;

            if (GameManager.Instance.speed < GameManager.Instance.maxSpeed)
            {
                countObstacles++;

                if (countObstacles % targetNumberOfObstacles == 0)
                {
                    GameManager.Instance.IncreaseSpeed();
                }
            }
            MoveObject();
        }

        if (other.CompareTag(tagsHandler.gem) || other.CompareTag(tagsHandler.specialGem))
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
