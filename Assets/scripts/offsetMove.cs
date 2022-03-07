using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class offsetMove : MonoBehaviour
{
    [SerializeField]
    private float speed, maxValueOf;
    [SerializeField]
    private int direction;
    [SerializeField]
    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * direction * Time.fixedDeltaTime, transform.position.z);
        if (transform.position.y >= maxValueOf)
        {
            transform.position = startpos;
        }
    }

}
