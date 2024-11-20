using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{   
    public static ScreenShake Instance { get; private set;}
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineImpulseSource impulseSource;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private void Start()
    {
        
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(CinemachineImpulseSource impulseSource, float intensity){
        impulseSource.GenerateImpulseWithForce(intensity);
    }

    // Update is called once per frame
    void Update()
    {   
        if(shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
                if(shakeTimer <= 0f){
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));  
                }
        }
        
    }
}
