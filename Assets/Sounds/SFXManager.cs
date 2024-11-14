using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField]
    private AudioSource soundFXObject;

    private void awake(){
        if(instance == null){
            instance = this;
        }  
    }

    public void playSFX(AudioClip audioClip, Transform spawnTransform, float volume){        
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
        
}
