using UnityEngine;

public class ObjectsMergePrevention : MonoBehaviour
{
    public TagsHandler tagsHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag(tagsHandler.world) | other.gameObject.CompareTag(tagsHandler.player)))
        {
            GameManager.Instance.RandomSpawnPos();

            this.transform.position = GameManager.Instance.pointToRelocateObstacle;
        }
    }
}
