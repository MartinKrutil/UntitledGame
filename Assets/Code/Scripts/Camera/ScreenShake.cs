using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration; //duration of the shake
    [SerializeField] private float shakeMagnitude; //strength of the shake
    [SerializeField] private float shakeFrequency; //speed of the shake

    private CinemachineVirtualCamera virtualCamera;    
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise; //component for simulating screen shake via perlin noise

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    } 

    public async void Shake()
    {
        virtualCameraNoise.m_AmplitudeGain = shakeMagnitude;
        virtualCameraNoise.m_FrequencyGain = shakeFrequency;

        await Task.Delay((int)(shakeDuration * 1000));

        virtualCameraNoise.m_AmplitudeGain = 0f;
        virtualCameraNoise.m_FrequencyGain = 0f;
    }
}

