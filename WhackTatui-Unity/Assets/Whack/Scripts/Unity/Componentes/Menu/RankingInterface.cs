using UnityEngine;
using UnityEngine.UI;

public class RankingInterface : MenuInterface
{
    [Header("Ranking")]
    [SerializeField] private Button btnAtualizar;
    [SerializeField] private AudioClip btnAtualizarClip;

    private void Start()
    {
        AddBotao(btnAtualizar, Atualizar);
    }

    private void Atualizar()
    {
        fonteDeAudio.Tocar(btnAtualizarClip);
        //Atualizar ranking
    }
}
