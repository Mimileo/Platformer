using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip brickBreakClip;
    public AudioClip coinClip;
    public AudioMixerGroup brickBreakGroup;
    public AudioMixerGroup coinGroup;

    private AudioSource brickBreakSource;
    private AudioSource coinSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep  AudioManager across scenes
            SetupAudioSources();
        }
        else
        {
            Destroy(gameObject); // keep only 1  AudioManager
        }
    }

    private void SetupAudioSources()
    {
        brickBreakSource = gameObject.AddComponent<AudioSource>();
        brickBreakSource.clip = brickBreakClip;
        brickBreakSource.outputAudioMixerGroup = brickBreakGroup;

        coinSource = gameObject.AddComponent<AudioSource>();
        coinSource.clip = coinClip;
        coinSource.outputAudioMixerGroup = coinGroup;
    }

    public void PlayBrickBreak()
    {
        brickBreakSource.Play();
    }

    public void PlayCoin()
    {
        coinSource.Play();
    }
}
