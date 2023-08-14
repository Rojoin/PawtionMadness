using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToScreen : MonoBehaviour
{
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.TryGetComponent<Outline>(out var component))
        {
            component.enabled = true;
        }

    }
}