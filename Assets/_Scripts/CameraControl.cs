using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    private CinemachineVirtualCamera _cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private void Awake()
    {
        instance = this;
        _cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        _noise = _cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShaking();
    }
    public void ShakeCamera(float intensity, float time)
    {
        _noise.m_AmplitudeGain = intensity;
        Invoke(nameof(StopShaking), time);
    }
    public void StopShaking()
    {
        _noise.m_AmplitudeGain = 0f;
    }
    public void IncreaseCamera()
    {
        _cinemachineCamera.m_Lens.OrthographicSize = 5f;
    }
}
