using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector2 savedOffset;
    private Vector2 offset;

    private Renderer rndrer;

    private float speed;
    private float speedFactorObjects = 1f;
    private float floorScale;
    private float conversionFactor;


    private void Awake()
    {
        GetTexture();
        ConvertPixelsToUnits();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            RepeatTexture();
            speed = GameManager.Instance.speed;
        }
    }

    private void GetTexture()
    {
        rndrer = GetComponent<Renderer>();
        savedOffset = rndrer.material.GetTextureOffset("_MainTex");
        offset = savedOffset;
    }

    //texture without conversion moves much faster than objects, so convert pixels to units, not the other way around
    private void ConvertPixelsToUnits()
    {
        floorScale = transform.localScale.x;
        conversionFactor = speedFactorObjects / floorScale;
    }

    private void RepeatTexture()
    {
        offset += new Vector2(-(speed * conversionFactor * Time.deltaTime), 0);
        this.rndrer.material.SetTextureOffset("_MainTex", offset);
    }
}

