using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBg : MonoBehaviour
{
    public float max = 38.4f;
    public Transform camer,bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camer.transform.position.y >= transform.position.y + max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + max, transform.position.z);
        }else if(camer.transform.position.y <= transform.position.y - max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - max, transform.position.z);
        }
    }
}
