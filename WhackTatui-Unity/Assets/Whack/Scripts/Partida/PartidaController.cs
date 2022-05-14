using System.Collections.Generic;
using UnityEngine;

public class PartidaController : MonoBehaviour
{
    private PartidaModel model;
    private PartidaCanvas canvas;
    
    [SerializeField] private GameObject fimDeJogo;

    private FonteDeAudio fonteDeAudio;

    private Contador contador;

    private void Start()
    {
        model = GetComponent<PartidaModel>();
        canvas = GetComponentInChildren<PartidaCanvas>();

        fonteDeAudio = GetComponent<FonteDeAudio>();

        ContarTempoCriarTatuis();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        contador.Tick(Time.deltaTime);
    }

    private void ContarTempoCriarTatuis()
    {
        contador = new Contador(Random.Range(model.TempoMinEntreTatuis, model.TempoMaxEntreTatuis));
        contador.AoTerminarTempo += CriarTatuis;
    }

    private void CriarTatuis()
    {
        ContarTempoCriarTatuis();

        int nTatuis = Random.Range(model.MinTatuisAoMesmoTempo, model.MaxTatuisAoMesmoTempo + 1);

        for (int i = 0; i < nTatuis; i++)
        {
            if (model.BuracosDisponiveis.Count <= 0)
            {
                return;
            }
            model.BuracosDisponiveis[Random.Range(0, model.BuracosDisponiveis.Count)].CriarTatui(model.Tatuis[Random.Range(0, model.Tatuis.Length)]);
        }
    }

    public void Acabar()
    {
        Time.timeScale = 0f;

        foreach(Buraco b in model.Buracos)
        {
            b.Liberar();
        }

        Instantiate(fimDeJogo, canvas.transform);
        fonteDeAudio.Parar();

        Destroy(this);
    }
}
