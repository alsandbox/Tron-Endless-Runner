using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonByKeyboard : MonoBehaviour, ISelectHandler
{
    [SerializeField] private AudioClip hoverAudio;

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.PlaySound(hoverAudio);
    }
}
