using UnityEngine;

public class TatuiModel : MonoBehaviour
{
    [SerializeField] private float velocidadeMin = 4;
    [SerializeField] private float velocidadeMax = 4;

    [SerializeField] private float variacaoVelocidadeSubindo = 1.5f;
    [SerializeField] private float variacaoVelocidadeDescendo = 2f;

    [SerializeField] private float posMin = -1.5f;
    [SerializeField] private float posMax = 0.05f;

    [SerializeField] private float tempoParadoMin = 2f;
    [SerializeField] private float tempoParadoMax = 3f;

    [SerializeField] private float tempoAEsperarAoSerAcertado = 0.6f;

    [SerializeField] private GameObject acertouEfeito;
    [SerializeField] private AudioClip[] acertouClips;
    [SerializeField] private AudioClip[] acertou2Clips;


    [SerializeField] private GameObject pontosEfeito;
    [SerializeField] private int mostrarPontosAPartirDe = 5;
    [SerializeField, Min(1)] private float multPontos = 1f;

    private float Velocidade { get; set; }
    public float VelocidadeSubindo { get { return Velocidade * variacaoVelocidadeSubindo; } }
    public float VelocidadeDescendo { get { return Velocidade * variacaoVelocidadeDescendo; } }

    public float PosMin { get { return posMin; } }
    public float PosMax { get { return posMax; } }

    public float TempoAFicarParado { get; private set; }
    public float TempoAEsperarAoSerAcertado { get { return tempoAEsperarAoSerAcertado; } }

    public GameObject AcertouEfeito { get { return acertouEfeito; } }
    public AudioClip AcertouClip { get { return acertouClips[Random.Range(0, acertouClips.Length)]; } }
    public AudioClip Acertou2Clip { get { return acertou2Clips[Random.Range(0, acertou2Clips.Length)]; } }

    public float MultPontos { get { return multPontos; } }
    public GameObject PontosEfeito { get { return pontosEfeito; } }
    public int MostrarPontosAPartirDe { get { return mostrarPontosAPartirDe; } }

    public float TempoParado { get; set; }
    
    public bool FoiAcertado { get; set; }

    private void Awake()
    {
        Velocidade = Random.Range(velocidadeMin, velocidadeMax); 

        TempoAFicarParado = Random.Range(tempoParadoMin, tempoParadoMax);

        TempoParado = 0;

        FoiAcertado = false;
    }
}
