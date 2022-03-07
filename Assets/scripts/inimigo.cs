using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public Transform delimiter;
    // Start is called before the first frame update
    void Start()
    {
        delimiter = GameObject.FindGameObjectWithTag("Delimiter").GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if ( transform.position.y < (delimiter.position.y+3f))
        {
            this.gameObject.SetActive(false);
        }
    }
}
