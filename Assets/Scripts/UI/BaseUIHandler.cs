using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BaseUIHandler : MonoBehaviour
{
    [SerializeField] protected AudioClip hoverAudio;
    [SerializeField] protected AudioClip clickAudio;
    
    protected bool firstSelectedNone = true;

    protected virtual void Update()
    {
        if (Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2)) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    protected void SetSelectedButton(GameObject targetButton)
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) & firstSelectedNone)
        {
            EventSystem.current.SetSelectedGameObject(targetButton);
            firstSelectedNone = false;
        }
        
        if (EventSystem.current.currentSelectedGameObject == null & (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            EventSystem.current.SetSelectedGameObject(targetButton);
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
            SceneManager.LoadScene(0);
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

