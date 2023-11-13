using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class UIAnimation
    {
        public IEnumerator PlayAnimation(RectTransform rectTransform, float size, float timeUntilEndOfAnimation,
            TextMeshProUGUI text)
        {
            Rect initialRec = rectTransform.rect;
            Vector2 initialRecSize = initialRec.size;
            Vector2 maxRezSize = initialRecSize * size;
            Color baseColor = text.faceColor;
            Color initialColor = new Color32(255, 255, 255, 0);
            text.color = initialColor;
            float timer = 0.0f;
            text.enabled = true;
            //TODO: Fix scale not working
            while (timer < timeUntilEndOfAnimation)
            {
                timer += Time.deltaTime;
                var currentSize = Vector2.Lerp(initialRecSize, maxRezSize, timer / timeUntilEndOfAnimation);
                var currentColor = Mathf.Lerp(initialColor.a, baseColor.a, timer / timeUntilEndOfAnimation);
                initialColor.a = currentColor;
                text.color = initialColor;
                initialRec.size = currentSize;
                yield return null;
            }

            text.enabled = false;
            text.faceColor = initialColor;
            initialRec.size = initialRecSize;
            yield break;
        }
    }
}