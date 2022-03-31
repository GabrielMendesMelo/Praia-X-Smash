using UnityEngine;

public class Fase : MonoBehaviour
{
    [SerializeField, Min(0)] private float tempoMinCriacao;
    [SerializeField, Min(0)] private float tempoMaxCriacao;
    [SerializeField, Min(1)] private int maxInimigos;

    [SerializeField] private GameObject gameObjAudioSource;
    [SerializeField] private AudioClip musicaClip;
    private AudioSource musicaSrc;
    
    [SerializeField] private float musicaVolume;

    private float tempo = 0;

    public static bool Rodando = true;

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
        musicaSrc.clip = musicaClip;
        musicaSrc.volume = musicaVolume;
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
    }
}
