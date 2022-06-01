using System;

public class Contador
{
    public float TempoRestante { get; private set; }

    public Contador(float duracao)
    {
        TempoRestante = duracao;
    }

    public event Action AoTerminarTempo;

    public void Tick(float deltaTime)
    {
        if (TempoRestante == 0f) return;

        TempoRestante -= deltaTime;

        ChecarTerminouTempo();
    }

    private void ChecarTerminouTempo()
    {
        if (TempoRestante > 0f) return;

        TempoRestante = 0f;

        AoTerminarTempo?.Invoke();
    }
}
