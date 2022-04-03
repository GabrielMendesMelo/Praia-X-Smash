using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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

    [SerializeField] private Slider musicaSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Dropdown graficosDd;

    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private GameObject gameObjAudioSource;
    
    [SerializeField] private AudioClip sfxPause;
    [SerializeField] private AudioClip sfxAvancar;
    [SerializeField] private AudioClip sfxVoltar;
    [SerializeField] private AudioClip sfxTempoAcabando;
    private AudioSource sfxSrc;
    private AudioSource tempoAcabandoSrc;

    [SerializeField] private GameObject menuPrincipal, menuOpcoes, menuLoad;

    [SerializeField] private Slider loadSlider;
    [SerializeField] private TextMeshProUGUI loadTxt;

    [SerializeField] private Fase fase;

    public static int Pontos { get; set; }

    private bool tempoFinal = false;
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

        musicaSlider.value = PreferenciasUsuario.musica;
        musicaSlider.onValueChanged.AddListener(delegate { MudancaDeMusica(); });

        sfxSlider.value = PreferenciasUsuario.sfx;
        sfxSlider.onValueChanged.AddListener(delegate { MudancaDeSfx(); });

        graficosDd.value = PreferenciasUsuario.grafico;
        graficosDd.onValueChanged.AddListener(delegate { MudancaDeGrafico(); });

        Fase.Rodando = true;

        sfxSrc = gameObjAudioSource.AddComponent<AudioSource>();
        sfxSrc.outputAudioMixerGroup = sfxMixer;
        sfxSrc.volume = PreferenciasUsuario.sfx;
        sfxSrc.playOnAwake = false;

        tempoAcabandoSrc = gameObjAudioSource.AddComponent<AudioSource>();
        tempoAcabandoSrc.outputAudioMixerGroup = sfxMixer;
        tempoAcabandoSrc.volume = PreferenciasUsuario.sfx;
        tempoAcabandoSrc.playOnAwake = false;
        tempoAcabandoSrc.clip = sfxTempoAcabando;
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

            if (tempo <= 11 && !tempoFinal)
            {
                txtTempo.color = Color.red;
                tempoFinal = true;
                StartCoroutine(TempoFinal());
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
        fase.Finalizar();
        jogoAcabou = true;
        Fase.Rodando = false;
        gameOver.SetActive(true);
        txtPontosGameOver.text = "Você fez " + Pontos + " pontos!";
    }

    #region Interface

    public void Pausar()
    {
        sfxSrc.clip = sfxPause;
        sfxSrc.Play();
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

    public void VoltarProJogo()
    {
        sfxSrc.clip = sfxPause;
        sfxSrc.Play();
        menuPause.SetActive(false);
        Fase.Rodando = true;
    }

    public void AbrirOpcoes()
    {
        sfxSrc.clip = sfxAvancar;
        sfxSrc.Play();
        menuOpcoes.SetActive(true);
        menuPrincipal.SetActive(false);
    }

    private IEnumerator TempoFinal()
    {
        tempoAcabandoSrc.Play();
        yield return new WaitForSeconds(1);
    }

    public void VoltarMenu()
    {
        sfxSrc.clip = sfxVoltar;
        sfxSrc.Play();
        menuPrincipal.SetActive(true);
        menuOpcoes.SetActive(false);
    }

    public void RecomecarPartida()
    {
        CarregarNovaCena(SceneManager.GetActiveScene().buildIndex);
    }

    public void Sair()
    {
        CarregarNovaCena(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void CarregarNovaCena(int id)
    {
        sfxSrc.clip = sfxAvancar;
        sfxSrc.Play();

        menuLoad.SetActive(true);

        SalvarRanking();
        StartCoroutine(CarregarCena.LoadAsync(id, loadSlider, loadTxt));
    }

    private void SalvarRanking()
    {
        if (jogoAcabou)
        {
            string nome = string.IsNullOrEmpty(txtNomeRanking.text) ? "AAA" : txtNomeRanking.text;

            Ranking.Adicionar(Pontos, nome);
        }
    }
    private void MudancaDeMusica()
    {
        PreferenciasUsuario.musica = musicaSlider.value;
    }
    private void MudancaDeSfx()
    {
        PreferenciasUsuario.sfx = sfxSlider.value;
    }

    private void MudancaDeGrafico()
    {
        PreferenciasUsuario.grafico = graficosDd.value;
    }

    #endregion
}
