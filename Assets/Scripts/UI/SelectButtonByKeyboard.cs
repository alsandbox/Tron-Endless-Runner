using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonByKeyboard : MonoBehaviour, ISelectHandler
{
    [SerializeField] private AudioClip hoverAudio;
    private Animator transitionAnimator;

    private void Awake()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.PlaySound(hoverAudio);
        transitionAnimator.SetTrigger("Selected");
    }
}
