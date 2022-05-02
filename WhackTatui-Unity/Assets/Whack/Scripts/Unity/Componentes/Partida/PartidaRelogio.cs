using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PartidaRelogio : MonoBehaviour
{
    [SerializeField] private float tempoInicial;
    [SerializeField] private float tempoFinal;
    private float tempo;
    [SerializeField] private TextMeshProUGUI txtTempo;

    [SerializeField] private UnityEvent aoAcabarTempo = null;

    private Whack.Scripts.Csharp.Contador contador;

    private FonteDeAudio fonteDeAudio;

    private void Start()
    {
        txtTempo.text = tempoInicial.ToString();
        tempo = tempoInicial;

        fonteDeAudio = GetComponent<FonteDeAudio>();

        Contar();
    }

    private void Update()
    {
        contador.Tick(Time.deltaTime);
    }

    private void DiminuirTempo()
    {
        tempo--;
        txtTempo.text = tempo.ToString();

        if (tempo > -1) Contar();
        else
        {
            aoAcabarTempo.Invoke();
            Destroy(this);
        }
    }

    private void Contar()
    {
        if (tempo < tempoFinal + 1)
        {
            fonteDeAudio.Tocar();
            txtTempo.color = Color.red;
        }

        contador = new Whack.Scripts.Csharp.Contador(1);

        contador.AoTerminarTempo += DiminuirTempo;
    }
}
