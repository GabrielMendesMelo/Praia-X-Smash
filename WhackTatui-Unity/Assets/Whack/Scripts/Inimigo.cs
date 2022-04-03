using UnityEngine;
using UnityEngine.Audio;

public class Inimigo : MonoBehaviour
{
    [SerializeField] private float min, max;
    [SerializeField] private float velocidade;
    [SerializeField] private float multiplicadorSubindo;
    [SerializeField] private float multiplicadorDescendo;
    [SerializeField] private float minTempoNaTela;
    [SerializeField] private float maxTempoNaTela;

    [SerializeField] private GameObject modelo;
    [SerializeField] private Material[] materiaisOpcoes;

    [SerializeField] private GameObject acertouFx;

    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioClip[] sfxApareceu;
    [SerializeField] private AudioClip[] sfxAcertou;
    private AudioSource[] sfxSrcs = new AudioSource[2];

    private int direcao = 1;
    private bool parado = false;
    private float tempoNaTela = 0;

    private GameObject buraco;

    private Animator anim;

    private void Awake()
    {
        for (int i = 0; i < sfxSrcs.Length; i++)
        {
            sfxSrcs[i] = gameObject.AddComponent<AudioSource>();
            sfxSrcs[i].outputAudioMixerGroup = sfxMixer;
            sfxSrcs[i].volume = PreferenciasUsuario.sfx;
        }

        sfxSrcs[0].clip = sfxApareceu[Random.Range(0, sfxApareceu.Length)];
        sfxSrcs[1].clip = sfxAcertou[Random.Range(0, sfxAcertou.Length)];

        modelo.GetComponent<SkinnedMeshRenderer>().material = materiaisOpcoes[Random.Range(0, materiaisOpcoes.Length)];
        
        anim = gameObject.GetComponent<Animator>();

        anim.SetFloat("subindoOffset", Random.Range(0f, 1f));
        anim.SetFloat("idleOffset", Random.Range(0f, 1f));

        maxTempoNaTela = Random.Range(minTempoNaTela, maxTempoNaTela);
    }

    private void Start()
    {
        sfxSrcs[0].Play();
    }

    void Update()
    {
        if (Fase.Rodando)
        {
            if (transform.position.y >= max && tempoNaTela == 0)
            {
                transform.position = new Vector3(transform.position.x, max, transform.position.z);
                parado = true;
                direcao = -1;
            }
            else if (transform.position.y < min)
            {
                buraco.GetComponentInChildren<MeshRenderer>().enabled = true;
                buraco.GetComponent<Buraco>().Liberar();
                Destroy(gameObject);
            }

            if (!parado)
            {
                transform.position += Vector3.up * Time.deltaTime * velocidade * (direcao > 0 ? direcao * multiplicadorSubindo : direcao * multiplicadorDescendo);
            }
            else
            {
                tempoNaTela += Time.deltaTime;
                if (tempoNaTela >= maxTempoNaTela)
                {
                    parado = false;
                }
            }
        }
    }

    public void setBuraco(GameObject buraco)
    {
        this.buraco = buraco;
    }

    private void OnMouseDown()
    {
        Acertar();
    }

    private void Acertar()
    {
        sfxSrcs[1].Play();

        anim.SetTrigger("acertou");

        Jogador.Pontos++;
        Jogador.AtualizarPontos();

        max = transform.position.y;

        transform.position = new Vector3(transform.position.x, max, transform.position.z);
        tempoNaTela = 0;
        maxTempoNaTela = 0.6f;

        gameObject.GetComponent<Collider>().enabled = false;

        Vector3 bPos = buraco.transform.position;

        Instantiate(acertouFx, new Vector3(bPos.x, bPos.y + 0.5f, bPos.z - 0.2f), Quaternion.identity);
    }
}
