using UnityEngine;
using TMPro;

public class Jogador : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPontos;
    [SerializeField, Min(1)] private int maxPontosPorAcerto = 10;
    [SerializeField, Min(1)] private int taxaComboIncremento = 10;

    private float pontos;
    private float pontosPorAcerto;    

    public int MaxPontosPorAcerto { get { return maxPontosPorAcerto; } }

    public int Pontos
    {
        get
        {
            return Mathf.FloorToInt(pontos);
        }
        private set
        {
            pontos = value;
        }
    }

    public float PontosPorAcerto
    { 
        get 
        { 
            return pontosPorAcerto; 
        } 
        private set
        {
            if (value < 0) pontosPorAcerto = 1;
            else if (value > MaxPontosPorAcerto) pontosPorAcerto = MaxPontosPorAcerto;
            else pontosPorAcerto = value;
        }
    }

    private void Start()
    {
        Pontos = 0;
        PontosPorAcerto = 1;

        txtPontos.text = Pontos.ToString();
    }

    public void Pontuar()
    {
        Pontos += Mathf.FloorToInt(PontosPorAcerto);
        IncrementarCombo();

        txtPontos.text = Pontos.ToString();
    }

    private void IncrementarCombo()
    {
        PontosPorAcerto += PontosPorAcerto / taxaComboIncremento;
        if (PontosPorAcerto > MaxPontosPorAcerto)
        {
            PontosPorAcerto = MaxPontosPorAcerto;
        }
    }

    public void ZerarCombo()
    {
        PontosPorAcerto = 1;
    }
}
