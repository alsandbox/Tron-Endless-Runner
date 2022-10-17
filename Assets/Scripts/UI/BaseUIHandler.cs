using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class BaseUIHandler : MonoBehaviour
{
    [SerializeField] protected AudioClip hoverAudio;
    [SerializeField] protected AudioClip clickAudio;

    protected EventSystem eventSystem;
    protected bool firstSelectedNone = true;

    protected virtual void Awake()
    {
        eventSystem = EventSystem.current;
    }

    protected void SetSelectedButton(GameObject targetButton)
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) & firstSelectedNone)
        {
            eventSystem.SetSelectedGameObject(targetButton);
            firstSelectedNone = false;
        }
        
        if (eventSystem.currentSelectedGameObject == null & (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            eventSystem.SetSelectedGameObject(targetButton);
        }
    }

    protected void HoverCursor()
    {
        GameManager.Instance.PlaySound(hoverAudio);
    }
    
    IEnumerator ExitAfterWait()
    {
        yield return new WaitForSecondsRealtime(0.073f);

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
        #else
            Application.Quit();
        #endif
    }

    protected void ExitGame()
    {
        GameManager.Instance.PlaySound(clickAudio);
        StartCoroutine(ExitAfterWait());
    }
}

