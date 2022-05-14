using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : Menu
{
    [Header("Menu Principal")]
    [SerializeField] private Button btnComecar;
    [SerializeField] private AudioClip btnComecarClip;
    private CarregaCena carregaCena;

    [SerializeField] private Button btnOpcoes;
    [SerializeField] private AudioClip btnOpcoesClip;
    [SerializeField] private GameObject menuOpcoes;

    [SerializeField] private Button btnRanking;
    [SerializeField] private AudioClip btnRankingClip;
    [SerializeField] private GameObject menuRanking;

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
        AbrirMenu(gameObject, menuOpcoes, btnOpcoesClip);
    }

    private void AbrirRanking()
    {
        AbrirMenu(gameObject, menuRanking, btnRankingClip);
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
