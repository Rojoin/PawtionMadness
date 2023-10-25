using System;
using Player;
using UnityEngine;

public class VisualPlayerInteract : MonoBehaviour
{
    private PlayerInteract _playerInteract;
    private ModelReference model;

    private void Start()
    {
        _playerInteract = GetComponent<PlayerInteract>();
        _playerInteract.OnInteractableAtRange.AddListener(MarkHit);
    }

    private void OnDisable()
    {
        _playerInteract.OnInteractableAtRange.RemoveAllListeners();
    }

    private void MarkHit(RaycastHit hit)
    {
        if (hit.collider)
        {
            ModelReference newModelHit = hit.collider.GetComponentInChildren<ModelReference>();

            if (newModelHit && newModelHit != model)
            {
                if (model)
                    model.DeactivateOutline();

                model = newModelHit;
                model.ActivateOutline();
            }
        }
        else if (model)
        {
            model.DeactivateOutline();
            model = null;
        }
    }
}