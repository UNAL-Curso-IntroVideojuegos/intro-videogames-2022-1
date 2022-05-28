using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AudioChannel { Master, Sfx, Music };

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    private bool _hasInitialize = false;

    private Dictionary<AudioMusicType, AudioClip> _musicGroup = new Dictionary<AudioMusicType, AudioClip>();
    private Dictionary<string, AudioClip[]> _soundsGroup = new Dictionary<string, AudioClip[]>();

    Transform _audioListener;
    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    int activeMusicSourceIndex;
    
    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void Init()
    {
        if (_hasInitialize)
        {
            return;
        }

        _hasInitialize = true;
        OnInit();
    }

    private void OnInit()
    {
        Load();

        //Get/Create AudioListener
        GameObject audioListener = new GameObject("AudioListener");
        audioListener.transform.parent = transform;
        audioListener.AddComponent<AudioListener>();
        _audioListener = audioListener.transform;
        
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music source " + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            musicSources[i].loop = true;
            newMusicSource.transform.parent = transform;
        }

        GameObject newsfx2DSource = new GameObject("2D sfx source");
        sfx2DSource = newsfx2DSource.AddComponent<AudioSource>();
        newsfx2DSource.transform.parent = transform;

        masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 0.8f);
      
        musicVolumePercent = PlayerPrefs.GetFloat("music vol", 0.2f);
    }

    private void Update()
    {
        _audioListener.position = GameManager.Instance.Player.position;
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }
    
    public void PlayMusic(AudioMusicType type, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = GetClipForMusic(type);
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if(clip != null)
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
    }

    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;
        float speed = 1 / duration;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);

            yield return null;
        }
    }

    private void Load()
    {
        AudioLibrary library = Resources.Load<AudioLibrary>("Scriptable/Audio/Library");
        
        foreach (AudioMusicClip music in library.music)
        {
            if (!_musicGroup.ContainsKey(music.MusicType))
            {
                _musicGroup.Add(music.MusicType, music.Clip);
            }
        }

        foreach (AudioSoundGroup group in library.soundGroups)
        {
            _soundsGroup.Add(group.groupID, group.clips);
        }
    }
    
    private AudioClip GetClipForMusic(AudioMusicType type)
    {
        if (_musicGroup.ContainsKey(type))
        {
            return _musicGroup[type];
        }

        return null;
    }

    private AudioClip GetClipFromName(string soundName)
    {
        if (_soundsGroup.ContainsKey(soundName))
        {
            AudioClip[] sounds = _soundsGroup[soundName];
            return sounds[Random.Range(0, sounds.Length)];
        }

        Debug.Log(soundName + " - Null");
        return null;
    }


    IEnumerator Anim()
    {
        float  count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            yield return null;
        }
    }
}
