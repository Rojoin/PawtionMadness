using System;
using UnityEngine;

namespace UI
{
    public class ArrowPointer : MonoBehaviour
    {
        public Vector3 LookPosition { get; set; }

        private void Update()
        {
            transform.LookAt(LookPosition);
        }
    }
}