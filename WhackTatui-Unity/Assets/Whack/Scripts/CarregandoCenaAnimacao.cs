using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarregandoCenaAnimacao : MonoBehaviour
{
    [SerializeField] Slider progressoSlider;
    [SerializeField] TextMeshProUGUI progressoTxt;
    
    public void Carregar(int id)
    {
        StartCoroutine(CarregarAsync(id, progressoSlider, progressoTxt));
    }

    private IEnumerator CarregarAsync(int id, Slider progressoSlider, TextMeshProUGUI progressoTxt)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);

        while (!asyncLoad.isDone)
        {
            float progresso = Mathf.Clamp01(asyncLoad.progress / .9f);
            progressoSlider.value = progresso;
            progressoTxt.text = (int)(progresso * 100) + "%";
            yield return null;
        }
    }
}
