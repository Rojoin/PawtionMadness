using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelReference : MonoBehaviour
{
    [SerializeField] private OutlineConfig _outlineConfig;
    private List<Outline> outlinedMeshes = new List<Outline>();
    private Outline.Mode outlineMode => _outlineConfig.outlineMode;
    private Color normalColor => _outlineConfig.normalColor;
    private float outlineDefaultWidth => _outlineConfig.outlineDefaultWidth;
    private Color selectedColor => _outlineConfig.selectedColor;
    private float outlineSelectedWidth => _outlineConfig.outlineSelectedWidth;

    public void Awake()
    {
        SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        if (meshes.Length > 0)
        {
            foreach (SkinnedMeshRenderer mesh in meshes)
            {
                Outline newOutLine = mesh.gameObject.AddComponent<Outline>();
                newOutLine.OutlineMode = outlineMode;
                outlinedMeshes.Add(newOutLine);
            }
        }
        else
        {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshRenderers)
            {
                Outline newOutLine = mesh.gameObject.AddComponent<Outline>();
                newOutLine.OutlineMode = outlineMode;
                outlinedMeshes.Add(newOutLine);
            }
        }

        DeactivateOutline();
    }

    public void ActivateOutline()
    {
        foreach (Outline outLine in outlinedMeshes)
        {
            outLine.OutlineWidth = outlineSelectedWidth;
            outLine.OutlineColor = selectedColor;
        }
    }

    public void DeactivateOutline()
    {
        foreach (Outline outLine in outlinedMeshes)
        {
            outLine.OutlineWidth = outlineDefaultWidth;
            outLine.OutlineColor = normalColor;
        }
    }
}