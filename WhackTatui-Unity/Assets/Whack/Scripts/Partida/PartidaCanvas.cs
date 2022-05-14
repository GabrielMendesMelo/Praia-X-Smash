using UnityEngine;
using UnityEngine.UI;

public class PartidaCanvas : MonoBehaviour
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
        pause.SetActive(true);
        btnPause.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }
}
