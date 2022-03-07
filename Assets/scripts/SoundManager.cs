using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    [SerializeField]
    private bool muted = false;
    [SerializeField]
    private AudioSource[] audios;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            muted = false;
        }
        else
        {
            muted = true;

        }
        foreach (AudioSource a in audios)
        {
            a.mute = muted;
        }
        Debug.Log("primeiro");
    }
    private void Start()
    {
        Debug.Log("segundo");
    }
    public void Play(int idx)
    {
        audios[idx].Play();
    }

    public bool isMuted()
    {
        return muted;
    }

    public void mute()
    {
        
        muted = !audios[0].mute;
        foreach (AudioSource a in audios)
        {
            a.mute = !a.mute;
        }
        if (!isMuted())
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
}
