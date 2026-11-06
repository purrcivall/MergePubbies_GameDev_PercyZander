using UnityEngine;
using UnityEngine.UI;

//<Summary> this script is used for individual volumes for different sounds. - Zander :3 <Summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource pubbieBarkAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip successAudioClip;
    public AudioClip gameOverAudioClip;
    public AudioClip introludeAudioClip;
    public AudioClip gameplayAudioClip;

    public AudioClip[] percyBarks;
    public AudioClip[] zanderBarks;

    public Slider musicSlider;
    public Slider pubbieBarkSlider;
    public Slider sfxSlider;

    static private float musicVolume = 1;
    static private float pubbieBarkVolume = 1;
    static private float sfxVolume = 1;

    private void Start()
    {
        musicAudioSource.volume = musicVolume;
        pubbieBarkAudioSource.volume = pubbieBarkVolume;
        sfxAudioSource.volume = sfxVolume;

        musicSlider.value = musicVolume;
        pubbieBarkSlider.value = pubbieBarkVolume;
        sfxSlider.value = sfxVolume;
    }

    public void ChangeMusicVolume(float value)
    {
        musicAudioSource.volume = value;
        musicVolume = value;
    }

    public void ChangePubbieBarkVolume(float value)
    {
        pubbieBarkAudioSource.volume = value;
        pubbieBarkVolume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        sfxAudioSource.volume = value;
        sfxVolume = value;
    }

    public void PlayIntrolude()
    {
        musicAudioSource.clip = introludeAudioClip;
        musicAudioSource.Play();
    }

    public void PlaySuccess()
    {
        sfxAudioSource.clip = successAudioClip;
        sfxAudioSource.Play();
    }

    public void PlayBark(AudioClip bark)
    {
        pubbieBarkAudioSource.clip = bark;
        pubbieBarkAudioSource.Play();
    }

    public void PlayGameplay()
    {
        musicAudioSource.clip = gameplayAudioClip;
        musicAudioSource.Play();
    }

    public void PlayGameOver()
    {
        musicAudioSource.clip = gameOverAudioClip;
        musicAudioSource.Play();
    }

    public void PlayCreditsBark(int whichPubby)
    {
        if(whichPubby == 0)
        {
            pubbieBarkAudioSource.clip = percyBarks[Random.Range(0, percyBarks.Length)];
            pubbieBarkAudioSource.Play();
        }
        else
        {
            pubbieBarkAudioSource.clip = zanderBarks[Random.Range(0, zanderBarks.Length)];
            pubbieBarkAudioSource.Play();
        }
    }
}