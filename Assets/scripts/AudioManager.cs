using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        // Patrón Singleton limpio
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    #region 🎵 Música
    public void PlayMusic()
    {
        if (musicSource.isPlaying) return;

        musicSource.clip = backgroundMusic;
        musicSource.volume = 0.5f;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void SetMusicVolume(float volume) =>
        musicSource.volume = Mathf.Clamp01(volume);
    #endregion

    #region 🔊 Sonidos (SFX)
    public void PlaySFX(AudioClip clip, float volume = 0.4f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }

    public void PlayRandomSFX(IList<AudioClip> clips, float volume = 0.4f)
    {
        if (clips == null || clips.Count == 0) return;

        // Selecciona un sonido aleatorio y lo reproduce
        AudioClip randomClip = clips[Random.Range(0, clips.Count)];
        PlaySFX(randomClip, volume);
    }

    public void SetSFXVolume(float volume) =>
        sfxSource.volume = Mathf.Clamp01(volume);
    #endregion
}
