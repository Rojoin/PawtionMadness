using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour, IPointerClickHandler
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("TestLayout");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GoToGameScene();
    }
}
