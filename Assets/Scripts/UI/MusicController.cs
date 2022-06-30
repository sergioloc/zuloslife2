using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;

    private static GameObject instance;
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

     private void Start(){
         PlayMusic();
     }

     private void Update(){
         if (MenuValues.muteMusic)
            StopMusic();
        else
            PlayMusic();
     }
 
     public void PlayMusic()
     {
         if (!GameObject.Find("MusicMenu").GetComponent<AudioSource>().isPlaying)
            audioSource.Play();
     }
 
     public void StopMusic()
     {
         audioSource.Stop();
     }
}
