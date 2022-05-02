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

namespace Antigo
{
    public class MenuPrincipal : MonoBehaviour
    {
        [SerializeField] private GameObject conteudoRanking;
        [SerializeField] private GameObject conteudoPrefab;
        private List<GameObject> jogadores = new List<GameObject>();

        [SerializeField] private GameObject txtSemInternet;
        [SerializeField] private GameObject txtRecarregar;

        [SerializeField] private float distanciaY;

        [SerializeField] private AudioMixerGroup musicaMixer;

        [SerializeField] private GameObject gameObjAudioSource;
        [SerializeField] private AudioClip musicaClip;
        private AudioSource musicaSrc;

        [SerializeField] private bool fadeIn;
        [SerializeField] private float tempoFadeIn;

        [SerializeField] private AudioMixerGroup sfxMixer;

        private AudioSource btnsSrc;
        [SerializeField] private AudioClip btnComecarClip;
        [SerializeField] private AudioClip btnAvançarClip;
        [SerializeField] private AudioClip btnVoltarClip;

        [SerializeField] private Slider musicaSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private TMP_Dropdown graficosDd;

        [SerializeField] private Slider loadSlider;
        [SerializeField] private TextMeshProUGUI loadTxt;
        [SerializeField] private GameObject mar;

        private void Awake()
        {
            CarregarRanking();

            musicaSrc = gameObjAudioSource.GetComponent<AudioSource>();
            musicaSrc.clip = musicaClip;
            musicaSrc.outputAudioMixerGroup = musicaMixer;
            musicaSrc.volume = fadeIn ? 0 : 1;

            musicaSrc.Play();

            btnsSrc = gameObjAudioSource.AddComponent<AudioSource>();
            btnsSrc.outputAudioMixerGroup = sfxMixer;

            Combo.Set(10);
        }

        private void Start()
        {
            PreferenciasUsuario.Set(musicaMixer, sfxMixer, mar);

            musicaSlider.value = PreferenciasUsuario.musica;
            musicaSlider.onValueChanged.AddListener(delegate { MudancaDeMusica(); });

            sfxSlider.value = PreferenciasUsuario.sfx;
            sfxSlider.onValueChanged.AddListener(delegate { MudancaDeSfx(); });

            graficosDd.value = PreferenciasUsuario.grafico;
            graficosDd.onValueChanged.AddListener(delegate { MudancaDeGrafico(); });
        }

        private void Update()
        {
            if (fadeIn && musicaSrc.volume <= 1)
            {
                musicaSrc.volume += (float)(Time.deltaTime / tempoFadeIn);
            }
        }

        public void ComecarJogo()
        {
            btnsSrc.clip = btnComecarClip;
            btnsSrc.Play();
            StartCoroutine(CarregarCena.LoadAsync(SceneManager.GetActiveScene().buildIndex + 1, loadSlider, loadTxt));
        }

        public void AbrirMenu(GameObject menu)
        {
            btnsSrc.clip = btnAvançarClip;
            btnsSrc.Play();
            menu.SetActive(true);
        }

        public void FecharMenu(GameObject menu)
        {
            btnsSrc.clip = btnVoltarClip;
            btnsSrc.Play();
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

                    Ranking.Carregar(null);

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

        public void BtnAtualizar()
        {
            AtualizarRanking();
            btnsSrc.clip = btnAvançarClip;
            btnsSrc.Play();
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
    }
}