using UnityEngine;
using Whack.Scripts.Csharp.enums;
using Whack.Scripts.Unity;

public class Tatui : TatuiInfo
{
    private Animator animator;

    private Movimento movimento = Movimento.SUBINDO;
    private MovimentoVertical movimentoVertical;

    private FonteDeAudio[] fontesDeAudio;

    private Jogador jogador;

    private bool ChecarAcertou
    {
        get
        {
            //CELULAR
            if (Input.touchCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == gameObject.transform.position)
                    {
                        return true;
                    }
                }
            }

            //MOUSE
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == gameObject.transform.position)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("subindoOffset", Random.Range(0f, 1f));
        animator.SetFloat("idleOffset", Random.Range(0f, 1f));

        movimentoVertical = new MovimentoVertical(gameObject, Velocidade);

        fontesDeAudio = GetComponents<FonteDeAudio>();

        jogador = FindObjectOfType<Jogador>();
    }

    void Update()
    {
        if (ChecarAcertou) Acertar();

        Movimentar();
    }

    private void Movimentar()
    {
        switch (movimento)
        {
            case Movimento.SUBINDO:
                Subir();
                break;

            case Movimento.ESPERANDO:
                if (FoiAcertado)
                {
                    Esperar(TempoAEsperarAoSerAcertado, Movimento.DESCENDO);
                }
                else
                {
                    Esperar(TempoAFicarParado, Movimento.DESCENDO);
                }
                break;

            case Movimento.DESCENDO:
                Descer();
                break;
        }
    }

    private void Subir()
    {
        movimentoVertical.Movimentar(Vector3.up, VariacaoVelocidadeSubindo);
        if (gameObject.transform.position.y > PosMax)
        {
            movimento = Movimento.ESPERANDO;
        }
    }

    private void Esperar(float tempoAFicarParado, Movimento proximoMovimento)
    {
        TempoParado += Time.deltaTime;
        if (TempoParado > tempoAFicarParado) movimento = proximoMovimento;
    }

    private void Descer()
    {
        movimentoVertical.Movimentar(Vector3.down, VariacaoVelocidadeDescendo);
        if (gameObject.transform.position.y < PosMin)
        {
            GetComponentInParent<Buraco>().Liberar();
            if (!FoiAcertado)
            {
                jogador.ZerarCombo();
            }
            Destroy(gameObject);
        }
    }

    private void Acertar()
    {
        FoiAcertado = true;

        TempoParado = 0f;

        animator.SetTrigger("acertou");

        fontesDeAudio[0].Tocar(clip: AcertouClip);
        fontesDeAudio[1].Tocar(clip: Acertou2Clip);

        gameObject.GetComponent<Collider>().enabled = false;

        Vector3 pPos = gameObject.transform.parent.transform.position;
        Instantiate(AcertouEfeito, new Vector3(pPos.x, pPos.y + 0.5f, pPos.z - 0.2f), Quaternion.identity);

        if (jogador.PontosPorAcerto >= MostrarPontosAPartirDe)
        {
            Instantiate(PontosEfeito, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
        }

        jogador.Pontuar();
    }
}
