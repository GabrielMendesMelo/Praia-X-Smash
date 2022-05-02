using UnityEngine;

namespace Antigo
{
    public class Buraco : MonoBehaviour
    {
        [SerializeField] private GameObject inimigo;
        [SerializeField] private float offsetY;

        private GameObject instInimigo;

        public bool Ocupado { get; set; }

        public void CriarInimigo()
        {
            Vector3 pos = gameObject.transform.position;
            instInimigo = Instantiate(inimigo, new Vector3(pos.x, pos.y + offsetY, pos.z), Quaternion.identity, transform);
            ParticleSystem part = GetComponentInChildren<ParticleSystem>();
            part.Play();
            instInimigo.GetComponent<Inimigo>().setBuraco(gameObject);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            Ocupado = true;
        }

        public void Liberar()
        {
            Ocupado = false;
        }
    }
}
