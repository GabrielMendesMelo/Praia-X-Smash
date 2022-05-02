using UnityEngine;

public class Buraco : MonoBehaviour
{
    [SerializeField] private float tatuiPosYOffset = 0.085f;

    public bool Ocupado { get; private set; }

    public void CriarTatui(GameObject tatui)
    {
        GetComponent<FonteDeAudio>().Tocar();
        GetComponentInChildren<ParticleSystem>().Play();

        Vector3 pos = gameObject.transform.position;
        pos.y -= tatui.GetComponent<CapsuleCollider>().height / 2 + tatuiPosYOffset;

        Instantiate(tatui, pos, Quaternion.identity, transform);

        Ocupado = true;

        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void Liberar()
    {
        Ocupado = false;

        GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
