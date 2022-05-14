using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [Header("Pause")]
    [SerializeField] private GameObject principal;

    [SerializeField] private Button btnOpcoes;
    [SerializeField] private AudioClip btnOpcoesClip;
    [SerializeField] private GameObject opcoes;

    [SerializeField] private Button btnRecomecar;
    [SerializeField] private AudioClip btnRecomecarClip;

    [SerializeField] private Button btnSair;
    [SerializeField] private AudioClip btnSairClip;

    private CarregaCena carregaCena;

    void Start()
    {
        AddBotao(btnOpcoes, AbrirOpcoes);
        AddBotao(btnRecomecar, Recomecar);
        AddBotao(btnSair, Sair);

        VoltarExtra = () => Time.timeScale = 1f;

        carregaCena = gameObject.GetComponent<CarregaCena>();
    }

    private void AbrirOpcoes()
    {
        AbrirMenu(principal, opcoes, btnOpcoesClip);
    }

    private void Recomecar()
    {
        fonteDeAudio.Tocar(btnRecomecarClip);
        carregaCena.Carregar(SceneManager.GetActiveScene().buildIndex, gameObject.transform.parent);
    }

    private void Sair()
    {
        fonteDeAudio.Tocar(btnSairClip);
        carregaCena.Carregar(SceneManager.GetActiveScene().buildIndex - 1, gameObject.transform.parent);
    }
}
