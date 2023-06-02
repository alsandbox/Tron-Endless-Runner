using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    private float rotationGemSpeed = 90;
    public TagsHandler tagsHandler;


    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            BasicMovement();
            RotateGems();
        }
    }

    private void BasicMovement()
    {
        transform.Translate(Vector3.right * GameManager.Instance.speed * Time.deltaTime, Space.World);
    }

    private void RotateGems()
    {
        if (gameObject.CompareTag(tagsHandler.gem) | gameObject.CompareTag(tagsHandler.specialGem))
        {
            transform.Rotate(Vector3.forward, rotationGemSpeed * Time.deltaTime);
        }
    }
}
