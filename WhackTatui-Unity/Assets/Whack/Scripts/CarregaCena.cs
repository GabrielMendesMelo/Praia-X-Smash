using UnityEngine;

public class CarregaCena : MonoBehaviour
{
    [SerializeField] private CarregandoCenaAnimacao carregandoCenaAnimacao;

    public void Carregar(int id, Transform pai=null)
    {
        if (pai == null) pai = transform;
        GameObject carregando = Instantiate(carregandoCenaAnimacao.gameObject, pai);
        carregando.GetComponent<CarregandoCenaAnimacao>().Carregar(id);
    }
}
