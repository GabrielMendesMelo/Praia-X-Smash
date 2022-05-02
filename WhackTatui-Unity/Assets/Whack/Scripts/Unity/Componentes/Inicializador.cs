using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.Water;
using Whack.Scripts.Unity;

public class Inicializador : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicaMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private WaterBase mar;

    private void Start()
    {
        Preferencias();
        Ranking();
    }

    private void Preferencias()
    {
        PreferenciasUsuario.Inicializar(musicaMixer, sfxMixer, mar);
        Debug.Log(PreferenciasUsuario.VolumeMusica);
    }

    private void Ranking()
    {

    }
}
