#ifndef Include_NiloOutlineUtil
#define Include_NiloOutlineUtil

float GetCameraFOV()
{
    float t = unity_CameraProjection._m11;
    float Rad2Deg = 180 / 3.1415;
    float fov = atan(1.0f / t) * 2.0 * Rad2Deg;
    return fov;
}
float ApplyOutlineDistanceFadeOut(float inputMulFix)
{
    return saturate(inputMulFix);
}
float GetOutlineCameraFovAndDistanceFixMultiplier(float positionVS_Z)
{
    float cameraMulFix;
    if(unity_OrthoParams.w == 0)
    {  
        cameraMulFix = abs(positionVS_Z);

        cameraMulFix = ApplyOutlineDistanceFadeOut(cameraMulFix);

        cameraMulFix *= GetCameraFOV();       
    }
    else
    {
        float orthoSize = abs(unity_OrthoParams.y);
        orthoSize = ApplyOutlineDistanceFadeOut(orthoSize);
        cameraMulFix = orthoSize * 50;
    }

    return cameraMulFix * 0.00005; 
}
#endif

