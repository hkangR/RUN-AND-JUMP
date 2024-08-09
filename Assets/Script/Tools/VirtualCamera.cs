using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noiseProfile;
    [SerializeField]private Transform target;
    
    [Header("Shake info")]
    [SerializeField] private float duration = 0.25f;//时长
    [SerializeField] private float amplitude = 1f; //幅度
    [SerializeField] private float frequency = 1f;//频率

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        if (virtualCamera != null && target != null)
        {
            virtualCamera.Follow = target;

            // 获取并配置 Transposer 组件
            var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                transposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
                transposer.m_FollowOffset.z = -10f; // 锁定在 XY 平面
            }
        }
    }
    
    
    
    public void CameraShake()
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = Mathf.Clamp(amplitude, 0f, 5f);
            noiseProfile.m_FrequencyGain = Mathf.Clamp(frequency, 0f, 10f);
            Invoke(nameof(StopShaking), duration);
        }
    }

    public void CameraShake(float amplitude , float frequency, float duration)
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = Mathf.Clamp(amplitude, 0f, 5f);
            noiseProfile.m_FrequencyGain = Mathf.Clamp(frequency, 0f, 10f);
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
            //ResetCameraOrientation();
        }
    }
    


} 