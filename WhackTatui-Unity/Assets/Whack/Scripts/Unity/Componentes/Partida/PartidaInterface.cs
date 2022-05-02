using UnityEngine;
using UnityEngine.UI;
using Whack.Scripts.Unity;

public class PartidaInterface : MonoBehaviour
{
    [SerializeField] private Button btnPause;
    [SerializeField] private AudioClip btnPauseClip;
    [SerializeField] private GameObject pause;

    private FonteDeAudio fonteDeAudio;

    private void Start()
    {
        btnPause.onClick.AddListener(Pausar);
        fonteDeAudio = GetComponent<FonteDeAudio>();
    }

    private void Pausar()
    {
        fonteDeAudio.Tocar(btnPauseClip);
        PartidaInfo.Parar();
        pause.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }
}
