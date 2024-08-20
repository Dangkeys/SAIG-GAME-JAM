using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    [BoxGroup("Audio")]
    [field: SerializeField] public AudioSource musicSource { get; private set; }
    [field: SerializeField] public AudioSource walk1Source { get; private set; }
    [field: SerializeField] public AudioSource walk2Source { get; private set; }
    [field: SerializeField] public AudioSource sfxSource { get; private set; }

    [BoxGroup("Music")]
    [SerializeField] private List<AudioClip> backGroundMusic;

    [BoxGroup("SFX")]
    [SerializeField] private List<AudioClip> walkingSound;
    [SerializeField] private List<AudioClip> sounds;
    private int currentBackgroundIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        InitAudio();
    }

    private void InitAudio()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        walk1Source = gameObject.AddComponent<AudioSource>();
        walk2Source = gameObject.AddComponent<AudioSource>();
        if (backGroundMusic.Count > 0)
        {
            musicSource.volume = 0.1f;
            PlayNextBackgroundMusic();
        }
        SetWalkSound();
    }

    private void SetWalkSound()
    {
        walk1Source.clip = walkingSound[0];
        walk1Source.loop = true;
        walk1Source.PlayDelayed(6f);
        walk1Source.enabled = false;
        walk2Source.clip = walkingSound[1];
        walk2Source.loop = true;
        walk2Source.PlayDelayed(6f);
        walk2Source.enabled = false;
    }

    private void PlayNextBackgroundMusic()
    {
        musicSource.clip = backGroundMusic[currentBackgroundIndex];
        musicSource.loop = false;
        musicSource.Play();

        currentBackgroundIndex = (currentBackgroundIndex + 1) % backGroundMusic.Count;

        Invoke("PlayNextBackgroundMusic", musicSource.clip.length);
    }

    public void WalkingSound(bool walking, bool index)
    {
        if (index)
        {
            walk1Source.enabled = walking;
        }
        else
        {
            walk2Source.enabled = walking;
        }
    }

    public void PlaySound(int index)
    {
        sfxSource.clip = sounds[index];
        sfxSource.Play();
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }

    public void SetVolume(float volume)
    {
        StopSound();
        sfxSource.volume = volume;
    }

    public void SetVolumeBackground(float volume)
    {
        sfxSource.volume = volume;
    }
}
