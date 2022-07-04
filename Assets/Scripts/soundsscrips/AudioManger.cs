using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    // hellen code is uit di filmpje gehaald https://youtu.be/6OT43pvUyfY
    public Sound[] M_Sounds;
    public static AudioManger M_Instance;

    private void Awake()
    {
        if(M_Instance == null)
        {
            M_Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound _s in M_Sounds)
        {
            _s.M_source = gameObject.AddComponent<AudioSource>();
            _s.M_source.clip = _s.M_Clip;

            _s.M_source.volume = _s.M_Volume;
            _s.M_source.pitch = _s.M_Pitch;
            _s.M_source.loop = _s.M_Loop;
        }  
    }

    void Start()
    {
        PLay("BackGround");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PLay(string _name)
    {
        Sound _s = Array.Find(M_Sounds, Sound => Sound.M_Name == _name);
        if(_s == null)
        {
            Debug.LogWarning("sound: " + _name + " not found");
            return;
        }   
        
        if(_s.M_source.isPlaying == false)
        {
             _s.M_source.Play();
        }
    }

    public void Stop(string _name)
    {
        Sound _s = Array.Find(M_Sounds, Sound => Sound.M_Name == _name);
        if(_s == null)
        {
            Debug.LogWarning("sound " + _name + " not found");
            return;
        }

        _s.M_source.Stop();
    }
}
