using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FimDeJogoInterface : MenuInterface
{
    [Header("Fim de Jogo")]
    [SerializeField] private Button btnRecomecar;
    [SerializeField] private AudioClip btnRecomecarClip;

    [SerializeField] private Button btnSair;
    [SerializeField] private AudioClip btnSairClip;

    private CarregaCena carregaCena;

    private void Start()
    {
        AddBotao(btnRecomecar, Recomecar);
        AddBotao(btnSair, Sair);

        carregaCena = gameObject.GetComponent<CarregaCena>();
    }

    private void Recomecar()
    {
        CarregarCena(btnRecomecarClip, SceneManager.GetActiveScene().buildIndex);
    }

    private void Sair()
    {
        CarregarCena(btnSairClip, SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void CarregarCena(AudioClip btnClip, int cenaId)
    {
        fonteDeAudio.Tocar(btnClip);

        //Salvar Ranking

        carregaCena.Carregar(cenaId);
    }
}
