using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DestroyOrRelocate : MonoBehaviour
{
    private SpawnManager spawnManagerScript;
    [SerializeField] private GameObject spawnManager;

    private float randomRotationObstacle;

    private int countObstacles;
    GameObject targetObstacle;

    private void Start()
    {
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
    }

    private void Update()
    {
        randomRotationObstacle = spawnManagerScript.randomRotationObstacle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            targetObstacle = other.gameObject;
            countObstacles++;

            if (countObstacles % 5 == 0)
            {
                GameManager.Instance.IncreaseSpeed();
            }
            if (countObstacles == 5)
            {
                spawnManagerScript.StopSpawnObstacles();
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
        targetObstacle.transform.rotation = Quaternion.Euler(-90, 0, randomRotationObstacle);

        Vector3 pointToRelocateObstacle = new Vector3(GameManager.Instance.randomXPos, 2.94f, GameManager.Instance.randomSpawnObjPos);

        targetObstacle.transform.position = pointToRelocateObstacle;

        Renderer targetObstacleMesh = targetObstacle.GetComponent<Renderer>();

        if (!targetObstacleMesh.enabled)
        {
            targetObstacleMesh.enabled = true;
        }
    }
}
