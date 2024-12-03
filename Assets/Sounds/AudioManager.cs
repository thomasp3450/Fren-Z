using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

   public static AudioManager Instance;
   public AudioSource musicSource, SFXSource;
   public Sound[] musicTracks, SFXclips;
   public float pitchMin, pitchMax;

   private void Awake(){
      if(Instance == null){
         Instance = this;
         //DontDestroyOnLoad(gameObject);
      }else{
         Destroy(gameObject);
      }
      string currentSceneName = SceneManager.GetActiveScene().name;
      PlayMusic(currentSceneName);
   }

   
   public void PlayMusic(string name){
      Sound s = Array.Find(musicTracks, x => x.name == name); 
      
      if (s == null){
         Debug.Log("no music");
      }else{
         musicSource.clip = s.audio;
         musicSource.Play();
      }
   }

   public void PlaySFX(string name){

      Sound s = Array.Find(SFXclips, x => x.name == name); 
      
      if (s == null){
         Debug.Log("no sound");
      }
      else{
         SFXSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
         SFXSource.PlayOneShot(s.audio);
      }

   }
}
