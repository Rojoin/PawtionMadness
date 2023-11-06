using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UICountdown : MonoBehaviour
    {
        [SerializeField] private  string [] text;
        [SerializeField] private VoidChannelSO activateScript;
        [SerializeField][Range(1.0f, 2.5f)] private float timeBetweenNumbers = 1.3f;
        [SerializeField][Range(1.0f, 2.5f)] private float sizeExpand = 1.5f;
        [SerializeField] private bool shouldWaitForOneMore;
        private TMPro.TextMeshProUGUI _textMeshProUGUI;
        private UIAnimation _uiAnimation = new UIAnimation();
        private RectTransform rectTransform;

        public void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            rectTransform = GetComponent<RectTransform>();
       
        }

        public IEnumerator Start()
        {
            int currentText = 0;

            if (shouldWaitForOneMore)
            {
                yield return new WaitForSeconds(timeBetweenNumbers);
            }
            while (currentText<text.Length )
            {
                _textMeshProUGUI.text = text[currentText];
                StartCoroutine(_uiAnimation.PlayAnimation(rectTransform, sizeExpand, timeBetweenNumbers, _textMeshProUGUI));
                yield return new WaitForSeconds(timeBetweenNumbers);
                currentText++;
                yield return null;
            }
            gameObject.SetActive(false);
            yield break;
        }
        
    }
}