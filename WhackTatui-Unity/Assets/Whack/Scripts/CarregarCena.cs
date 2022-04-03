using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public static class CarregarCena
{/*
    private static Slider slider;
    private static TextMeshProUGUI txt;

    public static void Set(Slider _slider, TextMeshProUGUI _txt)
    {
        slider = _slider;
        txt = _txt;
    }
    */
    public static IEnumerator LoadAsync(int id, Slider slider, TextMeshProUGUI txt)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);

        while (!asyncLoad.isDone)
        {
            float progresso = Mathf.Clamp01(asyncLoad.progress / .9f);
            slider.value = progresso;
            txt.text = (int)(progresso * 100) + "%";
            yield return null;
        }
    }
}
