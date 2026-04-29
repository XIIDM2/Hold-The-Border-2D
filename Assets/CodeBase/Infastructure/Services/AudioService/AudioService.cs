using Gameplay.UI;
using Infrastructure.Events;
using Infrastructure.Services;
using System;
using UnityEngine;
using VContainer.Unity;

public class AudioService : IAudioService, IStartable, IDisposable
{
    public float SFXVolume
    {
        get
        {
            return _SFX.volume;
        }
        set
        {
            _SFX.volume = value;
        }
    }

    public float AmbienceVolume
    {
        get
        {
            return _ambience.volume;
        }
        set
        {
            _ambience.volume = value;
        }
    }

    public float MusicVolume
    {
        get
        {
            return _music.volume;
        }
        set
        {
            _music.volume = value;
        }
    }


    private const string AUDIO_GAMEOBJECT_NAME = "AudioSystem";
    private const string SFX_GAMEOBJECT_NAME = "SFX";
    private const string MUSIC_GAMEOBJECT_NAME = "Music";
    private const string AMBIENCE_GAMEOBJECT_NAME = "Ambience";

    private AudioSource _SFX;
    private AudioSource _music;
    private AudioSource _ambience;

    private IEventBus _eventBus;

    public AudioService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Start()
    {
        CreateAudioSystem();
        InitAudioSystem();

        CustomButton.ButtonClicked += PlaySound;
        CustomButton.ButtonHovered += PlaySound;

        _eventBus.Subscribe<LevelStartedEvent>(OnLevelStarted);
        _eventBus.Subscribe<InvokeSFX>(OnSFX);
    }

    public void Dispose()
    {
        CustomButton.ButtonClicked -= PlaySound;
        CustomButton.ButtonHovered -= PlaySound;

        _eventBus.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
        _eventBus.Unsubscribe<InvokeSFX>(OnSFX);
    }

    public void PlaySound(AudioClip clip)
    {
        _SFX.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        _music.clip = clip;
        _music.Play();
    }

    public void PlayAmbience(AudioClip clip)
    {
        _ambience.clip = clip;
        _ambience.Play();
    }

    private void CreateAudioSystem()
    {
        GameObject audioGameObject = new GameObject(AUDIO_GAMEOBJECT_NAME);

        GameObject SFXGameObject = new GameObject(SFX_GAMEOBJECT_NAME);
        SFXGameObject.transform.SetParent(audioGameObject.transform);

        GameObject MusicGameObject = new GameObject(MUSIC_GAMEOBJECT_NAME);
        MusicGameObject.transform.SetParent(audioGameObject.transform);

        GameObject AmbienceGameObject = new GameObject(AMBIENCE_GAMEOBJECT_NAME);
        AmbienceGameObject.transform.SetParent(audioGameObject.transform);

        GameObject.DontDestroyOnLoad(audioGameObject);

        _SFX = SFXGameObject.AddComponent<AudioSource>();
        _music = MusicGameObject.AddComponent<AudioSource>();
        _ambience = AmbienceGameObject.AddComponent<AudioSource>();
    }

    private void InitAudioSystem()
    {
        _SFX.playOnAwake = false;
        _music.playOnAwake = false;
        _ambience.playOnAwake = false;

        _music.loop = true;

        _SFX.volume = 0.5f;
        _music.volume = 0.5f;
        _ambience.volume = 0.5f;
    }

    private void OnSFX(InvokeSFX SFX)
    {
        PlaySound(SFX.Sound);
    }

    private void OnLevelStarted(LevelStartedEvent levelClips)
    {
       if (levelClips.Music) PlayMusic(levelClips.Music);   
       if (levelClips.Ambience) PlayAmbience(levelClips.Ambience);
    }

}
