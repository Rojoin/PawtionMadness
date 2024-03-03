using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
  
    [Header("Setup")]
    [SerializeField] private CinemachineVirtualCamera waveFocusCamera;
    [SerializeField] private CinemachineVirtualCamera gridFocusCamera;
    [SerializeField] private CinemachineVirtualCamera kitchenCamera;
    [SerializeField] private VoidChannelSO OnStartEnemySpawner;
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
        OnStartEnemySpawner.RaiseEvent();
    }
    public void ChangeToGridCamera(bool state)
    {
        kitchenCamera.gameObject.SetActive(!state);
        gridFocusCamera.gameObject.SetActive(state);
    }
}
