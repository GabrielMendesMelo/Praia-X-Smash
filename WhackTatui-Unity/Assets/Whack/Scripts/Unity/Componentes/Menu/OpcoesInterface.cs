using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Whack.Scripts.Unity;

public class OpcoesInterface : MenuInterface
{
    [Header("Preferências")]
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private TMP_Dropdown ddGraficos;

    void Start()
    {
        sliderMusica.value = PreferenciasUsuario.VolumeMusica;
        sliderMusica.onValueChanged.AddListener(delegate { MudarVolumeMusica(); });

        sliderSfx.value = PreferenciasUsuario.VolumeSfx;
        sliderSfx.onValueChanged.AddListener(delegate { MudarVolumeSfx(); });

        ddGraficos.value = PreferenciasUsuario.QualidadeGrafica;
        ddGraficos.onValueChanged.AddListener(delegate { MudarGrafico(); });
    }

    private void MudarVolumeMusica()
    {
        PreferenciasUsuario.VolumeMusica = sliderMusica.value;
    }

    private void MudarVolumeSfx()
    {
        PreferenciasUsuario.VolumeSfx = sliderSfx.value;
    }
    private void MudarGrafico()
    {
        PreferenciasUsuario.QualidadeGrafica = ddGraficos.value;
    }
}
