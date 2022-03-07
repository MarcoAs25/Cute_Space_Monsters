using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    public string papa;
    public float f_UpdateInterval = 0.5F;

    private float f_LastInterval;

    private int i_Frames = 0;

    private float f_Fps;

    void Start()
    {
        //Application.targetFrameRate=60;

        f_LastInterval = Time.realtimeSinceStartup;

        i_Frames = 0;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 1000, 1000), "podeJogar:" +GameManager.inst.podeJogar);
        GUI.Label(new Rect(0, 500, 1000, 1000), "timeScale:" + MonetizationManager.inst.aparecendoAnuncio);
    }

    void Update()
    {
        ++i_Frames;

        if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
        {
            f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);

            i_Frames = 0;

            f_LastInterval = Time.realtimeSinceStartup;
        }
    }
}
