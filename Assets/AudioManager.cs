using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioSource musicSource;

   public AudioClip music;

   private void Start(){
    musicSource.clip = music;
    musicSource.Play();
   }
}
