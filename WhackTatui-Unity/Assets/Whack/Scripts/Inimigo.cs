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
    [SerializeField] private AudioClip[] sfxAcertou2;
    private AudioSource[] sfxSrcs = new AudioSource[3];

    [SerializeField] private GameObject ponto;
    private GameObject pt;
    [SerializeField] private float minVelocidadePt, maxVelocidadePt;
    private float velocidadePt;
    private Color[] corPt = new Color[]
    {
        Color.cyan,
        Color.yellow
    };

    private int direcao = 1;
    private bool parado = false;
    private float tempoNaTela = 0;

    private bool acertou = false;

    private GameObject buraco;

    private Animator anim;

    private void Awake()
    {
        for (int i = 0; i < sfxSrcs.Length; i++)
        {
            sfxSrcs[i] = gameObject.AddComponent<AudioSource>();
            sfxSrcs[i].outputAudioMixerGroup = sfxMixer;
        }

        sfxSrcs[0].clip = sfxApareceu[Random.Range(0, sfxApareceu.Length)];
        sfxSrcs[1].clip = sfxAcertou[Random.Range(0, sfxAcertou.Length)];
        sfxSrcs[2].clip = sfxAcertou2[Random.Range(0, sfxAcertou2.Length)];

        modelo.GetComponent<SkinnedMeshRenderer>().material = materiaisOpcoes[Random.Range(0, materiaisOpcoes.Length)];
        
        anim = gameObject.GetComponent<Animator>();

        anim.SetFloat("subindoOffset", Random.Range(0f, 1f));
        anim.SetFloat("idleOffset", Random.Range(0f, 1f));

        maxTempoNaTela = Random.Range(minTempoNaTela, maxTempoNaTela);

        velocidadePt = Random.Range(minVelocidadePt, maxVelocidadePt);
    }

    private void Start()
    {
        sfxSrcs[0].Play();
    }

    void Update()
    {
        if (pt != null)
        {
            pt.transform.Translate(Vector3.up * velocidadePt * Time.deltaTime);
            pt.transform.localScale -= Vector3.one * velocidadePt * Time.deltaTime;
            if (pt.transform.localScale.x <= 0)
            {
                Destroy(pt);
            }
        }

        if (Fase.Rodando)
        {
            if (Input.touchCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == gameObject.transform.position)
                    {
                        Acertar();                        
                    }
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == gameObject.transform.position)
                    {
                        Acertar();
                    }
                }
            }

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
                
                if (!acertou)
                {
                    Combo.Set();
                }

                if (pt != null) Destroy(pt);
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
    
    private void Acertar()
    {
        acertou = true;

        TextMesh ptTxt;

        switch (Combo.Multiplicador)
        {
            case float n when n >= 5 && n < 10:
                pt = Instantiate(ponto, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                ptTxt = pt.GetComponent<TextMesh>();
                ptTxt.text = Mathf.FloorToInt(Combo.Multiplicador).ToString();

                ptTxt.color = corPt[0];
                pt.transform.localScale *= 1.1f;
                velocidadePt *= 1.5f;
                break;

            case float n when n >= 10:
                pt = Instantiate(ponto, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                ptTxt = pt.GetComponent<TextMesh>();
                ptTxt.text = Mathf.FloorToInt(Combo.Multiplicador).ToString();

                ptTxt.color = corPt[1];
                pt.transform.localScale *= 1.6f;
                velocidadePt *= 2f;
                break;                    
        }
        if (Combo.Multiplicador >= 30) { }

        Combo.Aumentar();

        sfxSrcs[1].Play();
        sfxSrcs[2].Play();

        anim.SetTrigger("acertou");

        Jogador.Pontos += Mathf.FloorToInt(Combo.Multiplicador);
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
