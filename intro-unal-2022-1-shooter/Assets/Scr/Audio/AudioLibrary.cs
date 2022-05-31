using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public AudioMusicClip[] music;
    public AudioSoundGroup[] soundGroups;
}


public enum AudioMusicType { Menu, Gameplay, End}

[Serializable]
public class AudioMusicClip
{
    public AudioMusicType MusicType;
    public AudioClip Clip;
}

[Serializable]
public class AudioSoundGroup
{
    public string groupID;
    public AudioClip[] clips;
}
