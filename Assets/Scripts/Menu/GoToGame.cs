using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour, IPointerClickHandler
{
    public void GoToGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GoToGameScene();
    }
    public void ExitAplication()
    {
        Application.Quit();
       
    }
}
