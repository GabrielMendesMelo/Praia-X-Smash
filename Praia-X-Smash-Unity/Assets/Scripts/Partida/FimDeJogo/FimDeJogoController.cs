using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FimDeJogoEscolha
{
    Recomecar,
    Sair
}

public class FimDeJogoController : MonoBehaviour
{
    [SerializeField] private GameObject salvando;
    
    [SerializeField] private GameObject tentarSalvarMenu;
    
    private CarregaCena carregaCena;

    private string jogadorNome;
    private int jogadorPontos;
    private FimDeJogoEscolha escolha;

    private int proximaCenaId;

    public void SalvarEContinuar(string jogadorNome, int jogadorPontos, FimDeJogoEscolha escolha)
    {
        carregaCena = GetComponent<CarregaCena>();

        this.jogadorNome = jogadorNome;
        this.jogadorPontos = jogadorPontos;
        this.escolha = escolha;

        StartCoroutine(SalvarRanking());
        
        switch (escolha)
        {
            case FimDeJogoEscolha.Recomecar:
                proximaCenaId = SceneManager.GetActiveScene().buildIndex;
                break;

            case FimDeJogoEscolha.Sair:
                proximaCenaId = SceneManager.GetActiveScene().buildIndex - 1;
                break;
        }
    }

    public void SalvarEContinuar()
    {
        SalvarEContinuar(jogadorNome, jogadorPontos, escolha);
    }

    public void Continuar()
    {
        carregaCena.Carregar(proximaCenaId, transform.parent);
        Destroy(gameObject);
    }

    private IEnumerator SalvarRanking()
    {
        salvando.SetActive(true);

        yield return StartCoroutine(Ranking.AdicionarJogador(jogadorNome, jogadorPontos));

        switch (Ranking.Estado)
        {
            case RankingEstado.Ok:
                Continuar();
                break;

            case RankingEstado.Erro:
                salvando.SetActive(false);
                tentarSalvarMenu.SetActive(true);
                break;
        }
    }
}
