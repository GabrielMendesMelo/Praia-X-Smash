using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class BackendRequests
{
    private static object resultadoGet;
    public static object ResultadoGet
    {
        get
        {
            if (resultadoGet == null)
            {
                return "<color=red>Nenhum GET Request foi realizado com sucesso até o momento.</color>";
            }
            return resultadoGet;
        }
    }

    public static IEnumerator Get(string url, Action<bool> action=null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            resultadoGet = www.downloadHandler.text;

            AposRequest(www, action);
        }
    }

    public static IEnumerator Post(string url, string postData, Action<bool> action=null)
    {
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            byte[] rawData = Encoding.UTF8.GetBytes(postData);
            www.uploadHandler = new UploadHandlerRaw(rawData);

            yield return www.SendWebRequest();

            AposRequest(www, action);
        }
    }

    private static void AposRequest(UnityWebRequest request, Action<bool> action=null)
    {
        if (request.result == UnityWebRequest.Result.Success)
        {
            action?.Invoke(true);
            Debug.Log("<color=green>" + request.method + " Request obteve sucesso!</color>");
        }
        else
        {
            action?.Invoke(false);
            Debug.Log("<color=red>" + request.method + " Request erro: " + request.result + "</color>");
        }
    }
}
