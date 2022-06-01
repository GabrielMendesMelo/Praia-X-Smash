using UnityEngine;
using UnityEngine.UI;

public class FimDeJogoTentarSalvarMenu : Menu
{
    [Header("Fim de Jogo")]
    [SerializeField] private Button btnTentar;
    [SerializeField] private AudioClip btnTentarClip;

    [SerializeField] private Button btnContinuar;
    [SerializeField] private AudioClip btnContinuarClip;

    private FimDeJogoController controller;

    private void Start()
    {
        AddBotao(btnTentar, TentarNovamente);
        AddBotao(btnContinuar, Continuar);

        controller = transform.parent.GetComponent<FimDeJogoController>();
    }

    private void TentarNovamente()
    {
        fonteDeAudio.Tocar(btnTentarClip);
        controller.SalvarEContinuar();
    }

    private void Continuar()
    {
        fonteDeAudio.Tocar(btnContinuarClip);
        controller.Continuar();
    }
}
