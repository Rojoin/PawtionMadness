using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UICountdown : MonoBehaviour
    {
        [SerializeField] private string[] text;
        [SerializeField] private VoidChannelSO activateScript;
        [SerializeField] [Range(1.0f, 2.5f)] private float timeBetweenNumbers = 1.3f;
        [SerializeField] [Range(1.0f, 2.5f)] private float sizeExpand = 1.5f;
        [SerializeField] private bool shouldWaitForOneMore;
        private TMPro.TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private AnimationCurve _animationCurve;
        private UIAnimation _uiAnimation = new UIAnimation();
        private RectTransform rectTransform;

        public void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            rectTransform = GetComponent<RectTransform>();
            activateScript.Subscribe(PlayAnimation);
        }

        private void OnDestroy()
        {
            activateScript.Unsubscribe(PlayAnimation);
        }

        private void PlayAnimation()
        {
            StartCoroutine(AnimationCorroutine());
        }

        public IEnumerator AnimationCorroutine()
        {
            int currentText = 0;

            if (shouldWaitForOneMore)
            {
                yield return new WaitForSeconds(timeBetweenNumbers);
            }

            while (currentText < text.Length)
            {
                _textMeshProUGUI.text = text[currentText];
                StartCoroutine(_uiAnimation.PlayAnimation(rectTransform, sizeExpand, timeBetweenNumbers,_animationCurve,
                    _textMeshProUGUI));
                yield return new WaitForSeconds(timeBetweenNumbers);
                currentText++;
                yield return null;
            }
            activateScript.Unsubscribe(PlayAnimation);
            activateScript.RaiseEvent();
            gameObject.SetActive(false);
            yield break;
        }
    }
}