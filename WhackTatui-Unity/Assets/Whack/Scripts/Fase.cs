using UnityEngine;
using UnityEngine.Audio;

namespace Antigo
{
    public class Fase : MonoBehaviour
    {
        [SerializeField, Min(0)] private float tempoMinCriacao;
        [SerializeField, Min(0)] private float tempoMaxCriacao;
        [SerializeField, Min(1)] private int maxInimigos;

        [SerializeField] private AudioMixerGroup musicaMixer;

        [SerializeField] private GameObject gameObjAudioSource;
        [SerializeField] private AudioClip[] musicaClip;
        //[SerializeField] private AudioClip gameOverClip;
        [SerializeField] private AudioClip musicaGameOver;
        private float timerGameOver = 0f;

        private AudioSource musicaSrc;

        private float tempo = 0;

        public static bool Rodando = true;

        private bool gameOver = false;

        private Buraco[] buracos
        {
            get
            {
                return FindObjectsOfType<Buraco>();
            }
        }

        private void Awake()
        {
            musicaSrc = gameObjAudioSource.GetComponent<AudioSource>();
            musicaSrc.clip = musicaClip[Random.Range(0, musicaClip.Length)];
            musicaSrc.outputAudioMixerGroup = musicaMixer;
            musicaSrc.loop = true;

            musicaSrc.Play();
        }

        private void Update()
        {
            if (Rodando)
            {
                float tInstancias = Random.Range(tempoMinCriacao, tempoMaxCriacao);
                int instancias = Random.Range(1, maxInimigos + 1);

                tempo += Time.deltaTime;

                if (tempo >= tInstancias)
                {
                    tempo = 0;
                    for (int i = 0; i < instancias; i++)
                    {
                        int bId = Random.Range(0, buracos.Length);
                        if (!buracos[bId].Ocupado)
                        {
                            buracos[bId].gameObject.GetComponent<Buraco>().CriarInimigo();
                        }
                    }
                }
            }

            if (gameOver)
            {
                timerGameOver += Time.deltaTime;
                if (timerGameOver > 5)
                {
                    if (musicaSrc.loop)
                    {
                        if (musicaSrc.volume <= 1)
                        {
                            musicaSrc.volume += Time.deltaTime / 30.0f;
                        }
                        return;
                    }
                    musicaSrc.clip = musicaGameOver;
                    musicaSrc.loop = true;
                    musicaSrc.volume = 0;
                    musicaSrc.Play();
                }
            }
        }

        public void Finalizar()
        {
            gameOver = true;
            musicaSrc.Stop();
            musicaSrc.loop = false;
            //musicaSrc.clip = gameOverClip;
            //musicaSrc.Play();
        }
    }

}