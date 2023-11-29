using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIAnimation
    {
        public IEnumerator PlayAnimation(RectTransform rectTransform, float size, float timeUntilEndOfAnimation,
            TextMeshProUGUI text)
        {
            Vector2 sizeDelta = rectTransform.sizeDelta;
            Vector2 initialSize = new Vector2(sizeDelta.x, sizeDelta.y);
            Vector2 maxRezSize = initialSize * size;
            Color baseColor = text.faceColor;
            Color initialColor = new Color32(255, 255, 255, 0);
            text.color = initialColor;
            float timer = 0.0f;
            text.enabled = true;
            while (timer < timeUntilEndOfAnimation)
            {
                timer += Time.deltaTime;
                Vector2 currentSize = Vector2.Lerp(initialSize, maxRezSize, timer / timeUntilEndOfAnimation);
                float currentColor = Mathf.Lerp(initialColor.a, baseColor.a, timer / timeUntilEndOfAnimation);
                initialColor.a = currentColor;
                text.color = initialColor;
                rectTransform.sizeDelta = currentSize;
                yield return null;
            }

            text.enabled = false;
            text.faceColor = initialColor;
            rectTransform.sizeDelta = initialSize;
            yield break;
        }
        
        public IEnumerator PlayAnimation(RectTransform rectTransform, float size, float timeUntilEndOfAnimation,
            AnimationCurve animationCurve,
            TextMeshProUGUI text)
        {
            Vector2 sizeDelta = rectTransform.sizeDelta;
            Vector2 initialSize = new Vector2(sizeDelta.x, sizeDelta.y);
            Vector2 maxSize = initialSize * size;
            Color baseColor = text.color;
            Color initialColor = new Color32(255, 255, 255, 0);
            text.color = initialColor;
            float timer = 0.0f;
            text.enabled = true;
            while (timer < timeUntilEndOfAnimation)
            {
                float lerpValue = timer / timeUntilEndOfAnimation;
                float curvedValue = animationCurve.Evaluate(lerpValue * animationCurve[animationCurve.length - 1].time);
                timer += Time.deltaTime;
                Vector2 currentSize = Vector2.Lerp(initialSize, maxSize, curvedValue);
                var currentColor = Mathf.Lerp(initialColor.a, baseColor.a, curvedValue);
                initialColor.a = currentColor;
                text.color = initialColor;
                rectTransform.sizeDelta = currentSize;
                yield return null;
            }

            text.enabled = false;
            text.color = baseColor;
            rectTransform.sizeDelta = initialSize;
            yield break;
        }
    }
}