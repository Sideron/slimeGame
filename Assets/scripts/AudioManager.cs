using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Detecta cuando se cambia de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayMainMenuMusic(); // Primera vez
    }

    // 🔄 Cambia la música automáticamente al cargar una escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            PlayMainMenuMusic();
        else
            PlayGameplayMusic();
    }

    #region MUSIC

    private void PlayMusic(AudioClip clip, float volume = 0.5f)
    {
        if (clip == null) return;

        // Evita reiniciar la misma música si ya está sonando
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMainMenuMusic() =>
        PlayMusic(mainMenuMusic, 0.5f);

    public void PlayGameplayMusic() =>
        PlayMusic(gameplayMusic, 0.6f);

    #endregion

    #region SFX (igual que antes)
    public void PlaySFX(AudioClip clip, float volume = 0.4f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }

    public void PlayRandomSFX(IList<AudioClip> clips, float volume = 0.4f)
    {
        if (clips == null || clips.Count == 0) return;

        AudioClip randomClip = clips[Random.Range(0, clips.Count)];
        PlaySFX(randomClip, volume);
    }

    public void SetSFXVolume(float volume) =>
        sfxSource.volume = Mathf.Clamp01(volume);
    #endregion
}
