using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    // Add your audio-related fields and methods here
    [BoxGroup("Audio")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        // Initialize your AudioSource or other audio components
        AudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void StopSound()
    {
        AudioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        AudioSource.volume = volume;
    }
}
