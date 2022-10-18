using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    private float speed;

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            speed = GameManager.Instance.speed;
            BasicMovement();
            RotateGems();
        }
    }

    private void BasicMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    private void RotateGems()
    {
        if (gameObject.CompareTag("Gem") | gameObject.CompareTag("SpecialGem"))
        {
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        }
    }
}
