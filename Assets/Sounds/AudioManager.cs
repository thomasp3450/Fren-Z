using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

   public static AudioManager Instance;
   public AudioSource musicSource, SFXSource;
   public Sound[] musicTracks, SFXclips;

   private void Awake(){
      if(Instance == null){
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }else{
         Destroy(gameObject);
      }
   }

   private void Start(){
      PlayMusic("Level1");
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
         SFXSource.PlayOneShot(s.audio);
      }

   }
}