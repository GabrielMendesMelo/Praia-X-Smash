using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingMenu : Menu
{
    [Header("Ranking")]
    [SerializeField] private Button btnAtualizar;
    [SerializeField] private AudioClip btnAtualizarClip;

    [SerializeField] private TextMeshProUGUI aviso;
    [SerializeField] private string avisoCarregando;
    [SerializeField] private string avisoNaoCarregou;

    [SerializeField] private int maxRanking;
    [SerializeField] private GameObject rankingConteudo;
    [SerializeField] private GameObject linhaPrefab;
    [SerializeField] private float linhaOffsetY;

    private List<GameObject> linhas;


    private void Start()
    {
        AddBotao(btnAtualizar, Atualizar);
    }

    private void OnEnable()
    {
        Atualizar();
    }

    private void Atualizar()
    {
        fonteDeAudio.Tocar(btnAtualizarClip);

        StartCoroutine(Ranking.Atualizar());
        StartCoroutine(Carregar());
    }

    private IEnumerator Carregar()
    {
        if (linhas == null)
        {
            linhas = new List<GameObject>();
        }
        else
        {
            foreach (GameObject l in linhas)
            {
                Destroy(l);
            }
            linhas.Clear();
        }

        if (Ranking.Estado == RankingEstado.Carregando)
        {
            aviso.text = avisoCarregando;
        }

        while (Ranking.Estado == RankingEstado.Carregando)
        {
            yield return null;
        }
        
        switch (Ranking.Estado)
        {
            case RankingEstado.Ok:
                ListarRanking();
                break;

            case RankingEstado.Erro:
                aviso.text = avisoNaoCarregou;
                break;
        }
    }

    private void ListarRanking()
    {
        aviso.text = "";

        int max = maxRanking > Ranking.Jogadores.Count ? Ranking.Jogadores.Count : maxRanking;

        for (int i = 0; i < max; i++)
        {
            Ranking.Jogador j = Ranking.Jogadores[i];

            GameObject linha = Instantiate(linhaPrefab, rankingConteudo.transform);
            linha.name = i + "_" + j.nome + "_" + j.pontuacao;
            linha.transform.localPosition = Vector3.up * (390 - (linhaOffsetY + (i * linhaOffsetY)));
            linhas.Add(linha);

            RankingLinha lR = linha.GetComponent<RankingLinha>();
            lR.Id = i + 1;
            lR.Nome = j.nome;
            lR.Pontuacao = j.pontuacao;
        }
    }
}
