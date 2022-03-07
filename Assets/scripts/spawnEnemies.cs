using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject[] inimigo;
    [SerializeField]
    private GameObject chegada;


    public void criarInimigos(int quantidade)
    {
        float altura = transform.position.y;

        for (int i = 0; i < quantidade; i++)
        {
            altura += Random.Range(4.0f, 8f);
            Instantiate(inimigo[Random.Range(0, inimigo.Length)], new Vector3(Random.Range(-8f, 8f), altura, 0.0f), Quaternion.identity);
        }
        altura += 8.0f;
        Instantiate(chegada, new Vector3(0.0f, altura, 0.0f), Quaternion.identity);
    }
    void Update()
    {
        
    }
}
