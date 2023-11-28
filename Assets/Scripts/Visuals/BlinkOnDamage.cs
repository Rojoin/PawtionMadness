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
        [SerializeField] private Material damagedMaterial;
        private float BlinkDuration => values.blinkTime;
        private float blinkSpeed => values.blinkSpeed;
        private List<Renderer> childRenderers;
        private List<Material> materials;
        private Coroutine _blinking;

        private void Awake()
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>();
            childRenderers = new List<Renderer>();
            materials = new List<Material>();

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].TryGetComponent<Renderer>(out var childRen))
                {
                    childRenderers.Add(childRen);
                    materials.Add(childRen.material);
                }
            }

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].TryGetComponent<Renderer>(out var childRen))
                {
                    childRen.material = childRen.sharedMaterial;
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
                    childRenderers[i].material = damagedMaterial;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < childRenderers.Count; i++)
            {
                childRenderers[i].material = materials[i];
            }

            yield break;
        }
    }
}