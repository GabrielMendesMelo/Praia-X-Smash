using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

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

    public static float musica
    {
        get
        {
            return prefs.musica;
        }
        set
        {
            prefs.musica = value;
            musicaMixer.SetFloat("Musica", prefs.musica);

            Salvar();
        }
    }

    public static float sfx
    {
        get
        {
            return prefs.sfx;
        }
        set
        {
            prefs.sfx = value;
            sfxMixer.SetFloat("SFX", prefs.sfx);

            Salvar();
        }
    }

    public static int grafico
    {
        get
        {
            return prefs.grafico;
        }
        set
        {
            prefs.grafico = value;
            QualitySettings.SetQualityLevel(prefs.grafico);

            Salvar();
        }
    }

    public static void Set(AudioMixerGroup musicaMixerGroup, AudioMixerGroup sfxMixerGroup)
    {
        musicaMixer = musicaMixerGroup.audioMixer;
        sfxMixer = sfxMixerGroup.audioMixer;

        if (!File.Exists(caminho))
        {
            prefs = new Prefs(20, 20, QualitySettings.GetQualityLevel());

            txt = JsonUtility.ToJson(prefs);
        }
        else
        {
            txt = File.ReadAllText(caminho);

            prefs = JsonUtility.FromJson<Prefs>(txt);
        }

        musicaMixer.SetFloat("Musica", prefs.musica);
        sfxMixer.SetFloat("SFX", prefs.sfx);
        QualitySettings.SetQualityLevel(prefs.grafico);

        File.WriteAllText(caminho, txt);
    }

    private static void Salvar()
    {
        txt = JsonUtility.ToJson(prefs);
        File.WriteAllText(caminho, txt);
    }
}
