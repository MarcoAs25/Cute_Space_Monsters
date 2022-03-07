using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lvl
{
    public string desc;
    public int tipo;//1 para Score
    public int scoreABater;
    public int TempoABater;
    public int inimigosSpawn;
}

public class levelManager : MonoBehaviour
{
    public lvl[] nivel;
    public int nivelAtual, quantidadeDeNiveis = 2, nivelMaisAltoTerminado;
    internal static levelManager inst;

    private void Awake()
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
    }

    void Start()
    {
        nivelAtual = PlayerPrefs.GetInt("nivelAtual3", 1);
        nivelMaisAltoTerminado = PlayerPrefs.GetInt("nivelMaisAltoTerminado3", 0);
    }
    public void ChangeNewLevel()
    {
        if (nivelAtual > nivelMaisAltoTerminado)
        {
            nivelAtual++;
            nivelMaisAltoTerminado++;
            PlayerPrefs.SetInt("nivelMaisAltoTerminado3", nivelMaisAltoTerminado);
        }
        else
        {
            nivelAtual++;
        }
    }

    internal bool isLastLevel()
    {
        return nivelAtual == quantidadeDeNiveis;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
