using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Data.Visuals;
using UnityEngine;

namespace Visuals
{
    public class BlinkOnDamage : MonoBehaviour
    {
        [SerializeField] private DamageBlink values;
        private float BlinkDuration => values.blinkTime;
        private float blinkSpeed => values.blinkSpeed;
        private List<Renderer> childRenderers;
        private List<Color> originalColors;
        private Coroutine _blinking;

        private void Awake()
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>();
            childRenderers = new List<Renderer>();
            originalColors = new List<Color>();

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].TryGetComponent<Renderer>(out var childRen))
                {
                    childRenderers.Add(childRen);
                    originalColors.Add(childRen.material.color);
                }
            }
        }

        public void StartBlinking()
        {
            if (_blinking != null)
            {
                StopCoroutine(_blinking);
            }

            _blinking = StartCoroutine(BlinkCorroutine());
        }

        private IEnumerator BlinkCorroutine()
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < BlinkDuration)
            {
                float t = Mathf.PingPong(elapsedTime * blinkSpeed, 1.0f);
                for (int i = 0; i < childRenderers.Count; i++)
                {
                    Color blinkColor = Color.Lerp(originalColors[i], Color.red, t);
                    childRenderers[i].material.color = blinkColor;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < childRenderers.Count; i++)
            {
                childRenderers[i].material.color = originalColors[i];
            }

            yield break;
        }
    }
}