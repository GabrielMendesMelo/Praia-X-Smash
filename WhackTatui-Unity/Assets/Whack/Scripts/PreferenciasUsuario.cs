using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public static class PreferenciasUsuario
{
    [Serializable]
    public class Prefs
    {
        public float volume;
        public int grafico;

        public Prefs(float volume, int grafico)
        {
            this.volume = volume;
            this.grafico = grafico;
        }
    }

    private static string caminho = Application.persistentDataPath + "/prefs.json";
    private static string txt;
    private static Prefs prefs;

    private static AudioMixer mixer;

    public static float volume
    {
        get
        {
            return prefs.volume;
        }
        set
        {
            prefs.volume = value;
            mixer.SetFloat("Volume", prefs.volume);

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

    public static void Set(AudioMixerGroup audioMixerGroup)
    {
        mixer = audioMixerGroup.audioMixer;

        if (!File.Exists(caminho))
        {
            prefs = new Prefs(20, QualitySettings.GetQualityLevel());

            txt = JsonUtility.ToJson(prefs);
        }
        else
        {
            txt = File.ReadAllText(caminho);

            prefs = JsonUtility.FromJson<Prefs>(txt);
        }

        mixer.SetFloat("Volume", prefs.volume);        
        QualitySettings.SetQualityLevel(prefs.grafico);

        File.WriteAllText(caminho, txt);
    }

    private static void Salvar()
    {
        txt = JsonUtility.ToJson(prefs);
        File.WriteAllText(caminho, txt);
    }
}
