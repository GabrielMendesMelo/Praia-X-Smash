using System;
using System.Collections;

public static class RankingRequests
{
    const string urlGet = "url do servidor";
    const string urlPost = "url do servidor";

    public static string JogadoresJson { get; set; }

    public static IEnumerator Get_Jogadores(Action<bool> action=null)
    {
        yield return BackendRequests.Get(urlGet,
            sucesso =>
            {
                if (sucesso)
                {
                    JogadoresJson = BackendRequests.ResultadoGet.ToString();
                }
                action?.Invoke(sucesso);
            });
    }

    public static IEnumerator Post_AddJogador(string jogadorJson, Action<bool> action=null)
    {
        yield return BackendRequests.Post(urlPost, jogadorJson, action);
    }
}
