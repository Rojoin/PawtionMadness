using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [FormerlySerializedAs("virtualCamera")]
    [Header("Setup")]
    [SerializeField] private CinemachineVirtualCamera cameraWithTracking;
    [SerializeField] private CinemachineVirtualCamera cameraWithTransposer;
    [SerializeField] private CameraLerp cameraLerp;

    private void Awake()
    {
        cameraLerp.endCameraLerp.AddListener(OnEndCameraLerp);
        cameraWithTracking.Follow = cameraLerp.cameraPosition;
    }

    private void OnDestroy()
    {
        cameraLerp.endCameraLerp.RemoveAllListeners();
    }

    private void OnEndCameraLerp()
    {
        cameraLerp.enabled = false;
        cameraWithTracking.gameObject.SetActive(false);
        cameraWithTransposer.gameObject.SetActive(true);
        Debug.Log("end lerp");
    }
}
