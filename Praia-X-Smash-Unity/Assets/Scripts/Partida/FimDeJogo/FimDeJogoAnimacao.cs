using System.Collections;
using TMPro;
using UnityEngine;

public class FimDeJogoAnimacao : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAnimacao;
    [SerializeField] float tempoMaxAnimacao = 1.5f;
    float tempoAnimacao;

    private void OnEnable()
    {
        StartCoroutine(RodarAnimacao());
    }

    IEnumerator RodarAnimacao()
    {
        tempoAnimacao = 0f;

        StartCoroutine(Ranking.Atualizar());

        while (Ranking.Estado == RankingEstado.Carregando)
        {
            Atualizar();
            yield return new WaitForEndOfFrame();
        }
        while (tempoAnimacao < tempoMaxAnimacao)
        {
            Atualizar();
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    void Atualizar()
    {
        txtAnimacao.text = Random.Range(0, 10000).ToString();
        tempoAnimacao += Time.unscaledDeltaTime;
    }
}
