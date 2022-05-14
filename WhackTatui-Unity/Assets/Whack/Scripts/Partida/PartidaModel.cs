using System.Collections.Generic;
using UnityEngine;


public class PartidaModel : MonoBehaviour
{
    [SerializeField] GameObject[] tatuis;
    [SerializeField, Min(0)] private float tempoMinEntreTatuis = 0;
    [SerializeField, Min(0)] private float tempoMaxEntreTatuis = 3;
    [SerializeField, Min(0)] private int minTatuisAoMesmoTempo = 1;
    [SerializeField, Min(1)] private int maxTatuisAoMesmoTempo = 2;

    public GameObject[] Tatuis { get { return tatuis; } }
    public float TempoMinEntreTatuis { get { return tempoMinEntreTatuis; } }
    public float TempoMaxEntreTatuis { get { return tempoMaxEntreTatuis; } }
    public int MinTatuisAoMesmoTempo { get { return minTatuisAoMesmoTempo; } }
    public int MaxTatuisAoMesmoTempo { get { return maxTatuisAoMesmoTempo; } }

    public Buraco[] Buracos
    {
        get
        {
            return Object.FindObjectsOfType<Buraco>();
        }
    }

    public List<Buraco> BuracosDisponiveis
    {
        get
        {
            List<Buraco> disponiveis = new List<Buraco>();

            foreach (Buraco b in Buracos)
            {
                if (!b.Ocupado)
                {
                    disponiveis.Add(b);
                }
            }
            return disponiveis;
        }
    }
}
