using System;
using System.Collections;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main_Menu
{
    public class PreTutorialController : MonoBehaviour
    {
        [SerializeField] private Button _playTutorialButton;
        [SerializeField] private Button _skipTutorialButton;
        [SerializeField] private SceneChangeData _tutorialScene;
        [SerializeField] private SceneChangeData _gameScene;

        public void Awake()
        {
            _playTutorialButton.onClick.AddListener(GoToTutorial);
            _skipTutorialButton.onClick.AddListener(GoToGame);
        }

        private IEnumerator Start()
        {
            yield return null;
            yield return null;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(_playTutorialButton.gameObject);
            _playTutorialButton.Select();
            yield break;
        }

        public void OnDestroy()
        {
            _playTutorialButton.onClick.RemoveAllListeners();
            _skipTutorialButton.onClick.RemoveAllListeners();
        }

        private void GoToTutorial()
        {
            SceneSwitcher.ChangeScene(_tutorialScene);
        }
        private void GoToGame()
        {
            SceneSwitcher.ChangeScene(_gameScene);
        }
    }
}