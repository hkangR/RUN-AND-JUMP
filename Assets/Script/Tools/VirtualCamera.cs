using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noiseProfile;
    
    [Header("Shake info")]
    [SerializeField] private float duration = 0.25f;//时长
    [SerializeField] private float amplitude = 1f; //幅度
    [SerializeField] private float frequency = 1f;//频率

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    public void CameraShake()
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = amplitude;
            noiseProfile.m_FrequencyGain = frequency;
            Invoke(nameof(StopShaking), duration);
        }
    }

    public void CameraShake(float amplitude , float frequency, float duration)
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