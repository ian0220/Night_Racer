using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // hellen code is uit dit filmpje gehaald https://youtu.be/6OT43pvUyfY
    public string M_Name;

    public AudioClip M_Clip;

    [Range(0f,1f)]
    public float M_Volume;
    [Range(.1f,3f)]
    public float M_Pitch;

    public bool M_Loop;

    [HideInInspector]
    public AudioSource M_source;
}
