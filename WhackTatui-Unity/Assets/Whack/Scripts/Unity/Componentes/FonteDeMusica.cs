using System.Collections;
using UnityEngine;

public class FonteDeMusica : FonteDeAudio
{
    [SerializeField] private bool fadeIn;
    [SerializeField] private float fadeInTempo;

    private void Start()
    {
        if (fadeIn)
        {
            audioSource.volume = 0;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        while (audioSource.volume < 1)
        {
            if (audioSource.isPlaying) audioSource.volume += (float)(Time.unscaledDeltaTime / fadeInTempo);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
