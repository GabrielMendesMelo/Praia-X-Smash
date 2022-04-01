using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject conteudoRanking;
    [SerializeField] private GameObject conteudoPrefab;
    private List<GameObject> jogadores = new List<GameObject>();

    [SerializeField] private GameObject txtSemInternet;
    [SerializeField] private GameObject txtRecarregar;

    [SerializeField] private float distanciaY;

    [SerializeField] private AudioMixerGroup audioMixer;

    [SerializeField] private GameObject gameObjAudioSource;
    [SerializeField] private AudioClip musicaClip;
    private AudioSource musicaSrc;
    
    [SerializeField] private float musicaMaxVolume;
    
    [SerializeField] private bool fadeIn;
    [SerializeField] private float tempoFadeIn;

    private AudioSource btnsSrc;
    [SerializeField] private AudioClip btnComecarClip;
    [SerializeField] private AudioClip btnAvançarClip;
    [SerializeField] private AudioClip btnVoltarClip;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown graficosDd;

    private void Awake()
    {
        CarregarRanking();

        musicaSrc = gameObjAudioSource.GetComponent<AudioSource>();
        musicaSrc.clip = musicaClip;
        musicaSrc.outputAudioMixerGroup = audioMixer;
        musicaSrc.volume = fadeIn ? 0 : musicaMaxVolume;

        musicaSrc.Play();

        btnsSrc = gameObjAudioSource.AddComponent<AudioSource>();
        btnsSrc.outputAudioMixerGroup = audioMixer;
        btnsSrc.loop = false;
    }

    private void Start()
    {
        PreferenciasUsuario.Set(audioMixer);

        volumeSlider.value = PreferenciasUsuario.volume;
        volumeSlider.onValueChanged.AddListener(delegate { MudancaDeVOlume(); });

        graficosDd.value = PreferenciasUsuario.grafico;
        graficosDd.onValueChanged.AddListener(delegate { MudancaDeGrafico(); });
    }

    private void Update()
    {
        if (fadeIn && musicaSrc.volume <= musicaMaxVolume)
        {
            musicaSrc.volume += (float)(Time.deltaTime / tempoFadeIn);
        }
    }
    
    public void ComecarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AbrirMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void FecharMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void GitHub()
    {
        Application.OpenURL("https://github.com/GabrielMendesMelo/Whack-Tatui-Unity");
    }

    public void Sair()
    {
        Application.Quit();
    }

    #region APAGAR
    public void APAGAR()
    {
        int pt = UnityEngine.Random.Range(320, 450);
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        StringBuilder nome = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            nome.Append(letras[UnityEngine.Random.Range(0, letras.Length)]);
        }
        Ranking.Adicionar(pt, nome.ToString());

        AtualizarRanking();
    }
    #endregion

    private IEnumerator ChecarConexao(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    private void CarregarRanking()
    {
        StartCoroutine(ChecarConexao(conectado =>
        {
            if (conectado)
            {
                txtSemInternet.SetActive(false);
                conteudoRanking.SetActive(true);
             
                Ranking.Carregar();

                if (Ranking.jogadores == null) txtRecarregar.SetActive(true);
                else txtRecarregar.SetActive(false);
            }
            else
            {
                txtSemInternet.SetActive(true);
                conteudoRanking.SetActive(false);

                throw new Exception("Conexão falhou!");
            }
        }));
    }

    public void AtualizarRanking()
    {
        conteudoRanking.SetActive(true);
        txtSemInternet.SetActive(false);

        if (Ranking.jogadores == null) txtRecarregar.SetActive(true);
        else txtRecarregar.SetActive(false);
        
        foreach (GameObject g in jogadores)
        {
            Destroy(g);
        }
        jogadores.Clear();
        CarregarRanking();

        if (Ranking.jogadores != null)
        {
            for (int i = 0; i < Ranking.jogadores.Count; i++)
            {
                Ranking.Jogador j = Ranking.jogadores[i];

                CriarTexto(i, j.nome, j.pontos);
            }
        }
    }

    private void CriarTexto(int id, string nome, int pontos)
    {
        GameObject gO = Instantiate(conteudoPrefab, conteudoRanking.transform);
        jogadores.Add(gO);
        gO.name = id + "_" + nome + "_" + pontos;
        gO.transform.localPosition = Vector3.up * (390 - (distanciaY + (id * distanciaY)));
        InstanciaRank iR = gO.GetComponent<InstanciaRank>();

        iR.Id = id + 1;
        iR.Nome = nome;
        iR.Pontos = pontos;
    }

    private void MudancaDeVOlume()
    {
        PreferenciasUsuario.volume = volumeSlider.value;
    }

    private void MudancaDeGrafico()
    {
        PreferenciasUsuario.grafico = graficosDd.value;
    }
}
