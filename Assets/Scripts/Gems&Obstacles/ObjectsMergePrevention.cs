using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMergePrevention : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("World") | other.gameObject.CompareTag("Player")))
        {
            this.transform.position = new Vector3(GameManager.Instance.randomXPos, 2.94f, GameManager.Instance.randomSpawnObjPos);
        }
    }
}
