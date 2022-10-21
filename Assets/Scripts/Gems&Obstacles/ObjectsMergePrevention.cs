using UnityEngine;
using UnityEngine.Events;

public class ObjectsMergePrevention : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("World") | other.gameObject.CompareTag("Player")))
        {
            GameManager.Instance.RandomSpawnPos();

            this.transform.position = GameManager.Instance.pointToRelocateObstacle;
        }
    }
}
