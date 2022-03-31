using UnityEngine;
using TMPro;

public class InstanciaRank : MonoBehaviour
{
    [SerializeField] private GameObject id;
    [SerializeField] private GameObject nome;
    [SerializeField] private GameObject pontos;

    public int Id
    {
        get
        {
            return int.Parse(id.GetComponent<TextMeshProUGUI>().text);
        }
        set
        {

            id.GetComponent<TextMeshProUGUI>().text = value < 10 ? "0" + value : value.ToString();
        }
    }

    public string Nome
    {
        get
        {
            return nome.GetComponent<TextMeshProUGUI>().text;
        }
        set
        {
            nome.GetComponent<TextMeshProUGUI>().text = value;
        }
    }

    public int Pontos
    {
        get
        {
            return int.Parse(pontos.GetComponent<TextMeshProUGUI>().text);
        }
        set
        {
            pontos.GetComponent<TextMeshProUGUI>().text = value.ToString();
        }
    }
}
