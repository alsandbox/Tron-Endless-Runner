using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuBikeMovement : MonoBehaviour
{
    public GameObject UIPosition;
    public GameObject ButtonsUIPosition;

    private RectTransform rectTransform;
    private RectTransform rectTransformUI;
    private Transform bikeTransform;

    private float targetPosLeft;
    private float targetPosRight;
    private readonly float speed = 200;

    private Vector3 targetPosition;
    private bool isRotated;


    void Start()
    {
        rectTransformUI = UIPosition.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        bikeTransform = GetComponent<Transform>();

        targetPosLeft = rectTransformUI.rect.width + (-rectTransform.rect.width);
        targetPosition = new Vector3(-(targetPosLeft), rectTransform.anchoredPosition.y);
    }


    void Update()
    {
        MoveBike();
    }


    private void MoveBike()
    {
        if (rectTransform.anchoredPosition.x != targetPosition.x)
        {
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, targetPosition, speed * Time.deltaTime);

            isRotated = false;
        }
        else
        {
            if (!isRotated)
            {
                isRotated = true;
                bikeTransform.Rotate(0f, 180f, 0f, Space.Self);
                targetPosRight = targetPosLeft - rectTransformUI.rect.width;

                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
                targetPosition = new Vector3(-(targetPosRight), rectTransform.anchoredPosition.y);
            }
            else
            {
                targetPosition = new Vector3(-(targetPosLeft), rectTransform.anchoredPosition.y);
            }
        }
    }
}
