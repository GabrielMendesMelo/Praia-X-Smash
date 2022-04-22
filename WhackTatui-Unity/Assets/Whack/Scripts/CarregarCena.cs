using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public static class CarregarCena
{
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
