using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
  
    [Header("Setup")]
    [SerializeField] private CinemachineVirtualCamera waveFocusCamera;
    [SerializeField] private CinemachineVirtualCamera gridFocusCamera;
    [SerializeField] private CinemachineVirtualCamera kitchenCamera;
    [FormerlySerializedAs("cameraLerp")] [SerializeField] private CameraLerp waveFocusCameraLerp;

    private void Awake()
    {
        if (!waveFocusCamera) return;
        waveFocusCameraLerp.endCameraLerp.AddListener(OnEndCameraLerp);
        waveFocusCamera.Follow = waveFocusCameraLerp.cameraPosition;
    }

    private void OnDestroy()
    {
        if (!waveFocusCamera) return;
        waveFocusCameraLerp.endCameraLerp.RemoveAllListeners();
    }

    private void OnEndCameraLerp()
    {
        waveFocusCameraLerp.enabled = false;
        waveFocusCamera.gameObject.SetActive(false);
        kitchenCamera.gameObject.SetActive(true);
        Debug.Log("end lerp");
    }
    public void ChangeToGridCamera(bool state)
    {
        kitchenCamera.gameObject.SetActive(!state);
        gridFocusCamera.gameObject.SetActive(state);
    }
}
