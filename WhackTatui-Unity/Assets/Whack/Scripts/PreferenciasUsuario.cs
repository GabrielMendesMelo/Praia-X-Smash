using System;

public static class PreferenciasUsuario
{
    [Serializable]
    public class Prefs
    {
        public static float volume;
        public static GRAFICO grafico;
    }
}

public enum GRAFICO
{
    MUITO_BAIXO,
    BAIXO,
    MEDIO,
    ALTO,
    MUITO_ALTO
}
