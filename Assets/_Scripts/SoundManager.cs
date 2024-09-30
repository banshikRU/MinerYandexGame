using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource pickaxeSfx;
    [SerializeField] private AudioSource efxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource reserveEfxSourcee;
    public static SoundManager instance = null;
    [SerializeField] private float lowPitchRange = .95f;
    [SerializeField] private float highPitchRange = 1.05f;
    void Awake()
    {
        instance = this;
    }
    private void Update()
    {

    }
    public void PlayPIckaxe (AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        pickaxeSfx.pitch = randomPitch;
        pickaxeSfx.clip = clip;
        pickaxeSfx.Play();
    }
    public void PlaySingle(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        if (efxSource.isPlaying)
        {
            reserveEfxSourcee.clip = clip;
            reserveEfxSourcee.pitch = randomPitch;
            reserveEfxSourcee.Play();
        }
        efxSource.pitch = randomPitch;
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void RandomizeSfx(List<AudioClip> clips)
    {
        int randomIndex = Random.Range(0, clips.Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
    public void OnOffAllSfx(bool onOff)
    {
        if (!onOff)
        {
            reserveEfxSourcee.volume = 0f;
            pickaxeSfx.volume = 0f;
            efxSource.volume = 0f;
        }
        else
        {
            reserveEfxSourcee.volume = 0.8f;
            pickaxeSfx.volume = 0.8f;
            efxSource.volume = 0.8f;
        }
    }
    public void OnOffAllSound(bool onOff)
    {
        if (!onOff)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = 0.8f;
        }
    }
    public void AdvOn()
    {
        OnOffAllSfx(false);
        OnOffAllSound(false);
    }
    public void AdvOff()
    {
        OnOffAllSound(true);
        OnOffAllSfx(true);
    }
}

