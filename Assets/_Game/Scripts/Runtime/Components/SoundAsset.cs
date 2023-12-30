using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundAsset : MonoBehaviour
{
    public AudioMixerGroup sfxMixer;

    public List<AudioSourcePoolObj> SourcesPool;    

    static SoundAsset _instance;
    public SoundAudioClip[] soundClipArray;

    public AudioSource musicHandlers;

    public static SoundAsset instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<SoundAsset>();

            return _instance;
        }
    }


    private void Awake()
    {
        if (SoundAsset.instance && SoundAsset.instance != this)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (SoundAsset.instance)
            SelectMusicClips();

        SoundManager.GetMusicMul();

        SoundManager.GetSfxMul();
    }

    private void SelectMusicClips()
    {
        musicHandlers.clip = SoundManager.GetAudioClipData(SoundManager.Sound.Music).clip;

        musicHandlers.Play();

    }

    public AudioSourcePoolObj GetAudioSource() 
    {
        foreach (AudioSourcePoolObj audioSource in SourcesPool)
        {
            if (!audioSource.gameObject.activeInHierarchy)
            {
                audioSource.gameObject.SetActive(true);

                return audioSource;
            }
        }
        return null;
    }

    public void ReturnPool(AudioSource source) 
    {
        source.gameObject.SetActive(false);
    }
}


[System.Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound sound;
    public AudioClip clip;
    public List<AudioClip> Clips = new List<AudioClip>();
    public float volume = .1f;
    public float destroyDelay = 2f;
}