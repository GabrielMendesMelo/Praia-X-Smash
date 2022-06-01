using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FimDeJogoMenu : Menu
{
    [Header("Fim de Jogo")]
    [SerializeField] private Button btnRecomecar;
    [SerializeField] private AudioClip btnRecomecarClip;

    [SerializeField] private Button btnSair;
    [SerializeField] private AudioClip btnSairClip;

    [SerializeField] private TextMeshProUGUI txtPontos;
    [SerializeField] private TMP_InputField inputNome;

    private FimDeJogoController controller;

    private int jogadorPontos
    {
        get
        {
            return FindObjectOfType<Jogador>().Pontos;
        }
    }

    private string jogadorNome
    {
        get
        {
            return string.IsNullOrEmpty(inputNome.text) ? "AAA" : inputNome.text;
        }
    }

    private void Start()
    {
        AddBotao(btnRecomecar, Recomecar);
        AddBotao(btnSair, Sair);

        controller = transform.parent.GetComponent<FimDeJogoController>();

        txtPontos.text = "Você fez " + jogadorPontos + " pontos!";
    }

    private void Recomecar()
    {
        fonteDeAudio.Tocar(btnRecomecarClip);

        controller.SalvarEContinuar(jogadorNome, jogadorPontos, FimDeJogoEscolha.Recomecar);
    }

    private void Sair()
    {
        fonteDeAudio.Tocar(btnSairClip);

        controller.SalvarEContinuar(jogadorNome, jogadorPontos, FimDeJogoEscolha.Sair);
    }
}
