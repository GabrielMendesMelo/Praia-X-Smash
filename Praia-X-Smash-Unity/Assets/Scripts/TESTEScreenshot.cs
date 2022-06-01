using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTEScreenshot : MonoBehaviour
{
    [SerializeField] private int prints;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Print();
    }

    private void Print()
    {
        ScreenCapture.CaptureScreenshot(@"D:\Documentos\MeusJogos\Whack A Mole\Icones\print-" + prints + ".png");

        prints++;
    }
}
