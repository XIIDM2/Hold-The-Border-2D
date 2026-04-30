using Cysharp.Threading.Tasks;
using Gameplay.UI;
using Infrastructure.Events;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;

public class AudioService : IAudioService, IDisposable
{
    public const float DEFAULT_VALUE = 0.5f;
    public float SFXVolume
    {
        get
        {
            return _SFXVolume;
        }
        set
        {
            _SFXVolume = Mathf.Clamp01(value);
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

    private const float PITCH_OFFSET = 0.15f;
    private const int SFX_POOL_DEFAULT_CAPACITY = 10;

    private GameObject _audioGameObject;

    private ObjectPool<AudioSource> _SFXPool;

    private float _SFXVolume;
    private AudioSource _music;
    private AudioSource _ambience;

    private IEventBus _eventBus;

    public AudioService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Init()
    {
        CreateAudioSystem();

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
        PlaySoundAsync(clip).Forget();
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
        _audioGameObject = new GameObject(AUDIO_GAMEOBJECT_NAME);

        _SFXPool = new ObjectPool<AudioSource>
        (
            createFunc: CreateSFXSource,
            actionOnGet: GetSFXSource,
            actionOnRelease: ReleaseSFXSource,
            actionOnDestroy: DestroySFXSource,
            collectionCheck: true,
            defaultCapacity: SFX_POOL_DEFAULT_CAPACITY,
            maxSize: 20
        );

        AudioSource[] SFXSourses = new AudioSource[SFX_POOL_DEFAULT_CAPACITY];

        for (int i = 0; i < SFX_POOL_DEFAULT_CAPACITY; i++)
        {
            SFXSourses[i] = _SFXPool.Get();

        }

        for (int i = 0; i < SFX_POOL_DEFAULT_CAPACITY; i++)
        {
            _SFXPool.Release(SFXSourses[i]);
        }

        CreateAmbienceSource();
        CreateMusicSource();

        GameObject.DontDestroyOnLoad(_audioGameObject);
    }

    private AudioSource CreateSFXSource()
    {
        GameObject SFXGameObject = new GameObject(SFX_GAMEOBJECT_NAME);
        SFXGameObject.transform.SetParent(_audioGameObject.transform);
        AudioSource SFX = SFXGameObject.AddComponent<AudioSource>();

        SFX.playOnAwake = false;
        SFX.volume = _SFXVolume;

        return SFX;
    }

    private void GetSFXSource(AudioSource source)
    {
        source.gameObject.SetActive(true);
        source.volume = _SFXVolume;
    }

    private void ReleaseSFXSource(AudioSource source)
    {
        source.gameObject.SetActive(false);
    }

    private void DestroySFXSource(AudioSource source)
    {
        GameObject.Destroy(source.gameObject);
    }

    private void CreateAmbienceSource()
    {
        GameObject AmbienceGameObject = new GameObject(AMBIENCE_GAMEOBJECT_NAME);
        AmbienceGameObject.transform.SetParent(_audioGameObject.transform);
        _ambience = AmbienceGameObject.AddComponent<AudioSource>();

        _ambience.playOnAwake = false;
        _ambience.volume = DEFAULT_VALUE;
    }

    private void CreateMusicSource()
    {
        GameObject MusicGameObject = new GameObject(MUSIC_GAMEOBJECT_NAME);
        MusicGameObject.transform.SetParent(_audioGameObject.transform);

        _music = MusicGameObject.AddComponent<AudioSource>();

        _music.playOnAwake = false;
        _music.loop = true;
        _music.volume = DEFAULT_VALUE;
    }

    private async UniTask PlaySoundAsync(AudioClip clip)
    {
        AudioSource SFX = _SFXPool.Get();

        SFX.pitch = UnityEngine.Random.Range(1 - PITCH_OFFSET, 1 + PITCH_OFFSET);
        SFX.PlayOneShot(clip);

        await UniTask.WaitForSeconds(clip.length, true);

        _SFXPool.Release(SFX);
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
