using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whack.Scripts.Unity;

public class CarregandoCenaInterface : MonoBehaviour
{
    [SerializeField] Slider progressoSlider;
    [SerializeField] TextMeshProUGUI progressoTxt;
    
    public void Carregar(int id)
    {
        StartCoroutine(CarregarCena.CarregarAsync(id, progressoSlider, progressoTxt));
    }
}
