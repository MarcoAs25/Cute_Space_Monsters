using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    float orininalPosX;
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        orininalPosX = camTransform.localPosition.x;
    }

    void Update()
    {
        originalPos = camTransform.localPosition;
        if (shakeDuration > 0)
        {
            Vector3 vet =  originalPos + Random.insideUnitSphere * shakeAmount;

            camTransform.localPosition = new Vector3(vet.x, vet.y, -10f);
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = new Vector3(orininalPosX, transform.position.y, originalPos.z);
        }
    }
    public void setshakeDuration(float x) {
        this.shakeDuration = x;
    }
}