using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalInterface : MenuInterface
{
    [Header("Menu Principal")]
    [SerializeField] private Button btnComecar;
    [SerializeField] private AudioClip btnComecarClip;
    private CarregaCena carregaCena;

    [SerializeField] private Button btnOpcoes;
    [SerializeField] private AudioClip btnOpcoesClip;
    [SerializeField] private GameObject opcoes;

    [SerializeField] private Button btnRanking;
    [SerializeField] private AudioClip btnRankingClip;
    [SerializeField] private GameObject ranking;

    [SerializeField] private Button btnSair;
    [SerializeField] private Button btnGitHub;

    void Start()
    {
        AddBotao(btnComecar, Comecar);
        AddBotao(btnOpcoes, AbrirOpcoes);
        AddBotao(btnRanking, AbrirRanking);
        AddBotao(btnSair, Sair);
        AddBotao(btnGitHub, AbrirPaginaGithub);

        carregaCena = gameObject.GetComponent<CarregaCena>();
    }

    private void Comecar()
    {
        fonteDeAudio.Tocar(btnComecarClip);

        carregaCena.Carregar(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void AbrirOpcoes()
    {
        AbrirMenu(gameObject, opcoes, btnOpcoesClip);
    }

    private void AbrirRanking()
    {
        AbrirMenu(gameObject, ranking, btnRankingClip);
    }

    private void Sair()
    {
        Application.Quit();
    }

    private void AbrirPaginaGithub()
    {
        Application.OpenURL("https://github.com/GabrielMendesMelo/Whack-Tatui-Unity");
    }
}
