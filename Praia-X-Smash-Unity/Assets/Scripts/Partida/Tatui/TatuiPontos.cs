using UnityEngine;

public class TatuiPontos : MonoBehaviour
{
    [SerializeField] private float velocidade;
    [SerializeField] private Color[] cores;

    private void Start()
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        Jogador jogador = FindObjectOfType<Jogador>();
        textMesh.text = Mathf.FloorToInt(jogador.PontosPorAcerto).ToString();
        textMesh.color = cores[CorId(jogador.MaxPontosPorAcerto, jogador.PontosPorAcerto, cores.Length)];
    }
    private void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
        transform.localScale -= Vector3.one * velocidade * Time.deltaTime;
        
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

    private int CorId(int maxPontosPorAcerto, float pontosPorAcertoAtual, float nCores)
    {
        return Mathf.CeilToInt(pontosPorAcertoAtual / (maxPontosPorAcerto / nCores)) - 1;
    }
}
