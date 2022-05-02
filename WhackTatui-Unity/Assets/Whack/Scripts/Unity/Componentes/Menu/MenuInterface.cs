using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuInterface : MonoBehaviour
{
    [Header("Botão voltar")]
    [SerializeField] private Button btnVoltar;
    [SerializeField] private AudioClip btnVoltarClip;
    [SerializeField] private GameObject origem;
    [SerializeField] private GameObject destino;

    protected FonteDeAudio fonteDeAudio;

    protected Action VoltarExtra { get; set; }
    
    void Awake()
    {
        if (btnVoltar != null) AddBotao(btnVoltar, Voltar);
        fonteDeAudio = GetComponent<FonteDeAudio>();
    }

    private void Voltar()
    {
        AbrirMenu(origem, destino, btnVoltarClip);
        VoltarExtra?.Invoke();
    }

    protected void AbrirMenu(GameObject paraFechar=null, GameObject paraAbrir=null, AudioClip clip=null)
    {
        if (paraFechar != null) paraFechar.SetActive(false);
        if (clip != null) fonteDeAudio.Tocar(clip);
        if (paraAbrir != null) paraAbrir.SetActive(true);
    }

    protected void AddBotao(Button botao, UnityEngine.Events.UnityAction action)
    {
        botao.onClick.AddListener(action);
    }
}
