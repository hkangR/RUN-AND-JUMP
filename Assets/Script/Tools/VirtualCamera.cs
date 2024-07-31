using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noiseProfile;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    /// <summary>
    /// 震屏
    /// </summary>
    /// <param name="duration">时长</param>
    /// <param name="amplitude">幅度</param>
    /// <param name="frequency">频率</param>
    public void CameraShake(float duration = 0.25f, float amplitude = 1f, float frequency = 1f)
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = amplitude;
            noiseProfile.m_FrequencyGain = frequency;
            Invoke(nameof(StopShaking), duration);
        }
    }

    /// <summary>
    /// 停止震屏
    /// </summary>
    private void StopShaking()
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = 0f;
            noiseProfile.m_FrequencyGain = 0f;
        }
    }
} 