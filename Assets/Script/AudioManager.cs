using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<AudioManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject(nameof(AudioManager));
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        CreateAudioSources();
        BuildClipDictionary();
    }

    private void CreateAudioSources()
    {
        GameObject bgmObj = new GameObject("BGM");
        bgmObj.transform.parent = transform;
        bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmSource.loop = true;

        GameObject sfxObj = new GameObject("SFX");
        sfxObj.transform.parent = transform;
        sfxSource = sfxObj.AddComponent<AudioSource>();
    }

    private void BuildClipDictionary()
    {
        clipDictionary.Clear();
        foreach (var clip in audioClips)
        {
            if (clip != null && !clipDictionary.ContainsKey(clip.name))
            {
                clipDictionary.Add(clip.name, clip);
            }
        }
    }

    public void PlayMusic(string clipName, float volume = 0.3f)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    public void StopMusic()
    {
        bgmSource.Stop();
    }

    public void PlaySound(string clipName, float volume = 1f)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
