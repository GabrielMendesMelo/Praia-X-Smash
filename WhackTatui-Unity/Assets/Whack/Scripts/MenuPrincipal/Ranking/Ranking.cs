using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class Ranking
{

    private static List<Jogador> jogadores;
    public static List<Jogador> Jogadores
    {
        get
        {
            return jogadores ?? (jogadores = new List<Jogador>());
        }
        private set
        {
            jogadores = value;
        }
    }

    public static RankingEstado Estado { get; private set; }

    public static IEnumerator Inicializar()
    {
        yield return Atualizar();
    }

    public static IEnumerator Atualizar()
    {
        Estado = RankingEstado.Carregando;

        jogadores?.Clear();

        yield return RankingRequests.Get_Jogadores(
            sucesso =>
            {
                if (sucesso)
                {
                    Jogadores = RankingJsonHelper.FromJson<Jogador>(RankingRequests.JogadoresJson).ToList();
                    Estado = RankingEstado.Ok;
                }
                else
                {
                    Estado = RankingEstado.Erro;
                }
            });
    }

    public static IEnumerator AdicionarJogador(string nome, int pontuacao)
    {
        yield return Atualizar();

        Estado = RankingEstado.Carregando;

        Jogador j = new Jogador(nome.ToUpper(), pontuacao);
        string jJson = JsonUtility.ToJson(j);

        yield return RankingRequests.Post_AddJogador(jJson,
            sucesso =>
            {
                if (sucesso)
                {
                    Estado = RankingEstado.Ok;
                }
                else
                {
                    Estado = RankingEstado.Erro;
                }
            });
    }

    [Serializable]
    public class Jogador
    {
        public string nome;
        public int pontuacao;

        public Jogador(string nome, int pontuacao)
        {
            this.nome = nome;
            this.pontuacao = pontuacao;
        }
    }
}
