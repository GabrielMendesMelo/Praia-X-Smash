using UnityEngine;

public class CarregaCena : MonoBehaviour
{
    [SerializeField] private CarregandoCenaInterface carregandoCena;

    public void Carregar(int id, Transform pai=null)
    {
        if (pai == null) pai = transform;
        GameObject carregando = Instantiate(carregandoCena.gameObject, pai);
        carregando.GetComponent<CarregandoCenaInterface>().Carregar(id);
    }
}
