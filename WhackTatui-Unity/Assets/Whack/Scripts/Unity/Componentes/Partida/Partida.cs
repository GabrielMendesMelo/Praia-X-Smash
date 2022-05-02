using System.Collections.Generic;
using UnityEngine;
using Whack.Scripts.Unity;

public class Partida : MonoBehaviour
{
    [SerializeField, Min(0)] private float tempoMinEntreTatuis = 0;
    [SerializeField, Min(0)] private float tempoMaxEntreTatuis = 3;
    [SerializeField, Min(0)] private int minTatuisAoMesmoTempo = 1;
    [SerializeField, Min(1)] private int maxTatuisAoMesmoTempo = 2;
    [SerializeField] private GameObject[] tatuis;

    [SerializeField] private GameObject fimDeJogo;

    private FonteDeAudio fonteDeAudio;

    Whack.Scripts.Csharp.Contador contador;

    private void Start()
    {
        fonteDeAudio = GetComponent<FonteDeAudio>();

        ContarTempoCriarTatuis();

        PartidaInfo.Rodar();
    }

    private void Update()
    {
        contador.Tick(Time.deltaTime);
    }

    private void ContarTempoCriarTatuis()
    {
        contador = new Whack.Scripts.Csharp.Contador(Random.Range(tempoMinEntreTatuis, tempoMaxEntreTatuis));
        contador.AoTerminarTempo += CriarTatuis;
    }

    private void CriarTatuis()
    {
        int nTatuis = Random.Range(minTatuisAoMesmoTempo, maxTatuisAoMesmoTempo + 1);

        for (int i = 0; i < nTatuis; i++)
        {
            List<Buraco> buracos = PartidaInfo.BuracosDisponiveis;
            if (buracos.Count > 0)
            {
                buracos[Random.Range(0, PartidaInfo.BuracosDisponiveis.Count)].CriarTatui(tatuis[Random.Range(0, tatuis.Length)]);
            }
        }
        ContarTempoCriarTatuis();
    }

    public void Acabar()
    {
        PartidaInfo.Parar();
        foreach(Buraco b in PartidaInfo.Buracos)
        {
            b.Liberar();
        }

        Instantiate(fimDeJogo, FindObjectOfType<PartidaInterface>().transform);
        fonteDeAudio.Parar();

        Destroy(this);
    }
}
