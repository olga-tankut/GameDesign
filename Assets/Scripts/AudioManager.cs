using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    public String startAudioName;
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
       if (instance == null)
           instance = this;
       else
       {
            Destroy(this);
            return;
       }
       
       foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        } 
    }

    private void Start()
    {
        Play(startAudioName);
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        try
        {
            s.source.Play();
        } catch
        {

        }

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
