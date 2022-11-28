using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float SpeedReductionSideways
    {
        get => GameManager.Instance.speed / 1.5f;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 playerInput = new Vector3(verticalInput, 0, horizontalInput);

            transform.Translate(playerInput * (SpeedReductionSideways * Time.deltaTime));

            PreventOutOfBounds();
        }
    }

    private void PreventOutOfBounds()
    {
        float preventUpperBoundary = -4.7f;
        float preventLowerBoundary = 15.7f;
        float preventLeftBoundary = -7.80f;
        float preventRightBoundary = 8.75f;

        if (transform.position.x <= preventUpperBoundary)
        {
            Vector3 newPosition = new Vector3(preventUpperBoundary, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        if (transform.position.x >= preventLowerBoundary)
        {
            Vector3 newPosition = new Vector3(preventLowerBoundary, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

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
