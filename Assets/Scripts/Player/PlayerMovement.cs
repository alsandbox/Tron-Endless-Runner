using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float SpeedReductionSideways
    {
        get => GameManager.Instance.speed / 1.5f;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            Vector3 playerInput = new Vector3(0, 0, horizontalInput);

            transform.Translate(playerInput * (SpeedReductionSideways * Time.deltaTime));

            PreventOutOfBounds();
        }
    }

    private void PreventOutOfBounds()
    {
        float preventLeftBoundary = -7.80f;
        float preventRightBoundary = 8.75f;

        if (transform.position.z <= preventLeftBoundary)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, preventLeftBoundary);
            transform.position = newPosition;
        }

        if (transform.position.z >= preventRightBoundary)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, preventRightBoundary);
            transform.position = newPosition;
        }
    }
}
