using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
//using UnityStandardAssets.Water;


public static class PreferenciasUsuario
{
    [Serializable]
    public class Prefs
    {
        public float musica;
        public float sfx;
        public int grafico;

        public Prefs(float musica, float sfx, int grafico)
        {
            this.musica = musica;
            this.sfx = sfx;
            this.grafico = grafico;
        }
    }

    private static string caminho = Application.persistentDataPath + "/prefs.json";
    private static string txt;
    private static Prefs prefs;

    private static AudioMixer musicaMixer;
    private static AudioMixer sfxMixer;
    //private static WaterBase waterBase;

    public static float VolumeMusica
    {
        get
        {
            return prefs.musica;
        }
        set
        {
            prefs.musica = value;
            musicaMixer.SetFloat("Musica", Mathf.Log10(prefs.musica) * 20);

            Salvar();
        }
    }

    public static float VolumeSfx
    {
        get
        {
            return prefs.sfx;
        }
        set
        {
            prefs.sfx = value;
            sfxMixer.SetFloat("SFX", Mathf.Log10(prefs.sfx) * 20);

            Salvar();
        }
    }

    public static int QualidadeGrafica
    {
        get
        {
            return prefs.grafico;
        }
        set
        {
            prefs.grafico = value;
            QualitySettings.SetQualityLevel(prefs.grafico);
            //waterBase.waterQuality = (WaterQuality)prefs.grafico;

            Salvar();
        }
    }

    public static void Inicializar(AudioMixerGroup musicaMixerGroup, AudioMixerGroup sfxMixerGroup)//, WaterBase water)
    {
        musicaMixer = musicaMixerGroup.audioMixer;
        sfxMixer = sfxMixerGroup.audioMixer;
        //waterBase = water;

        if (!File.Exists(caminho))
        {
            Criar();
        }
        else
        {
            Carregar();
        }

        musicaMixer.SetFloat("Musica", Mathf.Log10(prefs.musica) * 20);
        sfxMixer.SetFloat("SFX", Mathf.Log10(prefs.sfx) * 20);
        QualitySettings.SetQualityLevel(prefs.grafico);

        File.WriteAllText(caminho, txt);
    }

    private static void Criar()
    {
        prefs = new Prefs(1, 1, QualitySettings.GetQualityLevel());
        txt = JsonUtility.ToJson(prefs);
    }

    private static void Carregar()
    {
        txt = File.ReadAllText(caminho);
        prefs = JsonUtility.FromJson<Prefs>(txt);
    }

    private static void Salvar()
    {
        txt = JsonUtility.ToJson(prefs);
        File.WriteAllText(caminho, txt);
    }
}
