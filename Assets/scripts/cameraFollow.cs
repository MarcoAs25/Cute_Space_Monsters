using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]private Transform playerTransform;

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = position;
    }
}
