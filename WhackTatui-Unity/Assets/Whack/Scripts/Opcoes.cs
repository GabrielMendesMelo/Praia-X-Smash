using System;
using UnityEngine;

public class Opcoes : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuOpcoes;

    public void AbrirMenu(string menu)
    {
        menu = menu.ToLower();
        menu = menu.Trim();

        switch (menu)
        {
            case "pause":
                menuOpcoes.SetActive(false);
                menuPause.SetActive(true);
                break;

            case "opcoes":
                menuPause.SetActive(false);
                menuOpcoes.SetActive(true);
                break;

            default:
                throw new Exception("Não há menu com este nome (" + menu + ")");
        }
    }
}
