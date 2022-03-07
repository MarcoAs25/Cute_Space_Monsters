using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] public bool podeJogar = false, estanoanuncio = false, win = false;
    [SerializeField] public lvl lvll;
    [SerializeField] public int Score = 0;
    [SerializeField] public float Time = 0f;
    // Start is called before the first frame update
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
    }
    public void AddScore()
    {
        Score++;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        win = false;
        podeJogar = true;
        spawnEnemies sp = FindObjectOfType<spawnEnemies>();
        if(sp != null)
        {
            sp.criarInimigos(lvll.inimigosSpawn);
        }
        
    }

}
