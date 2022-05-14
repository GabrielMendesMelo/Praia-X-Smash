using UnityEngine;

public class TatuiController : MonoBehaviour
{
    private TatuiModel model;

    private Animator animator;

    private Movimento movimento = Movimento.SUBINDO;

    private FonteDeAudio[] fontesDeAudio;

    private Jogador jogador;

    private bool ChecarAcertou
    {
        get
        {
            //CELULAR
            if (Input.touchCount >= 1)
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
   
    private void Start()
    {
        model = GetComponent<TatuiModel>();

        animator = GetComponent<Animator>();

        animator.SetFloat("subindoOffset", Random.Range(0f, 1f));
        animator.SetFloat("idleOffset", Random.Range(0f, 1f));

        fontesDeAudio = GetComponents<FonteDeAudio>();

        jogador = FindObjectOfType<Jogador>();
    }

    private void Update()
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
                if (model.FoiAcertado)
                {
                    Esperar(model.TempoAEsperarAoSerAcertado);
                }
                else
                {
                    Esperar(model.TempoAFicarParado);
                }
                break;

            case Movimento.DESCENDO:
                Descer();
                break;
        }
    }

    private void Subir()
    {
        gameObject.transform.Translate(Vector3.up * model.VelocidadeSubindo * Time.deltaTime);

        if (gameObject.transform.position.y > model.PosMax)
        {
            movimento = Movimento.ESPERANDO;
        }
    }

    private void Esperar(float tempoAFicarParado)
    {
        model.TempoParado += Time.deltaTime;
        if (model.TempoParado > tempoAFicarParado) movimento = Movimento.DESCENDO;
    }

    private void Descer()
    {
        gameObject.transform.Translate(Vector3.down * model.VelocidadeDescendo * Time.deltaTime);

        if (gameObject.transform.position.y < model.PosMin)
        {
            GetComponentInParent<Buraco>().Liberar();
            if (!model.FoiAcertado)
            {
                jogador.ZerarCombo();
            }
            Destroy(gameObject);
        }
    }

    private void Acertar()
    {
        model.FoiAcertado = true;

        model.TempoParado = 0f;

        animator.SetTrigger("acertou");

        fontesDeAudio[0].Tocar(model.AcertouClip);
        fontesDeAudio[1].Tocar(model.Acertou2Clip);

        gameObject.GetComponent<Collider>().enabled = false;

        Vector3 pPos = gameObject.transform.parent.transform.position;
        Instantiate(model.AcertouEfeito, new Vector3(pPos.x, pPos.y + 0.5f, pPos.z - 0.2f), Quaternion.identity);

        if (jogador.PontosPorAcerto >= model.MostrarPontosAPartirDe)
        {
            Instantiate(model.PontosEfeito, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
        }

        jogador.Pontuar(model.MultPontos);
    }
}
