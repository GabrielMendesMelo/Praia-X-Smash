using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Whack.Scripts.Unity
{
    public static class CarregarCena
    {
        public static IEnumerator CarregarAsync(int id, Slider progressoSlider=null, TextMeshProUGUI progressoTxt=null)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);

            while (!asyncLoad.isDone)
            {
                float progresso = Mathf.Clamp01(asyncLoad.progress / .9f);
                if (progressoSlider != null) progressoSlider.value = progresso;
                if (progressoTxt != null) progressoTxt.text = (int)(progresso * 100) + "%";
                yield return null;
            }
        }
    }
}
