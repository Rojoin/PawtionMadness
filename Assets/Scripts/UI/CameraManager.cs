using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CameraLerp cameraLerp;

    private void Awake()
    {
        cameraLerp.endCameraLerp.AddListener(OnEndCameraLerp);
        virtualCamera.Follow = cameraLerp.cameraPosition;
    }

    private void OnEndCameraLerp()
    {
        cameraLerp.enabled = false;
        Debug.Log("end lerp");
    }
}
