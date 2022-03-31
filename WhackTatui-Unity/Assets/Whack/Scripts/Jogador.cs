using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPontos;
    private static TextMeshProUGUI txtPontos;

    [SerializeField] private int maxTempo;
    private float tempo;
    [SerializeField] private GameObject gameObjectTempo;
    private TextMeshProUGUI txtTempo;

    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject pausePrinc;
    [SerializeField] private GameObject pauseOp;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI txtPontosGameOver;

    [SerializeField] private TMP_InputField txtNomeRanking;

    [SerializeField] private Slider volumeSlider;

    public static int Pontos { get; set; }

    private bool jogoAcabou = false;
    private bool carregarRanking = false;

    private void Awake()
    {
        Pontos = 0;
        
        txtPontos = gameObjectPontos.GetComponent<TextMeshProUGUI>();        
        txtPontos.text = Pontos.ToString();

        tempo = maxTempo + 1;
        txtTempo = gameObjectTempo.GetComponent<TextMeshProUGUI>();
        txtTempo.text = tempo.ToString();

        volumeSlider.onValueChanged.AddListener(delegate { MudancaDeVOlume(); });

        Fase.Rodando = true;
    }

    private void Update()
    {
        if (Fase.Rodando)
        {
            tempo -= Time.deltaTime;

            if ((int)tempo == 0)
            {
                Finalizar();
            }

            if (tempo <= 5 && !carregarRanking)
            {
                Ranking.Carregar();
                carregarRanking = true;
            } 
            
            txtTempo.text = ((int)tempo).ToString();
        }
    }

    public static void AtualizarPontos()
    {
        txtPontos.text = Pontos.ToString();
    }

    private void Finalizar()
    {
        jogoAcabou = true;
        Fase.Rodando = false;
        gameOver.SetActive(true);
        txtPontosGameOver.text = "Você fez " + Pontos + " pontos!";
    }

    #region Interface

    private void Pausar()
    {
        if (Fase.Rodando)
        {
            Fase.Rodando = false;
            menuPause.SetActive(true);
        } else
        {
            Fase.Rodando = true;
            menuPause.SetActive(false);
            pausePrinc.SetActive(true);
            pauseOp.SetActive(false);
        }
    }

    private void VoltarProJogo()
    {
        menuPause.SetActive(false);
        Fase.Rodando = true;
    }

    private void RecomecarPartida()
    {
        SalvarRanking();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Sair()
    {
        SalvarRanking();
        Fase.Rodando = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void SalvarRanking()
    {
        if (jogoAcabou)
        {
            string nome = string.IsNullOrEmpty(txtNomeRanking.text) ? "AAA" : txtNomeRanking.text;

            Ranking.Adicionar(Pontos, nome);
        }
    }

    private void MudancaDeVOlume()
    {
        PreferenciasUsuario.Prefs.volume = volumeSlider.value;
    }

    #endregion
}
