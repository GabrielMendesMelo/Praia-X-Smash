using UnityEngine;

public class TatuiInfo : MonoBehaviour
{
    [SerializeField] private float velocidadeMin;
    [SerializeField] private float velocidadeMax;

    [SerializeField] private float variacaoVelocidadeSubindo;
    [SerializeField] private float variacaoVelocidadeDescendo;

    [SerializeField] private float posMin;
    [SerializeField] private float posMax;

    [SerializeField] private float tempoParadoMin;
    [SerializeField] private float tempoParadoMax;

    [SerializeField] private float tempoAEsperarAoSerAcertado;

    [SerializeField] private GameObject acertouEfeito;
    [SerializeField] private AudioClip[] acertouClips;
    [SerializeField] private AudioClip[] acertou2Clips;

    [SerializeField] private GameObject pontosEfeito;
    [SerializeField] private int mostrarPontosAPartirDe = 0;

    public float Velocidade { get; private set; }

    public float VariacaoVelocidadeSubindo { get { return variacaoVelocidadeSubindo; } }

    public float VariacaoVelocidadeDescendo { get { return variacaoVelocidadeDescendo; } }

    public float PosMin { get { return posMin; } }

    public float PosMax { get { return posMax; } }

    public float TempoAFicarParado { get; private set; }

    public float TempoAEsperarAoSerAcertado { get { return tempoAEsperarAoSerAcertado; } }

    public GameObject AcertouEfeito { get { return acertouEfeito; } }

    public AudioClip AcertouClip { get { return acertouClips[Random.Range(0, acertouClips.Length)]; } }

    public AudioClip Acertou2Clip { get { return acertou2Clips[Random.Range(0, acertou2Clips.Length)]; } }

    public GameObject PontosEfeito { get { return pontosEfeito; } }

    public int MostrarPontosAPartirDe { get { return mostrarPontosAPartirDe; } }

    public float TempoParado { get; set; }
    
    public bool FoiAcertado { get; set; }

    private void Awake()
    {
        Velocidade = Random.Range(velocidadeMin, velocidadeMax); ;

        TempoAFicarParado = Random.Range(tempoParadoMin, tempoParadoMax);

        TempoParado = 0;

        FoiAcertado = false;
    }
}
