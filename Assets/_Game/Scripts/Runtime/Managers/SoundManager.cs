using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Music,
        Walk,
        Payment,
        Item,
        Money,
        DropItem,
        DropMoney,
        BuyArea,
        TakeTrash,
    }

    static float musicMultiplier = 1;

    static float sfxMultiplier = 1;

    public static void PlaySound(Sound sound)
    {
        AudioSourcePoolObj audioSourceObj = SoundAsset.instance.GetAudioSource();
        {
            if (audioSourceObj != null) 
            {
                audioSourceObj.Source.volume = GetSfxMul();

                SoundAudioClip sac = GetAudioClipData(sound);

                audioSourceObj.Source.outputAudioMixerGroup = SoundAsset.instance.sfxMixer;

                if (sac.Clips.Count > 0)
                    sac.clip = sac.Clips[Random.Range(0, sac.Clips.Count)];

                audioSourceObj.DestroyDelay = sac.destroyDelay;

                audioSourceObj.Source.PlayOneShot(sac.clip, sac.volume * GetSfxMul());

                audioSourceObj.ReturnPool();
            }
        }
    }

    public static SoundAudioClip GetAudioClipData(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in SoundAsset.instance.soundClipArray)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip;
        }

        return null;
    }

    public static void MusicVolChange()
    {
        SoundAsset.instance.musicHandlers.volume = musicMultiplier;
    }

    public static float GetMusicMul() 
    {
        if (PlayerPrefs.HasKey("MusicMul"))
            musicMultiplier = PlayerPrefs.GetFloat("MusicMul");
        else
            musicMultiplier = 1.0f;

        MusicVolChange();

        return musicMultiplier;
    }

    public static void SetMusicMul(float f) 
    {
        musicMultiplier = f;

        MusicVolChange();

        PlayerPrefs.SetFloat("MusicMul", f);
    }

    public static float GetSfxMul()
    {
        if (PlayerPrefs.HasKey("SfxMul"))
            sfxMultiplier = PlayerPrefs.GetFloat("SfxMul");
        else
            sfxMultiplier = 1.0f;

        return sfxMultiplier;
    }

    public static void SetSfxMul(float f)
    {
        sfxMultiplier = f;

        PlayerPrefs.SetFloat("SfxMul", f);
    }
}
