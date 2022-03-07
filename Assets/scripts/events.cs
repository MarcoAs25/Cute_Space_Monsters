using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class events : MonoBehaviour
{
    private player plScript;
    private UiGame ui;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UiGame>();
        plScript = FindObjectOfType<player>();
        plScript.addScore += ui.AddScore;
        plScript.morte += ui.losingBottom;
        plScript.winned += ui.morte;
        plScript.losed += ui.losed;
        MonetizationManager.inst.finishAds += plScript.adsFinished;
    }
}
