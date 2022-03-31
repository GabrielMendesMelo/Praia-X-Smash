using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Extensions;
using Utilidades;

using UnityEngine;

public static class Ranking
{
    /*
    private Ranking() { }

    private static Ranking instancia;
    public static Ranking Instancia
    {
        get
        {
            Debug.Log(instancia);
            if (instancia == null)
            {
                instancia = new Ranking();
            }
            return instancia;
        }
    }
    */

    [Serializable]
    public class Jogador
    {
        public int pontos;
        public string nome;

        public Jogador(int pontos, string nome)
        {
            this.pontos = pontos;
            this.nome = nome;
        }
    }

    public const int MAX = 10;

    public static List<Jogador> jogadores;
    private static string txt;

    private static DatabaseReference dbRef = FirebaseDatabase.GetInstance("https://whack-tatui-320b9-default-rtdb.firebaseio.com/").RootReference;
    
    public static void Carregar()
    {
        dbRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (jogadores == null) jogadores = new List<Jogador>();
                jogadores = JsonHelper.FromJson<Jogador>(task.Result.GetRawJsonValue()).ToList();
            }
            else
            {
                throw new Exception("FirebaseDatabase.DatabaseReference.GetValueAsync(task => task.IsCompleted == false);");
            }
        });
    }
    
    public static void Salvar()
    {
        if (jogadores.Count > MAX)
        {
            /*
            for (int i = MAX; i < jogadores.Count; i++)
            {
                jogadores.RemoveAt(i);
            }
            */
            jogadores.RemoveAt(jogadores.Count - 1);
        }

        txt = JsonHelper.ToJson(jogadores.ToArray(), true);

        dbRef.SetRawJsonValueAsync(txt).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("SALVOU");
                Carregar();
            }
            else
            {
                throw new Exception("FirebaseDatabase.DatabaseReference.GetValueAsync(task => task.IsCompleted == false);");
            }
        });
    }

    public static void Adicionar(int pontos, string nome)
    {
        Jogador jogador = new Jogador(pontos, nome.ToUpper());
        jogadores.Add(jogador);
        Ordenar();

        Salvar();
    }

    private static void Ordenar()
    {
        int[] array = new int[jogadores.Count];

        List<Jogador> copia = new List<Jogador>();

        for (int i = 0; i < jogadores.Count; i++)
        {
            copia.Add(jogadores[i]);
            array[i] = jogadores[i].pontos;
        }

        jogadores.Clear();

        Ordenacao.HeapSort(array);

        Array.Reverse(array, 0, array.Length);
        
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < copia.Count; j++)
            {
                if (copia[j].pontos == array[i])
                {
                    jogadores.Add(copia[j]);
                    break;
                }
            }
        }
    }
}
