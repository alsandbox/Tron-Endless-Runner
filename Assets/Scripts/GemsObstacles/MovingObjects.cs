using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    private float rotationGemSpeed = 90;
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
        if (gameObject.CompareTag("Gem") | gameObject.CompareTag("SpecialGem"))
        {
            transform.Rotate(Vector3.forward, rotationGemSpeed * Time.deltaTime);
        }
    }
}
