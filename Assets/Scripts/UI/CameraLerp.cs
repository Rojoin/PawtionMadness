using Cinemachine;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public void LerpCameraToPostion(Transform oldCameraPositionLerp, Transform newCameraPositionLerp, ref Transform camera)
    {
       camera.position = Vector3.Lerp(oldCameraPositionLerp.position, newCameraPositionLerp.position, Time.deltaTime);
    }

    public void SetCameraViewToPlayer(ref CinemachineVirtualCamera virtualCamera, Transform player)
    {
        virtualCamera.Follow = player;
    }
}
