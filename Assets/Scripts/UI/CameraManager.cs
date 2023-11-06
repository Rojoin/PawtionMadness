using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Camera Positions")]
    [SerializeField] private Transform cameraPositionField;
    [SerializeField] private Transform cameraPositionKitchen;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
