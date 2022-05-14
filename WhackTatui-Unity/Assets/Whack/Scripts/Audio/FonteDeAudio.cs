using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FonteDeAudio : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool loop;
    [SerializeField] private bool tocarAoIniciar;
    [SerializeField] private float delayInicio;

    private void Awake()
    {
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = loop;
        audioSource.outputAudioMixerGroup = mixer;

        if (tocarAoIniciar) Tocar(delayInicio);
    }

    public void Tocar(float delay=0f)
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        TocarAux(delay);
    }

    public void Tocar(AudioClip clip, float delay=0f)
    {
        audioSource.clip = clip;
        TocarAux(delay);
    }

    private void TocarAux(float delay)
    {
        if (delay == 0f) audioSource.Play();
        else StartCoroutine(TocarComDelay(delay));
    }

    private IEnumerator TocarComDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        audioSource.Play();
    }

    public void Parar()
    {
        audioSource.Stop();
    }
}
