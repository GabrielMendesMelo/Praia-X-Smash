using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.Water;

public class Inicializador : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicaMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private WaterBase mar;

    private void Start()
    {
        PreferenciasUsuario.Inicializar(musicaMixer, sfxMixer, mar);

        StartCoroutine(Ranking.Inicializar());
    }
}
