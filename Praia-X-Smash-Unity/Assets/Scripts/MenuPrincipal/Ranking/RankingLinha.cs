using TMPro;
using UnityEngine;

public class RankingLinha : MonoBehaviour
{
    [SerializeField] private GameObject id;
    [SerializeField] private GameObject nome;
    [SerializeField] private GameObject pontuacao;

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
            return nome.GetComponent<TextMeshProUGUI>().text.ToUpper();
        }
        set
        {
            nome.GetComponent<TextMeshProUGUI>().text = value.ToUpper();
        }
    }

    public int Pontuacao
    {
        get
        {
            return int.Parse(pontuacao.GetComponent<TextMeshProUGUI>().text);
        }
        set
        {
            pontuacao.GetComponent<TextMeshProUGUI>().text = value.ToString();
        }
    }
}
