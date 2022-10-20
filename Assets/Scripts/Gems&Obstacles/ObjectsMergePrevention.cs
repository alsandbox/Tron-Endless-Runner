using UnityEngine;
using UnityEngine.Events;

public class ObjectsMergePrevention : MonoBehaviour
{
    UnityEvent getRandomPos = new UnityEvent();

    private void Start()
    {
        getRandomPos.AddListener(GameManager.Instance.RandomSpawnPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("World") | other.gameObject.CompareTag("Player")))
        {
            getRandomPos.Invoke();

            this.transform.position = GameManager.Instance.pointToRelocateObstacle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.CompareTag("World") | other.gameObject.CompareTag("Player")))
        {
            getRandomPos.RemoveListener(GameManager.Instance.RandomSpawnPos);
        }
    }
}
