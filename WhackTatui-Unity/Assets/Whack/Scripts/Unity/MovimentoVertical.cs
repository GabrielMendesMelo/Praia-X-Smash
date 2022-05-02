using UnityEngine;

namespace Whack.Scripts.Unity
{
    public class MovimentoVertical
    {
        private GameObject gameObject;
        private float velocidadeInicial;

        private float velocidade;

        public MovimentoVertical(GameObject gameObject, float velocidade)
        {
            this.gameObject = gameObject;
            velocidadeInicial = velocidade;
        }

        public void Movimentar(Vector3 direcao, float variacaoVelocidade)
        {
            velocidade = variacaoVelocidade != 0 ? velocidadeInicial * variacaoVelocidade : velocidadeInicial;

            gameObject.transform.Translate(direcao * velocidade * Time.deltaTime);
        }
    }
}
